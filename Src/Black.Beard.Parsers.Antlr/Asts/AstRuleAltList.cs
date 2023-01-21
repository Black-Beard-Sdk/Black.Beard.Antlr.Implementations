using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstRuleAltList : AstListBase<AstLabeledAlt>
    {

        public AstRuleAltList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleAltList(this);
        }

    }


}
