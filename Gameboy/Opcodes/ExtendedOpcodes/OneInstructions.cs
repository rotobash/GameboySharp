using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class OneInstructions : Opcode
    {
        public OneInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.HL.word);
            return 12;
        }
        public override int FSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.AF, true);
            return 4;
        }
    }
}

