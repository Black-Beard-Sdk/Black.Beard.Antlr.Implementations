using Antlr4.Runtime;

namespace Bb.Asts
{

    
    public class AstPrequelConstructList : AstListBase<AstPrequelConstruct>
    {

        public AstPrequelConstructList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        public AstPrequelConstructList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitPrequelConstructList(this);
        }

    }


}
