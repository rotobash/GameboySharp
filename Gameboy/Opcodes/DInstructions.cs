using System;
using Gameboy.Utility;
using Condition=Gameboy.Utility.Flow.Condition;

namespace Gameboy.Opcodes
{
    public class DInstructions : Opcode
    {
        public DInstructions(CPU cpu) : base (cpu) 
        {
        }

        /// <summary>
        /// Instruction: RET NC (0xD0)
        /// Return to last address if Carry flag is set.
        /// </summary>
        /// <returns>8 cycles</returns>
        public override int ZeroSuffix() 
        {
            Flow.CONDITIONALRETURN(cpu, Condition.CFLAGRESET);
            return 8;
        }
        public override int OneSuffix() 
        {
            Load.POPSTACKINTOREG(cpu, cpu.DE);
            return 12;
        }

        /// <summary>
        /// Instruction: JUMP NC,nn (0xD2)
        /// Jump to immediate address if Carry flag is reset
        /// </summary>
        public override int TwoSuffix() 
        {
            Flow.CONDITIONALJUMP(cpu, Condition.CFLAGRESET);
            return 12;
        }

        //Not used
        public override int ThreeSuffix() 
        {
            return NOP();
        }

        /// <summary>
        /// Instruction: Call NC,nn (0xD4)
        /// Call immediate address if Carry flag is reset
        /// </summary>
        public override int FourSuffix() 
        {
            Flow.CONDITIONALCALL(cpu, Condition.CFLAGRESET);
            return 12;
        }
        public override int FiveSuffix() 
        {
            Load.PUSHREGONTOSTACK(cpu, cpu.DE);
            return 16;
        }
        public override int SixSuffix() 
        {
            Arithmetic.SUB8BIT(cpu);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Flow.RESTART(cpu, 0x10);
            return 32;
        }

        /// <summary>
        /// Instruction: RET C (0xD8)
        /// Return to last address if Carry flag is set.
        /// </summary>
        /// <returns>8 cycles</returns>
        public override int EightSuffix() 
        {
            Flow.CONDITIONALRETURN(cpu, Condition.CFLAGSET);
            return 8;
        }
        public override int NineSuffix() 
        {
            Misc.ENABLEINTERUPTS(cpu);
            Flow.JUMP(cpu, cpu.PopWord());
            return 16;
        }


        /// <summary>
        /// Instruction: JUMP C,nn (0xDA)
        /// Jump to immediate address if Carry flag is set
        /// </summary>
        public override int ASuffix() 
        {
            Flow.CONDITIONALJUMP(cpu, Condition.CFLAGSET);
            return 12;
        }

        //not used
        public override int BSuffix() 
        {
            return NOP();
        }

        /// <summary>
        /// Instruction: Call C,nn (0xDC)
        /// Call immediate address if Carry flag is set
        /// </summary>
        public override int CSuffix() 
        {
            Flow.CONDITIONALCALL(cpu, Condition.CFLAGSET);
            return 12;
        }

        //not used
        public override int DSuffix() 
        {
            return NOP();
        }

        public override int ESuffix() 
        {
            Arithmetic.SBC8BIT(cpu);
            return 8;
        }
        public override int FSuffix() 
        {
            Flow.RESTART(cpu, 0x18);
            return 32;
        }
    }
}

