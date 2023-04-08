using Bb.Asts;
using System.Data.SqlTypes;
using System.Xml.Linq;

namespace Bb.Parsers
{

    public class RuleIdVisitor : IAstVisitor<TreeRuleItem>
    {

        public void Visit(AstGrammarSpec rootAst)
        {
            rootAst.Accept(this);
        }

        public TreeRuleItem VisitRules(AstRules a)
        {
            a.Terminals.Accept(this);
            return a.Rules.Accept(this);
        }

        public TreeRuleItem VisitRule(AstRule a)
        {
            return a.Alternatives.Accept(this);
        }

        public TreeRuleItem VisitRuleAltList(AstRuleAltList a)
        {

            var result = new TreeRuleItem(null)
            {
                IsAlternative = true,
                Origin = a,                
            };

            foreach (var item in a)
            {
                var b = item.Accept(this);
                if (b != null)
                    result.Add(b);
            }

            if (result.Count == 0)
                return null;

            if (result.Count == 1)
                return result[0];

            return result;

        }

        public TreeRuleItem VisitAlternativeList(AstAlternativeList a)
        {

            var result = new TreeRuleItem(null)
            {
                IsAlternative = true,
                Origin = a,
            };

            foreach (var item in a)
            {
                var b = item.Accept(this);
                if (b != null)
                    result.Add(b);
            }

            if (result.Count == 0)
                return null;

            if (result.Count == 1)
                return result[0];

            return result;
        }


        public TreeRuleItem VisitLabeledAlt(AstLabeledAlt a)
        {

            if (a.Name != null)
            {


            }

            var result = a.Rule.Accept(this);

            return result;

        }


        public TreeRuleItem VisitElementList(AstElementList a)
        {

            var result = new TreeRuleItem(null)
            {
                Origin = a,
            };

            foreach (var item in a)
            {
                var b = item.Accept(this);
                if (b != null)
                    result.Add(b);
            }

            if (result.Count == 0)
                return null;

            if (result.Count == 1)
                return result[0];

            return result;

        }


        public TreeRuleItem VisitAlternative(AstAlternative a)
        {
            return a.Rule.Accept(this);
        }


        public TreeRuleItem VisitAtom(AstAtom a)
        {

            var result = a.Value?.Accept(this);

            if (result != null)
                result.Occurence = a.Occurence.Clone();

            return result;

        }

        public TreeRuleItem VisitGrammarSpec(AstGrammarSpec a)
        {
            return a.Rules.Accept(this);
        }

        public TreeRuleItem VisitBlock(AstBlock a)
        {

            var result = a.AlternativeList.Accept(this);

            if (result != null)
            {
                result.Occurence = a.Occurence;
                result.IsBlock = true;
            }

            return result;
        }

        public IDictionary<string, TreeRuleItem> Items { get => _items; }

        public TreeRuleItem VisitRulesList(AstRulesList a)
        {

            foreach (var item in a)
            {

                //Console.WriteLine(item.ToString());
                var b = item.Accept(this);

                //var o = b.ToString();
                //Console.WriteLine(o);

                if (b != null)
                    _items.Add(item.Name.Text, b);

            }

            return null;

        }

        public TreeRuleItem VisitTerminal(AstTerminal a)
        {
            return a.Value.Accept(this);
        }

        public TreeRuleItem VisitTerminalText(AstTerminalText a)
        {
            if (a.Text != "EOF")
            {
                // var p = a.Link as AstLexerRule;
                return new TreeRuleItem(a.Text) 
                {
                    IsTerminal = true,  
                    IsConstant = a.TerminalKind == TokenTypeEnum.Constant,
                    Origin = a,
                };
            }
            return null;
        }

        public TreeRuleItem VisitLabeledElement(AstLabeledElement a)
        {
            var u = a.Occurence;
            var result = a.Rule.Accept(this);
            result.Label = a.Name.Text;
            return result;

        }

        public TreeRuleItem VisitRuleRef(AstRuleRef a)
        {
            return new TreeRuleItem(a.ResolveName()) 
            {
                IsRuleRef = true,
                Origin = a
            };
        }

        public TreeRuleItem VisitActionBlock(AstActionBlock a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitElement(AstElement a)
        {
            return null;
        }

        public TreeRuleItem VisitElementOption(AstElementOption a)
        {
            return null;
        }

        public TreeRuleItem VisitElementOptionList(AstElementOptionList a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitArgActionBlock(AstArgActionBlock a)
        {
            var u = a.Occurence;

            return null;
        }

        public TreeRuleItem VisitEbnfSuffix(AstEbnfSuffix a)
        {
            return null;
        }

        public TreeRuleItem VisitExceptionGroup(AstExceptionGroup a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitModeSpecList(AstModeSpecList a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitExceptionHandler(AstExceptionHandler a)
        {
            return null;
        }

        public TreeRuleItem VisitFinallyClause(AstFinallyClause a)
        {
            return null;
        }

        public TreeRuleItem VisitGrammerDecl(AstGrammarDecl a)
        {
            return null;
        }

        public TreeRuleItem VisitIdentifierList(AstIdentifierList a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitModeSpec(AstModeSpec a)
        {
            return null;
        }

        public TreeRuleItem VisitOption(AstOption a)
        {
            return null;
        }

        public TreeRuleItem VisitOptionList(AstOptionList a)
        {
            return null;
        }

        public TreeRuleItem VisitParserRuleSpec(AstParserRuleSpec a)
        {
            return null;
        }

        public TreeRuleItem VisitPrequel(AstPrequel a)
        {
            return null;
        }

        public TreeRuleItem VisitPrequelConstruct(AstPrequelConstruct a)
        {
            return null;
        }

        public TreeRuleItem VisitPrequelConstructList(AstPrequelConstructList a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitPrequelList(AstPrequelList a)
        {
            var u = a.Occurence;
            return null;
        }

        public TreeRuleItem VisitRuleAction(AstRuleAction a)
        {
            return null;
        }

        public TreeRuleItem VisitRuleModifier(AstRuleModifier a)
        {
            return null;
        }

        public TreeRuleItem VisitRuleModifierList(AstRuleModifierList a)
        {
            return null;
        }

        public TreeRuleItem AstLexerRulesList(AstLexerRulesList a)
        {
            throw new NotImplementedException();
        }

        public TreeRuleItem VisitLexerRulesList(AstLexerRulesList a)
        {

            foreach (var item in a)
            {

                //Console.WriteLine(item.ToString());
                var b = item.Accept(this);

                if (b != null)
                    _items.Add(item.Name.Text, b);

            }

            return null;

        }

        public TreeRuleItem VisitLexerAlternativeList(AstLexerAlternativeList a)
        {

            var result = new TreeRuleItem()
            {
                Origin = a
            };

            foreach (var item in a)
            {
                var i = item.Accept(this);
                if (i != null)
                    result.Add(i);
            }

            if (result.Count == 1)
                return result[0];

            return result;

        }

        public TreeRuleItem VisitLexerRule(AstLexerRule a)
        {
            return a.Value.Accept(this);
        }

        public TreeRuleItem VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            throw new NotImplementedException();
        }

        public TreeRuleItem VisitLexerElementList(AstLexerElementList a)
        {

            var result = new TreeRuleItem()
            {
                Origin = a
            };

            foreach (var item in a)
            {
                var o = item.Accept(this);
                if (o != null)
                    result.Add(o);
            }

            if (result.Count == 0)
                return null;

            if (result.Count == 1)
                return result[0];

            return result;

        }

        public TreeRuleItem VisitLexerBlock(AstLexerBlock a)
        {

            var result = new TreeRuleItem()
            {
                Origin = a
            };

            foreach (var item in a.AlternativeList)
            {
                var p = item.Accept(this);
                if (p != null)
                    result.Add(p);
            }

            if (result.Count == 1)
                return result[0];

            return result;

        }

        public TreeRuleItem VisitLexerAlternative(AstLexerAlternative a)
        {
            return a.Rule.Accept(this);
        }

        public TreeRuleItem VisitLexerElementList(AstLexerCommandList a)
        {
            throw new NotImplementedException();
        }

        public TreeRuleItem VisitLexerCommand(AstLexerCommand a)
        {
            throw new NotImplementedException();
        }

        public TreeRuleItem VisitNot(AstNot a)
        {

            var result = new TreeRuleItem("~")
            {
                Origin = a,
            };

            var r = a.Rule.Accept(this);
            if (r != null)
                result.Add(r);

            return result;

        }

        public TreeRuleItem VisitRange(AstRange a)
        {

            var result = new TreeRuleItem("..") 
            {
                IsRange = true,
                Origin = a,
            };

            result.Add(a.ValueStart.Accept(this));
            result.Add(a.ValueEnd.Accept(this));

            return result;

        }


        private Dictionary<string, TreeRuleItem> _items = new Dictionary<string, TreeRuleItem>();


    }

}
