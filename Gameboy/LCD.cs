using System;

using Microsoft.Xna.Framework;

namespace Gameboy
{
    public class LCD
    {
        //takes this many clock cycles to draw a scan line
        const int SCANLINECYCLES = 456;

        const ushort LCDREG = 0xFF40;
        const ushort STATREG = 0xFF41;
        const ushort SCROLLYREG = 0xFF42;
        const ushort SCROLLXREG = 0xFF43;
        const ushort VERTLINEREG = 0xFF44;
        const ushort VERTLINECOMPAREREG = 0xFF45;
        const int SIZEOFTILE = 16;

        internal Color[,] buffer;
        CPU cpu;

        int scanlineCounter;

        public LCD(CPU cpu)
        {
            this.cpu = cpu;
            scanlineCounter = SCANLINECYCLES;
            //lcd is 160x144 with 3 RGB values
            buffer = new Color[144,160];
        }

        internal void UpdateGraphics(int cycles)
        {
            SetLCDStatus();

            if (LCDEnabled())
                scanlineCounter -= cycles;
            
            if (scanlineCounter <= 0)
            {
                cpu.IncrementScanLineMemory();
                byte currentLine = cpu.FetchByteFromMemory(VERTLINEREG);
                scanlineCounter = SCANLINECYCLES;

                if (currentLine == 144)
                    cpu.RequestInterupt(0);
                if (currentLine > 153)
                    cpu.WriteToMemory(VERTLINEREG, 0);
                else if (currentLine < 144)
                    DrawScanLine();
            }
        }

        void DrawScanLine() 
        {
            byte data = cpu.FetchByteFromMemory(LCDREG);
            if (LCDEnabled())
            {
                if (cpu.TestBit(data, 0))
                    RenderTiles(data);
                if (cpu.TestBit(data, 1))
                    RenderSprites(data);
            }
        }

        void RenderTiles(byte lcdStatus)
        {
            ushort tileDataAddress;
            ushort bgDataAddress;

            byte scrollY = cpu.FetchByteFromMemory(SCROLLYREG);
            byte scrollX = cpu.FetchByteFromMemory(SCROLLXREG);
            byte windowY = cpu.FetchByteFromMemory(0xFF4A);
            byte windowX = (byte)(cpu.FetchByteFromMemory(0xFF4B) - 7);
            bool usingWindow = false;
            bool unsigned = false;

            if (cpu.TestBit(lcdStatus, 5))
            {
                if (windowY <= cpu.FetchByteFromMemory(VERTLINEREG))
                    usingWindow = true;
            }

            if (cpu.TestBit(lcdStatus, 4))
            {
                tileDataAddress = 0x8000;
                unsigned = true;
            }
            else
                tileDataAddress = 0x8800;

            byte yCoord;

            if (!usingWindow)
            {
                if (cpu.TestBit(lcdStatus, 3))
                    bgDataAddress = 0x9C00;
                else
                    bgDataAddress = 0x9800;

                yCoord = (byte)(scrollY + cpu.FetchByteFromMemory(VERTLINEREG));
            }
            else
            {
                if (cpu.TestBit(lcdStatus, 6))
                    bgDataAddress = 0x9C00;
                else
                    bgDataAddress = 0x9800;
                
                yCoord = (byte)(cpu.FetchByteFromMemory(VERTLINEREG) - windowY);
            }

            ushort row = (ushort)(((byte)(yCoord / 8)) * 32);

            for (int pixel = 0; pixel < 160; pixel++)
            {
                byte xCoord = (byte)(pixel + scrollX);

                if (usingWindow)
                {
                    if (pixel >= windowX)
                        xCoord = (byte)(pixel - windowX);
                }

                ushort column = (ushort)(xCoord / 8);
                short tileNum;
                
                if (unsigned)
                {
                    tileNum = cpu.FetchByteFromMemory((ushort)(bgDataAddress + row + column));
                    tileDataAddress += (byte)(tileNum * 16);
                }
                else
                {
                    tileNum = (sbyte)cpu.FetchByteFromMemory((ushort)(bgDataAddress + row + column));
                    tileDataAddress += (byte)((tileNum  + 128) * 16);
                }

                byte line = (byte)(yCoord % 8);
                line *= 2; // each vertical line takes up two bytes of memory
                byte first = cpu.FetchByteFromMemory((byte)(tileDataAddress + line)); 
                byte second = cpu.FetchByteFromMemory((byte)(tileDataAddress + line + 1));

                int colourBit = xCoord % 8;
                colourBit -= 7;
                colourBit *= -1;

                int colourNum = cpu.TestBit(second, colourBit) ? 1 : 0;
                colourNum <<= 1;
                colourNum |= cpu.TestBit(first, colourBit) ? 1 : 0;

                Color colour = GetColour(colourNum, 0xFF47);
                int yBounds = cpu.FetchByteFromMemory(VERTLINEREG);
                if (yBounds < 0 || yBounds > 143 || pixel < 0 || pixel > 159)
                    continue;

                buffer[yBounds, pixel] = colour;
            }
        }

        void RenderSprites(byte lcdStatus)
        {
            int ysize = 8; 
            if (cpu.TestBit(lcdStatus, 2))
                ysize = 16;

            for (int sprite = 0; sprite < 40; sprite++)
            { 
                // sprite occupies 4 bytes in the sprite attributes table
                byte index = (byte)(sprite*4); 
                byte yPos = (byte)(cpu.FetchByteFromMemory((ushort)(0xFE00 + index)) - 16);
                byte xPos = (byte)(cpu.FetchByteFromMemory((ushort)(0xFE00 + index + 1)) - 8);
                byte tileLocation = (byte)(cpu.FetchByteFromMemory((ushort)(0xFE00 + index + 2)));
                byte attributes = (byte)(cpu.FetchByteFromMemory((ushort)(0xFE00 + index + 3)));

                bool yFlip = cpu.TestBit(attributes,6);
                bool xFlip = cpu.TestBit(attributes,5);

                int scanline = cpu.FetchByteFromMemory(VERTLINEREG);

                // does this sprite intercept with the scanline?
                if ((scanline >= yPos) && (scanline < (yPos+ysize)))
                {
                    int line = scanline - yPos;

                    // read the sprite in backwards in the y axis
                    if (yFlip)
                    {
                        line -= ysize;
                        line *= -1;
                    }

                    line *= 2; // same as for tiles
                    ushort dataAddress = (ushort)((0x8000 + (tileLocation * 16)) + line);
                    byte first = cpu.FetchByteFromMemory(dataAddress);
                    byte second = cpu.FetchByteFromMemory((ushort)(dataAddress + 1));

                    // its easier to read in from right to left as pixel 0 is 
                    // bit 7 in the colour data, pixel 1 is bit 6 etc...
                    for (int tilePixel = 7; tilePixel >= 0; tilePixel--)
                    { 
                        int colourbit = tilePixel;
                        // read the sprite in backwards for the x axis 
                        if (xFlip) 
                        {
                            colourbit -= 7;
                            colourbit *= -1;
                        }


                        int colourNum = cpu.TestBit(second, colourbit) ? 1 : 0;
                        colourNum <<= 1;
                        colourNum |= cpu.TestBit(first, colourbit) ? 1 : 0;

                        ushort colourAddress = cpu.TestBit(attributes, 4) ? (ushort)0xFF49 : (ushort)0xFF48;
                        Color colour = GetColour(colourNum, colourAddress);

                        int xPix = 0 - tilePixel;
                        xPix += 7;

                        int pixel = xPos + xPix;

                        // sanity check
                        if (colour == Color.White || scanline < 0 || scanline > 143 || pixel < 0 || pixel > 159)
                            continue;

                        if (cpu.TestBit(attributes, 7))
                        {
                            if (buffer[scanline, pixel] != Color.White)
                                continue;
                        }
                        buffer[scanline, pixel] = colour;
                    }
                }
            }
        }

        Color GetColour(int colourNum, ushort address) 
        {
            byte palette = cpu.FetchByteFromMemory(address);
            byte high = 0;
            byte low = 0;

            switch (colourNum)
            {
                case 0:
                    high = 1;
                    low = 0;
                    break;
                case 1:
                    high = 3;
                    low = 2;
                    break;
                case 2:
                    high = 5;
                    low = 4;
                    break;
                case 3:
                    high = 7;
                    low = 6;
                    break;
            }

            int colour = cpu.TestBit(palette, high) ? 1 : 0;
            colour <<= 1;
            colour |= cpu.TestBit(palette, low) ? 1 : 0;

            switch (colour)
            {
                case 0:
                    return Color.White;
                case 1:
                    return Color.LightGray;
                case 2:
                    return Color.DarkGray;
                case 3:
                    return Color.Black;
                default:
                    return Color.White;
            }
        }

        bool LCDEnabled() 
        {
            return cpu.TestBit(cpu.FetchByteFromMemory(LCDREG), 7);
        }

        internal void Halt() 
        {
            cpu.ResetBit(cpu.FetchByteFromMemory(LCDREG), 7);
        }

        internal void Resume()
        {
            //this may have came after a halt and not a stop
            if (!LCDEnabled())
            {
                cpu.SetBit(cpu.FetchByteFromMemory(LCDREG), 7);
            }
        }

        void SetLCDStatus() 
        {
            byte lcdStatus = cpu.FetchByteFromMemory(STATREG);
            if (!LCDEnabled())
            {
                scanlineCounter = 0;
                cpu.WriteToMemory(VERTLINEREG, 0);
                lcdStatus &= 252;
                lcdStatus = cpu.SetBit(lcdStatus, 0);
                cpu.WriteToMemory(STATREG, lcdStatus);
                return;
            }

            byte ly = cpu.FetchByteFromMemory(VERTLINEREG);

            byte currentMode = (byte)(lcdStatus & 0x3);

            int mode = 0;
            bool reqInt = false;

            // set mode as vertical blank
            if (ly >= 144)
            {
                // mode 1
                mode = 1;
                lcdStatus = cpu.SetBit(lcdStatus, 0);
                lcdStatus = cpu.ResetBit(lcdStatus, 1);
                reqInt = cpu.TestBit(lcdStatus, 4);
            }
            else
            {
                int mode2Bounds = (376 - 80);
                int mode3Bounds = (mode2Bounds - 172);


                // mode 2
                if (scanlineCounter >= mode2Bounds)
                {
                    mode = 2;
                    lcdStatus = cpu.SetBit(lcdStatus, 1);
                    lcdStatus = cpu.ResetBit(lcdStatus, 0);
                    reqInt = cpu.TestBit(lcdStatus, 5);
                }
                // mode 3
                else if (scanlineCounter >= mode3Bounds)
                {
                    mode = 3;
                    lcdStatus = cpu.SetBit(lcdStatus, 1);
                    lcdStatus = cpu.SetBit(lcdStatus, 0);
                }
                // mode 3
                else
                {
                    mode = 0;
                    lcdStatus = cpu.ResetBit(lcdStatus, 1);
                    lcdStatus = cpu.ResetBit(lcdStatus, 0);
                    reqInt = cpu.TestBit(lcdStatus, 3);
                }

            }

            // just entered a new mode. Request interupt
            if (reqInt && (currentMode != mode))
                cpu.RequestInterupt(1);

            // check for coincidence flag
            if (ly == cpu.FetchByteFromMemory(VERTLINECOMPAREREG))
            {
                lcdStatus = cpu.SetBit(lcdStatus, 2);

                if (cpu.TestBit(lcdStatus, 6))
                    cpu.RequestInterupt(1);
            }
            else
                lcdStatus = cpu.ResetBit(lcdStatus, 2);

            cpu.WriteToMemory(0xFF41, lcdStatus);
        }
    }
}

