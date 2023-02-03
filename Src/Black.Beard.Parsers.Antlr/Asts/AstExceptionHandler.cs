using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstExceptionHandler : AstBase
    {

        public AstExceptionHandler(ParserRuleContext ctx, AstArgActionBlock argActionBlock, AstActionBlock actionBlock)
            : base(ctx)
        {
            this.ArgActionBlock = argActionBlock;
            this.ActionBlock = actionBlock;
        }

        public AstArgActionBlock ArgActionBlock { get; }
        public AstActionBlock ActionBlock { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitExceptionHandler(this);
        }


        public override bool ContainsTerminals { get => this.ActionBlock?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.ActionBlock?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.ActionBlock?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.ActionBlock?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => ActionBlock?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.ActionBlock?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => ActionBlock?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.ActionBlock?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.ActionBlock?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.ActionBlock?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.ActionBlock?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives { get => this.ActionBlock?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return ActionBlock?.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return ActionBlock?.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return ActionBlock?.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return ActionBlock?.GetAlternatives();
        }

    }


}
