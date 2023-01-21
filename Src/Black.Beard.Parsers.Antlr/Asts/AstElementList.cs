using Antlr4.Runtime;

namespace Bb.Asts
{

    public class AstElementList : AstListBase<AstElement>
    {

        public AstElementList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        public AstElementList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElementList(this);
        }

    }


}
