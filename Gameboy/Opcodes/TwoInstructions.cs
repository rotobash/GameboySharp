using System;
using Gameboy.Utility;
using Condition=Gameboy.Utility.Flow.Condition;

namespace Gameboy.Opcodes
{
    public class TwoInstructions : Opcode
    {
        public TwoInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            Flow.CONDITIONALJUMPN(cpu, Condition.ZFLAGRESET);
            return 12;
        }
        public override int OneSuffix() 
        {
            Load.LOADWORDTOREG(cpu, cpu.HL);
            return 12;
        }
        public override int TwoSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, cpu.AF, cpu.HL.word++, true);
            return 8;
        }

        /// <summary>
        /// Instruction: INC HL (0x23)
        /// Increment HL register
        /// </summary>
        public override int ThreeSuffix() 
        {
            Arithmetic.INC16BIT(cpu.HL);
            return 8;
        }

        /// <summary>
        /// Instruction: INC H (0x24)
        /// Increment H register
        /// </summary>
        public override int FourSuffix() 
        {
            Arithmetic.INC8BIT(cpu, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction: DEC H (0x25)
        /// Decrement H register
        /// </summary>
        public override int FiveSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, cpu.HL, true);
            return 4;
        }

        /// <summary>
        /// Instruction: LD H,d8 (0x26)
        /// Load D with immediate byte
        /// </summary>
        public override int SixSuffix() 
        {
            Load.LOADBYTETOREG(cpu, cpu.HL, true);
            return 8;
        }
        public override int SevenSuffix() 
        {
            BitOperations.DECIMALADJUSTA(cpu);
            return 4;
        }

        /// <summary>
        /// Instruction JR Z n (0x28)
        /// If Zero flag is set, jump to PC + n
        /// </summary>
        /// <returns>8 cycles</returns>
        public override int EightSuffix() 
        {
            Flow.CONDITIONALJUMPN(cpu, Condition.ZFLAGSET);
            return 8;
        }

        /// <summary>
        /// Instruction: ADD HL,HL (0x29)
        /// Add DE to HL
        /// </summary>
        public override int NineSuffix() 
        {
            Arithmetic.ADDREGISTERTOHL(cpu, cpu.HL);
            return 8;
        }

        /// <summary>
        /// Instruction: LD A,(HL) (0x2A)
        /// Load A with byte stored at address (HL) then increment HL
        /// </summary>
        public override int ASuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.AF, cpu.HL.word++, true);
            return 8;
        }

        /// <summary>
        /// Instruction: DEC HL (0xO2B)
        /// Decrement register HL
        /// </summary>
        public override int BSuffix() 
        {
            Arithmetic.DEC16BIT(cpu, cpu.HL);
            return 8;
        }

        /// <summary>
        /// Instruction: INC L (0x2C)
        /// Increment register L
        /// </summary>
        public override int CSuffix() 
        {
            Arithmetic.INC8BIT(cpu, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction: DEC L (0x2D)
        /// Decrement register L
        /// </summary>
        public override int DSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, cpu.HL, false);
            return 4;
        }

        /// <summary>
        /// Instruction: LD L,d8 (0x2E)
        /// Load E with immediate byte
        /// </summary>
        public override int ESuffix() 
        {
            Load.LOADBYTETOREG(cpu, cpu.HL, false);
            return 8;
        }

        /// <summary>
        /// Instruction: CPL (0x2F)
        /// Flip all bits in register A.
        /// </summary>
        /// <returns>The suffix.</returns>
        public override int FSuffix() 
        {
            cpu.AF.high ^= (byte)(0xFF);
            return 4;
        }
    }
}

