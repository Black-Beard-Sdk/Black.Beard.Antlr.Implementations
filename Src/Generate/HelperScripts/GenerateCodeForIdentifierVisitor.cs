using Antlr4.Runtime.Misc;
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
            _rules = _items.Rules;
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
                if (item.WhereRuleOrIdentifiers())
                {
                    var code = item.Accept(this);
                    list.Add(code);
                }
                else
                {

                }

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

            var n = "Ast" + a.GetPropertyName();

            foreach (var item in a.Alternatives)
            {
                var args = new List<CodeExpression>();


                var r = item.Accept(this);


                var m = CodeHelper.Call(n.AsType(), "New", args.ToArray());

            }


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

            var itemRule = _rules.Rules.Where(c => c.Name.Text == a.Name.Text).First();
            _currentRules.Add(itemRule.Name.Text);

            List<CodeExpression> list = new List<CodeExpression>();
            foreach (var item in itemRule.Alternatives)
            {
                var b = item.Accept(this);
                if (b != null)
                    list.Add(b);

                if (a.Name.Text == "id_")
                    break;

            }

            _currentRules.RemoveAt(_currentRules.Count - 1);

            if (list.Count == 1)
                return list[0];

            return null;

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

            if (a.WhereRuleOrIdentifiers())
            {
                var name = GetVariable(a.Type());
                return name.Var();
            }


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


        private string GetVariable(string type)
        {

            var varName = "varname";
            if (_currentRules.Count > 0)
            {
                int i = 1;
                string n = string.Empty;
                while (_currentRules[_currentRules.Count - i] == "id_")
                {
                    i++;
                    n = _currentRules[_currentRules.Count - i];
                }

                if (!string.IsNullOrEmpty(n))
                    varName = n;

            }

            varName = varName + (_variables.Count + 1);
            _variables.Add((varName, type));

            return varName;

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        private List<string> _currentRules = new List<string>();
        private List<(string, string)> _variables = new List<(string, string)>();
        private readonly AstGrammarSpec _items;
        private readonly AstRules _rules;
    }


}
