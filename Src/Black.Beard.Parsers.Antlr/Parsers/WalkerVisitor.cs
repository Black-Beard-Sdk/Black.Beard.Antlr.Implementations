using Bb.Asts;

namespace Bb.Parsers
{



    public class WalkerVisitor : IAstBaseVisitor
    {

        public WalkerVisitor()
        {

        }

        public virtual void Visit(AstBase a)
        {
            _stack.Push(a);
            a.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitGrammarSpec(AstGrammarSpec a)
        {
            _stack.Push(a);
            a.Declaration?.Accept(this);
            a.Modes?.Accept(this);
            a.Prequels?.Accept(this);
            a.Rules?.Accept(this);
            _stack.Pop();
        }


        public virtual void VisitActionBlock(AstActionBlock a)
        {
            _stack.Push(a);
            _stack.Pop();
        }

        public virtual void VisitAlternative(AstAlternative a)
        {
            _stack.Push(a);
            a.Rule?.Accept(this);
            a.Options?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitArgActionBlock(AstArgActionBlock a)
        {
            _stack.Push(a);
            _stack.Pop();
        }

        public virtual void VisitAtom(AstAtom a)
        {
            _stack.Push(a);
            a.Value.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitBlock(AstBlock a)
        {
            _stack.Push(a);

            _stack.Pop();
        }

        public virtual void VisitElement(AstElement a)
        {
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitElementList(AstElementList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitElementOptionList(AstElementOptionList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitElementOption(AstElementOption a)
        {
            _stack.Push(a);
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitExceptionGroup(AstExceptionGroup a)
        {
            _stack.Push(a);
            a.FinallyClause?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitExceptionHandler(AstExceptionHandler a)
        {
            _stack.Push(a);
            a.ArgActionBlock.Accept(this);
            a.ActionBlock.Accept(this);
            _stack.Pop();
            throw new NotImplementedException();
        }

        public virtual void VisitFinallyClause(AstFinallyClause a)
        {
            _stack.Push(a);
            a.Block.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitGrammerDecl(AstGrammarDecl a)
        {
            _stack.Push(a);
            a.Name.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitIdentifierList(AstIdentifierList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLabeledAlt(AstLabeledAlt a)
        {
            _stack.Push(a);
            a.Identifier?.Accept(this);
            a.Rule?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLabeledElement(AstLabeledElement a)
        {
            _stack.Push(a);
            a.Left.Accept(this);
            a.Right.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitModeSpec(AstModeSpec a)
        {
            _stack.Push(a);

            _stack.Pop();
        }

        public virtual void VisitModeSpecList(AstModeSpecList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitOption(AstOption a)
        {
            _stack.Push(a);
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitOptionList(AstOptionList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitPrequel(AstPrequel a)
        {
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitPrequelConstruct(AstPrequelConstruct a)
        {
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitPrequelConstructList(AstPrequelConstructList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitPrequelList(AstPrequelList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRule(AstRule a)
        {
            _stack.Push(a);
            a.Return?.Accept(this);
            a.Modifiers?.Accept(this);
            a.Alternatives?.Accept(this);
            a.Rule?.Accept(this);
            a.ThrowsSpec?.Accept(this);
            a.Local?.Accept(this);
            a.Prequels?.Accept(this);
            a.ExceptionGroup?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRuleAction(AstRuleAction a)
        {
            _stack.Push(a);
            a.Action?.Accept(this);
            a.Name?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRuleAltList(AstRuleAltList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRuleModifierList(AstRuleModifierList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRuleRef(AstRuleRef a)
        {
            _stack.Push(a);
            a.Identifier?.Accept(this);
            a.Action?.Accept(this);
            a.Option?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRulesList(AstRulesList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitTerminal(AstTerminal a)
        {
            _stack.Push(a);
            a.Options?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitAstAlternativeList(AstAlternativeList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitTerminalText(AstTerminalText a)
        {


            
        }

        public virtual void VisitParserRuleSpec(AstParserRuleSpec a)
        {

        }

        public virtual void VisitRuleModifier(AstRuleModifier a)
        {

        }

        public virtual void VisitAstEbnfSuffix(AstEbnfSuffix a)
        {
            _stack.Push(a);

            _stack.Pop();
        }

        public virtual void AstLexerRulesList(AstLexerRulesList a)
        {
            _stack.Push(a);

            _stack.Pop();
        }

        public virtual void VisitLexerRulesList(AstLexerRulesList a)
        {
            _stack.Push(a);
            foreach(var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitRules(AstRules a)
        {
            _stack.Push(a);
            a.Terminals.Accept(this);
            a.Rules.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerRule(AstLexerRule a)
        {
            _stack.Push(a);
            a.Alternatives?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            _stack.Push(a);
            a.Left?.Accept(this);
            a.Right?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerElementList(AstLexerElementList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerBlock(AstLexerBlock a)
        {
            _stack.Push(a);
            a.AlternativeList?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerAlternative(AstLexerAlternative a)
        {
            _stack.Push(a);
            a.Rule?.Accept(this);
            a.Commands?.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerCommandList(AstLexerCommandList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public virtual void VisitLexerCommand(AstLexerCommand a)
        {

        }

        public virtual void VisitNot(AstNot a)
        {
            _stack.Push(a);
            a.Value.Accept(this);
            _stack.Pop();
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        protected void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        public void VisitRange(AstRange a)
        {
            _stack.Push(a);
            a.ValueStart.Accept(this);
            a.ValueEnd.Accept(this);
            _stack.Pop();
        }

        protected AstBase Parent { get => _stack.Peek(); }

        private Stack<AstBase> _stack = new Stack<AstBase>();


    }


}
