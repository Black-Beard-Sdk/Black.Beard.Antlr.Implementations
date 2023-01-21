using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstParserRuleSpec : AstBase
    {

        public AstParserRuleSpec(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitParserRuleSpec(this);
        }

    }


}
