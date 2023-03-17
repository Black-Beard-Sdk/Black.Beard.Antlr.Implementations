using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{
    public class EvaluateExpression : IAstConfigBaseWithResultVisitor<bool>
    {

        public EvaluateExpression(AstRule rule, GrammarSpec grammarSpec)
        {
            this._rule = rule;
            this._grammarSpec = grammarSpec;
        }

        public bool VisitAdditionalValue(AdditionalValue a)
        {
            throw new NotImplementedException();
        }

        public bool VisitAdditionalValues(AdditionalValues a)
        {
            throw new NotImplementedException();
        }

        public bool VisitCalculatedTemplateSetting(CalculatedTemplateSetting a)
        {
            throw new NotImplementedException();
        }

        public bool VisitDefaultTemplateSetting(DefaultTemplateSetting a)
        {
            throw new NotImplementedException();
        }

        public bool VisitGammarDeclaration(GrammarConfigDeclaration a)
        {
            throw new NotImplementedException();
        }

        public bool VisitGrammarSpec(GrammarSpec a)
        {
            throw new NotImplementedException();
        }

        public bool VisitIdentifier(IdentifierConfig a)
        {
            throw new NotImplementedException();
        }

        public bool VisitListDeclaration(ListDeclaration a)
        {
            throw new NotImplementedException();
        }

        public bool VisitRule(GrammarRuleConfig a)
        {
            throw new NotImplementedException();
        }

        public bool VisitSelectorExpression(SelectorExpressionItem a)
        {
            throw new NotImplementedException();
        }

        public bool VisitSelectorExpressionItemBinary(SelectorExpressionItemBinary a)
        {

            if (a.Operator == SelectorExpressionOperationEnum.Or)
                return a.Left.Accept(this) || a.Right.Accept(this);

            return a.Left.Accept(this) && a.Right.Accept(this);

        }


        public bool VisitSelectorExpressionItemHas(SelectorExpressionItemHas a)
        {

            if (a.IsOutput)
            {

                switch (a.Target)
                {

                    case FilterExpressionTargetEnum.Rule:
                        return this._rule.OutputContainsAlwayOneRule;

                    case FilterExpressionTargetEnum.Term:
                        return this._rule.OutputContainsAlwayOneTerminal;

                    case FilterExpressionTargetEnum.Block:
                        break;

                    case FilterExpressionTargetEnum.Alternative:
                        break;

                    case FilterExpressionTargetEnum._Undefined:
                    default:
                        break;
                }

            }
            else
            {

                switch (a.Target)
                {
                    case FilterExpressionTargetEnum._Undefined:
                        break;
                    case FilterExpressionTargetEnum.Block:
                        switch (a.FilterCount)
                        {
                            case FilterExpressionEnum.One:
                                return _rule.ContainsOneBlock;
                               
                            case FilterExpressionEnum.Only:
                                return _rule.ContainsOnlyBlocks;

                            case FilterExpressionEnum.Any:
                                return _rule.ContainsBlocks;

                            case FilterExpressionEnum.Many:
                                return _rule.GetBlocks().Count() > 1;

                            case FilterExpressionEnum.No:
                                return !_rule.ContainsBlocks;

                            case FilterExpressionEnum._Undefined:
                            default:
                                break;
                        }
                        break;

                    case FilterExpressionTargetEnum.Rule:
                        switch (a.FilterCount)
                        {
                            case FilterExpressionEnum.One:
                                return _rule.ContainsOneRule;
                                
                            case FilterExpressionEnum.Only:
                                return _rule.ContainsOnlyRules;

                            case FilterExpressionEnum.Any:
                                return _rule.ContainsRules;

                            case FilterExpressionEnum.Many:
                                return _rule.GetRules().Count() > 1;

                            case FilterExpressionEnum.No:
                                return !_rule.ContainsRules;

                            case FilterExpressionEnum._Undefined:
                            default:
                                break;
                        }
                        break;

                    case FilterExpressionTargetEnum.Term:
                        switch (a.FilterCount)
                        {
                            case FilterExpressionEnum.One:
                                return _rule.ContainsOneTerminal;
                                
                            case FilterExpressionEnum.Only:
                                return _rule.ContainsOnlyTerminals;

                            case FilterExpressionEnum.Any:
                                return _rule.ContainsTerminals;

                            case FilterExpressionEnum.Many:
                                return _rule.GetTerminals().Count() > 1;

                            case FilterExpressionEnum.No:
                                return !_rule.ContainsTerminals;
                            
                            case FilterExpressionEnum._Undefined:
                            default:
                                break;
                        }
                        break;

                    case FilterExpressionTargetEnum.Alternative:
                        switch (a.FilterCount)
                        {
                            case FilterExpressionEnum.One:
                                return _rule.ContainsOneAlternative;
                                
                            case FilterExpressionEnum.Only:
                                return _rule.ContainsOnlyAlternatives;

                            case FilterExpressionEnum.Any:
                                return _rule.ContainsAlternatives;

                            case FilterExpressionEnum.Many:
                                return _rule.GetAlternatives().Count() > 1;

                            case FilterExpressionEnum.No:
                                return !_rule.ContainsAlternatives;

                            case FilterExpressionEnum._Undefined:
                            default:
                                break;
                        }
                        break;
                }

            }

            return false;

        }

        public bool VisitSelectorExpressionItemIs(SelectorExpressionItemIs a)
        {

            foreach (var item in a.Targets)
                switch (item)
                {

                    case FilterExpressionTargetEnum.Block:
                        break;

                    case FilterExpressionTargetEnum.Rule:
                        break;

                    case FilterExpressionTargetEnum.Term:
                        break;

                    case FilterExpressionTargetEnum.Alternative:
                        break;

                    case FilterExpressionTargetEnum._Undefined:
                    default:
                        break;
                }

            return false;
        }

        public bool VisitSelectorExpressionItemIsIn(SelectorExpressionItemIsIn a)
        {


            foreach (var item in _rule.GetRules().ToList())
            {

                var result = a.List.Find(item.Identifier.Text);

                if (result && a.IsNot)
                    return false;

                if (!result && !a.IsNot)
                    return false;

            }

            foreach (var item in _rule.GetTerminals().ToList())
            {

                var result = a.List.Find(item.ResolveName());

                if (result && a.IsNot)
                    return false;

                if (!result && !a.IsNot)
                    return false;

            }

            return true;

        }

        public bool VisitTemplateSelector(TemplateSelector a)
        {
            throw new NotImplementedException();
        }

        public bool VisitTemplateSelectorExpression(TemplateSelectorExpression a)
        {

            return a.Filter.Accept(this);

        }

        public bool VisitTemplateSetting(TemplateSetting a)
        {
            throw new NotImplementedException();
        }

        public bool VisitGammarDeclaration(GrammarConfigTermDeclaration a)
        {
            return a.Config.Accept(this);
        }

        public bool VisitRuleTerm(GrammarRuleTermConfig a)
        {
            throw new NotImplementedException();
        }

        public bool VisitConstant(ConstantStringConfig a)
        {
            throw new NotImplementedException();
        }

        private readonly AstRule _rule;
        private readonly GrammarSpec _grammarSpec;

    }

}
