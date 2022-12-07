using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;

namespace JsonHtml; 

public class HtmlBuilder : JsonHtmlBaseVisitor<string> {
    public override string VisitRoot(JsonHtmlParser.RootContext context) => "<html>" + string.Join("", context.element().Select(Visit)) + "</html>";

    public override string VisitElementContent(JsonHtmlParser.ElementContentContext context) {
        var str = "<";
        str += Visit(context.declaration());
        str += ">";
        str += Visit(context.content()).Replace("\r\n", "<br/>").Replace("\n", "<br/>").Replace("\t", "").Replace("  ", "");

        str += $"</{context.declaration().IDENTIFIER().GetText()}>";
        return str;
    }

    public override string VisitElementContentNoEscape(JsonHtmlParser.ElementContentNoEscapeContext context) {
        var str = "<";
        str += Visit(context.declaration());
        str += ">";
        str += Visit(context.content()).Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace("  ", "");
        str += $"</{context.declaration().IDENTIFIER().GetText()}>";
        return str;
    }

    public override string VisitElementElements(JsonHtmlParser.ElementElementsContext context) {
        var str = "<";
        str += Visit(context.declaration());
        str += ">";
        str += string.Join("", context.element().Select(Visit));
        str += $"</{context.declaration().IDENTIFIER().GetText()}>";
        return str;
    }

    public override string VisitDeclaration(JsonHtmlParser.DeclarationContext context) {
        var properties = new List<string> { context.IDENTIFIER().GetText() };
        if (context.id() is { } id) {
            properties.Add($"id=\"{Visit(id)}\"");
        }

        var classes = GetClasses(context);
        if (classes.Length > 0) {
            properties.Add(classes);
        }

        if (context.options() is { } options) {
            properties.Add(Visit(options));
        }
        
        return string.Join(" ", properties);
    }

    public override string VisitId(JsonHtmlParser.IdContext context) => context.IDENTIFIER().GetText();

    public override string VisitClass(JsonHtmlParser.ClassContext context) => context.IDENTIFIER().GetText();

    public override string VisitContent(JsonHtmlParser.ContentContext context) => StringToString(context.STRING());

    public override string VisitOptions(JsonHtmlParser.OptionsContext context) => string.Join(' ', context.option().Select(Visit));

    public override string VisitOption(JsonHtmlParser.OptionContext context) => $"{context.IDENTIFIER()}=\"{StringToString(context.STRING())}\"";

    private string StringToString(ITerminalNode node) {
        var str = node.GetText();
        str = str.Substring(1, str.Length - 2);
        str = str.Replace("\"\"", "\"");
        return str;
    }

    private string GetClasses(JsonHtmlParser.DeclarationContext context) {
        var classes = "";
        if (context.@class().Length > 0) {
            classes += string.Join(" ", context.@class().Select(Visit));
        }

        if (context.options() is { } options) {
            foreach (var option in options.option()) {
                if (option.IDENTIFIER().GetText().Trim().ToLowerInvariant() == "class") {
                    classes += " " + StringToString(option.STRING());
                }
            }
        }

        if (classes.Length > 0) {
            classes = $"class=\"{classes}\"";
        }
        return classes ;
    }
}