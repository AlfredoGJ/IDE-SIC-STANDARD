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
        public int CP { get; set; }
        public string Etiqueta { get; set;}
        public string Proposicion { get; set;}
        public string Operando { get; set;}
        public string CodigoObjeto { get; set; }


        public CodeRow()
        {
        }

        public CodeRow( string etiq, string prop, string operando)
        {
            
            Etiqueta = etiq;
            Proposicion = prop;
            Operando = operando;
        }

    }

    class MyGramarVisitor : SIC_STDBaseListener
    {

        public override void EnterProposicion([NotNull] SIC_STDParser.ProposicionContext context)
        {
            Console.WriteLine("Proposition:"+context.GetText());
            //base.EnterProposicion(context);
            

        }

        public override void ExitProposicion([NotNull] SIC_STDParser.ProposicionContext context)
        {


            CodeRow line= new CodeRow();  // Inicializacion solo para que no chille el compilador, mas adelante se cambian los valores


            // Si la proposicion es una instruccion
            var isInstruccion = context.instruccion();
            if ( isInstruccion!= null)
            {
                var id = isInstruccion.ID();
                var instruccion = isInstruccion.INSTRUCCION();
                var operando = isInstruccion.opinstruccion();
                line = new CodeRow(id?.GetText(), instruccion?.GetText(), operando?.GetText());
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
                    App.Codigo.Add(line);
                }
                // Si la directiva es BYTE
                else
                {
                    var id = isDirectiva.ID();
                    var b= byteType.BYTE();
                    var operando = byteType.BYTEOP();
                    line = new CodeRow(id?.GetText(), b?.GetText(), operando?.GetText());
                    App.Codigo.Add(line);

                }

            }

            // Si la proposicion es RSUB
            var isRsub = context.rsub();
            if (isRsub != null)
            {
                line = new CodeRow("", isRsub.GetText().Trim(),"");
                App.Codigo.Add(line);
            }



            //TODO: checar por error en el que etiqueta es una instruccion, hacer el calculo del cp para cada instruccion y asignarlo a line



            // Se añade a la tabla de simbolos
            if (!String.IsNullOrEmpty( line.Etiqueta))
            {
                if (!App.Tabsim.ContainsKey(line.Etiqueta))
                {
                    App.Tabsim.Add(line.Etiqueta,line.CP.ToString());
                }
                
            }


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

        //public override rs
    }
}
