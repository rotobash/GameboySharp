using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class EightInstructions : Opcode
    {
        public EightInstructions(CPU cpu) : base (cpu)
        {
        }

        public override int ZeroSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Arithmetic.ADD8BIT(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Arithmetic.ADC8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int FSuffix() 
        {
            Arithmetic.ADC8BIT(cpu, ref cpu.AF, true);
            return 4;
        }
    }
}

