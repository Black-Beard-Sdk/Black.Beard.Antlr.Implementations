using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstRules : AstListBase<AstLexerRule>
    {

        public AstRules(ParserRuleContext ctx)
            : base(ctx)
        {
            this.Rules = new AstRulesList(ctx);
            this.Terminals = new AstLexerRulesList(ctx);
        }

        public AstLexerRulesList Terminals { get; }

        public AstRulesList Rules { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRules(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitRules(this);
        }


    }
    

}
