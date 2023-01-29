﻿using Bb.Asts;
using Bb.Configurations;
using Bb.Generators;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Bb.Parsers
{

    public class CodeGeneratorVisitor : IAstBaseVisitor
    {



        public CodeGeneratorVisitor(Context ctx)
        {
            this._ctx = ctx;
            this._items = new AstGenerators();
        }

        public CodeGeneratorVisitor Add(string name, Action<AstGenerator> action)
        {

            var g = new AstGenerator()
            {
                Name = name
            };

            this._items.Add(g);

            action(g);

            return this;

        }

        public void Visit(AstBase a)
        {
            this._items.Clear();
            a.Accept(this);
            Generate();
        }

        private void Generate()
        {
            CleanFolder();
            _items.Generate(_ctx);
        }

        private void CleanFolder()
        {
            var dir = new DirectoryInfo(this._ctx.Path);

            if (!dir.Exists)
                dir.Create();

            foreach (var item in dir.GetFiles("*.generated.cs"))
                item.Delete();

        }

        public void VisitRule(AstRule a)
        {

            //if (a.RuleName.Text == "batch_level_statement")
            //{

            //}

            //Debug.WriteLine("------------------------");
            //Debug.WriteLine(a.ToString());
            //Debug.WriteLine("   ContainsOneItemInOutput : " + a.ContainsOneItemInOutput);
            //Debug.WriteLine("   ContainsJustOneAlternative : " + a.ContainsJustOneAlternative);
            //Debug.WriteLine("   ContainsOneRule : " + a.ContainsOneRule);
            //Debug.WriteLine("   ContainsOneTerminal : " + a.ContainsOneTerminal);
            //Debug.WriteLine("   ContainsOnlyRuleReferences : " + a.ContainsOnlyRuleReferences);
            //Debug.WriteLine("   ContainsOnlyTerminals : " + a.ContainsOnlyTerminals);
            //Debug.WriteLine("");
            //Debug.WriteLine("");

            //this._currentRule = a;
            //if (a.RuleBlock != null)
            //{
            //    a.RuleBlock?.Accept(this);

            //}
            //else
            //{
            //    Stop();
            //}

            a.Return?.Accept(this);
            a.RuleName?.Accept(this);
            a.Modifiers?.Accept(this);


            a.Rule?.Accept(this);
            a.ThrowsSpec?.Accept(this);
            a.Local?.Accept(this);
            a.Prequels?.Accept(this);
            a.ExceptionGroup?.Accept(this);

            if (a.RuleBlock != null)
                a.CanBeRemoved = a.RuleBlock.CanBeRemoved;

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
            a.Identifier?.Accept(this);
            a.Rule?.Accept(this);
            if (a.Identifier != null)
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
            e = a.IsRuleReference;
            e = a.IsTerminal;
            e = a.ContainsOneRule;
            e = a.ContainsOneTerminal;
            e = a.ContainsOnlyRuleReferences;
            e = a.ContainsOnlyTerminals;

            a.Value.Accept(this);
        }

        public void VisitRuleRef(AstRuleRef a)
        {

            bool e;
            e = a.IsRuleReference;
            e = a.IsTerminal;
            //e = a.ContainsOneRule;
            e = a.ContainsOneTerminal;
            e = a.ContainsOnlyRuleReferences;
            e = a.ContainsOnlyTerminals;

            a.Identifier?.Accept(this);
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

            a.Left.Accept(this);
            a.Right.Accept(this);


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

            a.Child?.Accept(this);



        }

        public void VisitPrequelConstruct(AstPrequelConstruct a)
        {

            a.Child?.Accept(this);



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

        private readonly Context _ctx;
        private readonly AstGenerators _items;
        private AstRule _currentRule;
    }

}
