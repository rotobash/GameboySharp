using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class ThreeInstructions : Opcode
    {
        public ThreeInstructions(CPU cpu) : base (cpu)  
        {
        }

        public override int ZeroSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            BitOperations.SWAP(cpu, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            BitOperations.SWAP(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.BC, true, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.BC, false, true);
            return 4;
        }
        public override int ASuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.DE, true, true);
            return 4;
        }
        public override int BSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.DE, false, true);
            return 4;
        }
        public override int CSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.HL, true, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.HL, false, true);
            return 4;
        }
        public override int ESuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, cpu.HL.word, true);
            return 12;
        }
        public override int FSuffix() 
        {
            Rotates.SHIFTRIGHT(cpu, ref cpu.AF, true, true);
            return 4;
        }
    }
}

