using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class CInstructions : Opcode
    {
        
        public CInstructions(CPU cpu) : base (cpu)  
        {
        }

        public override int ZeroSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            BitOperations.SETBIT(cpu, 0, cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.HL.word);
            return 12;
        }
        public override int FSuffix() 
        {
            BitOperations.SETBIT(cpu, 1, cpu.AF, true);
            return 4;
        }
    }
}

