using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstModeSpecList : AstListBase<AstModeSpec>
    {

        public AstModeSpecList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        public AstModeSpecList(ParserRuleContext ctx)
            : base(ctx)
        {

        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitModeSpecList(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitModeSpecList(this);
        }

    }


}
