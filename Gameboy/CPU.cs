using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

using Gameboy.Opcodes;
using Gameboy.Utility;

namespace Gameboy
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Register {
        [FieldOffset(0)]
        public byte low;
        [FieldOffset(1)]
        public byte high;
        [FieldOffset(0)]
        public ushort word;
    }

    public enum Flags { 
        Zero = 7, 
        Subtract = 6, 
        HalfCarry = 5,
        Carry = 4
    }

    public class CPU
    {
        const int CLOCKSPEED = 4194304;
        bool isHalted;
        bool isStopped;

        bool setInterupt;
        bool changeInterupt;

        byte[] bootBin;

        Memory memory;
        LCD lcd;
        Joypad joypad;

        Opcode[] instructions;
        public bool MasterInteruptEnabled;
        
        public CPU()
        {
            memory = new Memory(this);
            lcd = new LCD(this);
            joypad = new Joypad(this);
            instructions = new Opcode[] { new ZeroInstructions(this), new OneInstructions(this), new TwoInstructions(this), 
                new ThreeInstructions(this), new FourInstructions(this), new FiveInstructions(this), new SixInstructions(this), 
                new SevenInstructions(this), new EightInstructions(this), new NineInstructions(this), new AInstructions(this),
                new BInstructions(this), new CInstructions(this), new DInstructions(this), new EInstructions(this), 
                new FInstructions(this)};
            
            bootBin = File.ReadAllBytes("boot.bin");
            for (int i = 0; i < 256; i++)
                memory.internalMemory[i] = bootBin[i];
        }


        public Register AF = new Register();
        public Register BC = new Register();
        public Register DE = new Register();
        public Register HL = new Register();
        public Register PC = new Register();
        public Register SP = new Register();

        public Color[] GFXBuffer
        {
            get 
            {
                return lcd.buffer;
            }
        }

        public void ResetCPU() {
            Flow.JUMP(this, 0);
        }

        internal void SetFlag(Flags flag) 
        {
            byte fVal = SetBit(AF.low, (int)flag);
            AF.low = fVal;
        }

        internal void ResetFlag(Flags flag) 
        {
            byte fVal = ResetBit(AF.low, (int)flag);
            AF.low = fVal;
        }

        public void Step(double FPS) 
        {
            int cycleCounter = 0;

            if (isStopped)
            {
                CheckInterupts();
                return;
            }

            while (cycleCounter < (int)(CLOCKSPEED * FPS))
            {
                int cycles = 4;
                if (!isHalted)
                {
                    byte nextInstruction = FetchNextInstruction();
                    cycles = DecodeAndExecute(nextInstruction);
                }
                cycleCounter += cycles;
                memory.UpdateTimers(cycles);
                lcd.UpdateGraphics(cycles);
                CheckInterupts();
                if(changeInterupt) 
                {
                    MasterInteruptEnabled = setInterupt;
                    changeInterupt = false;
                }
            }
        }

        internal byte FetchNextInstruction() 
        {
            byte nextByte = FetchByteFromMemory(PC.word++);
            return nextByte;
        }

        internal byte FetchByteFromMemory(ushort address) 
        {
            return memory.FetchByteFromMemory(address);
        }

        internal ushort FetchWordFromMemory(ushort address) 
        {
            return memory.FetchWordFromMemory(address);
        }

        internal void WriteToMemory(ushort address, byte data) 
        {
            memory.WriteToMemory(address, data);
        }

        internal void IncrementScanLineMemory()
        {
            memory.internalMemory[0xFF44]++;
        }

        public void LoadCartridge(Cartidge cartridge)
        {
            memory.SetRom(cartridge);
            ResetCPU();
        }

        internal byte GetJoyPadState()
        {
            return memory.internalMemory[0xFF00];
        }

        public void PushWord(ushort data) 
        {
            byte high = (byte)((data & 0xFF00) >> 8);
            byte low = (byte)(data & 0xFF);
            PushByte(low);
            PushByte(high);
        }

        public void PushByte(byte data)
        {
            WriteToMemory(SP.word, data);
            SP.word -= 1;
        }

        public byte PopByte() 
        {
            SP.word++;
            return FetchByteFromMemory(SP.word);
        }

        public ushort PopWord() 
        {
            byte high = PopByte();
            byte low = PopByte();
            return (ushort)((high << 8) + low);
        }

        internal void RequestInterupt(int code) 
        {
            byte request = FetchByteFromMemory(0xFF0F);
            request = SetBit(request, code);
            WriteToMemory(0xFF0F, request);
        }

        void CheckInterupts()
        {
            if (MasterInteruptEnabled)
            {
                //check which interupts are requested
                byte requested = FetchByteFromMemory(0xFF0F);
                //check which ones are allowed to interupt
                byte enabled = FetchByteFromMemory(0xFFFF);

                //and both to get only the interupts we care about
                byte interuptsToService = (byte)(requested & enabled);
                for (int i = 0; i < 5; i++)
                {
                    if (TestBit(interuptsToService, i))
                    {
                        ServiceInterupt(i);
                    }
                }
            }
        }

        void ServiceInterupt(int code)
        {
            MasterInteruptEnabled = false;
            isHalted = false;
            byte request = FetchByteFromMemory(0xFF0F);
            request = ResetBit(request, code);
            WriteToMemory(0xFF0F, request);

            switch (code)
            {
                case 0:
                    //VBLANK
                    Flow.CALL(this, 0x40);
                    break;
                case 1:
                    //LCD
                    Flow.CALL(this, 0x48);
                    break;
                case 2:
                    //TIMER
                    Flow.CALL(this, 0x50);
                    break;
                case 4:
                    //CONTROLLER
                    Flow.CALL(this, 0x60);
                    break;
            }
        }

        internal bool TestBit(byte data, int bitNumber) 
        {
            byte newdata = (byte)((data >> bitNumber) & 0x1);
            return (newdata == 1);
        }

        internal byte SetBit(byte data, int bitNumber) 
        {
            return (byte)(data | (0x1 << bitNumber));
        }

        internal byte ResetBit(byte data, int bitNumber) 
        {
            return (byte)(data & ((0x1 << bitNumber) ^ 0xFF));
        }

        internal int DecodeAndExecute(byte op) 
        {
            byte identifier = (byte)((op & 0xF0) >> 4);
            return instructions[identifier].Decode(op);
        }

        public void KeyPressed(int code)
        {
            joypad.KeyPressed(code);
        }

        public void KeyReleased(int code)
        {
            joypad.KeyReleased(code);
        }

        public void Halt(bool haltLCD)
        {
            isHalted = true;
            if (haltLCD)
                lcd.Halt();
        }

        public void Resume()
        {
            isHalted = false;
            lcd.Resume();
        }

        internal void EnableInterupt()
        {
            changeInterupt = true;
            setInterupt = true;
        }

        internal void DisableInterupt()
        {
            changeInterupt = true;
            setInterupt = false;
        }

    }
}

