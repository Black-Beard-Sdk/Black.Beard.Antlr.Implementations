using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    public enum LabeledElementAssignEnum
    {
        Assign,
        PlusAssign,

    }


    [DebuggerDisplay("{Left} = {Right}")]
    public class AstLabeledElement : AstBase
    {

        public AstLabeledElement(ParserRuleContext ctx, AstIdentifier identifier, LabeledElementAssignEnum mode, AstBase value)
            : base(ctx)
        {
            this.Left = identifier;
            this.Assign = mode;
            this.Right = value;
        }

        public AstIdentifier Left { get; }

        public LabeledElementAssignEnum Assign { get; }

        public AstBase Right { get; }

        public OccurenceEnum Occurence { get; internal set; }

        public override bool ContainsTerminals { get => this.Right?.ContainsTerminals ?? false; }
        public override bool ContainsRules { get => this.Right?.ContainsRules ?? false; }
        public override bool ContainsBlocks { get => this.Right?.ContainsBlocks ?? false; }
        public override bool ContainsAlternatives { get => this.Right?.ContainsAlternatives ?? false; }



        public override bool ContainsOneTerminal { get => this.Right?.ContainsOneTerminal ?? false; }
        public override bool ContainsOneRule { get => this.Right?.ContainsOneRule ?? false; }
        public override bool ContainsOneBlock { get => this.Right?.ContainsOneBlock ?? false; }
        public override bool ContainsOneAlternative { get => this.Right?.ContainsOneAlternative ?? false; }



        public override bool ContainsOnlyTerminals { get => this.Right?.ContainsOnlyTerminals ?? false; }
        public override bool ContainsOnlyRules { get => this.Right?.ContainsOnlyRules ?? false; }
        public override bool ContainsOnlyBlocks { get => this.Right?.ContainsOnlyBlocks ?? false; }
        public override bool ContainsOnlyAlternatives{ get => this.Right?.ContainsOnlyAlternatives ?? false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.Right?.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return this.Right?.GetBlocks();
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.Right?.GetTerminals();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return this.Right?.GetAlternatives();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLabeledElement(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLabeledElement(this);
        }

    }


}
