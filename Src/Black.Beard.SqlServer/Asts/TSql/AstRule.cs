using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.Asts.TSql
{


    public abstract class AstRule : AstRoot
    {

        public AstRule(ITerminalNode t, List<AstRoot> list)
            : base(t)
        {
            List = list;
        }

        public AstRule(ParserRuleContext ctx, List<AstRoot> list)
            : base(ctx)
        {
            List = list;
        }

        public AstRule(Position p, List<AstRoot> list)
            : base(p)
        {
            List = list;
        }

        public List<AstRoot> List { get; }

    }


}
