using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class ZeroInstructions : Opcode
    {
        public ZeroInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            Rotates.ROTATELEFT(cpu, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Rotates.ROTATERIGHT(cpu, cpu.HL.word);
            return 12;
        }
        public override int FSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.AF, true);
            return 4;
        }
    }
}

