using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstRuleAction : AstBase
    {

        public AstRuleAction(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstRuleAction(ParserRuleContext ctx, AstIdentifier name, AstActionBlock action) : this(ctx)
        {
            this.Name = name;
            this.Action = action;
        }

        public AstIdentifier Name { get; }
        public AstActionBlock Action { get; }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.Action?.GetTerminals();
        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.Action?.GetRules();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleAction(this);
        }

    }


}
