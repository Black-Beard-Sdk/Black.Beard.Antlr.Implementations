using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstParserRuleSpec : AstBase
    {

        public AstParserRuleSpec(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitParserRuleSpec(this);
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


    }


}
