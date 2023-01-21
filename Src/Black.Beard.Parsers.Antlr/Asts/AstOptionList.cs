using Antlr4.Runtime;

namespace Bb.Asts
{


    public class AstOptionList : AstListBase<AstOption>
    {

        public AstOptionList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitOptionList(this);
        }

    }


}
