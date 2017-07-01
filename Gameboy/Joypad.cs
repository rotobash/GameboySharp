using System;

namespace Gameboy
{
    public class Joypad
    {
        const ushort STATEREG = 0xFF00;
        CPU cpu;
        byte joypadState;

        public Joypad(CPU cpu)
        {
            this.cpu = cpu;
            joypadState = 0xFF;
        }

        internal void KeyPressed(int code)
        {
            bool wasPressed = true;

            if (!cpu.TestBit(joypadState, code))
                wasPressed = false;

            joypadState = cpu.ResetBit(joypadState, code);

            bool buttonPressed = true;

            if (code < 4)
                buttonPressed = false;

            byte keyRequest = cpu.FetchByteFromMemory(STATEREG);

            if (buttonPressed && !cpu.TestBit(keyRequest, 5) && wasPressed)
                cpu.RequestInterupt(4);
            else if (!buttonPressed && !cpu.TestBit(keyRequest, 4) && wasPressed)
                cpu.RequestInterupt(4);

        }

        internal void KeyReleased(int code)
        {
            joypadState = cpu.SetBit(joypadState, code);
        }

        internal byte GetJoypadState(byte stateRegMemory) 
        {
            byte result = (byte)(stateRegMemory ^ 0xFF);

            if (!cpu.TestBit(result, 4))
            {
                byte joypadTop = (byte)(joypadState >> 4);
                joypadTop |= 0xF0;
                result &= joypadTop;
            }
            else if (!cpu.TestBit(result, 5)) 
            {
                byte joypadBottom = (byte)(joypadState & 0xF);
                joypadBottom |= 0xF0;
                result &= joypadBottom;
            }
            return result;
        }
    }
}

