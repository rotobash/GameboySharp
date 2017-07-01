using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class BInstructions : Opcode
    {
        public BInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Arithmetic.OR8BIT(cpu, cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.HL.word);
            return 8;
        }
        public override int FSuffix() 
        {
            Arithmetic.CP8BIT(cpu, cpu.AF, true);
            return 4;
        }
    }
}

