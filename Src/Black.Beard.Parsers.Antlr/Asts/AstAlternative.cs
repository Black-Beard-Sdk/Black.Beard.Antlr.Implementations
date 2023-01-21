using Antlr4.Runtime;

namespace Bb.Asts
{


    public class AstAlternative : AstBase
    {

        public AstAlternative(ParserRuleContext ctx, AstElementList rule, AstElementOptionList options)
            : base(ctx)
        {
            this.Rule = rule;
            this.Options = options;
        }

        public AstElementList Rule { get; }

        public AstElementOptionList Options { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAlternative(this);
        }

    }


}
