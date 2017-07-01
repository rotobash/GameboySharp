using System;

namespace Gameboy.Utility
{
    public static class BitOperations
    {
        public static void SWAP(CPU cpu, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            byte upperNibble = (byte)((regValue >> 4) & 0xF);
            byte lowerNibble = (byte)(regValue & 0xF);

            byte newdata = (byte)((lowerNibble << 4) + upperNibble);

            if (highRegister)
                register.high = newdata;
            else
                register.low = newdata;

            Misc.SetZeroFlag(cpu, newdata);
            cpu.ResetFlag(Flags.Subtract);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.HalfCarry);
        }

        public static void SWAP(CPU cpu, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            byte upperNibble = (byte)((value >> 4) & 0xF);
            byte lowerNibble = (byte)(value & 0xF);

            byte newdata = (byte)((lowerNibble << 4) + upperNibble);
            cpu.WriteToMemory(address, newdata);

            Misc.SetZeroFlag(cpu, newdata);
            cpu.ResetFlag(Flags.Subtract);
            cpu.ResetFlag(Flags.Carry);
            cpu.ResetFlag(Flags.HalfCarry);
        }

        public static void TESTBIT(CPU cpu, int bitNumber, Register register, bool highRegister) 
        {
            byte regValue = (highRegister) ? register.high : register.low;
            if (cpu.TestBit(regValue, bitNumber))
                cpu.SetFlag(Flags.Zero);
            else 
                cpu.ResetFlag(Flags.Zero);
            
            cpu.SetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void TESTBIT(CPU cpu, int bitNumber, ushort address) 
        {
            byte value = cpu.FetchByteFromMemory(address);
            if (cpu.TestBit(value, bitNumber))
                cpu.SetFlag(Flags.Zero);
            else 
                cpu.ResetFlag(Flags.Zero);
            
            cpu.SetFlag(Flags.HalfCarry);
            cpu.ResetFlag(Flags.Subtract);
        }

        public static void SETBIT(CPU cpu, int bitNumber, Register register, bool highRegister) 
        {
            if (highRegister)
                register.high = cpu.SetBit(register.high, bitNumber);
            else
                register.low = cpu.SetBit(register.low, bitNumber);
        }

        public static void SETBIT(CPU cpu, int bitNumber, ushort address) 
        {
            byte value = cpu.SetBit(cpu.FetchByteFromMemory(address), bitNumber);
            cpu.WriteToMemory(address, value);
        }

        public static void RESETBIT(CPU cpu, int bitNumber, Register register, bool highRegister) 
        {
            if (highRegister)
                register.high = cpu.ResetBit(register.high, bitNumber);
            else
                register.low = cpu.ResetBit(register.low, bitNumber);
        }

        public static void RESETBIT(CPU cpu, int bitNumber, ushort address) 
        {
            byte value = cpu.ResetBit(cpu.FetchByteFromMemory(address), bitNumber);
            cpu.WriteToMemory(address, value);
        }

        public static void DECIMALADJUSTA(CPU cpu)
        {
            byte aReg = cpu.AF.high;

            if ((aReg & 0xF) > 9 | cpu.TestBit(cpu.AF.low, (int)Flags.HalfCarry))
                aReg += 6;

            if (aReg > 0x9F | cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
            {
                aReg += 0x60;
                cpu.SetFlag(Flags.Carry);
            }
            else
                cpu.ResetFlag(Flags.Carry);
            
            cpu.ResetFlag(Flags.HalfCarry);
            Misc.SetZeroFlag(cpu, aReg);
            cpu.AF.high = aReg;
        }
    }
}

