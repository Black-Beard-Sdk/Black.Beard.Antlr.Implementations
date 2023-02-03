using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Child}")]

    public class AstElement : AstBase
    {

        public AstElement(ParserRuleContext ctx, AstBase child)
            : base(ctx)
        {
            this.Child = child;
        }

        public AstBase Child { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElement(this);
        }

        public override bool ContainsTerminals { get => this.Child?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.Child?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.Child?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.Child?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => Child?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.Child?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => Child?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.Child?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.Child?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.Child?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.Child?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives { get => this.Child?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return Child.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return Child.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return Child.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return Child.GetAlternatives();
        }


    }


}
