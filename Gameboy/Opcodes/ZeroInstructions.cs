using System;
using Gameboy.Utility;

namespace Gameboy.Opcodes
{
    public class ZeroInstructions : Opcode
    {
        public ZeroInstructions(CPU cpu) : base (cpu) 
        {
        }
        //0x00
        public override int ZeroSuffix() 
        {
            return NOP();
        }
        //0x01
        public override int OneSuffix() 
        {
            Load.LOADWORDTOREG(cpu, ref cpu.BC);
            return 12;
        }
        //0x02
        public override int TwoSuffix() 
        {
            Load.LOADREGTOADDRESS(cpu, ref cpu.AF, cpu.BC.word, true);
            return 8;
        }
        public override int ThreeSuffix() 
        {
            Arithmetic.INC16BIT(ref cpu.BC);
            return 8;
        }
        public override int FourSuffix() 
        {
            Arithmetic.INC8BIT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int FiveSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, ref cpu.BC, true);
            return 4;
        }
        public override int SixSuffix() 
        {
            Load.LOADBYTETOREG(cpu, ref cpu.BC, true);
            return 8;
        }
        public override int SevenSuffix() 
        {
            Rotates.ROTATELEFT(cpu, ref cpu.AF, true);
            return 4;
        }
        public override int EightSuffix() 
        {
            byte low = cpu.FetchNextInstruction();
            byte high = cpu.FetchNextInstruction();
            ushort address = (ushort)((high << 8) + low);

            Load.LOADWORDTOADDRESS(cpu, ref cpu.SP, address);
            return 20;
        }
        public override int NineSuffix() 
        {
            Arithmetic.ADDREGISTERTOHL(cpu, ref cpu.BC);
            return 8;
        }
        public override int ASuffix() 
        {
            Load.LOADBYTEFROMADDRESS(cpu, ref cpu.AF, cpu.BC.word, true);
            return 8;
        }
        public override int BSuffix() 
        {
            Arithmetic.DEC16BIT(cpu, ref cpu.BC);
            return 8;
        }
        public override int CSuffix() 
        {
            Arithmetic.INC8BIT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int DSuffix() 
        {
            Arithmetic.DEC8BIT(cpu, ref cpu.BC, false);
            return 4;
        }
        public override int ESuffix() 
        {
            Load.LOADBYTETOREG(cpu, ref cpu.BC, false);
            return 8;
        }
        public override int FSuffix() 
        {
            Rotates.ROTATERIGHT(cpu, ref cpu.AF, true);
            return 4;
        }
    }
}

