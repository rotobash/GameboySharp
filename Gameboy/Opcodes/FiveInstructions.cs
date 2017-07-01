using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class FiveInstructions : Opcode
    {
        public FiveInstructions(CPU cpu) : base (cpu) 
        {
        }

        /// <summary>
        /// Instruction:  (0x50)
        /// 
        /// </summary>
        public override int ZeroSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x51)
        /// 
        /// </summary>
        public override int OneSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x52)
        /// 
        /// </summary>
        public override int TwoSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x53)
        /// 
        /// </summary>
        public override int ThreeSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x54)
        /// 
        /// </summary>
        public override int FourSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x55)
        /// 
        /// </summary>
        public override int FiveSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x56)
        /// 
        /// </summary>
        public override int SixSuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.DE, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x57)
        /// 
        /// </summary>
        public override int SevenSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, true, cpu.AF, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x58)
        /// 
        /// </summary>
        public override int EightSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x59)
        /// 
        /// </summary>
        public override int NineSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x5A)
        /// 
        /// </summary>
        public override int ASuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x5B)
        /// 
        /// </summary>
        public override int BSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x5C)
        /// 
        /// </summary>
        public override int CSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x5D)
        /// 
        /// </summary>
        public override int DSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x5E)
        /// 
        /// </summary>
        public override int ESuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.DE, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x5F)
        /// 
        /// </summary>
        public override int FSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.DE, false, cpu.AF, true);
            return 4;
        }
    }
}

