using Antlr4.Runtime;

namespace Bb.Asts
{


    public class AstRulesList : AstListBase<AstRule>
    {

        public AstRulesList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRulesList(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitRulesList(this);
        }


    }


}
