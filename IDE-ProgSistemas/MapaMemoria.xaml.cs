using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static IDE_ProgSistemas.App;

namespace IDE_ProgSistemas
{
    /// <summary>
    /// Lógica de interacción para MapaMemoria.xaml
    /// </summary>
    public partial class MapaMemoria : Window
    {
        delegate void ExecuteInstruction(int m);
        Dictionary<int, Tuple<string, string, ExecuteInstruction>> Instructions;
        Dictionary<string, MyInt> Registers = new Dictionary<string, MyInt>() { { "CP", new MyInt(255) }, { "A", new MyInt(255) }, { "X", new MyInt(255) }, { "L", new MyInt(255) }, { "SW", new MyInt(255) }, { "CC", new MyInt(255) } };
        MemoryMap MemoryMap;
        int FirstInstructionAddress;

        public MapaMemoria(string rgs)
        {
            InitializeComponent();

            FillInstructions();

            int dirInicio;
            int tamanio;
            string nombre;
            char[] sep = { '\n','\r' };

            string[] registros = rgs.Split(sep);

            nombre = registros[0].Substring(1,6);
            dirInicio = Convert.ToInt32( registros[0].Substring(7, 6),16);
            tamanio= Convert.ToInt32(registros[0].Substring(13, 6), 16);

            NombrePrograma.Text = nombre;
            DirInicio.Text=dirInicio.ToString("X4");
            TamanoPrograma.Text = tamanio.ToString("X4");

            MemoryMap = new MemoryMap(dirInicio,tamanio);


            foreach (string registro in registros)
            {
                if (!string.IsNullOrEmpty(registro) )
                {
                    if (registro[0] == 'T')
                    {
                        int address = Convert.ToInt32(registro.Substring(3, 4), 16);

                        for (int i = 9; i < registro.Length; i = i + 2)
                        {
                            MemoryMap.WriteByte(address, Convert.ToInt32(registro.Substring(i, 2), 16));
                            address++;
                        }
                    }
                    if (registro[0] == 'E')
                    {
                        FirstInstructionAddress= Convert.ToInt32(registro.Substring(1, 6), 16);
                    }
                    
                }    
            }

            Mapa.ItemsSource = MemoryMap.Slots ;
            Registros.ItemsSource = Registers; 

        }

        private void FillInstructions()
        {
            Instructions = new Dictionary<int, Tuple<string, string, ExecuteInstruction>>();
            Instructions.Add(24, new Tuple<string, string, ExecuteInstruction>("ADD", "A <-- (A) + (m...m+2) ", ADD));
            Instructions.Add(64, new Tuple<string, string, ExecuteInstruction>("AND", "A <-- (A) & (m...m+2) ", AND));
            Instructions.Add(40, new Tuple<string, string, ExecuteInstruction>("COMP", "(A) : (A) & (m...m+2) ", COMP));
            Instructions.Add(36, new Tuple<string, string, ExecuteInstruction>("DIV", "A <-- (A) / (m...m+2) ", DIV));
            Instructions.Add(60, new Tuple<string, string, ExecuteInstruction>("J", "CP <-- m ", J));
            Instructions.Add(48, new Tuple<string, string, ExecuteInstruction>("JEQ","CP <-- m si CC está en =  ", JEQ));
            Instructions.Add(52, new Tuple<string, string, ExecuteInstruction>("JGT", "CP <-- m si CC está en > ", JGT));
            Instructions.Add(56, new Tuple<string, string, ExecuteInstruction>("JLT", "CP <-- m si CC está en < ", JLT));
            Instructions.Add(72, new Tuple<string, string, ExecuteInstruction>("JSUB", "L <-- (CP); CP <--m", JSUB));
            Instructions.Add(00, new Tuple<string, string, ExecuteInstruction>("LDA", "A <-- (m...m+2)", LDA));
            Instructions.Add(80, new Tuple<string, string, ExecuteInstruction>("LDCH", "A[el byte más a la derecha] <-- (m)", LDCH));
            Instructions.Add(08, new Tuple<string, string, ExecuteInstruction>("LDL", "L <-- (m...m+2)", LDL));
            Instructions.Add(04, new Tuple<string, string, ExecuteInstruction>("LDX", "X <-- (m...m+2)", LDX));
            Instructions.Add(32, new Tuple<string, string, ExecuteInstruction>("MUL", "A <-- (A) * (m...m+2)", MUL));
            Instructions.Add(68, new Tuple<string, string, ExecuteInstruction>("OR", "A <-- (A) | (m...m+2)", OR));
            //Instructtions.Add(00, new Tuple<string, string, ExecuteInstruction>("RD", "", RD));
            Instructions.Add(76, new Tuple<string, string, ExecuteInstruction>("RSUB", "PC <-- (L)", RSUB));
            Instructions.Add(12, new Tuple<string, string, ExecuteInstruction>("STA", "m...m+2 <-- (A)",STA));
            Instructions.Add(84, new Tuple<string, string, ExecuteInstruction>("STCH", "m <-- (A) [el byte más a la derecha]", STCH));
            Instructions.Add(20, new Tuple<string, string, ExecuteInstruction>("STL", "m...m+2 <-- L ", STL));
            Instructions.Add(232, new Tuple<string, string, ExecuteInstruction>("STSW", "m...m+2 <-- (SW)", STSW));
            Instructions.Add(16, new Tuple<string, string, ExecuteInstruction>("STX", "m...m+2 <-- (X)", STX));
            Instructions.Add(28, new Tuple<string, string, ExecuteInstruction>("SUB", "A <-- (A) - (m...m+2)", SUB));
            Instructions.Add(44, new Tuple<string, string, ExecuteInstruction>("SUB", "X <-- (X) + 1 ; (X) : (m...m+2)", TIX));
        }

        private void TIX(int m)
        {
            throw new NotImplementedException();


        }

        private void SUB(int m)
        {
            throw new NotImplementedException();
        }

        private void STX(int m)
        {
            throw new NotImplementedException();
        }

        private void STSW(int m)
        {
            throw new NotImplementedException();
        }

        private void STL(int m)
        {
            throw new NotImplementedException();
        }

        private void STCH(int m)
        {
            throw new NotImplementedException();
        }

        private void STA(int m)
        {
            throw new NotImplementedException();
        }

        private void RSUB(int m)
        {
            throw new NotImplementedException();
        }

        private void OR(int m)
        {
            throw new NotImplementedException();
        }

        private void MUL(int m)
        {
            throw new NotImplementedException();
        }

        private void LDX(int m)
        {
            throw new NotImplementedException();
        }

        private void LDL(int m)
        {
            throw new NotImplementedException();
        }

        private void LDCH(int m)
        {
            throw new NotImplementedException();
        }

        private void LDA(int m)
        {
            throw new NotImplementedException();
        }

        private void JSUB(int m)
        {
            throw new NotImplementedException();
        }

        private void JLT(int m)
        {
            throw new NotImplementedException();
        }

        private void JGT(int m)
        {
            throw new NotImplementedException();
        }

        private void JEQ(int m)
        {
            throw new NotImplementedException();
        }

        private void J(int m)
        {
            throw new NotImplementedException();
        }

        private void DIV(int m)
        {
            throw new NotImplementedException();
        }

        private void COMP(int m)
        {
            throw new NotImplementedException();
        }

        private void ADD(int m)
        {
            Console.WriteLine("Se hizo suma bien perra");
            Registers["A"] = new MyInt(Registers["A"].Value + MemoryMap.ReadWord(m));
        }
        private void AND(int m)
        {
            Console.WriteLine("Se hizo AND bien perra");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var first = FirstInstructionAddress;
            var g = MemoryMap.ReadInstruction(first)*/
            

        }

        private void ejecutaInstruccion(int m)
        {
            /*for (int i = 0; i < Instructions.Count; i++)
            {
                
                    case Instructions[i].
                
            */
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }




    public class MemorySlot
    {

        public string B0 { get =>values[0].ToString("X2"); set=> values[0] = Convert.ToInt32( value); }
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
        public string BE{ get => values[14].ToString("X2"); set => values[14] = Convert.ToInt32(value); }
        public string BF { get => values[15].ToString("X2"); set => values[15] = Convert.ToInt32(value); }

        public string Address { get => address.ToString("X4"); set => address= Convert.ToInt32(value); }
        public int AddresNum { get => address; }
        private int address;
        private List<int> values;
        public List<int> Values { get=> values; }

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
