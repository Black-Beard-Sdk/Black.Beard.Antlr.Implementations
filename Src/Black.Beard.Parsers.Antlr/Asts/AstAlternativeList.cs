using Antlr4.Runtime;

namespace Bb.Asts
{


    public class AstAlternativeList : AstListBase<AstAlternative>
    {

        public AstAlternativeList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstAlternativeList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

        }

        public bool OutputContainsAlwayOneTerminal
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (AstAlternative item in this)
                {

                    if (!item.Rule.OutputContainsAlwayOneTerminal)
                        return false;
                   
                }

                return true;

            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAstAlternativeList(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitAlternativeList(this);
        }

    }


}
