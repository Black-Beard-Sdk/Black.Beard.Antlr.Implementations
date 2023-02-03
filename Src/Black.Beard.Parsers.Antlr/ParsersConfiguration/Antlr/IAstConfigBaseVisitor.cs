namespace Bb.ParsersConfiguration.Antlr
{
 
    public interface IAstConfigBaseVisitor
    {
        void VisitConstant(ConstantConfig a);
        void VisitDefaults(GrammarSpecDefaultValues a);
        void VisitDefaultValue(GrammarSpecDefaultValue a);
        void VisitGamarDeclaration(GrammarConfigDeclaration a);
        void VisitGrammarSpec(GrammarSpec a);
        void VisitIdentifier(IdentifierConfig a);

        void VisitTemplate(GrammarRuleConfig a);

    }

}
