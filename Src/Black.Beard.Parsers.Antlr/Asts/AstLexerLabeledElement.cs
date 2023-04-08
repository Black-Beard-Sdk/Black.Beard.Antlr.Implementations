using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("{Left} = {Right}")]
    public class AstLexerLabeledElement : AstBase
    {

        public AstLexerLabeledElement(ParserRuleContext ctx, AstIdentifier identifier, LabeledElementAssignEnum mode, AstBase value)
            : base(ctx)
        {
            this.Name = identifier;
            this.Assign = mode;
            this.Rule = value;
        }

        public AstIdentifier Name { get; }

        public LabeledElementAssignEnum Assign { get; }

        public AstBase Rule { get; }

        public Occurence Occurence { get; internal set; }


        public override bool ContainsTerminals { get => this.Rule?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.Rule?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.Rule?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.Rule?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => this.Rule?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.Rule?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => this.Rule?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.Rule?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.Rule?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.Rule?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.Rule?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives { get => this.Rule?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.Rule?.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return this.Rule?.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.Rule?.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return this.Rule?.GetAlternatives();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerLabeledElement(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerLabeledElement(this);
        }

    }


}
