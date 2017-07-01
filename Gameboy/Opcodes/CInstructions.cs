using System;
using Gameboy.Opcodes.ExtendedOpcodes;
using Gameboy.Utility;
using Condition=Gameboy.Utility.Flow.Condition;

namespace Gameboy.Opcodes
{
    public class CInstructions : Opcode
    {
        Opcode[] extendedInstructions;

        public CInstructions(CPU cpu) : base (cpu)  
        {
            extendedInstructions = new Opcode[] { new ExtendedOpcodes.ZeroInstructions(cpu), new ExtendedOpcodes.OneInstructions(cpu), new ExtendedOpcodes.TwoInstructions(cpu), 
                new ExtendedOpcodes.ThreeInstructions(cpu), new ExtendedOpcodes.FourInstructions(cpu), new ExtendedOpcodes.FiveInstructions(cpu), new ExtendedOpcodes.SixInstructions(cpu), 
                new ExtendedOpcodes.SevenInstructions(cpu), new ExtendedOpcodes.EightInstructions(cpu), new ExtendedOpcodes.NineInstructions(cpu), new ExtendedOpcodes.AInstructions(cpu),
                new ExtendedOpcodes.BInstructions(cpu), new ExtendedOpcodes.CInstructions(cpu), new ExtendedOpcodes.DInstructions(cpu), new ExtendedOpcodes.EInstructions(cpu), 
                new ExtendedOpcodes.FInstructions(cpu)};
        }

        /// <summary>
        /// Instruction: RET NZ (0xC0)
        /// Return to last address if Zero flag is reset.
        /// </summary>
        /// <returns>8 cycles</returns>
        public override int ZeroSuffix() 
        {
            Flow.CONDITIONALRETURN(cpu, Condition.ZFLAGRESET);
            return 8;
        }

        public override int OneSuffix() 
        {
            Load.POPSTACKINTOREG(cpu, cpu.BC);
            return 8;
        }

        /// <summary>
        /// Instruction: JUMP NZ,nn (0xC2)
        /// Jump to immediate address if Zero flag is reset
        /// </summary>
        public override int TwoSuffix() 
        {
            Flow.CONDITIONALJUMP(cpu, Condition.ZFLAGRESET);
            return 12;
        }

        /// <summary>
        /// Instruction JP nn (0xC3)
        /// Jumps to immediate 2 byte address (LSB first)
        /// </summary>
        /// <returns>12 cycles.</returns>
        public override int ThreeSuffix() 
        {
            byte low = cpu.FetchNextInstruction();
            byte high = cpu.FetchNextInstruction();

            ushort address = (ushort)((high << 8) + low);
            Flow.JUMP(cpu, address);

            return 12;
        }

        /// <summary>
        /// Instruction: CALL NZ,nn (0xC4)
        /// Call immediate address if Zero flag is reset
        /// </summary>
        public override int FourSuffix() 
        {
            Flow.CONDITIONALCALL(cpu, Condition.ZFLAGRESET);
            return 12;
        }
        public override int FiveSuffix() 
        {
            Load.PUSHREGONTOSTACK(cpu, cpu.BC);
            return 0;
        }
        public override int SixSuffix() 
        {
            Arithmetic.ADD8BIT(cpu);
            return 0;
        }
        public override int SevenSuffix() 
        {
            Flow.RESTART(cpu, 0x0);
            return 32;
        }

        /// <summary>
        /// Instruction: RET Z (0xC8)
        /// Return to last address if Zero flag is set.
        /// </summary>
        /// <returns>8 cycles</returns>
        public override int EightSuffix() 
        {
            Flow.CONDITIONALRETURN(cpu, Condition.ZFLAGSET);
            return 8;
        }
        public override int NineSuffix() 
        {
            Flow.JUMP(cpu, cpu.PopWord());
            return 8;
        }
            
        /// <summary>
        /// Instruction: JUMP Z,nn (0xCA)
        /// Jump to immediate address if Zero flag is set
        /// </summary>
        public override int ASuffix() 
        {
            Flow.CONDITIONALJUMP(cpu, Condition.ZFLAGSET);
            return 12;
        }

        /// <summary>
        /// Initiate extended instructions
        /// </summary>
        /// <returns>The suffix.</returns>
        public override int BSuffix() 
        {
            byte nextInstruction = this.cpu.FetchNextInstruction();
            return 4 + extendedInstructions[(byte)((nextInstruction & 0xF0) >> 4)].Decode(nextInstruction);
        }

        /// <summary>
        /// Instruction: CALL Z,nn (0xCC)
        /// Call immediate address if Zero flag is set
        /// </summary>
        public override int CSuffix() 
        {
            Flow.CONDITIONALCALL(cpu, Condition.ZFLAGSET);
            return 12;
        }

        /// <summary>
        /// Instruction: CALL nn (0xCD)
        /// Call immediate 2 byte value (LSB first)
        /// </summary>
        public override int DSuffix() 
        {
            Flow.CALL(cpu);
            return 12;
        }
        public override int ESuffix() 
        {
            Arithmetic.ADC8BIT(cpu);
            return 8;
        }
        public override int FSuffix() 
        {
            Flow.RESTART(cpu, 0x08);
            return 32;
        }
    }
}

