using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class SixInstructions : Opcode
    {
        public SixInstructions(CPU cpu) : base (cpu)  
        {
        }

        /// <summary>
        /// Instruction:  (0x60)
        /// 
        /// </summary>
        public override int ZeroSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x61)
        /// 
        /// </summary>
        public override int OneSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x62)
        /// 
        /// </summary>
        public override int TwoSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x63)
        /// 
        /// </summary>
        public override int ThreeSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x64)
        /// 
        /// </summary>
        public override int FourSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x65)
        /// 
        /// </summary>
        public override int FiveSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x66)
        /// 
        /// </summary>
        public override int SixSuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.HL, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x67)
        /// 
        /// </summary>
        public override int SevenSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, true, cpu.AF, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x68)
        /// 
        /// </summary>
        public override int EightSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x69)
        /// 
        /// </summary>
        public override int NineSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x6A)
        /// 
        /// </summary>
        public override int ASuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x6B)
        /// 
        /// </summary>
        public override int BSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x6C)
        /// 
        /// </summary>
        public override int CSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x6D)
        /// 
        /// </summary>
        public override int DSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x6E)
        /// 
        /// </summary>
        public override int ESuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.HL, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x6F)
        /// 
        /// </summary>
        public override int FSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.HL, false, cpu.AF, true);
            return 4;
        }
    }
}

