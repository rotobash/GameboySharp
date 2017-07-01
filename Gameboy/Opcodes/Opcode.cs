using System;

namespace Gameboy.Opcodes
{
    public abstract class Opcode
    {
        protected CPU cpu;

        public Opcode(CPU cpu)
        {
            this.cpu = cpu;
        }
        /// <summary>
        /// Decode the specified op and execute it.
        /// Returns amount of clock cycles taken.
        /// </summary>
        /// <param name="op">Op.</param>
        public int Decode(byte op) 
        {
            int b = op & 0xF;
            switch (b)
            {
                case 0:
                    return ZeroSuffix();
                case 1:
                    return OneSuffix();
                case 2:
                    return TwoSuffix();
                case 3:
                    return ThreeSuffix();
                case 4:
                    return FourSuffix();
                case 5:
                    return FiveSuffix();
                case 6:
                    return SixSuffix();
                case 7:
                    return SevenSuffix();
                case 8:
                    return EightSuffix();
                case 9:
                    return NineSuffix();
                case 10:
                    return ASuffix();
                case 11:
                    return BSuffix();
                case 12:
                    return CSuffix();
                case 13:
                    return DSuffix();
                case 14:
                    return ESuffix();
                case 15:
                    return FSuffix();
                default:
                    return NOP();
            }
        }

        protected int NOP() 
        {
            return 4;
        }

        public abstract int ZeroSuffix();
        public abstract int OneSuffix();
        public abstract int TwoSuffix();
        public abstract int ThreeSuffix();
        public abstract int FourSuffix();
        public abstract int FiveSuffix();
        public abstract int SixSuffix();
        public abstract int SevenSuffix();
        public abstract int EightSuffix();
        public abstract int NineSuffix();
        public abstract int ASuffix();
        public abstract int BSuffix();
        public abstract int CSuffix();
        public abstract int DSuffix();
        public abstract int ESuffix();
        public abstract int FSuffix();
    }
}

