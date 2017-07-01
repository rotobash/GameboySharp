using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class FInstructions : Opcode
    {
        public FInstructions(CPU cpu) : base (cpu)  
        {
        }
        public override int ZeroSuffix() 
        {
            byte offset = cpu.FetchNextInstruction();
            Load.LOADBYTEFROMADDRESS(cpu, cpu.AF, (ushort)(0xFF00 + offset), true);
            return 12;
        }
        public override int OneSuffix() 
        {
            Load.POPSTACKINTOREG(cpu, cpu.AF);
            return 12;
        }
        public override int TwoSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, cpu.AF, cpu.BC.low, true);
            return 8;
        }
        public override int ThreeSuffix() 
        {
            cpu.DisableInterupt();
            return 4;
        }
        public override int FourSuffix() 
        {
            return NOP();
        }
        public override int FiveSuffix() 
        {
            Load.PUSHREGONTOSTACK(cpu, cpu.AF);
            return 16;
        }
        public override int SixSuffix() 
        {
            Arithmetic.OR8BIT(cpu);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Flow.RESTART(cpu, 0x30);
            return 32;
        }
        public override int EightSuffix() 
        {
            sbyte offset = (sbyte)cpu.FetchNextInstruction();
            cpu.HL.word = (ushort)(cpu.SP.word + offset);
            return 12;
        }
        public override int NineSuffix() 
        {
            Load.LOADWORDREGTOREG(cpu, cpu.SP, cpu.HL);
            return 8;
        }
        public override int ASuffix() 
        {
            ushort address = (ushort)(cpu.FetchNextInstruction() << 8);
            address += cpu.FetchNextInstruction();

            Load.LOADBYTEFROMADDRESS(cpu, cpu.AF, address, true);
            return 16;
        }
        public override int BSuffix() 
        {
            cpu.EnableInterupt();
            return 4;
        }
        public override int CSuffix() 
        {
            return NOP();
        }
        public override int DSuffix() 
        {
            return NOP();
        }
        public override int ESuffix() 
        {
            Arithmetic.CP8BIT(cpu);
            return 8;
        }
        public override int FSuffix() 
        {
            Flow.RESTART(cpu, 0x38);
            // gameboy pdf says it's 32, website says 16
            return 32;
        }
    }
}

