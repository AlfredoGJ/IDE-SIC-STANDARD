using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_ProgSistemas
{
    class MemoryMap
    {
        private List<MemorySlot> slots = new List<MemorySlot>();
        public List<MemorySlot> Slots { get => slots; }

        public MemoryMap(int statrAddress, int size)
        {
            for (int i = statrAddress; i < statrAddress + size; i += 16)
            {

                slots.Add(new MemorySlot(i));

            }
        }

        public bool WriteByte(int address, int value)
        {
            bool result = false;
            int absoluteAddress = address - slots[0].AddresNum;

            if (absoluteAddress >=0 && absoluteAddress<= slots.LastOrDefault().AddresNum)
            {
                int slot = (absoluteAddress / 16); // Localidad de memoria donde se encuentra el byte
                int offset =( absoluteAddress % 16); // Posicion en la localidad de memoria donde se encuentra el byte

                slots[slot].Values[offset] = value;
            
                result = true;
            }

            return result;
        }

        public int ReadByte(int address)
        {
            int value = -7;

            int absoluteAddress = address - slots[0].AddresNum;

            if (absoluteAddress >= 0 && absoluteAddress <= slots.LastOrDefault().AddresNum)
            {
                int slot = (absoluteAddress / 16); // Localidad de memoria donde se encuentra el byte
                int offset = (absoluteAddress % 16); // Posicion en la localidad de memoria donde se encuentra el byte
                value = slots[slot].Values[offset];
            }
            return value;
        }

        public int ReadWord(int address)
        {
            
            string word="";
            for (int i = address; i < address+3; i++)
            {
               
                int absoluteAddress = i - slots[0].AddresNum;

                if (absoluteAddress >= 0 && absoluteAddress <= slots.LastOrDefault().AddresNum)
                {
                    int slot = (absoluteAddress / 16); // Localidad de memoria donde se encuentra el byte
                    int offset = (absoluteAddress % 16); // Posicion en la localidad de memoria donde se encuentra el byte
                    word+= slots[slot].Values[offset].ToString("X2");



                }
                
            }
            return Convert.ToInt32(word, 16);




        }

        public Tuple<int, int> ReadInstruction(int address)
        {
            Tuple<int, int> instruction;

            int opcode = ReadByte(address);
            int m;
            string mString = "";
            
            for (int i = address; i < address+2; i++)
            {
                int absoluteAddress = i - slots[0].AddresNum;
                if (absoluteAddress >= 0 && absoluteAddress <= slots.LastOrDefault().AddresNum)
                {
                    int slot = (absoluteAddress / 16); // Localidad de memoria donde se encuentra el byte
                    int offset = (absoluteAddress % 16); // Posicion en la localidad de memoria donde se encuentra el byte
                    mString += slots[slot].Values[offset].ToString("X2");



                }

            }

            m= Convert.ToInt32(mString,16);

            instruction = new Tuple<int, int>(opcode,m);
            return instruction;

        }
    }
}
