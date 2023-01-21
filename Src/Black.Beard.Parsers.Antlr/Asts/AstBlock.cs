using Antlr4.Runtime;

namespace Bb.Asts
{

    public class AstBlock : AstBase
    {

        public AstBlock(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitBlock(this);
        }

    }


}
