namespace Bb.ParsersConfiguration.Ast
{

    public interface IAstConfigBaseVisitor
    {
        void VisitAdditionalValue(AdditionalValue a);
        void VisitAdditionalValues(AdditionalValues a);
        void VisitCalculatedTemplateSetting(CalculatedTemplateSetting a);
        void VisitConstant(ConstantStringConfig a);
        void VisitDefaultTemplateSetting(DefaultTemplateSetting a);
        void VisitGamarDeclaration(GrammarConfigDeclaration a);
        void VisitGammarTermDeclaration(GrammarConfigTermDeclaration a);
        void VisitGrammarSpec(GrammarSpec a);
        void VisitCalculatedTuneInherit(CalculatedRuleTuneInherit a);
        void VisitTuneInherit(RuleTuneInherit a);
        void VisitListDeclaration(ListDeclaration a);
        void VisitRule(GrammarRuleConfig a);
        void VisitRuleTerm(GrammarRuleTermConfig a);
        void VisitSelectorExpression(SelectorExpressionItem a);
        void VisitSelectorExpressionItemBinary(SelectorExpressionItemBinary a);
        void VisitSelectorExpressionItemHas(SelectorExpressionItemHas a);
        void VisitSelectorExpressionItemIs(SelectorExpressionItemIs a);
        void VisitSelectorExpressionItemIsIn(SelectorExpressionItemIsIn a);
        void VisitTemplateSelector(TemplateSelector a);
        void VisitTemplateSelectorExpression(TemplateSelectorExpression a);
        void VisitTemplateSetting(TemplateSetting a);
    }

}
