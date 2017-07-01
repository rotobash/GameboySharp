using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class TwoInstructions : Opcode
    {
        public TwoInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            Rotates.SHIFTLEFT(cpu, cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.BC, true, false);
            return 4;
        }
        public override int NineSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.BC, false, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.DE, true, false);
            return 4;
        }
        public override int BSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.DE, false, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.HL, true, false);
            return 4;
        }
        public override int DSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.HL, false, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.HL.word, false);
            return 12;
        }
        public override int FSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.AF, true, false);
            return 4;
        }
    }
}

