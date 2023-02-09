using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstElementOptionList : AstListBase<AstElementOption>
    {

        public AstElementOptionList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {
        }

        public AstElementOptionList(ParserRuleContext ctx)
            : base(ctx)
        {
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElementOptionList(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitElementOptionList(this);
        }


    }


}
