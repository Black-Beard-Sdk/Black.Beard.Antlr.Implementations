using Antlr4.Runtime;
using System.Text;

namespace Bb.Asts
{
    public class AstRuleAltList : AstListBase<AstLabeledAlt>
    {

        public AstRuleAltList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {

            this._charSplit = " | ";

        }

        public bool ContainsJustOneAlternative
        {
            get
            {
                return this.Count == 1;
            }
        }

        public bool OutputContainsAlwayOneItem
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (AstLabeledAlt item in this)
                    if (item.Rule.Count > 1)
                        return false;

                return true;

            }
        }

        public bool OutputContainsAlwayOneRule
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (AstLabeledAlt item in this)
                {

                    if (item.Rule.Count != 1)
                        return false;

                    if (!item.Rule.Rule[0].IsRuleReference)
                        return false;

                }

                return true;

            }
        }

        public bool OutputContainsAlwayOneTerminal
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (AstLabeledAlt item in this)
                {

                    if (item.Rule.Count != 1)
                        return false;

                    if (!item.Rule.Rule[0].IsTerminal)
                        return false;

                }

                return true;

            }
        }

        public override bool ContainsOnlyRuleReferences
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (AstLabeledAlt item in this)
                {
                    if (!item.ContainsOnlyRuleReferences)
                        return false;
                }

                return true;

            }
        }

        public override bool ContainsOnlyTerminals
        {
            get
            {
                if (this.Count == 0)
                    return false;

                foreach (AstLabeledAlt item in this)
                {
                    if (!item.ContainsOnlyTerminals)
                        return false;
                }

                return true;

            }
        }

        public override bool ContainsTerminals
        {
            get
            {
                if (this.Count == 0)
                    return false;

                foreach (AstLabeledAlt item in this)
                {
                    if (item.ContainsTerminals)
                        return true;
                }

                return false;

            }
        }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {

            foreach (AstLabeledAlt item in this)
                foreach (var t in item.GetTerminals())
                    yield return t;

        }

        public override IEnumerable<AstRuleRef> GetRules()
        {

            foreach (AstLabeledAlt item in this)
                foreach (var t in item.GetRules())
                    yield return t;

        }

        public IEnumerable<IEnumerable<AstBase>> GetAlternatives()
        {
            foreach (AstLabeledAlt item in this)
                yield return item.GetItems();
        }

        public bool CanBeRemoved { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleAltList(this);
        }

    }


}
