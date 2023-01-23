using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstAlternativeList : AstListBase<AstAlternative>
    {

        public AstAlternativeList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstAlternativeList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAstAlternativeList(this);
        }

    }


}
