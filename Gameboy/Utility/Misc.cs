using System;

namespace Gameboy.Utility
{
    public class Misc
    {
        public static void SetZeroFlag(CPU cpu, ushort result)
        {
            if (result == 0)
                cpu.SetFlag(Flags.Zero);
        }

        public static void SetZeroFlag(CPU cpu, byte result)
        {
            if (result == 0)
                cpu.SetFlag(Flags.Zero);
        }

        public static byte RESETMSB(byte input) 
        {
            return (byte)(input & 0x7F); 
        }

        public static byte RESETLSB(byte input) 
        {
            return (byte)(input & 0xFE); 
        }

        public static void ENABLEINTERUPTS(CPU cpu)
        {
            cpu.EnableInterupt();
        }

        public static void DISABLEINTERUPTS(CPU cpu)
        {
            cpu.DisableInterupt();
        }
    }
}

