// Template generated code from Antlr4BuildTasks.Template v 8.17

namespace JsonHtml {
    using Antlr4.Runtime;
    using System.Text;

    public class Program {
        static void Main(string[] args) {
            Try("""
            :root {
                head {        
                   "";
               }
               body {
                    " ma baddy";
               }
            }
            """
            );
        }

        static void Try(string input) {
            var str = new AntlrInputStream(input);
            System.Console.WriteLine(input);
            var lexer = new JsonHtmlLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new JsonHtmlParser(tokens);
            var listener_lexer = new ErrorListener<int>();
            var listener_parser = new ErrorListener<IToken>();
            lexer.AddErrorListener(listener_lexer);
            parser.AddErrorListener(listener_parser);
            var tree = parser.root();
            if (listener_lexer.had_error || listener_parser.had_error)
                System.Console.WriteLine("error in parse.");
            else
                System.Console.WriteLine("parse completed.");
        }

        static string ReadAllInput(string fn) {
            var input = System.IO.File.ReadAllText(fn);
            return input;
        }
    }
}