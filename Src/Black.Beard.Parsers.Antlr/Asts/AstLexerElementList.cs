using Antlr4.Runtime;
using Newtonsoft.Json.Linq;

namespace Bb.Asts
{

    public class AstLexerElementList : AstListBase<AstBase>
    {

        public AstLexerElementList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {
            _charSplit = "  ";
        }

        public AstLexerElementList(ParserRuleContext ctx)
            : base(ctx)
        {
        }


        public bool IsConstant
        {
             get
            {
                if (this.Count == 1)
                    if (this[0] != null && this[0] is AstAtom a)
                        return a.IsConstant;
                return false;
            }
        }

        public bool IsKeyword
        {
            get
            {
                if (this.Count == 1)
                    if (this[0] != null && this[0] is AstAtom a)
                        return a.IsKeyword;
                return false;
            }
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
            visitor.VisitLexerElementList(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerElementList(this);
        }


    }

}
