﻿using Antlr4.Runtime.Misc;
using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;
using System.Linq;

namespace Generate.HelperScripts
{

    public class GenerateCodeForIdentifierVisitor : IAstVisitor<CodeExpression>
    {

        public GenerateCodeForIdentifierVisitor(AstGrammarSpec items)
        {
            _items = items;
        }

        public static CodeExpression GetExpression(AstRule rule)
        {
            AstGrammarSpec items = rule.Root();
            var visitor = new GenerateCodeForIdentifierVisitor(items);
            return rule.Accept(visitor);
        }

        public CodeExpression VisitAlternative(AstAlternative a)
        {

            if (a.Options != null)
            {
                Stop();
            }

            return a.Rule.Accept(this);

        }

        public CodeExpression VisitElementList(AstElementList a)
        {

            List<CodeExpression> list = new List<CodeExpression>(a.Count);

            foreach (var item in a)
            {
                var code = item.Accept(this);
                list.Add(code);
            }

            if (list.Count == 1)
                return list[0];

            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLabeledAlt(AstLabeledAlt a)
        {

            if (a.Name != null)
            {
                Stop();
            }

            return a.Rule.Accept(this);

        }

        public CodeExpression VisitRule(AstRule a)
        {

            Stop();
            throw new NotImplementedException();


        }

        public CodeExpression VisitAlternativeList(AstAlternativeList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitArgActionBlock(AstArgActionBlock a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitAtom(AstAtom a)
        {
            return a.Value.Accept(this);
        }

        public CodeExpression VisitBlock(AstBlock a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression AstLexerRulesList(AstLexerRulesList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitActionBlock(AstActionBlock a)
        {
            Stop();
            throw new NotImplementedException();

        }

        public CodeExpression VisitEbnfSuffix(AstEbnfSuffix a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitElement(AstElement a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLabeledElement(AstLabeledElement a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerAlternative(AstLexerAlternative a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerBlock(AstLexerBlock a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerCommand(AstLexerCommand a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerElementList(AstLexerElementList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerElementList(AstLexerCommandList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerRule(AstLexerRule a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitLexerRulesList(AstLexerRulesList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitModeSpec(AstModeSpec a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitModeSpecList(AstModeSpecList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitNot(AstNot a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitOption(AstOption a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitOptionList(AstOptionList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitParserRuleSpec(AstParserRuleSpec a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitPrequel(AstPrequel a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitPrequelConstruct(AstPrequelConstruct a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitPrequelConstructList(AstPrequelConstructList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitPrequelList(AstPrequelList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRange(AstRange a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRuleAction(AstRuleAction a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRuleAltList(AstRuleAltList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRuleModifier(AstRuleModifier a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRuleModifierList(AstRuleModifierList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRuleRef(AstRuleRef a)
        {
            return "context".Var().Call(a.Name.Text);
        }

        public CodeExpression VisitRules(AstRules a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitRulesList(AstRulesList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitTerminal(AstTerminal a)
        {
            return "context".Var().Call(a.Value.Text);
        }

        public CodeExpression VisitTerminalText(AstTerminalText a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitElementOption(AstElementOption a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitElementOptionList(AstElementOptionList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitExceptionGroup(AstExceptionGroup a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitExceptionHandler(AstExceptionHandler a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitFinallyClause(AstFinallyClause a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitGrammarSpec(AstGrammarSpec a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitGrammerDecl(AstGrammarDecl a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public CodeExpression VisitIdentifierList(AstIdentifierList a)
        {
            Stop();
            throw new NotImplementedException();
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        private readonly AstGrammarSpec _items;


    }


}
