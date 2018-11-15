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
    delegate void ExecuteInstruction(int m);

    /// <summary>
    /// Lógica de interacción para MapaMemoria.xaml
    /// </summary>
    public partial class MapaMemoria : Window
    {
        
        Dictionary<int, Tuple<string, string, ExecuteInstruction>> Instructions;
        Dictionary<string, MyInt> Registers;
        MemoryMap MemoryMap;
        int FirstInstructionAddress;
        int ProgramSize;
        int StartAddress;
        string numero = "";

        public MapaMemoria(string rgs)
        {
            
            InitializeComponent();
           
            FillInstructions();

        
            
            string nombre;
            char[] sep = { '\n','\r' };

            string[] registros = rgs.Split(sep);

            nombre = registros[0].Substring(1,6);
            StartAddress = Convert.ToInt32( registros[0].Substring(7, 6),16);
            ProgramSize= Convert.ToInt32(registros[0].Substring(13, 6), 16);

            NombrePrograma.Text = nombre;
            DirInicio.Text=StartAddress.ToString("X4");
            TamanoPrograma.Text = ProgramSize.ToString("X4");

            MemoryMap = new MemoryMap(StartAddress,ProgramSize);


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

            Registers["CP"].Value = FirstInstructionAddress; 

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
            Instructions.Add(216, new Tuple<string, string, ExecuteInstruction>("RD", "", RD));
            Instructions.Add(76, new Tuple<string, string, ExecuteInstruction>("RSUB", "PC <-- (L)", RSUB));
            Instructions.Add(12, new Tuple<string, string, ExecuteInstruction>("STA", "m...m+2 <-- (A)",STA));
            Instructions.Add(84, new Tuple<string, string, ExecuteInstruction>("STCH", "m <-- (A) [el byte más a la derecha]", STCH));
            Instructions.Add(20, new Tuple<string, string, ExecuteInstruction>("STL", "m...m+2 <-- L ", STL));
            Instructions.Add(232, new Tuple<string, string, ExecuteInstruction>("STSW", "m...m+2 <-- (SW)", STSW));
            Instructions.Add(16, new Tuple<string, string, ExecuteInstruction>("STX", "m...m+2 <-- (X)", STX));
            Instructions.Add(28, new Tuple<string, string, ExecuteInstruction>("SUB", "A <-- (A) - (m...m+2)", SUB));
            Instructions.Add(224, new Tuple<string, string, ExecuteInstruction>("TD", "", TD));
            Instructions.Add(44, new Tuple<string, string, ExecuteInstruction>("TIX", "X <-- (X) + 1 ; (X) : (m...m+2)", TIX));
            Instructions.Add(220, new Tuple<string, string, ExecuteInstruction>("WD", "", WD));


            Registers = new Dictionary<string, MyInt>()
            {
              { "CP", new MyInt(0xffffff)},
              { "A", new MyInt(0x000000) },
              { "X", new MyInt(0xffffff) },
              { "L", new MyInt(0xffffff) },
              { "SW", new MyInt(0xffffff)},
              { "CC", new MyInt(0xffffff)}
            };

        }

        private void WD(int m)
        {
            
        }

        private void TD(int m)
        {
            
        }

        private void RD(int m)
        {
            
        }

        private void TIX(int m)
        {
            int X = Registers["X"].Value + 1;
            Registers["X"].Value = X;

            if (X < MemoryMap.ReadWord(m))
                Registers["CC"].Value = 0xffffff;  //valor para < 

            if (X == MemoryMap.ReadWord(m))
                Registers["CC"].Value = 0x00;  //valor para =

            if (X > MemoryMap.ReadWord(m))
                Registers["CC"].Value = 0x0fffff;  //valor para > 

        }

        private void SUB(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(Registers["A"].Value) - MemoryMap.ReadWord(m);
        }

        private void STX(int m)
        {
            MemoryMap.WriteWord(m,Registers["X"].HEX6);
        }

        private void STSW(int m)
        {
            MemoryMap.WriteWord(m, Registers["SW"].HEX6);
        }

        private void STL(int m)
        {
            MemoryMap.WriteWord(m,Registers["L"].HEX6);
        }

        //PEDO
        private void STCH(int m)
        {
            int t =Registers["A"].Value & 0x000000ff;
            MemoryMap.WriteByte(m, t);
        }

        private void STA(int m)
        {

            MemoryMap.WriteWord(m,MemoryMap.ReadWord(Registers["A"].Value).ToString("X6"));
        }

       
        private void RSUB(int m)
        {
            Registers["CP"].Value = MemoryMap.ReadWord(Registers["L"].Value);
        }

        
        private void OR(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(Registers["A"].Value) | MemoryMap.ReadWord(m);
        }

        private void MUL(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(Registers["A"].Value) * MemoryMap.ReadWord(m);
        }

        private void LDX(int m)
        {
            Registers["X"].Value = MemoryMap.ReadWord(m);
        }

        private void LDL(int m)
        {
            Registers["L"].Value = MemoryMap.ReadWord(m);
        }

        private void LDCH(int m)
        {
            int aAux = Registers["A"].Value & 0xffff00; // Variable auxiliar con el byte mas a la derecha en 0´s
            Registers["A"].Value = aAux | MemoryMap.ReadByte(m);
            //Registers["A"].Value= Convert.ToInt32(Registers["A"].HEX4 + MemoryMap.ReadByte(m).ToString("X2"),16);           
        }

        private void LDA(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(m);
        }

        private void JSUB(int m)
        {
            Registers["L"].Value = MemoryMap.ReadWord(Registers["CP"].Value);
            Registers["CP"].Value = m;
        }

        private void JLT(int m)
        {
            if (Registers["CC"].Value == 0xffffff)
                Registers["CP"].Value = m;
        }

        private void JGT(int m)
        {
            if(Registers["CC"].Value == 0x0fffff)
            {
                Registers["CP"].Value = m;
            }
        }

        private void JEQ(int m)
        {
            if (Registers["CC"].Value == 0x00)
            {
                Registers["CP"].Value = m;
            }
        }

        private void J(int m)
        {  
                Registers["CP"].Value = m;
        }

        private void DIV(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(Registers["A"].Value) / MemoryMap.ReadWord(m);
        }

        private void COMP(int m)
        {
            

            if (MemoryMap.ReadWord(Registers["A"].Value) < MemoryMap.ReadWord(m))
                Registers["CC"].Value = 0xffffff;  //valor para < 

            if (MemoryMap.ReadWord(Registers["A"].Value) < MemoryMap.ReadWord(m))
                Registers["CC"].Value = 0x00;  //valor para =

            if (MemoryMap.ReadWord(Registers["A"].Value) < MemoryMap.ReadWord(m))
                Registers["CC"].Value = 0x0fffff;  //valor para > 
        }

        private void ADD(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(Registers["A"].Value) + MemoryMap.ReadWord(m);
        }

        private void AND(int m)
        {
            Registers["A"].Value = MemoryMap.ReadWord(Registers["A"].Value) & MemoryMap.ReadWord(m);
        }

       

      


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroInstrucciones.Text == "")
            {
                NumeroInstrucciones.Text = "1";
            }
            for (int i = 0; i <Int32.Parse(NumeroInstrucciones.Text); i++)
            {
                if (!Fetch())
                    Objeto.Text = "Programa Terminado !";

                // Se actualiza la interfaz
                UpdateView();
            }
        }

        private void NextInstruction_Click(object sender, RoutedEventArgs e)
        {
            Fetch();
            UpdateView();
        }


        

        private bool Fetch()
        {

            int m;

            // Se leen 3 bytes de memoria en la direccion de memoria que indica el CP
            // ReaInstruction ya realiza la decodificacion de la instruccion
            var instruction = MemoryMap.ReadInstruction(Registers["CP"].Value);

            //Se incrementa el CP
            Registers["CP"].Value = Registers["CP"].Value + 3;

            // Si es una instruccion valida
            if (Instructions.ContainsKey(instruction.Item1))
            {
                // Si la instruccion es indexada
                if (instruction.Item2)
                {
                    Instruccion.Text += Instructions[instruction.Item1].Item1 + " " + instruction.Item3.ToString("X4") + ", X\n";
                    m = instruction.Item3 + Registers["X"].Value;
                }
                // Si la instruccion no es indexada
                else
                {
                    Instruccion.Text += Instructions[instruction.Item1].Item1 + " " + instruction.Item3.ToString("X4") + "\n";
                    m = instruction.Item3;
                }

                // Se verifica que el programa no se salga del area de memoria
                if (!(m <= StartAddress + ProgramSize && m >= StartAddress))
                {
                    MessageBox.Show("el programa finalizo");
                    return false;
                }

                // Cambio de valores en la interfaz
                Efecto.Text += Instructions[instruction.Item1].Item2 + "\n";
                Objeto.Text += instruction.Item4 + "\n";


                // Delegado de la funcion que se ejecutará
                var instructionDelegate = Instructions[instruction.Item1].Item3;

                // Se manda llamar a la funcion de la instruccion
                instructionDelegate.Invoke(m);
            }
            else
                return false;

            return true;

        }

        private void UpdateView()
        {
            Mapa.ItemsSource = null;
            Registros.ItemsSource = null;
            Mapa.ItemsSource = MemoryMap.Slots;
            Registros.ItemsSource = Registers;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                Fetch();
            }
        }
    }




   
}
