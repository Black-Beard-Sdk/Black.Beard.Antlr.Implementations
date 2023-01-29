using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using System.Diagnostics;

namespace Bb.Asts
{

    public abstract class AstTerminal : AstRoot
    {

        public AstTerminal(ITerminalNode n)
            : base(n)
        {
        }

        public AstTerminal(ParserRuleContext ctx)
            : base(ctx)
        {
        }

        public AstTerminal(Position n)
            : base(n)
        {

        }

    }


}
