using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Child}")]
    public class AstPrequel : AstBase
    {

        public AstPrequel(ParserRuleContext ctx, AstBase item)
            : base(ctx)
        {
            this.Rule = item;
        }

        public AstBase Rule { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitPrequel(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitPrequel(this);
        }



        public override bool ContainsTerminals { get => this.Rule?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.Rule?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.Rule?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.Rule?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => Rule?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.Rule?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => Rule?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.Rule?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.Rule?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.Rule?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.Rule?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives { get => this.Rule?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return Rule.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return Rule.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return Rule.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return Rule.GetAlternatives();
        }



    }


}
