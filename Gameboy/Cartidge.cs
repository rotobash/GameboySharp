using System;

namespace Gameboy
{
    public class Cartidge
    {
        internal byte[] cartridgeMemory;
        byte[] ramBanks;

        byte currentRomBank;
        byte currentRamBank;
        bool romBanking;
        bool enableRam;

        public Cartidge(byte[] data)
        { 
            //max cartridge size was 2MB, addressable through bank switching
            cartridgeMemory = new byte[0x200000];

            currentRomBank = 1;

            ramBanks = new byte[0x8000];
            currentRamBank = 0;
            LoadCartridge(data);
        }

        public bool MBController1Enabled
        {
            get;
            private set;
        }

        public bool MBController2Enabled 
        {
            get;
            private set;
        }

        internal byte ReadFromRam(ushort address)
        {
            return ramBanks[address + (currentRamBank*0x2000)] ;
        }

        internal byte ReadFromRom(ushort address)
        {
            return cartridgeMemory[address + (currentRomBank*0x4000)];
        }

        internal void WriteToRam(ushort address, byte data)
        {
            if (enableRam)
            {
                ramBanks[address + (currentRamBank * 0x2000)] = data;
            }
        }


        void LoadCartridge(byte[] cartridgeBytes)
        {
            for (int i = 0; i < cartridgeBytes.Length; i++)
            {
                cartridgeMemory[i] = cartridgeBytes[i];
            }
            switch (cartridgeMemory[0x147])
            {
                case 1:
                    MBController1Enabled = true;
                    break;
                case 2:
                    MBController1Enabled = true;
                    break;
                case 3:
                    MBController1Enabled = true;
                    break;
                case 5:
                    MBController2Enabled = true;
                    break; 
                case 6:
                    MBController2Enabled = true;
                    break;
                default:
                    break;
            }
        }

        internal void HandleBanking(ushort address, byte data) 
        {
            if (address < 0x2000)
            {
                if (MBController1Enabled || MBController2Enabled)
                    EnableRamBank(address, data);
            }
            else if ((address >= 0x200) && (address < 0x4000))
            {
                if (MBController1Enabled || MBController2Enabled)
                    EnableLowRomBank(address, data);
            }
            else if ((address >= 0x4000) && (address < 0x6000))
            {
                if (MBController1Enabled)
                {
                    if (romBanking)
                        EnableHighRamBank(address, data);
                    else
                        ChangeRamBank(address, data);
                }
            }
            else if ((address >= 0x6000) && (address < 0x8000))
            {
                if (MBController1Enabled)
                    ChangeRomRamMode(address, data);
            }
        }

        void EnableRamBank(ushort address, byte data) 
        {
            if (MBController2Enabled)
                return;

            byte test = (byte)(data & 0xF);
            if (test == 0xA)
                enableRam = true;
            else if (test == 0)
                enableRam = false;
        }

        void ChangeRamBank(ushort address, byte data) 
        {
            currentRamBank = (byte)(data & 0x3);
        }

        void ChangeRomRamMode(ushort address, byte data) 
        { 
            byte newdata = (byte)(data & 0x1);
            romBanking = (newdata == 0) ? true : false;
            if (romBanking)
                currentRamBank = 0;
        }

        void EnableLowRomBank(ushort address, byte data) 
        { 
            if (MBController2Enabled)
            {
                currentRomBank = (byte)(data & 0xF);
                if (currentRomBank == 0)
                {
                    currentRomBank++;
                    return;
                }
            }
            byte lower5 = (byte)(data & 31);
            currentRomBank &= 224;
            currentRomBank |= lower5;
            if (currentRomBank == 0)
                currentRomBank++;
        }

        void EnableHighRamBank(ushort address, byte data) 
        {  
            currentRomBank &= 31;
            data &= 224;
            currentRomBank |= data;
            if (currentRomBank == 0)
                currentRomBank++;
        }


    }
}

