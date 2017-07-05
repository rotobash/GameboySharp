using System;

namespace Gameboy.Utility
{
    public static class Load
    {
        /// <summary>
        /// Fetch immediate byte through cpu and load it to specified register.
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register">Register.</param>
        /// <param name="highRegister">If set to <c>true</c> high register.</param>
        public static void LOADBYTETOREG(CPU cpu, ref Register register, bool highRegister) 
        {
            if (highRegister)
                register.high = cpu.FetchNextInstruction();
            else
                register.low = cpu.FetchNextInstruction();
        }

        /// <summary>
        /// Fetch immediate word through cpu and load it to specified register.
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register">Register.</param>
        public static void LOADWORDTOREG(CPU cpu, ref Register register) 
        {
            register.low = cpu.FetchNextInstruction();
            register.high = cpu.FetchNextInstruction();
        }

        /// <summary>
        /// Store specified register into specified address.
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register">Register.</param>
        /// <param name="address">Address.</param>
        /// <param name="highRegister">If set to <c>true</c> high register.</param>
        public static void LOADREGTOADDRESS(CPU cpu, ref Register register, ushort address, bool highRegister)
        {
            if (highRegister)
                cpu.WriteToMemory(address, register.high);
            else
                cpu.WriteToMemory(address, register.low);
        }

        /// <summary>
        /// Store byte from register2 in register1
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register1">Register1.</param>
        /// <param name="highRegister1">If set to <c>true</c> high register.</param>
        /// <param name="register2">Register2.</param>
        /// <param name="highRegister2">If set to <c>true</c> high register.</param>
        public static void LOADBYTEREGTOREG(CPU cpu, ref Register register1, bool highRegister1, ref Register register2, bool highRegister2) 
        {
            byte otherRegVal = highRegister2 ? register2.high : register2.low;

            if (highRegister1)
                register1.high = otherRegVal;
            else
                register1.low = otherRegVal;
        }

        /// <summary>
        /// Store word from register2 into register1
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register1">Register1.</param>
        /// <param name="register2">Register2.</param>
        public static void LOADWORDREGTOREG(CPU cpu, ref Register register1, ref Register register2) 
        {
            register1.word = register2.word;
        }

        /// <summary>
        /// Fetch byte from specified address and store it into register
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register">Register.</param>
        /// <param name="address">Address.</param>
        /// <param name="highRegister">If set to <c>true</c> high register.</param>
        public static void LOADBYTEFROMADDRESS(CPU cpu, ref Register register, ushort address, bool highRegister) 
        {
            if (highRegister)
                register.high = cpu.FetchByteFromMemory(address);
            else
                register.low = cpu.FetchByteFromMemory(address);
        }

        /// <summary>
        /// Store a register's word to a specified address
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register">Register.</param>
        /// <param name="address">Address.</param>
        public static void LOADWORDTOADDRESS(CPU cpu, ref Register register, ushort address) 
        {
            cpu.WriteToMemory(address, register.low);
            cpu.WriteToMemory((ushort)(address + 1), register.high);
        }

        /// <summary>
        /// Fetch word from address and store it into the register
        /// </summary>
        /// <param name="cpu">Cpu.</param>
        /// <param name="register">Register.</param>
        /// <param name="address">Address.</param>
        public static void LOADWORDFROMADDRESS(CPU cpu, ref Register register, ushort address) 
        {
            register.word = cpu.FetchWordFromMemory(address);
        }

        public static void PUSHREGONTOSTACK(CPU cpu, ref Register register)
        {
            cpu.PushWord(register.word);
        }

        public static void POPSTACKINTOREG(CPU cpu, ref Register register)
        {
            register.word = cpu.PopWord();
        }
    }
}

