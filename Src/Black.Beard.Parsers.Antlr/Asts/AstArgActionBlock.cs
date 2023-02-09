using Antlr4.Runtime;

namespace Bb.Asts
{

    public class AstArgActionBlock : AstListBase<AstIdentifier>
    {

        public AstArgActionBlock(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstArgActionBlock(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitArgActionBlock(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitArgActionBlock(this);
        }


    }

}
