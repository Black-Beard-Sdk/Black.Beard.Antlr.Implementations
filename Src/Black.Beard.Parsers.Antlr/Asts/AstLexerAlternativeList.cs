using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstLexerAlternativeList : AstListBase<AstLexerAlternative>
    {

        public AstLexerAlternativeList(ParserRuleContext ctx)
            : base(ctx)
        {
            this._charSplit = " | ";
        }

        public AstLexerAlternativeList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {
            this._charSplit = " | ";
        }


        public bool IsConstant
        {
            get
            {
                if (this.Count == 1)
                    if (this[0] != null && this[0].IsConstant)
                        return true;
                return false;
            }
        }

        public bool IsKeyword
        {
            get
            {
                if (this.Count == 1)
                    if (this[0] != null && this[0].IsKeyword)
                        return true;
                return false;
            }
        }

        public bool OutputContainsAlwayOneTerminal
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (AstLexerAlternative item in this)
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
            visitor.VisitLexerAlternativeList(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerAlternativeList(this);
        }

    }


}
