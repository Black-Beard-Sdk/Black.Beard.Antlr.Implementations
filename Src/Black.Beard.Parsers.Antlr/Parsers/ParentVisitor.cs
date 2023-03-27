using Bb.Asts;
using Bb.Generators;
using Bb.ParsersConfiguration.Ast;
using System.Diagnostics;

namespace Bb.Parsers
{


    public class ParentVisitor : WalkerVisitor
    {

        public ParentVisitor()
        {
            
        }

        public override void VisitActionBlock(AstActionBlock a)
        {
            a.Parent = Parent;
            base.VisitActionBlock(a);
        }

        public override void VisitAlternative(AstAlternative a)
        {
            a.Parent = Parent;
            base.VisitAlternative(a);
        }

        public override void VisitArgActionBlock(AstArgActionBlock a)
        {
            a.Parent = Parent;
            base.VisitArgActionBlock(a);
        }

        public override void VisitAtom(AstAtom a)
        {
            a.Parent = Parent;
            base.VisitAtom(a);
        }

        public override void VisitBlock(AstBlock a)
        {
            a.Parent = Parent;
            base.VisitBlock(a);
        }

        public override void VisitElement(AstElement a)
        {
            a.Parent = Parent;
            a.Child?.Accept(this);
        }

        public override void VisitElementList(AstElementList a)
        {
            a.Parent = Parent;
            base.VisitElementList(a);
        }

        public override void VisitElementOptionList(AstElementOptionList a)
        {
            a.Parent = Parent;
            base.VisitElementOptionList(a);
        }

        public override void VisitElementOption(AstElementOption a)
        {
            a.Parent = Parent;
            base.VisitElementOption(a);
        }

        public override void VisitExceptionGroup(AstExceptionGroup a)
        {
            a.Parent = Parent;
            base.VisitExceptionGroup(a);
        }

        public override void VisitExceptionHandler(AstExceptionHandler a)
        {
            a.Parent = Parent;
            base.VisitExceptionHandler(a);
        }

        public override void VisitFinallyClause(AstFinallyClause a)
        {
            a.Parent = Parent;
            base.VisitFinallyClause(a);
        }

        public override void VisitGrammerDecl(AstGrammarDecl a)
        {
            a.Parent = Parent;
            base.VisitGrammerDecl(a);
        }

        public override void VisitIdentifierList(AstIdentifierList a)
        {
            a.Parent = Parent;
            base.VisitIdentifierList(a);
        }

        public override void VisitLabeledAlt(AstLabeledAlt a)
        {
            a.Parent = Parent;
            base.VisitLabeledAlt(a);
        }

        public override void VisitLabeledElement(AstLabeledElement a)
        {
            a.Parent = Parent;
            base.VisitLabeledElement(a);
        }

        public override void VisitModeSpec(AstModeSpec a)
        {
            a.Parent = Parent;
            base.VisitModeSpec(a);
        }

        public override void VisitModeSpecList(AstModeSpecList a)
        {
            a.Parent = Parent;
            base.VisitModeSpecList(a);
        }

        public override void VisitOption(AstOption a)
        {
            a.Parent = Parent;
            base.VisitOption(a);
        }

        public override void VisitOptionList(AstOptionList a)
        {
            a.Parent = Parent;
            base.VisitOptionList(a);
        }

        public override void VisitParserRuleSpec(AstParserRuleSpec a)
        {
            a.Parent = Parent;
            base.VisitParserRuleSpec(a);
        }

        public override void VisitPrequel(AstPrequel a)
        {
            a.Parent = Parent;
            base.VisitPrequel(a);
        }

        public override void VisitPrequelConstruct(AstPrequelConstruct a)
        {
            a.Parent = Parent;
            base.VisitPrequelConstruct(a);
        }

        public override void VisitPrequelConstructList(AstPrequelConstructList a)
        {
            a.Parent = Parent;
            base.VisitPrequelConstructList(a);
        }

        public override void VisitPrequelList(AstPrequelList a)
        {
            a.Parent = Parent;
            base.VisitPrequelList(a);
        }


        private void ResolveInheritedClass(AstRule a, AstRule b)
        {

            var rules = b.GetRules().ToList();

            foreach (var rule in rules)
            {

                if (_rules.TryGetValue(rule.Identifier.Text, out var v1))
                {
                    if (v1 is AstRule r1)
                    {
                    
                        var c2 = r1.Configuration.Config;
                        if (c2.Generate)
                        {
                            if (c2.Inherit == null)
                                c2.Inherit = new IdentifierConfig(@"""" + "Ast" + CodeHelper.FormatCsharp(a.Name.Text) + @"""");
                        }
                        else
                        {
                            ResolveInheritedClass(a, r1);
                        }
                    }
                    else
                    {

                    }
                }
            }

        }

        public override void VisitRule(AstRule a)
        {

            a.Parent = Parent;
            base.VisitRule(a);

            var c = a.Configuration;
            if (!c.Config.Generate)
            {

                List<AstRule> references = a.Root.GetReferences(a);
                if (references.Count == 1)
                    ResolveInheritedClass(a, a);
                
            }

            if (a.Alternatives.Count == 1 && a.ContainsOneTerminal)
            {
                // a.TerminalKind = a.Alternatives[0].TerminalKind;
            }


        }

        public override void VisitRuleAction(AstRuleAction a)
        {
            a.Parent = Parent;
            base.VisitRuleAction(a);
        }

        public override void VisitRuleAltList(AstRuleAltList a)
        {
            a.Parent = Parent;
            base.VisitRuleAltList(a);
        }

        public override void VisitRuleModifier(AstRuleModifier a)
        {
            a.Parent = Parent;
            base.VisitRuleModifier(a);
        }

        public override void VisitRuleModifierList(AstRuleModifierList a)
        {
            a.Parent = Parent;
            base.VisitRuleModifierList(a);
        }

        public override void VisitRuleRef(AstRuleRef a)
        {
            a.Parent = Parent;
            base.VisitRuleRef(a);
            if (a != null)
            {                
                if (_rules.TryGetValue(a.ResolveName(), out var value))
                    a.Link = value;
            }

        }

        public override void VisitRulesList(AstRulesList a)
        {
            a.Parent = Parent;
            base.VisitRulesList(a);
        }

        public override void VisitTerminal(AstTerminal a)
        {
            a.Parent = Parent;

            if (a != null)
            {
                if (_rules.TryGetValue(a.ResolveName(), out var value))
                {
                    a.Link = value;
                    a.TerminalKind = value.TerminalKind;
                }
            }


            base.VisitTerminal(a);
        }

        public override void VisitTerminalText(AstTerminalText a)
        {
            a.Parent = Parent;
            if (a != null && !string.IsNullOrEmpty(a.Text))
                if (_rules.TryGetValue(a.Text, out var value))
                    a.Link = value;

            base.VisitTerminalText(a);

        }


        public override void VisitAstAlternativeList(AstAlternativeList a)
        {
            a.Parent = Parent;
            base.VisitAstAlternativeList(a);
        }

        public override void VisitAstEbnfSuffix(AstEbnfSuffix a)
        {
            a.Parent = Parent;
            base.VisitAstEbnfSuffix(a);
        }

        public override void AstLexerRulesList(AstLexerRulesList a)
        {
            a.Parent = Parent;
            base.AstLexerRulesList(a);
        }

        public override void VisitLexerRulesList(AstLexerRulesList a)
        {
            a.Parent = Parent;
            base.VisitLexerRulesList(a);
        }

        public override void VisitRules(AstRules a)
        {
            this._rules = a;
            a.Parent = Parent;
            base.VisitRules(a);
        }

        public override void VisitLexerRule(AstLexerRule a)
        {
            a.Parent = Parent;
            base.VisitLexerRule(a);
            if (_rules.TryGetValue(a.Name.Text, out var value))
                a.Link = value;

        }

        public override void VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            a.Parent = Parent;
            base.VisitLexerLabeledElement(a);
        }

        public override void VisitLexerElementList(AstLexerElementList a)
        {
            a.Parent = Parent;
            base.VisitLexerElementList(a);
        }

        public override void VisitLexerBlock(AstLexerBlock a)
        {
            a.Parent = Parent;
            base.VisitLexerBlock(a);
        }

        public override void VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            a.Parent = Parent;
            base.VisitLexerAlternativeList(a);
        }

        public override void VisitLexerAlternative(AstLexerAlternative a)
        {
            a.Parent = Parent;
            base.VisitLexerAlternative(a);
        }

        public override void VisitLexerCommandList(AstLexerCommandList a)
        {
            a.Parent = Parent;
            base.VisitLexerCommandList(a);
        }

        public override void VisitLexerCommand(AstLexerCommand a)
        {
            a.Parent = Parent;
            base.VisitLexerCommand(a);
        }

        private AstRules _rules;        

    }


}
