using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace JsonHtml {
    public class ErrorListener<S> : ConsoleErrorListener<S>{
        public bool HadError;
        private readonly List<string> _errors = new();

        public string Errors => string.Join("\n", _errors);
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, S offendingSymbol, int line,
            int col, string msg, RecognitionException e) {
            HadError = true;
            _errors.Add($"Syntax error: did not expect '{e.OffendingToken.Text}' at line {line}:{col}, {msg}");
        }
    }
}