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
            //Console.WriteLine("Proposition:"+context.GetText());
            //base.EnterProposicion(context);
            

        }

        public override void ExitProposicion([NotNull] SIC_STDParser.ProposicionContext context)
        {
            //base.ExitProposicion(context);
            Console.WriteLine("Proposition:" + context.GetText());
            //if (context.exception != null)
            //    Console.WriteLine("Proposition:" + context.GetText());


        }

        public override void ExitInstruccion([NotNull] SIC_STDParser.InstruccionContext context)
        {
            //base.ExitInstruccion(context);

            //Console.WriteLine(String.Format("ID={0}, Instruccion= {1}, Operador={2}", context.ID().GetText(), context.INSTRUCCIONES().GetText(), context.opinstruccion().GetText()));


            var id  = context.ID();
            var instruccion = context.INSTRUCCION();
            var operando = context.opinstruccion();
            CodeRow line = new CodeRow(id?.GetText(),instruccion?.GetText(), operando?.GetText());
            App.Codigo.Add(line);
        }

        public override void ExitDirectiva([NotNull] SIC_STDParser.DirectivaContext context)
        {
            var id = context.ID();
            var instruccion = context.INSTRUCCION();
            var operando = context.opinstruccion();
            CodeRow line = new CodeRow(id?.GetText(), instruccion?.GetText(), operando?.GetText());
            App.Codigo.Add(line);
        }
    }
}
