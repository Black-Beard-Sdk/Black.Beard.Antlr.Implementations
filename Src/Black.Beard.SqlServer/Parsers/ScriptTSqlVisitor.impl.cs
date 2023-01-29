using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers.Tsql;

namespace Bb.Parsers
{

    public partial class ScriptTSqlVisitor
    {


        public override AstRoot VisitTerminal(ITerminalNode node)
        {
            return new AstTerminalIdentifier(node,node.Symbol.Type.ToString(), node.GetText());
        }


     
    }

}


