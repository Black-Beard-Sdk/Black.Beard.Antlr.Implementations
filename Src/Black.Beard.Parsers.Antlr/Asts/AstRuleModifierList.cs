using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstRuleModifierList : AstListBase<AstRuleModifier>
    {

        public AstRuleModifierList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstRuleModifierList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleModifierList(this);
        }

    }


}
