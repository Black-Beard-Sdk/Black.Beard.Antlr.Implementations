using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstFinallyClause : AstBase
    {

        public AstFinallyClause(ParserRuleContext ctx, AstActionBlock block)
            : base(ctx)
        {
            this.Block = block;
        }

        public AstActionBlock Block { get; }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitFinallyClause(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitFinallyClause(this);
        }


        public override bool ContainsTerminals { get => this.Block?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.Block?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.Block?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.Block?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => Block?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.Block?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => Block?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.Block?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.Block?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.Block?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.Block?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives { get => this.Block?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return Block?.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return Block?.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return Block?.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return Block?.GetAlternatives();
        }

    }


}
