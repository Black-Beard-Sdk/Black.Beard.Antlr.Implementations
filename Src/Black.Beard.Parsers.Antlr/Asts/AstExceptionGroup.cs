using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstExceptionGroup : AstListBase<AstExceptionHandler>
    {

        public AstExceptionGroup(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstFinallyClause? FinallyClause { get; set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitExceptionGroup(this);
        }

    }


}
