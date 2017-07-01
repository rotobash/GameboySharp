using System;

namespace Gameboy.Utility
{
    public static class Rotates
    {
        #region ROTATES
        public static void ROTATELEFT(CPU cpu, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte MSB = (byte)((regValue & 0x80) >> 7);
            byte result = (byte)((regValue << 1) | MSB);

            if (highRegister)
                register.high = result;
            else
                register.low = result;

            if (MSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);
            
            SETFLAGS(cpu, result);
        }

        public static void ROTATELEFT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte MSB = (byte)((value & 0x80) >> 7);
            byte result = (byte)((value << 1) | MSB);

            cpu.WriteToMemory(address, result);

            if (MSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void ROTATELEFTTHROUGHCARRY(CPU cpu, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte carryValue = (byte)((cpu.AF.low & (byte)Flags.Carry) >> 4);
            byte MSB = (byte)((regValue & 0x80) >> 7);
            byte result = (byte)((regValue << 1) | carryValue);

            if (highRegister)
                register.high = result;
            else
                register.low = result;

            if (MSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void ROTATELEFTTHROUGHCARRY(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte carryValue = (byte)((cpu.AF.low & (byte)Flags.Carry) >> 4);
            byte MSB = (byte)((value & 0x80) >> 7);
            byte result = (byte)((value << 1) | carryValue);

            cpu.WriteToMemory(address, result);

            if (MSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void ROTATERIGHT(CPU cpu, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte LSB = (byte)((regValue & 0x1) << 7);
            byte result = (byte)((regValue >> 1) | LSB);

            if (highRegister)
                register.high = result;
            else
                register.low = result;

            if (LSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void ROTATERIGHT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte LSB = (byte)((value & 0x1) << 7);
            byte result = (byte)((value >> 1) | LSB);

            cpu.WriteToMemory(address, result);

            if (LSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void ROTATERIGHTTHROUGHCARRY(CPU cpu, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte carryValue = (byte)((cpu.AF.low & (byte)Flags.Carry) << 3);
            byte LSB = (byte)(regValue & 0x01);
            byte result = (byte)((regValue >> 1) | carryValue);

            if (highRegister)
                register.high = result;
            else
                register.low = result;
            
            if (LSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void ROTATERIGHTTHROUGHCARRY(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte carryValue = (byte)((cpu.AF.low & (byte)Flags.Carry) << 3);
            byte LSB = (byte)(value & 0x01);
            byte result = (byte)((value >> 1) | carryValue);

            cpu.WriteToMemory(address, result);

            if (LSB == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }
        #endregion

        #region SHIFTS
        public static void SHIFTLEFT(CPU cpu, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte oldNibble = (byte)((regValue & 0x80) >> 7);
            byte result = (byte)(regValue << 1);

            if (highRegister)
                register.high = result;
            else
                register.low = result;

            if (oldNibble == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void SHIFTLEFT(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte oldNibble = (byte)((value & 0x80) >> 7);
            byte result = (byte)(value << 1);

            Misc.RESETLSB(result);
            cpu.WriteToMemory(address, result);

            if (oldNibble == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void SHIFTRIGHT(CPU cpu, Register register, bool highRegister, bool resetMSB) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte oldNibble = (byte)(regValue & 0x1);
            byte result = (byte)(regValue >> 1);

            if (resetMSB)
                Misc.RESETMSB(result);

            if (highRegister)
                register.high = result;
            else
                register.low = result;

            if (oldNibble == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }

        public static void SHIFTRIGHT(CPU cpu, ushort address, bool resetMSB) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte oldNibble = (byte)(value & 0x1);
            byte result = (byte)(value >> 1);

            if (resetMSB)
                Misc.RESETMSB(result);

            cpu.WriteToMemory(address, result);

            if (oldNibble == 1)
                cpu.SetFlag(Flags.Carry);
            else
                cpu.ResetFlag(Flags.Carry);

            SETFLAGS(cpu, result);
        }
        #endregion

        static void SETFLAGS(CPU cpu, byte result)
        {
            Misc.SetZeroFlag(cpu, result);
            cpu.ResetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Subtract);
        }
    }
}

