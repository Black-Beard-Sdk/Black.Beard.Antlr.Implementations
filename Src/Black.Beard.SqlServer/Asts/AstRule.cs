using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.Asts
{


    public abstract class AstRule : AstRoot
    {

        public AstRule(ITerminalNode t, List<AstRoot> list)
            : base(t)
        {
            this.List = list;
        }

        public AstRule(ParserRuleContext ctx, List<AstRoot> list)
            : base(ctx)
        {
            this.List = list;
        }

        public AstRule(Position p, List<AstRoot> list)
            : base(p)
        {
            this.List = list;
        }

        public List<AstRoot> List { get; }

    }


}
