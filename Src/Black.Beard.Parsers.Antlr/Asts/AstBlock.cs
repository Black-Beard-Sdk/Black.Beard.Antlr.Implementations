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
        public override IEnumerable<AstBlock> GetBlocks()
        {
            yield return this;
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return this.AlternativeList?.GetAlternatives();
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
        public override bool ContainsOnlyRules
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyRules)
                        return false;

                return true;

            }
        }
        public override bool ContainsOnlyBlocks
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyBlocks)
                        return false;

                return true;

            }
        }
        public override bool ContainsOnlyAlternatives
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyAlternatives)
                        return false;

                return true;

            }
        }




        public override bool ContainsOneTerminal
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                {
                    if (!item.ContainsOneTerminal)
                        return false;
                }

                return true;

            }
        }
        public override bool ContainsOneRule
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyRules)
                        return false;

                return true;

            }
        }
        public override bool ContainsOneBlock
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOneBlock)
                        return false;

                return true;

            }
        }
        public override bool ContainsOneAlternative
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOneAlternative)
                        return false;

                return true;

            }
        }




        public override bool ContainsTerminals
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                {
                    if (!item.ContainsTerminals)
                        return false;
                }

                return true;

            }
        }
        public override bool ContainsRules
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsRules)
                        return false;

                return true;

            }
        }
        public override bool ContainsBlocks
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyBlocks)
                        return false;

                return true;

            }
        }
        public override bool ContainsAlternatives
        {
            get
            {
                if (this.AlternativeList.Count == 0)
                    return false;

                foreach (var item in this.AlternativeList)
                    if (!item.ContainsOnlyAlternatives)
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



        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitBlock(this);
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
