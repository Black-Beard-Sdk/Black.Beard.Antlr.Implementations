using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers.Tsql;
using Bb.Parsers.TSql;

namespace Bb.Parsers
{

    public partial class ScriptTSqlVisitor
    {


        public override AstRoot VisitTerminal(ITerminalNode node)
        {

            var symbolicName = TSqlLexer.DefaultVocabulary.GetSymbolicName(node.Symbol.Type);
            var txt = node.GetText();

            if (txt == symbolicName)
                return new AstTerminalKeyword(node, txt) { Type = symbolicName } ;

            var literalName = TSqlLexer.DefaultVocabulary.GetLiteralName(node.Symbol.Type);
            if (IsSymbol(txt, literalName))
            {
                //return new AstTerminalSymbol(node, txt, symbolicName);
            }

            return new AstTerminalIdentifier(node, node.GetText(), symbolicName);
        }

        private static bool IsSymbol(string txt, string r)
        {
            bool test = false;

            for (int i = 0; i < txt.Length; i++)
            {
                char c = txt[i];
                if (c != ' ' && !char.IsLetter(c))
                {
                    test = true;
                    continue;
                }
            }

            if (test)
                test = (txt == r.Trim('\''));

            return test;

        }


    }

}


