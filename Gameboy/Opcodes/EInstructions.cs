using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class EInstructions : Opcode
    {
        public EInstructions(CPU cpu) : base (cpu) 
        {
        }

        public override int ZeroSuffix() 
        {
            ushort address = (ushort)(0xFF00 + cpu.FetchNextInstruction());
            Load.LOADREGTOADDRESS(cpu, cpu.AF, address, true);
            return 12;
        }
        public override int OneSuffix() 
        {
            Load.POPSTACKINTOREG(cpu, cpu.AF);
            return 12;
        }
        public override int TwoSuffix() 
        {
            ushort address = (ushort)(0xFF00 + cpu.BC.low);
            Load.LOADREGTOADDRESS(cpu, cpu.AF, address, true);
            return 8;
        }
        public override int ThreeSuffix() 
        {
            return NOP();
        }
        public override int FourSuffix() 
        {
            return NOP();
        }
        public override int FiveSuffix() 
        {
            Load.PUSHREGONTOSTACK(cpu, cpu.AF);
            return 16;
        }
        public override int SixSuffix() 
        {
            Arithmetic.AND8BIT(cpu);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Flow.RESTART(cpu, 0x20);
            return 32;
        }
        public override int EightSuffix() 
        {
            Arithmetic.ADDIMMEDIATETOSP(cpu);
            return 16;
        }

        /// <summary>
        /// Instruction: JP (HL) (0xE9)
        /// Jumps to address at HL
        /// </summary>
        /// <returns>The suffix.</returns>
        public override int NineSuffix() 
        {
            Flow.JUMP(cpu, cpu.HL.word);
            return 4;
        }
        public override int ASuffix() 
        {
            ushort address = (ushort)(cpu.FetchNextInstruction() << 8);
            address += cpu.FetchNextInstruction();
            Load.LOADBYTEFROMADDRESS(cpu, cpu.AF, address, true);
            return 16;
        }
        public override int BSuffix() 
        {
            return NOP();
        }
        public override int CSuffix() 
        {
            return NOP();
        }
        public override int DSuffix() 
        {
            return NOP();
        }
        public override int ESuffix() 
        {
            Arithmetic.XOR8BIT(cpu);
            return 8;
        }
        public override int FSuffix() 
        {
            Flow.RESTART(cpu, 0x28);
            return 32;
        }
    }
}

