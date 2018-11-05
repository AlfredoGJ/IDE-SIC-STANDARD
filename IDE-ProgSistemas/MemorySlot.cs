using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE_ProgSistemas
{
    public class MemorySlot
    {

        public string B0 { get => values[0].ToString("X2"); set => values[0] = Convert.ToInt32(value); }
        public string B1 { get => values[1].ToString("X2"); set => values[1] = Convert.ToInt32(value); }
        public string B2 { get => values[2].ToString("X2"); set => values[2] = Convert.ToInt32(value); }
        public string B3 { get => values[3].ToString("X2"); set => values[3] = Convert.ToInt32(value); }
        public string B4 { get => values[4].ToString("X2"); set => values[4] = Convert.ToInt32(value); }
        public string B5 { get => values[5].ToString("X2"); set => values[5] = Convert.ToInt32(value); }
        public string B6 { get => values[6].ToString("X2"); set => values[6] = Convert.ToInt32(value); }
        public string B7 { get => values[7].ToString("X2"); set => values[7] = Convert.ToInt32(value); }
        public string B8 { get => values[8].ToString("X2"); set => values[8] = Convert.ToInt32(value); }
        public string B9 { get => values[9].ToString("X2"); set => values[9] = Convert.ToInt32(value); }
        public string BA { get => values[10].ToString("X2"); set => values[10] = Convert.ToInt32(value); }
        public string BB { get => values[11].ToString("X2"); set => values[11] = Convert.ToInt32(value); }
        public string BC { get => values[12].ToString("X2"); set => values[12] = Convert.ToInt32(value); }
        public string BD { get => values[13].ToString("X2"); set => values[13] = Convert.ToInt32(value); }
        public string BE { get => values[14].ToString("X2"); set => values[14] = Convert.ToInt32(value); }
        public string BF { get => values[15].ToString("X2"); set => values[15] = Convert.ToInt32(value); }

        public string Address { get => address.ToString("X4"); set => address = Convert.ToInt32(value); }
        public int AddresNum { get => address; }
        private int address;
        private List<int> values;
        public List<int> Values { get => values; }

        public MemorySlot(int address)
        {
            this.address = address;
            values = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                values.Add(255);
            }

        }
    }
}
