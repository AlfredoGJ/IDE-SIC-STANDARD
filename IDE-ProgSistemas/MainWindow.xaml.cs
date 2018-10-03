using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using ICSharpCode.AvalonEdit;
using Antlr4.Runtime;

namespace IDE_ProgSistemas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String currentFilePath = "";

        public MainWindow()
        {
            InitializeComponent();
            //testGrammar();
        }

        private void Archivo_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            currentFilePath = "";
            CodigoFuente.Clear();
            ClearEverything();
        }

        private void Abrir_Archivo_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                Stream stream = File.OpenRead(openFile.FileName);
                currentFilePath = openFile.FileName;
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                string sinput = "";
                foreach (byte c in data)
                {
                    sinput += (char)c;
                }

                stream.Close();
                CodigoFuente.Text = sinput;

               

            }
        }

        private void Guardar_Archivo_Click(object sender, RoutedEventArgs e)
        {


            SaveFileDialog g = new SaveFileDialog();
            // guardar en el archivo original
            g.Filter = "s Files (*.s)|*.s";
            if (g.ShowDialog() == true)
            {
                Stream stream = File.Create(g.FileName);
                currentFilePath = g.FileName;

                string per = CodigoFuente.Text;
                StreamWriter writer = new StreamWriter(stream);

                writer.Write(per);

                writer.Close();
                stream.Close();
            }


        }

        private void Analizar_Click(object sender, RoutedEventArgs e)
        {
            tam.Content = "";
            tam.Content = "tamaño: ";

            // Clean Errors list, tabsim, archivo intermedio, interfaz, etc. 
            ClearEverything();

            string input = CodigoFuente.Text;
            

            // Create Listeners

            MyErrorListener myErrorListener = new MyErrorListener();
            MyErrorStrategy myErrorStrategy = new MyErrorStrategy();
            MyGramarVisitor myGramarVisitor = new MyGramarVisitor();

            // Create and initialize Lexer

            SIC_STDLexer lex = new SIC_STDLexer(new AntlrInputStream(input));
            CommonTokenStream tokens = new CommonTokenStream(lex);
            lex.RemoveErrorListeners();
            lex.AddErrorListener(myErrorListener);


            // Create and initialize Parser

            SIC_STDParser parser = new SIC_STDParser(tokens);
            parser.ErrorHandler = myErrorStrategy;

            parser.RemoveErrorListeners();
            parser.AddErrorListener(myErrorListener);

            parser.RemoveParseListeners();
            parser.AddParseListener(myGramarVisitor);
          

            // Do the parseichon 
            parser.programa();
           


            // Vaciar los datos obtenidos en la interfaz


            // Lista de errores
            if (App.ListaErrores.Count > 0)
            {
                foreach (string s in App.ListaErrores)
                    Errores.Text += s+'\n';
            }   
            else
                Errores.Text = "No se encontraron errores";

            ArchivoIntermedio.ItemsSource = App.Codigo;


            // Tabsim
            Tabsim.ItemsSource= App.Tabsim.ToList();
            tam.Content += App.tamaño;

            Paso2();



        }

        private void Paso2()
        {

            foreach (CodeRow row in App.Codigo)
            {

                // Si es una proposicion que genera codigo objeto
                if (row.Proposicion != "START" & row.Proposicion != "END" & row.Proposicion != "RESW" & row.Proposicion != "RESB")
                {
                    // Si es una instruccion
                    if (row.Proposicion != "BYTE" && row.Proposicion != "WORD")
                    {
                        string opCode = App.OpCodes[row.Proposicion].ToString("X2");
                        string symbol;
                        char[] sep = new char[1];
                        sep[0] = ',';

                        // si tiene operando (no es instruccion RSUB)
                        if (!string.IsNullOrEmpty(row.Operando))
                        {
                            // Si el modo de direccionamiento es indeado
                            if (row.Operando.Contains(','))
                                symbol = row.Operando.Split(sep)[0].Trim();
                            // El modo de direccionamiento es directo
                            else
                                symbol = row.Operando;

                            row.CodigoObjeto = opCode + App.Tabsim[symbol];

                        }
                        // Es la instruccion RSUB
                        else
                            row.CodigoObjeto = "4C0000";
                    }

                    // Es una directiva
                    else
                    {
                        if (row.Proposicion == "WORD")
                        {
                            char[] sep = new char[] { 'h', 'H' };
                            int value = 0;
                            if (row.Operando.Contains('h') || row.Operando.Contains('H'))
                                value = Convert.ToInt32(row.Operando.Trim(sep), 16);
                            else
                                value = Convert.ToInt32(row.Operando);

                            row.CodigoObjeto = value.ToString("X6");
                        }

                        if (row.Proposicion == "BYTE")
                        {
                        }
                    }





                }
                else
                    row.CodigoObjeto = "--------";


                    


                }

            }

          
        


        private void ClearEverything()
        {
            Errores.Text = "";
            Tabsim.ItemsSource = null;
            Tabsim.Items.Clear();

            ArchivoIntermedio.ItemsSource = null;
            ArchivoIntermedio.Items.Clear();

            App.ListaErrores.Clear();
            App.Codigo.Clear();
            App.Tabsim.Clear();
        }

        private void testGrammar()
        {
            //ClearEverything();

            //string input = CodigoFuente.Text;
            //string errores = "";


            //SIC_STDLexer lex = new SIC_STDLexer(new AntlrInputStream("BAS	BYTE	C'fer'"));
            //CommonTokenStream tokens = new CommonTokenStream(lex);


            //Stream errorOutput = new MemoryStream();
            //StreamWriter streamWriter = new StreamWriter(errorOutput);
            ////Console.SetError(streamWriter);

            //SIC_STDParser parser = new SIC_STDParser(tokens);

            //parser.AddErrorListener(new MyErrorListener());
            //parser.AddParseListener(new MyGramarVisitor());
            //parser.proposicion();
        }

    }
    }

