using Antlr4.Runtime;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace Bb.Asts
{

    [DebuggerDisplay("{Rule} = {Identifier}")]
    public class AstLabeledAlt : AstBase
    {

        public AstLabeledAlt(ParserRuleContext ctx, AstAlternative rule, AstIdentifier? identifier = null)
            : base(ctx)
        {
            this.Rule = rule;
            this.Identifier = identifier;
        }


        public AstIdentifier Identifier { get; }

        public AstAlternative Rule { get; }


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
            foreach (AstBase item in this.Rule.Rule)
                if (item != null)
                    foreach (var t in item.GetRules())
                        yield return t;
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            foreach (AstBase item in this.Rule.Rule)
                if (item != null)
                    foreach (var t in item.GetBlocks())
                        yield return t;
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            foreach (AstBase item in this.Rule.Rule)
                if (item != null)
                    foreach (var t in item.GetTerminals())
                        yield return t;
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            foreach (AstBase item in this.Rule.Rule)
                if (item != null)
                    foreach (var t in item.GetAlternatives())
                        yield return t;
        }




        public IEnumerable<AstBase> GetItems()
        {
            foreach (AstBase item in this.Rule.Rule)
                yield return item;
        }

        public override bool IsTerminal
        {
            get
            {
                if (Rule.Count != 1)
                    return false;

                return Rule.Rule[0].IsTerminal;

            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLabeledAlt(this);
        }

        public override void ToString(Writer wrt)
        {

            if (Identifier != null)
                Identifier.ToString(wrt);

            Rule.ToString(wrt);

        }
    }


}
