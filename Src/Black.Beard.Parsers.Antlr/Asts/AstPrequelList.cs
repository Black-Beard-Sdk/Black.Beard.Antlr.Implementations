using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstPrequelList : AstListBase<AstPrequel>
    {

        public AstPrequelList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstPrequelList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitPrequelList(this);
        }

    }


}
