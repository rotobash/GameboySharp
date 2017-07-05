using System;
using Gameboy;

namespace Gameboy.Utility
{
    public static class Arithmetic
    {
        #region Adding
        public static void ADD8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            SETADDFLAGS8BIT(cpu, cpu.AF.high, regValue);
            cpu.AF.high = (byte)(cpu.AF.high + regValue);
        }

        public static void ADD8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            SETADDFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high + value);
        }

        public static void ADD8BIT(CPU cpu) 
        {
            byte value = cpu.FetchNextInstruction();
            SETADDFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high + value);
        }

        public static void ADC8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;

            if (cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                regValue++;

            SETADDFLAGS8BIT(cpu, cpu.AF.high, regValue);
            cpu.AF.high = (byte)(cpu.AF.high + regValue);
        }

        public static void ADC8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);

            if (cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                value++;
           
            SETADDFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high + value);
        }

        public static void ADC8BIT(CPU cpu) 
        {
            byte value = cpu.FetchNextInstruction();

            if (cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                value++;

            SETADDFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high + value);
        }

        public static void ADDREGISTERTOHL(CPU cpu, ref Register reg) 
        {
            cpu.ResetFlag(Flags.Subtract);
            SETCARRYFLAGS16BIT(cpu, cpu.HL.word, reg.word);
            cpu.HL.word = (ushort)(cpu.HL.word + reg.word);
        }

        public static void ADDIMMEDIATETOSP(CPU cpu) 
        {
            sbyte data = (sbyte)cpu.FetchNextInstruction();
            cpu.ResetFlag(Flags.Zero);
            cpu.ResetFlag(Flags.Subtract);
            SETCARRYFLAGS16BIT(cpu, cpu.SP.word, data);
            cpu.SP.word = (ushort)(cpu.SP.word + data);
        }

        public static void INC8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;

            regValue = (regValue == 0xFF) ?  (byte)0 : (byte)(regValue + 1);

            if (highRegister)
            {
                SETINCFLAGS8BIT(cpu, reg.high);
                reg.high = regValue;
            }
            else
            {
                SETINCFLAGS8BIT(cpu, reg.low);
                reg.low = regValue;
            }
        }

        public static void INC8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);

            value = (value == 0xFF) ?  (byte)0 : (byte)(value + 1);
            
            SETINCFLAGS8BIT(cpu, (byte)(value - 1));
            cpu.WriteToMemory(address, value);
        }

        public static void INC16BIT(ref Register reg) 
        {
            //cast to prevent overflow
            reg.word = (ushort)(reg.word + 1);
        }
        #endregion

        #region SUBRACTING
        public static void SUB8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            SETSUBFLAGS8BIT(cpu, cpu.AF.high, regValue);
            cpu.AF.high = (byte)(cpu.AF.high - regValue);
        }

        public static void SUB8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            SETSUBFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high - value);
        }

        public static void SUB8BIT(CPU cpu) 
        {
            byte value = cpu.FetchNextInstruction();
            SETSUBFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high - value);
        }

        public static void SBC8BIT(CPU cpu, ref Register reg, bool highRegister)  
        {
            byte regValue = (highRegister) ? reg.high : reg.low;

            if (cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                regValue++;

            SETADDFLAGS8BIT(cpu, cpu.AF.high, regValue);
            cpu.AF.high = (byte)(cpu.AF.high - regValue);
        }

        public static void SBC8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);

            if (cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                value++;

            SETSUBFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high - value);
        }

        public static void SBC8BIT(CPU cpu) 
        {
            byte value = cpu.FetchNextInstruction();

            if (cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                value++;

            SETSUBFLAGS8BIT(cpu, cpu.AF.high, value);
            cpu.AF.high = (byte)(cpu.AF.high - value);
        }

        public static void DEC8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            regValue = (regValue == 0xFF) ?  (byte)0 : (byte)(regValue - 1);
            if (highRegister)
            {
                SETDECFLAGS8BIT(cpu, reg.high);
                reg.high = regValue;
            }
            else
            {
                SETDECFLAGS8BIT(cpu, reg.low);
                reg.low = regValue;
            }
        }

               
        public static void DEC8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);

            value = (value == 0) ?  (byte)0xFF : (byte)(value - 1);

            SETDECFLAGS8BIT(cpu, (byte)(value + 1));
            cpu.WriteToMemory(address, value);
        }

        public static void DEC16BIT(CPU cpu, ref Register reg) 
        {
            //cast to prevent underflow
            reg.word = (ushort)(reg.word - 1);
        }

        #endregion

        #region LOGICAL
        public static void AND8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            cpu.AF.high &= regValue;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.SetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void AND8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            cpu.AF.high &= value;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.SetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void AND8BIT(CPU cpu) 
        {
            byte data = cpu.FetchNextInstruction();
            cpu.AF.high &= data;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.SetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void OR8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            cpu.AF.high |= regValue;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void OR8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            cpu.AF.high |= value;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void OR8BIT(CPU cpu) 
        {
            byte data = cpu.FetchNextInstruction();
            cpu.AF.high |= data;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void XOR8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            cpu.AF.high ^= regValue;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void XOR8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            cpu.AF.high ^= value;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void XOR8BIT(CPU cpu) 
        {
            byte data = cpu.FetchNextInstruction();
            cpu.AF.high ^= data;

            Misc.SetZeroFlag(cpu, cpu.AF.high);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void CP8BIT(CPU cpu, ref Register reg, bool highRegister) 
        {
            byte regValue = (highRegister) ? reg.high : reg.low;
            SETSUBFLAGS8BIT(cpu, cpu.AF.high, regValue);
        }

        public static void CP8BIT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            SETSUBFLAGS8BIT(cpu, cpu.AF.high, value);
        }

        public static void CP8BIT(CPU cpu) 
        {
            byte value = cpu.FetchNextInstruction();
            SETSUBFLAGS8BIT(cpu, cpu.AF.high, value);
        }
        #endregion

        #region SETFLAGS
        public static void SETSUBFLAGS8BIT(CPU cpu, byte first, byte second)
        {
            short result = (short)(first - second);

            Misc.SetZeroFlag(cpu, (byte)result);
            cpu.SetFlag(Flags.Subtract);

            if (result < 0)
                cpu.SetFlag(Flags.Carry);
            
            short halfc = (short)(first & 0xF);
            halfc -= (short)(second & 0xF);
            if(halfc < 0)
                cpu.SetFlag(Flags.HalfCarry);
        }

        public static void SETDECFLAGS8BIT(CPU cpu, byte first)
        {
            short result = (short)(first - 1);

            Misc.SetZeroFlag(cpu, (byte)result);
            cpu.SetFlag(Flags.Subtract);

            short halfc = (short)(first & 0xF);
            halfc -= 0x1;
            if(halfc < 0)
                cpu.SetFlag(Flags.HalfCarry);
        }

        public static void SETSUBFLAGS16BIT(CPU cpu, ushort first, ushort second)
        {
            int result = first - second;

            Misc.SetZeroFlag(cpu, (ushort)result);
            cpu.SetFlag(Flags.Subtract);

            if (result < 0)
                cpu.SetFlag(Flags.Carry);

            int halfc = (first & 0xFF);
            halfc -= (second & 0xFF);
            if(halfc < 0)
                cpu.SetFlag(Flags.HalfCarry);
        }

        public static void SETADDFLAGS8BIT(CPU cpu, byte first, byte second)
        {
            ushort result = (ushort)(first + second);
            
            Misc.SetZeroFlag(cpu, (byte)result);
            cpu.ResetFlag(Flags.Subtract);

            if (result > 0xFF)
                cpu.SetFlag(Flags.Carry);

            byte halfc = (byte)(first & 0xF);
            halfc += (byte)(second & 0xF);
            if(halfc > 0xF)
                cpu.SetFlag(Flags.HalfCarry);
        }

        public static void SETINCFLAGS8BIT(CPU cpu, byte first)
        {
            byte result = (byte)(first + 1);

            Misc.SetZeroFlag(cpu, result);
            cpu.ResetFlag(Flags.Subtract);

            byte halfc = (byte)(first & 0xF);
            halfc += (byte)(0x1);
            if(halfc > 0xF)
                cpu.SetFlag(Flags.HalfCarry);
        }

        public static void SETCARRYFLAGS16BIT(CPU cpu, ushort first, int second)
        {
            ushort result = (ushort)(first + second);
            if (result > 0xFFFF)
                cpu.SetFlag(Flags.Carry);

            ushort halfc = (ushort)(first & 0xFF);
            halfc += (ushort)(second & 0xFF);
            if(halfc > 0xFF)
                cpu.SetFlag(Flags.HalfCarry);
        }
        #endregion
    }
}

