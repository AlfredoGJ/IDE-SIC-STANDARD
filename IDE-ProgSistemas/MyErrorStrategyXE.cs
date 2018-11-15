using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace IDE_ProgSistemas
{
    class MyErrorStrategyXE : Antlr4.Runtime.DefaultErrorStrategy
    {
        public override void ReportError(Parser recognizer, RecognitionException e)
        {
            try
            {
                RecoverInline(recognizer);
            }
            catch
            {
                Console.WriteLine("EROROFOSFOSODC");
            }
        }

    }
}
