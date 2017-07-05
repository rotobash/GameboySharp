using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class NineInstructions : Opcode
    {
        public NineInstructions(CPU cpu) : base (cpu)  
        {
        }
        public override int ZeroSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            BitOperations.RESETBIT(cpu, 2, ref cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, cpu.HL.word);
            return 12;
        }
        public override int FSuffix() 
        {
            BitOperations.RESETBIT(cpu, 3, ref cpu.AF, true);
            return 4;
        }
    }
}

