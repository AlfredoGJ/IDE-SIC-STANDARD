using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace IDE_ProgSistemas
{
    class MyErrorListenerXE : Antlr4.Runtime.BaseErrorListener, IAntlrErrorListener<int>
    {


        // Recibe los errores del Parser
        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {

            App.ListaErrores?.Add(String.Format("Error en linea {0}, columna {1}", line, charPositionInLine));
            App.listalinea.Add(line);
        }

        // Recibe los errores del Lexer
        public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] int offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {

            App.ListaErrores?.Add(String.Format("Error en linea {0}, columna {1}", line, charPositionInLine));
            App.listalinea.Add(line);
        }
    }
}
