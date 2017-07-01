using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class AInstructions : Opcode
    {
        public AInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Arithmetic.AND8BIT(cpu, cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int FSuffix() 
        {
            Arithmetic.XOR8BIT(cpu, cpu.AF, true);
            return 4;
        }
    }
}

