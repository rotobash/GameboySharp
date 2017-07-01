using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class FourInstructions : Opcode
    {
        public FourInstructions(CPU cpu) : base (cpu) 
        {
        }

        /// <summary>
        /// Instruction:  (0x40)
        /// 
        /// </summary>
        public override int ZeroSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x41)
        /// 
        /// </summary>
        public override int OneSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x42)
        /// 
        /// </summary>
        public override int TwoSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x43)
        /// 
        /// </summary>
        public override int ThreeSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x44)
        /// 
        /// </summary>
        public override int FourSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x45)
        /// 
        /// </summary>
        public override int FiveSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x46)
        /// 
        /// </summary>
        public override int SixSuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.BC, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x47)
        /// 
        /// </summary>
        public override int SevenSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, true, cpu.AF, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x48)
        /// 
        /// </summary>
        public override int EightSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.BC, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x49)
        /// 
        /// </summary>
        public override int NineSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.BC, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x4A)
        /// 
        /// </summary>
        public override int ASuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x4B)
        /// 
        /// </summary>
        public override int BSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x4C)
        /// 
        /// </summary>
        public override int CSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x4D)
        /// 
        /// </summary>
        public override int DSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction:  (0x4E)
        /// 
        /// </summary>
        public override int ESuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.BC, cpu.HL.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction:  (0x4F)
        /// 
        /// </summary>
        public override int FSuffix() 
        {
            Load.LOADBYTEREGTOREG(cpu, cpu.BC, false, cpu.AF, true);
            return 4;
        }
    }
}

