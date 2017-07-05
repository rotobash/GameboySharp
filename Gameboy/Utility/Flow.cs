using System;

namespace Gameboy.Utility
{
    public static class Flow
    {
        public enum Condition
        {
            ZFLAGSET,
            ZFLAGRESET,
            CFLAGSET,
            CFLAGRESET
        }

        public static void JUMP(CPU cpu) 
        {
            byte low = cpu.FetchNextInstruction();
            byte high = cpu.FetchNextInstruction();
            ushort address = (ushort)((high << 8) + low);
            cpu.PC.word = address;
        }

        public static void JUMP(CPU cpu, ushort address) 
        {
            cpu.PC.word = address;
        }

        public static void JUMPN(CPU cpu) 
        {
            int data = (sbyte)cpu.FetchNextInstruction();
            ushort temp = (ushort)(cpu.PC.word + data);

            cpu.PC.word = temp;
        }

        public static void CALL(CPU cpu)
        {
            byte low = cpu.FetchNextInstruction();
            byte high = cpu.FetchNextInstruction();
            ushort address = (ushort)((high << 8) + low);

            cpu.PushWord(cpu.PC.word);
            JUMP(cpu, address);
        }

        public static void CALL(CPU cpu, ushort address)
        {
            cpu.PushWord(cpu.PC.word);
            JUMP(cpu, address);
        }

        public static void CONDITIONALJUMP(CPU cpu, Condition condition) 
        {
            byte low = cpu.FetchNextInstruction();
            byte high = cpu.FetchNextInstruction();
            ushort address = (ushort)((high << 8) + low);

            if(CheckCondition(cpu, condition))
                cpu.PC.word = address;
        }

        public static void CONDITIONALJUMPN(CPU cpu, Condition condition) 
        {
            if (CheckCondition(cpu, condition))
                JUMPN(cpu);
            else
                cpu.FetchNextInstruction();

        }

        static bool CheckCondition(CPU cpu, Condition condition)
        { 
            switch (condition)
            {
                case Condition.CFLAGRESET:
                    {
                        if(!cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                            return true;
                        return false;
                    }
                case Condition.CFLAGSET:
                    {
                        if(cpu.TestBit(cpu.AF.low, (int)Flags.Carry))
                            return true;
                        return false;
                    }
                case Condition.ZFLAGRESET:
                    {
                        if(!cpu.TestBit(cpu.AF.low, (int)Flags.Zero))
                            return true;
                        return false;
                    }
                case Condition.ZFLAGSET:
                    {
                        if(cpu.TestBit(cpu.AF.low, (int)Flags.Zero))
                            return true;
                        return false;
                    }
            }
            return false;
        }

        public static void CONDITIONALCALL(CPU cpu, Condition condition) 
        {
            byte low = cpu.FetchNextInstruction();
            byte high = cpu.FetchNextInstruction();
            ushort address = (ushort)((high << 8) + low);

            if (CheckCondition(cpu, condition))
            {
                cpu.PushWord(cpu.PC.word);
                JUMP(cpu, address);
            }
        }

        public static void RESTART(CPU cpu, byte address)
        {
            cpu.PushWord(cpu.PC.word);
            JUMP(cpu, address);
        }

        public static void CONDITIONALRETURN(CPU cpu, Condition condition) 
        {
            if (CheckCondition(cpu, condition))
                JUMP(cpu, cpu.PopWord());
        }

    }
}

