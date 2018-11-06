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
            for (int i = 0; i <3; i++)
            {

                word = word+ReadByte(address+i).ToString("X2");
                
            }
            return Convert.ToInt32(word, 16);
        }

        public void WriteWord(int address, string word)
        {

            for (int i=0;i<3;i++)
            {
                    WriteByte(address+i,Convert.ToInt32( word.Substring(i*2,2),16));
            }


          
        }


        // Lee la instruccion y regresa ina tupla con(codigo de operacion,es indexada o no, m,el codigo objeto en una cadena)
        public Tuple<int,bool, int,string> ReadInstruction(int address)
        {
            Tuple<int,bool, int, string> instruction;

            int opcode = ReadByte(address);
            int m;
            string mString = "";
            bool isIndexed=false;
            
            for (int i = address+1; i <= address+2; i++)
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
            if (m >= 32768)
            {
                m = m - 32768;
                isIndexed = true;
            }
            mString = opcode.ToString("X2") + mString;


            instruction = new Tuple<int,bool, int,string>(opcode,isIndexed,m,mString);
            return instruction;


        }
    }
}
