using Antlr4.Runtime;
using System.Text;

namespace Bb.Asts
{


    public class AstAlternative : AstBase
    {

        public AstAlternative(ParserRuleContext ctx, AstElementList rule, AstElementOptionList options)
            : base(ctx)
        {
            this.Rule = rule;
            this.Options = options;
        }

        public int Count { get => Rule.Count; }

        public AstElementList Rule { get; }

        public AstElementOptionList Options { get; }


        public override bool ContainsRules => this.Rule?.ContainsRules ?? false;
        public override bool ContainsTerminals => this.Rule?.ContainsTerminals ?? false;
        public override bool ContainsBlocks => this.Rule?.ContainsBlocks ?? false;
        public override bool ContainsAlternatives => this.Rule?.ContainsAlternatives ?? false;



        public override bool ContainsOneRule
        {
            get
            {
                return this.Rule?.ContainsOneRule ?? false;
            }
        }
        public override bool ContainsOneTerminal
        {
            get
            {
                return Rule?.ContainsOneTerminal ?? false;
            }
        }
        public override bool ContainsOneAlternative
        {
            get
            {
                return Rule?.ContainsOneAlternative ?? false;
            }
        }
        public override bool ContainsOneBlock
        {
            get
            {
                return Rule?.ContainsOneBlock ?? false;
            }
        }



        public override bool ContainsOnlyRules => Rule.ContainsOnlyRules;
        public override bool ContainsOnlyBlocks => Rule.ContainsOnlyBlocks;
        public override bool ContainsOnlyAlternatives => Rule.ContainsOnlyAlternatives;
        public override bool ContainsOnlyTerminals => this.Rule?.ContainsOnlyTerminals ?? false;



        public override IEnumerable<AstRuleRef> GetRules()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetRules())
                        yield return t;
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetBlocks())
                        yield return t;
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetTerminals())
                        yield return t;
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetAlternatives())
                        yield return t;
        }






        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAlternative(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitAlternative(this);
        }

        public override bool ToString(Writer wrt, StrategySerializationItem strategy)
        {
            wrt.ToString(Options);
            wrt.ToString(Rule);
            return true;
        }
    }


}
