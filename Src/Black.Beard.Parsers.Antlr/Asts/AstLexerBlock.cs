using Antlr4.Runtime;

namespace Bb.Asts
{


    public class AstLexerBlock : AstBase
    {

        public AstLexerBlock(ParserRuleContext ctx, AstLexerAlternativeList? alternativeList)
            : base(ctx)
        {
            AlternativeList = alternativeList;
        }


        public AstLexerAlternativeList? AlternativeList { get; }


        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.AlternativeList?.GetTerminals();
        }
        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.AlternativeList?.GetRules();
        }

        //public override IEnumerable<AstBlock> GetBlocks()
        //{
        //    yield return this;
        //}

        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return this.AlternativeList?.GetAlternatives();
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerBlock(this);
        }



        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerBlock(this);
        }


        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            writer.Append("(");
            writer.ToString(AlternativeList);
            writer.Append(")");
            return true;
        }

    }


}
