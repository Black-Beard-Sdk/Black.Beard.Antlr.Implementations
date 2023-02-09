using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Key} = {Value}")]
    public class AstOption : AstBase
    {

        public AstOption(ParserRuleContext ctx, AstIdentifier key, AstBase value)
            : base(ctx)
        {
            this.Key = key;
            this.Value = value;
        }

        public AstIdentifier Key { get; private set; }

        public AstBase Value { get; private set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitOption(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitOption(this);
        }

        public override bool ContainsTerminals { get => false; }
        public override bool ContainsRules { get => false; }
        public override bool ContainsBlocks { get => false; }
        public override bool ContainsAlternatives { get => false; }



        public override bool ContainsOneTerminal { get => false; }
        public override bool ContainsOneRule { get => false; }
        public override bool ContainsOneBlock { get => false; }
        public override bool ContainsOneAlternative { get => false; }



        public override bool ContainsOnlyTerminals { get => false; }
        public override bool ContainsOnlyRules { get => false; }
        public override bool ContainsOnlyBlocks { get => false; }
        public override bool ContainsOnlyAlternatives { get => false; }





        public override IEnumerable<AstRuleRef> GetRules()
        {
            yield break;
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            yield break;
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            yield break;
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            yield break;
        }

    }


}
