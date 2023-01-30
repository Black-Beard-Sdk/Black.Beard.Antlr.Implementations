using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.Asts
{


    public abstract class AstRule : AstRoot
    {

        public AstRule(ITerminalNode n)
            : base(n)
        {
        }

        public AstRule(ParserRuleContext ctx, List<AstRoot> list)
            : base(ctx)
        {
        }

        public AstRule(Position n)
            : base(n)
        {

        }

    }


}
