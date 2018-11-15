using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace IDE_ProgSistemas
{
    class MyGrammarVisitorXE: SIC_XEBaseListener
    {
        public override void ExitProposicion([NotNull] SIC_XEParser.ProposicionContext context)
        {
            base.ExitProposicion(context);

        }

        public override void ExitInstruccion([NotNull] SIC_XEParser.InstruccionContext context)
        {

            CodeRow line= new CodeRow();
            line.Etiqueta=context.ID()?.GetText();
            line.CP = App.CP.ToString("X4");

            // Formato 1
            if (context.format1()!=null)
            {
                App.CP++;
                line.Proposicion = context.format1().INST1().GetText();
            }
            // Formato 2
            if (context.format1() != null)
            {

            }
            // Formato 3
            if (context.EXT() == null)
            {
                App.CP += 3;
                var Format3 = context.format3();

                line.Proposicion =Format3.INST3().GetText();

                if (Format3.ID() != null)
                {
                    line.Operando = Format3.MODIR()?.GetText() + Format3.ID()?.GetText() + Format3.INDEX()?.GetText();
                }
                else
                {
                    line.Operando = Format3.MODIR()?.GetText() + Format3.NUM()?.GetText() + Format3.INDEX()?.GetText();
                }

            }
            // Formato 4
            else
            {
                App.CP += 4;
                var Format3 = context.format3();

                line.Proposicion = context.EXT().GetText()+ Format3.INST3().GetText();

                if (Format3.ID() != null)
                {
                    line.Operando = Format3.MODIR()?.GetText() + Format3.ID()?.GetText() + Format3.INDEX()?.GetText();
                }
                else
                {
                    line.Operando = Format3.MODIR()?.GetText() + Format3.NUM()?.GetText() + Format3.INDEX()?.GetText();
                }
            }


            App.Codigo.Add(line);


        }

        public override void ExitDirectiva([NotNull] SIC_XEParser.DirectivaContext context)
        {

            CodeRow line = new CodeRow();


            var id = context.ID(0);
            var directiva = context.TIPODIRECTIVA();
            var num = context.NUM();

            line = new CodeRow(id.GetText(), directiva?.GetText(), num?.GetText());
            line.CP = App.CP.ToString("X16") ;

            // SI NO ES BYTE
            var byteType = context.bytedir();
            if (byteType == null)
            {

                if (directiva?.GetText() == "RESW")
                {
                    string c = num?.GetText();

                    if (num.GetText().Contains("H") || num.GetText().Contains("h"))
                    {
                        c = num?.GetText().Remove(num.GetText().Length - 1, 1);
                        if (c != "")
                        {
                            App.CP += (Convert.ToInt32(c, 16) * 3);
                        }
                    }
                    else
                    {
                        if (c != "")
                            App.CP += (Int32.Parse(c) * 3);
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
                            App.CP += Convert.ToInt32(c, 16);
                        }
                    }
                    else
                    {
                        if (c != "")
                        {
                            App.CP += Int32.Parse(c);
                        }
                    }
                }
                if (directiva?.GetText() == "WORD")
                {
                    App.CP += 3;
                }


            }
            // SI ES BYTE
            else
            {

                var b = byteType.BYTE();
                var operando = byteType.BYTEOP();
                line = new CodeRow(id.GetText(), b?.GetText(), operando?.GetText());


                string t = operando.GetText().Remove(1, operando.GetText().Length - 1);
                if (t == "C")
                {
                    string J = operando.GetText().Remove(0, 2);
                    J = J.Remove(J.Length - 1, 1);


                    App.CP += J.Length;


                }
                if (t == "X")
                {
                    string J = operando.GetText().Remove(0, 2);
                    J = J.Remove(J.Length - 1, 1);



                    if (J.Length % 2 == 0)
                    {
                        App.CP += (J.Length / 2);
                    }
                    else
                    {
                        App.CP += ((J.Length + 1) / 2);
                    }


                }
            }

            // SI ES BASE
            if (context.BASE() != null)
            {
                var ID1 = context.ID(1);
                if (ID1 !=null)
                    line = new CodeRow(context.ID(0).GetText(), "BASE", ID1.GetText());
                else
                    line = new CodeRow("", "BASE", context.ID(0).GetText());



            }
            line.CP = App.CP.ToString("X4");
            App.Codigo.Add(line);

        }

        public override void EnterDirectiva([NotNull] SIC_XEParser.DirectivaContext context)
        {
            base.EnterDirectiva(context);
        }


    }

    
}
