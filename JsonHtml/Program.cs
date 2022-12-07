// Template generated code from Antlr4BuildTasks.Template v 8.17

using System;
using Antlr4.Runtime;
using JsonHtml;

void Try(string input) {
    var str = new AntlrInputStream(input);
    // lex
    var lexer = new JsonHtmlLexer(str);
    lexer.RemoveErrorListener(ConsoleErrorListener<int>.Instance);
    var listenerLexer = new ErrorListener<int>();
    lexer.AddErrorListener(listenerLexer);
    var tokens = new CommonTokenStream(lexer);
    
    // parse
    var parser = new JsonHtmlParser(tokens);
    parser.RemoveErrorListener(ConsoleErrorListener<IToken>.Instance);
    var listenerParser = new ErrorListener<IToken>();
    parser.AddErrorListener(listenerParser);
    var tree = parser.root();
    
    // build html
    var builder = new HtmlBuilder();
    
    if (listenerLexer.HadError)
        Console.WriteLine(listenerLexer.Errors);
    else if(listenerParser.HadError)
        Console.WriteLine(listenerParser.Errors);
    else {
        Console.WriteLine("parse completed.");
        Console.WriteLine(builder.Visit(tree));
    }
}

Try(""" 
    :root {
        head {        
           style: {"
                #myId {
                    font-color: red; 
                }
                
                h1 {
                    background-image: linear-gradient(to left, violet, indigo, blue, green, yellow, orange, red);   -webkit-background-clip: text; 
                    color: transparent; 
                }
           "}
       }
       body#myId.myClass1.my-class-2[class="test more classes"] {
            h1 {"Beautiful webpage"}
            p {"This is a paragraph"}
            ul {
                li {"This"}
                li {"is"}
                li {"an"}
                li {"unordered"}
                li {"list"}
            }
            a[href="https://www.google.com/"]{"google.com"}
            p {" 
            This paragraph
            
            Has some line 
            breaks
            "}
       }
    }
    """
);
