using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class SevenInstructions : Opcode
    {
        public SevenInstructions(CPU cpu) : base (cpu) 
        {
        }

        /// <summary>
        /// Instruction:  (0x70)
        /// 
        /// </summary>
        public override int ZeroSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.BC, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x71)
        /// 
        /// </summary>
        public override int OneSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.BC, cpu.HL.word, false);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x72)
        /// 
        /// </summary>
        public override int TwoSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.DE, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x73)
        /// 
        /// </summary>
        public override int ThreeSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.DE, cpu.HL.word, false);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x74)
        /// 
        /// </summary>
        public override int FourSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.HL, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x75)
        /// 
        /// </summary>
        public override int FiveSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.HL, cpu.HL.word, false);
            return 8;
        }

        /// <summary>
        /// Instruction: HALT (0x76)
        /// Halts until interupt
        /// </summary>
        public override int SixSuffix() 
        {
            cpu.Halt(false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x77)
        /// 
        /// </summary>
        public override int SevenSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.AF, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x78)
        /// 
        /// </summary>
        public override int EightSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x79)
        /// 
        /// </summary>
        public override int NineSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x7A)
        /// 
        /// </summary>
        public override int ASuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x7B)
        /// 
        /// </summary>
        public override int BSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x7C)
        /// 
        /// </summary>
        public override int CSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x7D)
        /// 
        /// </summary>
        public override int DSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x7E)
        /// 
        /// </summary>
        public override int ESuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, ref cpu.AF, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x7F)
        /// 
        /// </summary>
        public override int FSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, ref cpu.AF, false, ref cpu.AF, true);
            return 4;
        }
    }
}