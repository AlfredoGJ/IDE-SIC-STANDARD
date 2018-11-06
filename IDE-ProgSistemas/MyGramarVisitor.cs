using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace IDE_ProgSistemas
{

    public class CodeRow
    {
        public string CP { get; set; }
        public string Etiqueta { get; set; }
        public string Proposicion { get; set; }
        public string Operando { get; set; }
        public string CodigoObjeto { get; set; }


        public CodeRow()
        {
        }

        public CodeRow(string etiq, string prop, string operando)
        {

            Etiqueta = etiq;
            Proposicion = prop;
            Operando = operando;
        }

    }

    class MyGramarVisitor : SIC_STDBaseListener
    {
        public string PC { get; set; }

        public override void EnterProposicion([NotNull] SIC_STDParser.ProposicionContext context)
        {
           


        }

        public override void ExitProposicion([NotNull] SIC_STDParser.ProposicionContext context)
        {


            CodeRow line = new CodeRow();  // Inicializacion solo para que no chille el compilador, mas adelante se cambian los valores


            // Si la proposicion es una instruccion
            var isInstruccion = context.instruccion();


            if (isInstruccion != null)
            {
                var id = isInstruccion.ID();
                var instruccion = isInstruccion.INSTRUCCION();
                var operando = isInstruccion.opinstruccion();
                line = new CodeRow(id?.GetText(), instruccion?.GetText(), operando?.GetText());
                uint t = (uint)Int32.Parse(PC, System.Globalization.NumberStyles.HexNumber);
                line.CP = t.ToString("X");
                if (hayerror(context.start.Line) == false)
                {
                    t = t + 3;
                    PC = t.ToString("X");
                }
               
                App.Codigo.Add(line);
            }

            // Si la proposicion es una directiva
            var isDirectiva = context.directiva();
            if (isDirectiva != null)
            {


                // Si la directiva no es BYTE
                var byteType = isDirectiva.bytedir();
                if (byteType == null)
                {
                    var id = isDirectiva.ID();
                    var directiva = isDirectiva.TIPODIRECTIVA();
                    var num = isDirectiva.NUM();

                    line = new CodeRow(id?.GetText(), directiva?.GetText(), num?.GetText());
                    uint t = (uint)Int32.Parse(PC, System.Globalization.NumberStyles.HexNumber);
                    line.CP = t.ToString("X");
                    if (hayerror(context.start.Line) == false)
                    {

                        if (directiva?.GetText() == "RESW")
                        {
                            string c = num?.GetText();
                            uint t1;

                            if (num.GetText().Contains("H") || num.GetText().Contains("h"))
                            {
                                c = num?.GetText().Remove(num.GetText().Length - 1, 1);
                                if (c != "")
                                {

                                    if (hayerror(context.start.Line) != true)
                                    {
                                        t = t + (t1 = (uint)Int32.Parse(c, System.Globalization.NumberStyles.HexNumber) * 3);
                                        PC = t.ToString("X");
                                    }
                                }
                            }
                            else
                            {
                                if (c != "")
                                {

                                    line.CP = t.ToString("X");
                                    if (hayerror(context.start.Line) != true)
                                    {
                                        t = t + (t1 = (uint)Int32.Parse(c) * 3);
                                        PC = t.ToString("X");
                                    }
                                }
                            }
                        }
                        if (directiva?.GetText() == "RESB")
                        {
                            string c = num?.GetText();

                            if (num.GetText().Contains("H") || num.GetText().Contains("h"))
                            {
                                c = num?.GetText().Remove(num.GetText().Length - 1, 1);
                                if (c != "")
                                {
                                    line.CP = t.ToString("X");
                                    if (hayerror(context.start.Line) != true)
                                    {
                                        t = t + (uint)Int32.Parse(c, System.Globalization.NumberStyles.HexNumber);
                                        PC = t.ToString("X");
                                    }
                                }
                            }
                            else
                            {
                                if (c != "")
                                {
                                    line.CP = t.ToString("X");
                                    if (hayerror(context.start.Line) != true)
                                    {
                                        t = t + (uint)Int32.Parse(c);
                                        PC = t.ToString("X");
                                    }
                                }
                            }
                        }
                        if (directiva?.GetText() == "WORD")
                        {

                            line.CP = t.ToString("X");
                            if (hayerror(context.start.Line) != true)
                            {
                                t = t + 3;
                                PC = t.ToString("X");
                            }
                        }
                    }


                    App.Codigo.Add(line);
                }
                // Si la directiva es BYTE
                else
                {
                    var id = isDirectiva.ID();
                    var b = byteType.BYTE();
                    var operando = byteType.BYTEOP();
                    line = new CodeRow(id?.GetText(), b?.GetText(), operando?.GetText());
                    uint P = (uint)Int32.Parse(PC, System.Globalization.NumberStyles.HexNumber);
                    line.CP = P.ToString("X");




                    if (!hayerror(context.start.Line))
                    {
                        string t = operando.GetText().Remove(1, operando.GetText().Length - 1);
                        if (t == "C")
                        {
                            string J = operando.GetText().Remove(0, 2);
                            J = J.Remove(J.Length - 1, 1);

                            if (hayerror(context.start.Line) != true)
                            {
                                P = P + (uint)J.Length;
                                PC = P.ToString("X");
                            }
                        }
                        if (t == "X")
                        {
                            string J = operando.GetText().Remove(0, 2);
                            J = J.Remove(J.Length - 1, 1);

                            line.CP = P.ToString("X");
                            if (hayerror(context.start.Line) != true)
                            {
                                if (J.Length % 2 == 0)
                                {
                                    P = P + ((uint)J.Length / 2);
                                }
                                else
                                {
                                    P = P + (((uint)J.Length + 1) / 2);
                                }
                                PC = P.ToString("X");
                            }
                        }
                    }


                    App.Codigo.Add(line);

                }

            }

            // Si la proposicion es RSUB
            var isRsub = context.rsub();
            if (isRsub != null)
            {
                line = new CodeRow("", isRsub.GetText().Trim(), "");
                uint t = (uint)Int32.Parse(PC, System.Globalization.NumberStyles.HexNumber);
                line.CP = t.ToString("X");
                if (hayerror(context.start.Line) != true)
                {
                    t = t + 3;
                    PC = t.ToString("X");
                }
                App.Codigo.Add(line);
            }



            //TODO: checar por error en el que etiqueta es una instruccion, hacer el calculo del cp para cada instruccion y asignarlo a line



            // Se añade a la tabla de simbolos
            if (!String.IsNullOrEmpty(line.Etiqueta))
            {
                if (!App.Tabsim.ContainsKey(line.Etiqueta))
                {
                    if (hayerror(context.Start.Line) == false)
                    {
                        App.Tabsim.Add(line.Etiqueta, line.CP);
                    }
                }

            }


        }

     

        public override void ExitFin([NotNull] SIC_STDParser.FinContext context)
        {
            base.ExitFin(context);
            uint t = (uint)Int32.Parse(PC, System.Globalization.NumberStyles.HexNumber);
            uint t1 = (uint)Int32.Parse(App.direccionInicio, System.Globalization.NumberStyles.HexNumber);
            t = t - t1;
            App.tamaño = t.ToString("X6");

            CodeRow final = new CodeRow("","END",context.ID()?.GetText());
            final.CP = PC;
            App.Codigo.Add(final);
        }

        public override void ExitInicio([NotNull] SIC_STDParser.InicioContext context)
        {
            // Si no hubo error
            if (context.exception == null)
            {
                App.nombre = context.Start.Text;
                var dirInicio = context.NUM().GetText().Replace('H', ' ').Replace('h',' ');
                var id = context.ID();
                CodeRow inicio = new CodeRow(id?.GetText(),"START", dirInicio);
                inicio.CP = inicio.Operando;
                App.direccionInicio= inicio.CP;
                PC = inicio.CP;
                App.Codigo.Add(inicio);

            }
           

        }

        public bool hayerror(int line)
        {
            bool res = false;
            for (int i = 0; i < App.listalinea.Count; i++)
            {
                if (App.listalinea[i] == line)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public override void ExitInstruccion([NotNull] SIC_STDParser.InstruccionContext context)
        {
            //base.ExitInstruccion(context);

            //Console.WriteLine(String.Format("ID={0}, Instruccion= {1}, Operador={2}", context.ID().GetText(), context.INSTRUCCIONES().GetText(), context.opinstruccion().GetText()));


            //var id  = context.ID();
            //var instruccion = context.INSTRUCCION();
            //var operando = context.opinstruccion();
            //CodeRow line = new CodeRow(id?.GetText(),instruccion?.GetText(), operando?.GetText());
            //App.Codigo.Add(line);
        }

        public override void ExitDirectiva([NotNull] SIC_STDParser.DirectivaContext context)
        {
            //var byteType = context.BYTE();

            //if (byteType == null)
            //{
            //    var id = context.ID();
            //    var directiva = context.TIPODIRECTIVA();
            //    var num = context.NUM();

            //    CodeRow line = new CodeRow(id?.GetText(), directiva?.GetText(), num?.GetText());
            //    App.Codigo.Add(line);
            //}



        }
    }
}
