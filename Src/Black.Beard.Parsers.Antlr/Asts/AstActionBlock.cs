using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstActionBlock : AstListBase<AstIdentifier>
    {

        public AstActionBlock(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstActionBlock(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitActionBlock(this);
        }

    }


}
