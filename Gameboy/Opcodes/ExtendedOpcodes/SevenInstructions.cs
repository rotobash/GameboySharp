using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes.ExtendedOpcodes
{
    public class SevenInstructions : Opcode
    {
        public SevenInstructions(CPU cpu) : base (cpu) 
        {
        }
        public override int ZeroSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.BC, true);
            return 4;
        }
        public override int OneSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.BC, false);
            return 4;
        }
        public override int TwoSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.DE, true);
            return 4;
        }
        public override int ThreeSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.DE, false);
            return 4;
        }
        public override int FourSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.HL, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.HL, false);
            return 4;
        }
        public override int SixSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.HL.word);
            return 12;
        }
        public override int SevenSuffix() 
        {
            BitOperations.TESTBIT(cpu, 6, cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.BC, true);
            return 4;
        }
        public override int NineSuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.BC, false);
            return 4;
        }
        public override int ASuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.DE, true);
            return 4;
        }
        public override int BSuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.DE, false);
            return 4;
        }
        public override int CSuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.HL, true);
            return 4;
        }
        public override int DSuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.HL, false);
            return 4;
        }
        public override int ESuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.HL.word);
            return 12;
        }
        public override int FSuffix() 
        {
            BitOperations.TESTBIT(cpu, 7, cpu.AF, true);
            return 4;
        }
    }
}