using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstRuleModifier : AstBase
    {

        public AstRuleModifier(ParserRuleContext ctx, RuleModifierEnum modifier)
            : base(ctx)
        {
            this.Modifier = modifier;
        }

        public RuleModifierEnum Modifier { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleModifier(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitRuleModifier(this);
        }

    }

    public enum RuleModifierEnum
    {
        Public,
        Private,
        Protected,
        Fragment
    }


}
