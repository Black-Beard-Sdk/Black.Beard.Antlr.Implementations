using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstModeSpec : AstBase
    {

        public AstModeSpec(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitModeSpec(this);
        }

    }


}
