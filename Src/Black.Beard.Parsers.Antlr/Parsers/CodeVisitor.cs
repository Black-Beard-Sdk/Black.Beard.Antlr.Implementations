using Bb.Asts;

namespace Bb.Parsers
{



    public class CodeVisitor : IAstBaseVisitor
    {

        public CodeVisitor()
        {

        }

        public void Visit(AstBase a)
        {
            _stack.Push(a);
            a.Accept(this);
            _stack.Pop();
        }

        public void VisitGrammarSpec(AstGrammarSpec a)
        {
            _stack.Push(a);
            a.Declaration?.Accept(this);
            a.Modes?.Accept(this);
            a.Prequels?.Accept(this);
            a.Rules?.Accept(this);
            _stack.Pop();
        }


        public void VisitActionBlock(AstActionBlock a)
        {
            _stack.Push(a);
            _stack.Pop();
        }

        public void VisitAlternative(AstAlternative a)
        {
            _stack.Push(a);
            a.Rule?.Accept(this);
            a.Options?.Accept(this);
            _stack.Pop();
        }

        public void VisitArgActionBlock(AstArgActionBlock a)
        {
            _stack.Push(a);
            _stack.Pop();
        }

        public void VisitAtom(AstAtom a)
        {
            _stack.Push(a);
            a.Value.Accept(this);
            _stack.Pop();
        }

        public void VisitBlock(AstBlock a)
        {
            _stack.Push(a);

            _stack.Pop();
        }

        public void VisitElement(AstElement a)
        {
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public void VisitElementList(AstElementList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitElementOptionList(AstElementOptionList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitElementOption(AstElementOption a)
        {
            _stack.Push(a);
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitExceptionGroup(AstExceptionGroup a)
        {
            _stack.Push(a);
            a.FinallyClause?.Accept(this);
            _stack.Pop();
        }

        public void VisitExceptionHandler(AstExceptionHandler a)
        {
            _stack.Push(a);
            a.ArgActionBlock.Accept(this);
            a.ActionBlock.Accept(this);
            _stack.Pop();
            throw new NotImplementedException();
        }

        public void VisitFinallyClause(AstFinallyClause a)
        {
            _stack.Push(a);
            a.Block.Accept(this);
            _stack.Pop();
        }

        public void VisitGrammerDecl(AstGrammarDecl a)
        {
            _stack.Push(a);
            a.Name.Accept(this);
            a.Code.Name = a.Name.Text;
            _stack.Pop();

        }

        public void VisitIdentifierList(AstIdentifierList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitLabeledAlt(AstLabeledAlt a)
        {
            _stack.Push(a);
            a.Identifier?.Accept(this);
            a.Rule?.Accept(this);
            _stack.Pop();
        }

        public void VisitLabeledElement(AstLabeledElement a)
        {
            _stack.Push(a);
            a.Left.Accept(this);
            a.Right.Accept(this);
            _stack.Pop();
        }

        public void VisitModeSpec(AstModeSpec a)
        {

        }

        public void VisitModeSpecList(AstModeSpecList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitOption(AstOption a)
        {
            _stack.Push(a);
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitOptionList(AstOptionList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitParserRuleSpec(AstParserRuleSpec a)
        {


        }

        public void VisitPrequel(AstPrequel a)
        {
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public void VisitPrequelConstruct(AstPrequelConstruct a)
        {
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public void VisitPrequelConstructList(AstPrequelConstructList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitPrequelList(AstPrequelList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitRule(AstRule a)
        {
            _stack.Push(a);
            a.Return?.Accept(this);
            a.RuleName?.Accept(this);
            a.Modifiers?.Accept(this);
            a.RuleBlock?.Accept(this);
            a.Rule?.Accept(this);
            a.ThrowsSpec?.Accept(this);
            a.Local?.Accept(this);
            a.Prequels?.Accept(this);
            a.ExceptionGroup?.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleAction(AstRuleAction a)
        {
            _stack.Push(a);
            a.Action?.Accept(this);
            a.Name?.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleAltList(AstRuleAltList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleModifier(AstRuleModifier a)
        {

        }

        public void VisitRuleModifierList(AstRuleModifierList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleRef(AstRuleRef a)
        {
            _stack.Push(a);
            a.Identifier?.Accept(this);
            a.Action?.Accept(this);
            a.Option?.Accept(this);
            _stack.Pop();
        }

        public void VisitRulesList(AstRulesList a)
        {
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitTerminal(AstTerminal a)
        {
            _stack.Push(a);
            a.Options?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitAstAlternativeList(AstAlternativeList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitTerminalText(AstTerminalText a)
        {
            a.Code.Name = a.Text;
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }


        private Stack<AstBase> _stack = new Stack<AstBase>();


    }


}
