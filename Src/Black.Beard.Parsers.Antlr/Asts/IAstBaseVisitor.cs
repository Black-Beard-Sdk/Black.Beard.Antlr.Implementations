namespace Bb.Asts
{

    public interface IAstBaseVisitor
    {
    

        void VisitElementList(AstElementList a);
        void VisitIdentifierList(AstIdentifierList a);
        void VisitElementOptionList(AstElementOptionList a);
        void VisitModeSpecList(AstModeSpecList a);
        void VisitOptionList(AstOptionList a);
        void VisitRuleModifierList(AstRuleModifierList a);
        void VisitRuleAltList(AstRuleAltList a);
        void VisitPrequelConstructList(AstPrequelConstructList a);
        void VisitRulesList(AstRulesList a);



        void VisitActionBlock(AstActionBlock a);
        void VisitAlternative(AstAlternative a);
        void VisitArgActionBlock(AstArgActionBlock a);
        void VisitAtom(AstAtom a);
        void VisitBlock(AstBlock a);
        void VisitElement(AstElement a);
        void VisitElementOption(AstElementOption a);
        void VisitExceptionGroup(AstExceptionGroup a);
        void VisitExceptionHandler(AstExceptionHandler a);
        void VisitFinallyClause(AstFinallyClause a);
        void VisitGrammarSpec(AstGrammarSpec a);
        void VisitGrammerDecl(AstGrammarDecl a);
        void VisitLabeledAlt(AstLabeledAlt a);
        void VisitLabeledElement(AstLabeledElement a);
        void VisitModeSpec(AstModeSpec a);
        void VisitOption(AstOption a);
        void VisitParserRuleSpec(AstParserRuleSpec a);
        void VisitPrequel(AstPrequel a);
        void VisitPrequelConstruct(AstPrequelConstruct a);
        void VisitPrequelList(AstPrequelList a);
        void VisitRule(AstRule a);
        void VisitRuleAction(AstRuleAction a);
        void VisitRuleModifier(AstRuleModifier a);
        void VisitRuleRef(AstRuleRef a);
        void VisitTerminal(AstTerminal a);
        void VisitTerminalText(AstTerminalText a);
        void VisitAstAlternativeList(AstAlternativeList a);
    }


}
