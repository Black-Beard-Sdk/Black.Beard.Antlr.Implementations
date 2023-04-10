using Bb.Asts;

namespace Bb.Asts
{

    public class SelectorVisitor : IAstVisitor<IEnumerable<AstBase>>
    {

        public SelectorVisitor(Func<AstBase, bool>[] predicates)
        {
            this._predicates = predicates;
        }

        public static IEnumerable<AstBase> Select(AstBase ast, params Func<AstBase, bool>[] predicates)
        {
            var visitor = new SelectorVisitor(predicates);
            return ast.Accept(visitor);
        }

        public IEnumerable<AstBase> AstLexerRulesList(AstLexerRulesList a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitActionBlock(AstActionBlock a)
        {
            Stop();

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitAlternative(AstAlternative a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

            if (a.Options != null)
                foreach (var c in a.Options.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitAlternativeList(AstAlternativeList a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitArgActionBlock(AstArgActionBlock a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitAtom(AstAtom a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Value != null)
                foreach (var c in a.Value.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitBlock(AstBlock a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.AlternativeList != null)
                foreach (var b in a?.AlternativeList.Accept(this))
                    yield return b;

            if (a.Options != null)
                foreach (var c in a.Options.Accept(this))
                    yield return c;

            if (a.RuleAction != null)
                foreach (var c in a.RuleAction.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitEbnfSuffix(AstEbnfSuffix a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

        }

        public IEnumerable<AstBase> VisitElement(AstElement a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Child != null)
                foreach (var c in a.Child.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitElementList(AstElementList a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitElementOption(AstElementOption a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Key != null)
                foreach (var c in a.Key.Accept(this))
                    yield return c;

            if (a.Value != null)
                foreach (var c in a.Value.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitElementOptionList(AstElementOptionList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitExceptionGroup(AstExceptionGroup a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.FinallyClause != null)
                foreach (var c in a.FinallyClause.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitExceptionHandler(AstExceptionHandler a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.ArgActionBlock != null)
                foreach (var c in a.ArgActionBlock.Accept(this))
                    yield return c;

            if (a.ActionBlock != null)
                foreach (var c in a.ActionBlock.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitFinallyClause(AstFinallyClause a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Block != null)
                foreach (var c in a.Block.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitGrammarSpec(AstGrammarSpec a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Prequels != null)
                foreach (var c in a.Prequels.Accept(this))
                    yield return c;

            if (a.Declaration != null)
                foreach (var c in a.Declaration.Accept(this))
                    yield return c;

            if (a.Rules != null)
                foreach (var c in a.Rules.Accept(this))
                    yield return c;

            if (a.Modes != null)
                foreach (var c in a.Modes.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitGrammerDecl(AstGrammarDecl a)
        {

            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Name != null)
                foreach (var c in a.Name.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitIdentifierList(AstIdentifierList a)
        {

            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLabeledAlt(AstLabeledAlt a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Name != null)
                foreach (var c in a.Name.Accept(this))
                    yield return c;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLabeledElement(AstLabeledElement a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Name != null)
                foreach (var c in a.Name.Accept(this))
                    yield return c;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerAlternative(AstLexerAlternative a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

            if (a.Commands != null)
                foreach (var c in a.Commands.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerBlock(AstLexerBlock a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.AlternativeList != null)
                foreach (var c in a.AlternativeList.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerCommand(AstLexerCommand a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

        }

        public IEnumerable<AstBase> VisitLexerElementList(AstLexerElementList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerElementList(AstLexerCommandList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Name != null)
                foreach (var c in a.Name.Accept(this))
                    yield return c;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerRule(AstLexerRule a)
        {

            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Alternatives != null)
                foreach (var c in a.Alternatives.Accept(this))
                    yield return c;

            if (a.Value != null)
                foreach (var c in a.Value.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitLexerRulesList(AstLexerRulesList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitModeSpec(AstModeSpec a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

        }

        public IEnumerable<AstBase> VisitModeSpecList(AstModeSpecList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitNot(AstNot a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitOption(AstOption a)
        {

            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Key != null)
                foreach (var c in a.Key.Accept(this))
                    yield return c;

            if (a.Value != null)
                foreach (var c in a.Value.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitOptionList(AstOptionList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitParserRuleSpec(AstParserRuleSpec a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;
        }

        public IEnumerable<AstBase> VisitPrequel(AstPrequel a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitPrequelConstruct(AstPrequelConstruct a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Value != null)
                foreach (var c in a.Value.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitPrequelConstructList(AstPrequelConstructList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitPrequelList(AstPrequelList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRange(AstRange a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.ValueStart != null)
                foreach (var c in a.ValueStart.Accept(this))
                    yield return c;

            if (a.ValueEnd != null)
                foreach (var c in a.ValueEnd.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRule(AstRule a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Rule != null)
                foreach (var c in a.Rule.Accept(this))
                    yield return c;

            if (a.Alternatives != null)
                foreach (var c in a.Alternatives.Accept(this))
                    yield return c;

            if (a.Return != null)
                foreach (var c in a.Return.Accept(this))
                    yield return c;

            if (a.ThrowsSpec != null)
                foreach (var c in a.ThrowsSpec.Accept(this))
                    yield return c;

            if (a.Local != null)
                foreach (var c in a.Local.Accept(this))
                    yield return c;

            if (a.Prequels != null)
                foreach (var c in a.Prequels.Accept(this))
                    yield return c;

            if (a.ExceptionGroup != null)
                foreach (var c in a.ExceptionGroup.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRuleAction(AstRuleAction a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Name != null)
                foreach (var c in a.Name.Accept(this))
                    yield return c;

            if (a.Action != null)
                foreach (var c in a.Action.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRuleAltList(AstRuleAltList a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRuleModifier(AstRuleModifier a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;
        }

        public IEnumerable<AstBase> VisitRuleModifierList(AstRuleModifierList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRuleRef(AstRuleRef a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Name != null)
                foreach (var c in a.Name.Accept(this))
                    yield return c;

            if (a.Action != null)
                foreach (var c in a.Action.Accept(this))
                    yield return c;

            if (a.Option != null)
                foreach (var c in a.Option.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRules(AstRules a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            if (a.Rules != null)
                foreach (var c in a.Rules.Accept(this))
                    yield return c;

            if (a.Terminals != null)
                foreach (var c in a.Terminals.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitRulesList(AstRulesList a)
        {
            Stop();
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;

            foreach (var b in a)
                foreach (var c in b.Accept(this))
                    yield return c;

        }

        public IEnumerable<AstBase> VisitTerminal(AstTerminal a)
        {
            foreach (var item in _predicates)
                if (item(a))
                    yield return a;
        }

        public IEnumerable<AstBase> VisitTerminalText(AstTerminalText a)
        {

            foreach (var item in _predicates)
                if (item(a))
                    yield return a;
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }


        private readonly Func<AstBase, bool>[] _predicates;


    }


}
