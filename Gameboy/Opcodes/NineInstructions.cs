using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class NineInstructions : Opcode
    {
        public NineInstructions(CPU cpu) : base (cpu)  
        {
        }

        public override int ZeroSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Arithmetic.SUB8BIT(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Arithmetic.SBC8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int FSuffix() 
        {
            Arithmetic.SBC8BIT(cpu, ref cpu.AF, true);
            return 4;
        }
    }
}

