using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstFinallyClause : AstBase
    {

        public AstFinallyClause(ParserRuleContext ctx, AstActionBlock block)
            : base(ctx)
        {
            this.Block = block;
        }

        public AstActionBlock Block { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitFinallyClause(this);
        }

    }


}
