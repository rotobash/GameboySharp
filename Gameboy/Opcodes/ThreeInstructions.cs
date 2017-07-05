using System;
using Gameboy.Utility;
using Condition=Gameboy.Utility.Flow.Condition;

namespace Gameboy.Opcodes
{
    public class ThreeInstructions : Opcode
    {
        public ThreeInstructions(CPU cpu) : base (cpu)  
        {
        }

        public override int ZeroSuffix()
        {
            ushort temp = cpu.PC.word;
            Flow.CONDITIONALJUMPN(cpu, Condition.CFLAGRESET);
            return (temp == cpu.PC.word) ? 8 : 12;
        }
        public override int OneSuffix() 
        {
            Load.LOADWORDTOREG(cpu, ref cpu.SP);
            return 12;
        }
        public override int TwoSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.AF, cpu.HL.word--, true);
            return 8;
        }
        public override int ThreeSuffix() 
        {
            Arithmetic.INC16BIT(ref cpu.SP);
            return 8;
        }
        public override int FourSuffix() 
        {
            Arithmetic.INC8BIT(cpu, cpu.HL.word);
            return 12;
        }
        public override int FiveSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, cpu.HL.word);
            return 12;
        }
        public override int SixSuffix() 
        {
            Register temp = new Register();
            temp.high = cpu.FetchNextInstruction();
            Load.LOADREGTOADDRESS(cpu, ref temp, cpu.HL.word, true);
            return 12;
        }

        /// <summary>
        /// Instruction: SCF (0x37)
        /// Sets the carry flag
        /// </summary>
        /// <returns>The suffix.</returns>
        public override int SevenSuffix() 
        {
            cpu.SetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Subtract);
            return 4;
        }
        public override int EightSuffix()
        {
            ushort temp = cpu.PC.word;
            Flow.CONDITIONALJUMPN(cpu, Condition.CFLAGSET);
            return (temp == cpu.PC.word) ? 8 : 12;
        }
        public override int NineSuffix() 
        {
            Arithmetic.ADDREGISTERTOHL(cpu, ref cpu.SP);
            return 8;
        }
        public override int ASuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, ref cpu.AF, cpu.HL.word--, true);
            return 8;
        }
        public override int BSuffix() 
        {
            Arithmetic.DEC16BIT(cpu, ref cpu.SP);
            return 8;
        }
        public override int CSuffix() 
        {
            Arithmetic.INC8BIT(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int DSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int ESuffix() 
        {
            Load.LOADBYTETOREG(cpu, ref cpu.AF, true);
            return 8;
        }

        /// <summary>
        /// Instruction: CCF (0x3F)
        /// Flip the carry flag.
        /// </summary>
        /// <returns>The suffix.</returns>
        public override int FSuffix() 
        {
            
            cpu.AF.low ^= (byte)(0x1 << (int)Flags.Carry);
            
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Subtract);
            return 4;
        }
    }
}

