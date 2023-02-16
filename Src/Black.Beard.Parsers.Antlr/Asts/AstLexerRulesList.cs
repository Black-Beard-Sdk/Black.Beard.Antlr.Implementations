using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstLexerRulesList : AstListBase<AstLexerRule>
    {

        public AstLexerRulesList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerRulesList(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerRulesList(this);
        }


    }
    

}
