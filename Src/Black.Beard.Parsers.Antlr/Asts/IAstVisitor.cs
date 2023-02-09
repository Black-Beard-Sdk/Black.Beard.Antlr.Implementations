namespace Bb.Asts
{
    public interface IAstVisitor<T>
    {
        T VisitElementList(AstElementList a);
        T VisitIdentifierList(AstIdentifierList a);
        T VisitElementOptionList(AstElementOptionList a);
        T VisitModeSpecList(AstModeSpecList a);
        T VisitOptionList(AstOptionList a);
        T VisitRuleModifierList(AstRuleModifierList a);
        T VisitRuleAltList(AstRuleAltList a);
        T VisitPrequelConstructList(AstPrequelConstructList a);
        T VisitRulesList(AstRulesList a);



        T VisitActionBlock(AstActionBlock a);
        T VisitAlternative(AstAlternative a);
        T VisitArgActionBlock(AstArgActionBlock a);
        T VisitAtom(AstAtom a);
        T VisitBlock(AstBlock a);
        T VisitElement(AstElement a);
        T VisitElementOption(AstElementOption a);
        T VisitExceptionGroup(AstExceptionGroup a);
        T VisitExceptionHandler(AstExceptionHandler a);
        T VisitFinallyClause(AstFinallyClause a);
        T VisitGrammarSpec(AstGrammarSpec a);
        T VisitGrammerDecl(AstGrammarDecl a);
        T VisitLabeledAlt(AstLabeledAlt a);
        T VisitLabeledElement(AstLabeledElement a);
        T VisitModeSpec(AstModeSpec a);
        T VisitOption(AstOption a);
        T VisitParserRuleSpec(AstParserRuleSpec a);
        T VisitPrequel(AstPrequel a);
        T VisitPrequelConstruct(AstPrequelConstruct a);
        T VisitPrequelList(AstPrequelList a);
        T VisitRule(AstRule a);
        T VisitRuleAction(AstRuleAction a);
        T VisitRuleModifier(AstRuleModifier a);
        T VisitRuleRef(AstRuleRef a);
        T VisitTerminal(AstTerminal a);
        T VisitTerminalText(AstTerminalText a);
        T VisitAstAlternativeList(AstAlternativeList a);
        T VisitAstEbnfSuffix(AstEbnfSuffix a);

    }

}
