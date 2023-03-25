using Bb.Asts;

namespace Bb.Parsers
{

    public static class AstBaseExtension
    {

        public static IEnumerable<AstBase> GetAllItems(this AstBase ast, Func<AstBase, bool> filter = null)
        {

            var result = ItemsVisitor.Get(ast);

            if (filter != null)
                return result.Where(filter).ToList();

            return result;

        }

        public static IEnumerable<AstBase> KeepRuleRef(this IEnumerable<AstBase> astList, Func<AstRuleRef, bool> filter)
        {
            List<AstBase> result = new List<AstBase>(astList.Count());
            foreach (var ast in astList)
            {
                if (ast is AstRuleRef r && filter(r))
                    result.Add(ast);
                else
                    result.Add(ast);
            }
            return result;
        }

        public static IEnumerable<AstBase> KeepTerminal(this IEnumerable<AstBase> astList, Func<AstTerminal, bool> filter)
        {
            List<AstBase> result = new List<AstBase>(astList.Count());
            foreach (var ast in astList)
            {
                if (ast is AstTerminal t)
                {
                    if (filter(t))
                        result.Add(ast);
                }
                else
                    result.Add(ast);
            }
            return result;
        }

        public static IEnumerable<AstBase> KeepAlternatives(this IEnumerable<AstBase> astList, Func<AstAlternative, bool> filter)
        {
            List<AstBase> result = new List<AstBase>(astList.Count());
            foreach (var ast in astList)
                if (ast is AstAlternative a)
                {
                    if (filter(a))
                        result.Add(ast);
                }
                else
                    result.Add(ast);

            return result;
        }

        public static IEnumerable<AstBase> KeepBlocs(this IEnumerable<AstBase> astList, Func<AstBlock, bool> filter)
        {
            List<AstBase> result = new List<AstBase>(astList.Count());
            foreach (var ast in astList)
            {
                if (ast is AstBlock b)
                {
                    if (filter(b))
                        result.Add(ast);
                }
                else
                    result.Add(ast);
            }
            return result;
        }


    }


    public class ItemsVisitor : WalkerVisitor
    {

        public static IEnumerable<AstBase> Get(AstBase self)
        {
            var visitor = new ItemsVisitor();
            visitor.Visit(self);
            return visitor._listRules;
        }

        private ItemsVisitor()
        {
            _listRules = new List<AstBase>();
        }

        public override void Visit(AstBase a)
        {

            _listRules = new List<AstBase>();
            base.Visit(a);

        }

        public override void VisitRuleRef(AstRuleRef a)
        {
            _listRules.Add(a);
            base.VisitRuleRef(a);
        }

        public override void VisitTerminal(AstTerminal a)
        {
            _listRules.Add(a);
            base.VisitTerminal(a);
        }

        private List<AstBase> _listRules;

        //public override void VisitRule(AstRule a)
        //{            
        //    base.VisitRule(a);
        //}

        //public override void VisitBlock(AstBlock a)
        //{
        //    base.VisitBlock(a);
        //}

        //public override void VisitRules(AstRules a)
        //{
        //    base.VisitRules(a);
        //}

        //public override void AstLexerRulesList(AstLexerRulesList a)
        //{
        //    base.AstLexerRulesList(a);
        //}

        //public override void VisitActionBlock(AstActionBlock a)
        //{
        //    base.VisitActionBlock(a);
        //}

        //public override void VisitAlternative(AstAlternative a)
        //{
        //    base.VisitAlternative(a);
        //}

        //public override void VisitArgActionBlock(AstArgActionBlock a)
        //{
        //    base.VisitArgActionBlock(a);
        //}

        //public override void VisitAstEbnfSuffix(AstEbnfSuffix a)
        //{
        //    base.VisitAstEbnfSuffix(a);
        //}

        //public override void VisitAstAlternativeList(AstAlternativeList a)
        //{
        //    base.VisitAstAlternativeList(a);
        //}

        //public override void VisitAtom(AstAtom a)
        //{
        //    base.VisitAtom(a);
        //}

        //public override void VisitElement(AstElement a)
        //{
        //    base.VisitElement(a);
        //}

        //public override void VisitElementList(AstElementList a)
        //{
        //    base.VisitElementList(a);
        //}

        //public override void VisitElementOption(AstElementOption a)
        //{
        //    base.VisitElementOption(a);
        //}

        //public override void VisitElementOptionList(AstElementOptionList a)
        //{
        //    base.VisitElementOptionList(a);
        //}

        //public override void VisitExceptionGroup(AstExceptionGroup a)
        //{
        //    base.VisitExceptionGroup(a);
        //}

        //public override void VisitExceptionHandler(AstExceptionHandler a)
        //{
        //    base.VisitExceptionHandler(a);
        //}

        //public override void VisitFinallyClause(AstFinallyClause a)
        //{
        //    base.VisitFinallyClause(a);
        //}

        //public override void VisitGrammarSpec(AstGrammarSpec a)
        //{
        //    base.VisitGrammarSpec(a);
        //}

        //public override void VisitGrammerDecl(AstGrammarDecl a)
        //{
        //    base.VisitGrammerDecl(a);
        //}

        //public override void VisitIdentifierList(AstIdentifierList a)
        //{
        //    base.VisitIdentifierList(a);
        //}

        //public override void VisitLabeledAlt(AstLabeledAlt a)
        //{
        //    base.VisitLabeledAlt(a);
        //}

        //public override void VisitLabeledElement(AstLabeledElement a)
        //{
        //    base.VisitLabeledElement(a);
        //}

        //public override void VisitLexerAlternative(AstLexerAlternative a)
        //{
        //    base.VisitLexerAlternative(a);
        //}

        //public override void VisitLexerAlternativeList(AstLexerAlternativeList a)
        //{
        //    base.VisitLexerAlternativeList(a);
        //}

        //public override void VisitLexerBlock(AstLexerBlock a)
        //{
        //    base.VisitLexerBlock(a);
        //}

        //public override void VisitLexerCommand(AstLexerCommand a)
        //{
        //    base.VisitLexerCommand(a);
        //}

        //public override void VisitLexerCommandList(AstLexerCommandList a)
        //{
        //    base.VisitLexerCommandList(a);
        //}

        //public override void VisitLexerElementList(AstLexerElementList a)
        //{
        //    base.VisitLexerElementList(a);
        //}

        //public override void VisitLexerLabeledElement(AstLexerLabeledElement a)
        //{
        //    base.VisitLexerLabeledElement(a);
        //}

        //public override void VisitLexerRule(AstLexerRule a)
        //{
        //    base.VisitLexerRule(a);
        //}

        //public override void VisitLexerRulesList(AstLexerRulesList a)
        //{
        //    base.VisitLexerRulesList(a);
        //}

        //public override void VisitModeSpec(AstModeSpec a)
        //{
        //    base.VisitModeSpec(a);
        //}

        //public override void VisitModeSpecList(AstModeSpecList a)
        //{
        //    base.VisitModeSpecList(a);
        //}

        //public override void VisitOption(AstOption a)
        //{
        //    base.VisitOption(a);
        //}

        //public override void VisitOptionList(AstOptionList a)
        //{
        //    base.VisitOptionList(a);
        //}

        //public override void VisitParserRuleSpec(AstParserRuleSpec a)
        //{
        //    base.VisitParserRuleSpec(a);
        //}

        //public override void VisitPrequel(AstPrequel a)
        //{
        //    base.VisitPrequel(a);
        //}

        //public override void VisitNot(AstNot a)
        //{
        //    base.VisitNot(a);
        //}

        //public override void VisitPrequelConstruct(AstPrequelConstruct a)
        //{
        //    base.VisitPrequelConstruct(a);
        //}

        //public override void VisitPrequelConstructList(AstPrequelConstructList a)
        //{
        //    base.VisitPrequelConstructList(a);
        //}

        //public override void VisitPrequelList(AstPrequelList a)
        //{
        //    base.VisitPrequelList(a);
        //}

        //public override void VisitRuleAction(AstRuleAction a)
        //{
        //    base.VisitRuleAction(a);
        //}

        //public override void VisitRuleAltList(AstRuleAltList a)
        //{
        //    base.VisitRuleAltList(a);
        //}

        //public override void VisitRuleModifier(AstRuleModifier a)
        //{
        //    base.VisitRuleModifier(a);
        //}

        //public override void VisitRuleModifierList(AstRuleModifierList a)
        //{
        //    base.VisitRuleModifierList(a);
        //}

        //public override void VisitRulesList(AstRulesList a)
        //{
        //    base.VisitRulesList(a);
        //}

        //public override void VisitTerminalText(AstTerminalText a)
        //{
        //    base.VisitTerminalText(a);
        //}


    }


}
