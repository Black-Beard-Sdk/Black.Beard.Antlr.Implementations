using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstLexerCommandList : AstListBase<AstLexerCommand>
    {

        public AstLexerCommandList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {
            _charSplit = "  ";
        }

        public AstLexerCommandList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public bool OutputContainsAlwayOneTerminal
        {
            get
            {

                if (this.Count != 1)
                    return false;

                foreach (AstBase item in this)
                {

                    if (item is AstAtom a)
                    {

                        if (!a.Value.IsTerminal)
                            return false;

                    }
                    else
                    {

                    }

                }

                return true;

            }
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerCommandList(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerElementList(this);
        }


    }

}
