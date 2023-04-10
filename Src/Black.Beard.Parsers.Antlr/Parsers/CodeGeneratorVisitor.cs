using Bb.Asts;
using Bb.Generators;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Bb.Parsers
{


    public class CodeGeneratorVisitor : IAstBaseVisitor
    {



        public CodeGeneratorVisitor(Context ctx, ScriptBase root)
        {
            this._ctx = ctx;
            this._items = new AstGenerators();
            this._root = root;
        }

        public CodeGeneratorVisitor Add(string name, Action<AstGenerator> action)
        {

            var g = new AstGenerator()
            {
                Name = name,
                _root = this._root,
            };

            this._items.Add(g);

            action(g);

            return this;

        }

        public IEnumerable<string> Visit(AstBase a)
        {
            this._items.Clear();
            a.Accept(this);
            return Generate().ToList();
        }

        private IEnumerable<string> Generate()
        {
            return _items.Generate(_ctx);
        }

        public void VisitRules(AstRules a)
        {

            foreach (var item in a.Rules)
                item.Accept(this);

            foreach (var item in a.Terminals)
                item.Accept(this);

        }

        public void VisitLexerRule(AstLexerRule a)
        {                       
            if (a.Alternatives != null)
                a.CanBeRemoved = a.Alternatives.CanBeRemoved;
            _items.Add(a);
        }

        public void VisitRule(AstRule a)
        {

            a.Return?.Accept(this);
            a.Modifiers?.Accept(this);


            a.Rule?.Accept(this);
            a.ThrowsSpec?.Accept(this);
            a.Local?.Accept(this);
            a.Prequels?.Accept(this);
            a.ExceptionGroup?.Accept(this);

            if (a.Alternatives != null)
                a.CanBeRemoved = a.Alternatives.CanBeRemoved;

            //var config = _ctx.Configuration.GetConfiguration(a.RuleName.Text);
            //config.ProposalGenerate = !a.CanBeRemoved;

            _items.Add(a);

        }

        public void VisitRuleAltList(AstRuleAltList a)
        {

            a.CanBeRemoved = true;

            foreach (var item in a)
            {

                if (item.Rule.Count > 1)
                    a.CanBeRemoved = false;

                if (item.Rule.Count == 1)
                {
                    var p = item.Rule.Rule[0];
                    if (p is AstAtom b)
                    {
                        if (b.Value.Type == "AstTerminal")
                            a.CanBeRemoved = false;

                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                item.Accept(this);

            }

        }

        public void VisitLabeledAlt(AstLabeledAlt a)
        {
            a.Name?.Accept(this);
            a.Rule?.Accept(this);
            if (a.Name != null)
                _items.Add(a);

        }

        public void VisitGrammarSpec(AstGrammarSpec a)
        {
            a.Declaration?.Accept(this);
            a.Modes?.Accept(this);
            a.Prequels?.Accept(this);
            a.Rules?.Accept(this);

        }

        public void VisitAlternative(AstAlternative a)
        {

            a.Rule?.Accept(this);
            a.Options?.Accept(this);

        }
        public void VisitElementList(AstElementList a)
        {

            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitAtom(AstAtom a)
        {

            bool e;
            e = a.IsRule;
            e = a.IsTerminal;
            e = a.ContainsOneRule;
            e = a.ContainsOneTerminal;
            e = a.ContainsOnlyRules;
            e = a.ContainsOnlyTerminals;

            a.Value.Accept(this);
        }

        public void VisitRuleRef(AstRuleRef a)
        {

            bool e;
            e = a.IsRule;
            e = a.IsTerminal;
            //e = a.ContainsOneRule;
            e = a.ContainsOneTerminal;
            e = a.ContainsOnlyRules;
            e = a.ContainsOnlyTerminals;

            a.Name?.Accept(this);
            a.Action?.Accept(this);
            a.Option?.Accept(this);
        }

        public void VisitActionBlock(AstActionBlock a)
        {

        }

        public void VisitArgActionBlock(AstArgActionBlock a)
        {

        }

        public void VisitBlock(AstBlock a)
        {

        }

        public void VisitElement(AstElement a)
        {

            a.Child?.Accept(this);
        }

        public void VisitElementOptionList(AstElementOptionList a)
        {

            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitElementOption(AstElementOption a)
        {

            a.Key?.Accept(this);
            a.Value?.Accept(this);
        }

        public void VisitExceptionGroup(AstExceptionGroup a)
        {

            a.FinallyClause?.Accept(this);
        }

        public void VisitExceptionHandler(AstExceptionHandler a)
        {

            a.ArgActionBlock.Accept(this);
            a.ActionBlock.Accept(this);
        }

        public void VisitFinallyClause(AstFinallyClause a)
        {
            a.Block.Accept(this);
        }

        public void VisitGrammerDecl(AstGrammarDecl a)
        {
            a.Name.Accept(this);
        }

        public void VisitIdentifierList(AstIdentifierList a)
        {
            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitLabeledElement(AstLabeledElement a)
        {

            a.Name.Accept(this);
            a.Rule.Accept(this);


        }

        public void VisitModeSpec(AstModeSpec a)
        {


        }

        public void VisitModeSpecList(AstModeSpecList a)
        {
            foreach (var item in a)
                item.Accept(this);

        }

        public void VisitOption(AstOption a)
        {

            a.Key?.Accept(this);
            a.Value?.Accept(this);
        }

        public void VisitOptionList(AstOptionList a)
        {

            //foreach (var item in a)
            //    item.Accept(this);

            //
            //    

        }

        public void VisitParserRuleSpec(AstParserRuleSpec a)
        {



        }

        public void VisitPrequel(AstPrequel a)
        {

            a.Rule?.Accept(this);



        }

        public void VisitPrequelConstruct(AstPrequelConstruct a)
        {

            a.Value?.Accept(this);



        }

        public void VisitPrequelConstructList(AstPrequelConstructList a)
        {

            foreach (var item in a)
                item.Accept(this);



        }

        public void VisitPrequelList(AstPrequelList a)
        {

            foreach (var item in a)
                item.Accept(this);

        }

        public void VisitRuleAction(AstRuleAction a)
        {

            a.Action?.Accept(this);
            a.Name?.Accept(this);

        }

        public void VisitRuleModifier(AstRuleModifier a)
        {



        }

        public void VisitRuleModifierList(AstRuleModifierList a)
        {
            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitRulesList(AstRulesList a)
        {
            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitTerminal(AstTerminal a)
        {
            a.Options?.Accept(this);
            a.Value?.Accept(this);
        }

        public void VisitTerminalText(AstTerminalText a)
        {
            //a.Code.Name = "Ast" + a.Text;

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        public void VisitAstAlternativeList(AstAlternativeList a)
        {
            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitAstEbnfSuffix(AstEbnfSuffix a)
        {
            throw new NotImplementedException();
        }

        public void AstLexerRulesList(AstLexerRulesList a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerRulesList(AstLexerRulesList a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerElementList(AstLexerElementList a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerBlock(AstLexerBlock a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerAlternative(AstLexerAlternative a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerCommandList(AstLexerCommandList a)
        {
            throw new NotImplementedException();
        }

        public void VisitLexerCommand(AstLexerCommand astLexerCommand)
        {
            throw new NotImplementedException();
        }

        public void VisitNot(AstNot astNot)
        {
            throw new NotImplementedException();
        }

        public void VisitRange(AstRange a)
        {
            throw new NotImplementedException();
        }

        private readonly Context _ctx;
        private readonly AstGenerators _items;
        private readonly ScriptBase _root;
        private AstRule _currentRule;
    }

}
