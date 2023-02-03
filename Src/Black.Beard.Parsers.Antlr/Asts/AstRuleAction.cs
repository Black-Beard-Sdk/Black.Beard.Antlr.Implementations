using Antlr4.Runtime;
using System.Data;

namespace Bb.Asts
{
    public class AstRuleAction : AstBase
    {

        public AstRuleAction(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstRuleAction(ParserRuleContext ctx, AstIdentifier name, AstActionBlock action) : this(ctx)
        {
            this.Name = name;
            this.Action = action;
        }

        public AstIdentifier Name { get; }
        public AstActionBlock Action { get; }


        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.Action?.GetTerminals();
        }
        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.Action?.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return this.Action?.GetBlocks();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return this.Action?.GetAlternatives();
        }




        public override bool ContainsRules => this.Action?.ContainsRules ?? false;
        public override bool ContainsTerminals => this.Action?.ContainsTerminals ?? false;
        public override bool ContainsBlocks => this.Action?.ContainsBlocks ?? false;
        public override bool ContainsAlternatives => this.Action?.ContainsAlternatives ?? false;



        public override bool ContainsOneRule
        {
            get
            {
                return this.Action?.ContainsOneRule ?? false;
            }
        }
        public override bool ContainsOneTerminal
        {
            get
            {
                return this.Action?.ContainsOneTerminal ?? false;
            }
        }
        public override bool ContainsOneAlternative
        {
            get
            {
                return this.Action?.ContainsOneAlternative ?? false;
            }
        }
        public override bool ContainsOneBlock
        {
            get
            {
                return this.Action?.ContainsOneBlock ?? false;
            }
        }



        public override bool ContainsOnlyRules => this.Action?.ContainsOnlyRules ?? false;
        public override bool ContainsOnlyBlocks => this.Action?.ContainsOnlyBlocks ?? false;
        public override bool ContainsOnlyAlternatives => this.Action?.ContainsOnlyAlternatives ?? false;
        public override bool ContainsOnlyTerminals => this.Action?.ContainsOnlyTerminals ?? false;




        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleAction(this);
        }

    }


}
