using Bb.Asts;
using System.Diagnostics;

namespace Bb.Parsers
{


    public class ParentVisitor : IAstBaseVisitor
    {


        public ParentVisitor()
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

            a.Declaration.Accept(this);

            a.Modes?.Accept(this);

            a.Prequels?.Accept(this);

            a.Rules?.Accept(this);

            _stack.Pop();
        }


        public void VisitActionBlock(AstActionBlock a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitAlternative(AstAlternative a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Options?.Accept(this);
            a.Rule?.Accept(this);
            _stack.Pop();
        }

        public void VisitArgActionBlock(AstArgActionBlock a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitAtom(AstAtom a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitBlock(AstBlock a)
        {
            a.Parent = _stack.Peek();
        }

        public void VisitElement(AstElement a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public void VisitElementList(AstElementList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitElementOptionList(AstElementOptionList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitElmentOption(AstElementOption a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitExceptionGroup(AstExceptionGroup a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitExceptionHandler(AstExceptionHandler a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.ActionBlock?.Accept(this);
            a.ArgActionBlock?.Accept(this);
            _stack.Pop();
        }

        public void VisitFinallyClause(AstFinallyClause a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Block?.Accept(this);
            _stack.Pop();
        }

        public void VisitGrammerDecl(AstGrammarDecl a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Name.Accept(this);
            _stack.Pop();
        }

        public void VisitIdentifierList(AstIdentifierList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitLabeledAlt(AstLabeledAlt a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Rule?.Accept(this);
            a.Identifier?.Accept(this);
            _stack.Pop();
        }

        public void VisitLabeledElement(AstLabeledElement a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Left?.Accept(this);
            a.Right?.Accept(this);
            _stack.Pop();
        }

        public void VisitModeSpec(AstModeSpec a)
        {
            a.Parent = _stack.Peek();
        }

        public void VisitModeSpecList(AstModeSpecList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitOption(AstOption a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitOptionList(AstOptionList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitParserRuleSpec(AstParserRuleSpec a)
        {
            a.Parent = _stack.Peek();
        }

        public void VisitPrequel(AstPrequel a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Child?.Accept(this);
            _stack.Pop();
        }

        public void VisitPrequelConstruct(AstPrequelConstruct a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Child.Accept(this);
            _stack.Pop();
        }

        public void VisitPrequelConstructList(AstPrequelConstructList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitPrequelList(AstPrequelList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitRule(AstRule a)
        {
            a.Parent = _stack.Peek();
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
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Action?.Accept(this);
            a.Name?.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleAltList(AstRuleAltList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleModifier(AstRuleModifier a)
        {
            a.Parent = _stack.Peek();
        }

        public void VisitRuleModifierList(AstRuleModifierList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitRuleRef(AstRuleRef a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Identifier?.Accept(this);
            a.Action?.Accept(this);
            a.Option?.Accept(this);
            _stack.Pop();
        }

        public void VisitRulesList(AstRulesList a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            foreach (var item in a)
                item.Accept(this);
            _stack.Pop();
        }

        public void VisitTerminal(AstTerminal a)
        {
            a.Parent = _stack.Peek();
            _stack.Push(a);
            a.Options?.Accept(this);
            a.Value?.Accept(this);
            _stack.Pop();
        }

        public void VisitTerminalText(AstTerminalText a)
        {
            a.Parent = _stack.Peek();
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
