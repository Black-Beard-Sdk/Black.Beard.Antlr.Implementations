using Antlr4.Runtime;

namespace Bb.Asts
{

    public class AstBlock : AstBase
    {

        public AstBlock(ParserRuleContext ctx, AstOptionList? optionList, AstRuleAction? ruleAction, AstAlternativeList? alternativeList)
            : base(ctx)
        {
            Options = optionList;
            RuleAction = ruleAction;
            AlternativeList = alternativeList;
        }

        public AstOptionList? Options { get; }

        public AstRuleAction? RuleAction { get; }

        public AstAlternativeList? AlternativeList { get; }
        public OccurenceEnum Occurence { get; internal set; }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.AlternativeList?.GetTerminals();
        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.AlternativeList?.GetRules();
        }

        public override bool ContainsOnlyTerminals
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                {
                    if (!item.ContainsOnlyTerminals)
                        return false;
                }

                return true;

            }
        }

        public override bool ContainsOnlyRuleReferences
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyRuleReferences)
                        return false;

                return true;

            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitBlock(this);
        }

        public override void ToString(Writer writer)
        {
            if (Options != null) Options.ToString(writer);

            if (RuleAction != null) RuleAction.ToString(writer);

            if (AlternativeList != null)
            {
                AlternativeList.ToString(writer);
                WriteOccurence(writer, Occurence);
            }

        }

    }


}
