namespace Bb.ParsersConfiguration.Ast
{
    public interface IAstConfigBaseWithResultVisitor<T>
    {
        T VisitAdditionalValue(AdditionalValue a);
        T VisitAdditionalValues(AdditionalValues a);
        T VisitCalculatedTemplateSetting(CalculatedTemplateSetting a);
        T VisitConstant(ConstantStringConfig a);
        T VisitDefaultTemplateSetting(DefaultTemplateSetting a);
        T VisitGammarDeclaration(GrammarConfigDeclaration a);
        T VisitGammarDeclaration(GrammarConfigTermDeclaration a);
        T VisitGrammarSpec(GrammarSpec a);
        T VisitTuneInherit(RuleTuneInherit a);
        T VisitCalculatedTuneInherit(CalculatedRuleTuneInherit a);
        T VisitListDeclaration(ListDeclaration a);
        T VisitRule(GrammarRuleConfig a);
        T VisitRuleTerm(GrammarRuleTermConfig a);
        T VisitSelectorExpression(SelectorExpressionItem a);
        T VisitSelectorExpressionItemBinary(SelectorExpressionItemBinary a);
        T VisitSelectorExpressionItemHas(SelectorExpressionItemHas a);
        T VisitSelectorExpressionItemIs(SelectorExpressionItemIs a);
        T VisitSelectorExpressionItemIsIn(SelectorExpressionItemIsIn a);
        T VisitTemplateSelector(TemplateSelector a);
        T VisitTemplateSelectorExpression(TemplateSelectorExpression a);
        T VisitTemplateSetting(TemplateSetting a);
    }

}
