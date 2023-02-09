using Antlr4.Runtime;

namespace Bb.Asts
{

    public class AstIdentifierList : AstListBase<AstIdentifier>
    {

        public AstIdentifierList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstIdentifierList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitIdentifierList(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitIdentifierList(this);
        }

    }


}
