using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class OneInstructions : Opcode
    {
        public OneInstructions(CPU cpu) : base (cpu) 
        {
        }

        /// <summary>
        /// Instruction: STOP (0x1000)
        /// Halt LCD, Timers and CPU until button pressed
        /// </summary>
        public override int ZeroSuffix() 
        {
            cpu.FetchNextInstruction();
            cpu.Halt(true);
            return 4;
        }

        /// <summary>
        /// Instruction: LD DE,d16 (0x11)
        /// Load immediate 2 byte value into DE
        /// </summary>
        public override int OneSuffix() 
        {
            Load.LOADWORDTOREG(cpu, cpu.DE);
            return 12;
        }

        /// <summary>
        /// Instruction: LD (DE),A (0x12)
        /// Load the byte at address (DE) into A
        /// </summary>
        public override int TwoSuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.AF, cpu.DE.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction: INC DE (0x13)
        /// Increment DE register
        /// </summary>
        public override int ThreeSuffix() 
        {
            Arithmetic.INC16BIT(cpu.DE);
            return 8;
        }

        /// <summary>
        /// Instruction: INC D (0x14)
        /// Increment D register
        /// </summary>
        public override int FourSuffix() 
        {
            Arithmetic.INC8BIT(cpu, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction: DEC D (0x15)
        /// Decrement D register
        /// </summary>
        public override int FiveSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, cpu.DE, true);
            return 4;
        }

        /// <summary>
        /// Instruction: LD D,d8 (0x16)
        /// Load D with immediate byte
        /// </summary>
        public override int SixSuffix() 
        {
            Load.LOADBYTETOREG(cpu, cpu.DE, true);
            return 8;
        }

        /// <summary>
        /// Instruction: RLA (0x17)
        /// Rotate A left through carry
        /// </summary>
        public override int SevenSuffix() 
        {
            Rotates.ROTATELEFTTHROUGHCARRY(cpu, cpu.AF, true);
            return 4;
        }

        /// <summary>
        /// Instruction JR n (0x18)
        /// Add signed n to PC and jump to it.
        /// </summary>
        /// <returns>8 cycles</returns>
        public override int EightSuffix() 
        {
            Flow.JUMPN(cpu);
            return 8;
        }

        /// <summary>
        /// Instruction: ADD HL,DE (0x19)
        /// Add DE to HL
        /// </summary>
        public override int NineSuffix() 
        {
            Arithmetic.ADDREGISTERTOHL(cpu, cpu.DE);
            return 8;
        }

        /// <summary>
        /// Instruction: LD A,(DE) (0x1A)
        /// Load A with byte stored at address (DE)
        /// </summary>
        public override int ASuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, cpu.AF, cpu.DE.word, true);
            return 8;
        }

        /// <summary>
        /// Instruction: DEC DE (0x1B)
        /// Decrement register DE
        /// </summary>
        public override int BSuffix() 
        {
            Arithmetic.DEC16BIT(cpu, cpu.DE);
            return 8;
        }

        /// <summary>
        /// Instruction: INC E (0x1C)
        /// Increment register E
        /// </summary>
        public override int CSuffix() 
        {
            Arithmetic.INC8BIT(cpu, cpu.DE, false);
            return 4;
        }

        /// <summary>
        /// Instruction: DEC E (0x1D)
        /// Decrement register E
        /// </summary>
        public override int DSuffix() 
        {
            return 4;
        }

        /// <summary>
        /// Instruction: LD E,d8 (0x1E)
        /// Load E with immediate byte
        /// </summary>
        public override int ESuffix() 
        {
            Load.LOADBYTETOREG(cpu, cpu.DE, false);
            return 8;
        }

        /// <summary>
        /// Instruction: RRA (0x1F)
        /// Rotate A right through carry
        /// </summary>
        public override int FSuffix() 
        {
            Rotates.ROTATERIGHTTHROUGHCARRY(cpu, cpu.AF, true);
            return 4;
        }
    }
}

