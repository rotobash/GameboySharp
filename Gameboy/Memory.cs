using System;

namespace Gameboy
{
    public class Memory
    {
        //Timer addresses
        public const ushort TMC = 0xFF07;
        public const ushort TMA = 0xFF06;
        public const ushort TIMA = 0xFF05;

        internal byte[] internalMemory;

        internal Cartidge rom;

        //acessing timers and interupts
        CPU cpu;
         
        public Memory(CPU cpu)
        {
            internalMemory = new byte[0x10000];
            this.cpu = cpu;
        }

        public int DividerCounter
        {
            get;
            set;
        }

        public int TimerCounter
        {
            get;
            set;
        }

        public void Reset() 
        {
            internalMemory[0xFF05] = 0x00; 
            internalMemory[0xFF06] = 0x00; 
            internalMemory[0xFF07] = 0x00; 
            internalMemory[0xFF10] = 0x80; 
            internalMemory[0xFF11] = 0xBF; 
            internalMemory[0xFF12] = 0xF3; 
            internalMemory[0xFF14] = 0xBF; 
            internalMemory[0xFF16] = 0x3F; 
            internalMemory[0xFF17] = 0x00; 
            internalMemory[0xFF19] = 0xBF; 
            internalMemory[0xFF1A] = 0x7F; 
            internalMemory[0xFF1B] = 0xFF; 
            internalMemory[0xFF1C] = 0x9F; 
            internalMemory[0xFF1E] = 0xBF; 
            internalMemory[0xFF20] = 0xFF; 
            internalMemory[0xFF21] = 0x00; 
            internalMemory[0xFF22] = 0x00; 
            internalMemory[0xFF23] = 0xBF; 
            internalMemory[0xFF24] = 0x77; 
            internalMemory[0xFF25] = 0xF3;
            internalMemory[0xFF26] = 0xF1; 
            internalMemory[0xFF40] = 0x91; 
            internalMemory[0xFF42] = 0x00; 
            internalMemory[0xFF43] = 0x00; 
            internalMemory[0xFF45] = 0x00; 
            internalMemory[0xFF47] = 0xFC; 
            internalMemory[0xFF48] = 0xFF; 
            internalMemory[0xFF49] = 0xFF; 
            internalMemory[0xFF4A] = 0x00; 
            internalMemory[0xFF4B] = 0x00; 
            internalMemory[0xFFFF] = 0x00;
        }

        public void WriteToMemory(ushort address, byte data) 
        {
            if (address < 0x8000)
            {
                rom.HandleBanking(address, data);
            }
            else if ((address >= 0xA000) && (address < 0xC000))
            {
                ushort newAddress = (ushort)(address - 0xA000);
                rom.WriteToRam(newAddress, data);
            }
            else if ((address >= 0xC000) && (address < 0xE000))
            {
                // writing to ECHO ram also writes in RAM 
                internalMemory[address] = data;
            }
            else if ((address >= 0xE000) && (address < 0xFE00))
            {
                // writing to ECHO ram also writes in RAM 
                internalMemory[address] = data;
                WriteToMemory((ushort)(address - 0x2000), data);
            }
            else if ((address >= 0xFEA0) && (address < 0xFEFF))
            {
                // this area is restricted
            }
            else if (address == 0xFF04)
            {
                internalMemory[0xFF04] = 0;
                DividerCounter = 0;
            }
            else if (address == TMC)
            {
                byte currentFreq = GetClockFrequency();
                internalMemory[TMC] = data;
                byte newFreq = GetClockFrequency();
                if (currentFreq != newFreq)
                {
                    TimerCounter = 0;
                    SetClockFrequency();
                } 
            }
            else if (address == 0xFF44)
            {
                //reset scanline
                internalMemory[address] = 0;
            }
            else if (address == 0xFF46)
            {
                DMATransfer(data);
            }
            else
            {
                // no control needed over this area so write to memory
                internalMemory[address] = data ;
            }
        }

        public void SetRom(Cartidge cartridge)
        {
            Reset();
            rom = cartridge;
            for (int i = 0x100; i < 0x8000; i++)
                internalMemory[i] = cartridge.cartridgeMemory[i];
        }

        public byte FetchByteFromMemory(ushort address) 
        {
            if ((address>=0x4000) && (address <= 0x7FFF))
            {
                //reading from the rom memory bank
                ushort newAddress = (ushort)(address - 0x4000);
                return rom.ReadFromRom(newAddress);
            }
            else if ((address >= 0xA000) && (address <= 0xBFFF))
            {
                //reading from ram memory bank
                ushort newAddress = (ushort)(address - 0xA000);
                return rom.ReadFromRam(newAddress);
            }
            //else just normal memory
            return internalMemory[address];
        }

        public ushort FetchWordFromMemory(ushort address) 
        {
            byte first = FetchByteFromMemory(address);
            byte second = FetchByteFromMemory((byte)(address + 1));
            //little endian
            return (ushort)((second << 8) & first);
        }


        public void UpdateTimers(int cycles) 
        {
            if (IsClockEnabled())
            {
                TimerCounter -= cycles;
                if (TimerCounter <= 0)
                {
                    SetClockFrequency();
                    if (FetchByteFromMemory(TIMA) == 255)
                    {
                        WriteToMemory(TIMA, FetchByteFromMemory(TMA));
                        cpu.RequestInterupt(2);
                    }
                    else
                        WriteToMemory(TIMA, (byte)(FetchByteFromMemory(TMA) + 1));
                }
            }
        }

        /// <summary>
        /// Direct memory access transfer.
        /// Allows the cpu to access the sprite table in memory
        /// </summary>
        /// <param name="data">Data.</param>
        void DMATransfer(byte data) 
        {
            ushort address = (ushort)(data << 8); // same as data * 100
            for (byte i = 0; i < 0xA0; i++)
            {
                WriteToMemory((ushort)(0xFE00 + i), FetchByteFromMemory((ushort)(address * i)));
            }
        }

        byte GetClockFrequency()
        {
            return (byte)(FetchByteFromMemory(TMC) & 0x3);
        }

        void SetClockFrequency()
        {
            switch (GetClockFrequency())
            {
                case 0:
                    TimerCounter = 1024;
                    break;
                case 1:
                    TimerCounter = 16;
                    break;
                case 2:
                    TimerCounter = 64;
                    break;
                case 3:
                    TimerCounter = 256;
                    break;
            }
        }

        bool IsClockEnabled()
        {
            byte timer = FetchByteFromMemory(TMC);
            return cpu.TestBit(timer, 2) ? true : false;
        }

        void UpdateDividerRegister(int cycles) 
        {
            DividerCounter += cycles;
            if (DividerCounter >= 255)
            {
                DividerCounter = 0;
                internalMemory[0xFF04]++;
            }
        }
    }
}

