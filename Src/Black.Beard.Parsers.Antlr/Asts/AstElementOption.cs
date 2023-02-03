using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Key} {Value}")]
    public class AstElementOption : AstBase
    {

        public AstElementOption(ParserRuleContext ctx, AstIdentifier key)
            : base(ctx)
        {

            this.Key = key;

        }

        public AstIdentifier Key { get; }

        public AstBase Value { get; set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElementOption(this);
        }



        public override bool ContainsTerminals { get => this.Value?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.Value?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.Value?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.Value?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => Value?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.Value?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => Value?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.Value?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.Value?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.Value?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.Value?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives { get => this.Value?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return Value?.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return Value?.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return Value?.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return Value?.GetAlternatives();
        }


    }


}
