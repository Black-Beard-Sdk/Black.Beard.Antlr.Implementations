using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class GrammarSpec : AntlrConfigAstBase
    {


        public GrammarSpec(ParserRuleContext ctx)
            : base(ctx)
        {
            this._list = new Dictionary<string, GrammarConfigBaseDeclaration>();
            this.Defaults = new DefaultTemplateSetting(Position.Default, new TemplateSetting(Position.Default, "_"));
            this._selectors = new List<TemplateSelector>();
            this._lists = new List<ListDeclaration>();
        }


        public GrammarSpec(Position position)
            : base(position)
        {
            this._list = new Dictionary<string, GrammarConfigBaseDeclaration>();
            this.Defaults = new DefaultTemplateSetting(Position.Default, new TemplateSetting(Position.Default, "_"));
            this._selectors = new List<TemplateSelector>();
            this._lists = new List<ListDeclaration>();
        }


        public GrammarSpec Append(AstGrammarSpec ast)
        {

            foreach (AstLexerRule rule in ast.Rules.Terminals)
                this.Append(rule);

            foreach (AstRule rule in ast.Rules.Rules)
                this.Append(rule);

            return this;

        }

        private void Append(AstLexerRule rule)
        {

            if (rule.IsFragment)
                return;

            GrammarConfigTermDeclaration g = null;
            var name = rule.Name;
            if (!this._list.TryGetValue(name.Text, out GrammarConfigBaseDeclaration grammar))
            {
                this._list.Add(name.Text,
                    g = new GrammarConfigTermDeclaration(Position.Default, name,
                        new GrammarRuleTermConfig(Position.Default, TokenTypeEnum.Other, null)
                    )
                    );
            }
            else
                g = (GrammarConfigTermDeclaration)grammar;

            g.Rule = rule;
            rule.Configuration = g;

            if (g.Config.Kind != rule.TerminalKind && g.Config.Kind != TokenTypeEnum.Other)
            {
                rule.TerminalKind = g.Config.Kind;
            }

        }

        private void Append(AstRule rule)
        {

            GrammarConfigDeclaration g;
            var name = rule.Name;
            if (!this._list.TryGetValue(name.Text, out GrammarConfigBaseDeclaration grammar))
            {
                this._list.Add(name.Text,
                    g = new GrammarConfigDeclaration(Position.Default, name,
                        new GrammarRuleConfig(Position.Default, true, new IdentifierConfig(""), new TemplateSetting(Position.Default, null), new CalculatedTemplateSetting(Position.Default, new TemplateSetting(Position.Default, null)))
                    )
                    );
            }
            else
                g = (GrammarConfigDeclaration)grammar;
            
            g.Rule = rule;
            rule.Configuration = g;

        }


        protected override SerializationStrategy StrategySerialization()
        {
            return new StrategyConfiguration().GetStrategy();
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, this.ToString());
        }

        internal void Add(TemplateSelector templateSelector)
        {
            _selectors.Add(templateSelector);
        }

        public GrammarSpec Add(GrammarConfigDeclaration grammar)
        {
            this._list.Add(grammar.RuleName.Text, grammar);
            return this;
        }

        public GrammarSpec Add(GrammarConfigTermDeclaration grammar)
        {
            this._list.Add(grammar.RuleName.Text, grammar);
            return this;
        }

        public GrammarSpec AddRange(GrammarConfigDeclaration[] grammars)
        {
            foreach (GrammarConfigDeclaration grammar in grammars)
                Add(grammar);
            return this;
        }

        public GrammarSpec Remove(GrammarConfigDeclaration grammar)
        {
            this._list.Remove(grammar.RuleName.Text);
            return this;
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitGrammarSpec(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitGrammarSpec(this);
        }

        public override void ToString(Writer writer)
        {

            Defaults.ToString(writer);

            foreach (var item in _lists)
                item.ToString(writer);

            foreach (var item in _selectors)
                item.ToString(writer);

            foreach (var item in _list)
                item.Value.ToString(writer);

        }

        public ListDeclaration GetList(string listName)
        {
            return this._lists?.FirstOrDefault(c => c.ListName == listName);
        }


        public string Evaluate(AstRule rule)
        {

            var e = new EvaluateExpression(rule, this);

            foreach (var item in this._selectors)
            {
                if (item.SelectorExpression.Filter.Accept(e))
                    return item.Settings.TemplateName;
            }

            return null;

        }


        public void Add(ListDeclaration t4)
        {
            _lists.Add(t4);
        }

        internal void Prepare()
        {

            var p = new _prepare(this);

            foreach (var selector in _selectors)
                selector.Accept(p);

        }

        private readonly Dictionary<string, GrammarConfigBaseDeclaration> _list;
        private readonly List<TemplateSelector> _selectors;
        private readonly List<ListDeclaration> _lists;

        public DefaultTemplateSetting Defaults { get; internal set; }


        private class _prepare : IAstConfigBaseVisitor
        {

            public _prepare(GrammarSpec root)
            {
                this._root = root;
            }

            public void VisitAdditionalValue(AdditionalValue a)
            {
                throw new NotImplementedException();
            }

            public void VisitAdditionalValues(AdditionalValues a)
            {
                throw new NotImplementedException();
            }

            public void VisitCalculatedTemplateSetting(CalculatedTemplateSetting a)
            {
                throw new NotImplementedException();
            }

            public void VisitDefaultTemplateSetting(DefaultTemplateSetting a)
            {
                throw new NotImplementedException();
            }

            public void VisitGamarDeclaration(GrammarConfigDeclaration a)
            {
                throw new NotImplementedException();
            }

            public void VisitGrammarSpec(GrammarSpec a)
            {
                throw new NotImplementedException();
            }

            public void VisitIdentifier(IdentifierConfig a)
            {
                throw new NotImplementedException();
            }

            public void VisitListDeclaration(ListDeclaration a)
            {
                throw new NotImplementedException();
            }

            public void VisitRule(GrammarRuleConfig a)
            {
                throw new NotImplementedException();
            }

            public void VisitSelectorExpression(SelectorExpressionItem a)
            {
                throw new NotImplementedException();
            }

            public void VisitSelectorExpressionItemBinary(SelectorExpressionItemBinary a)
            {
                a.Left.Accept(this);
                a.Right.Accept(this);
            }

            public void VisitSelectorExpressionItemHas(SelectorExpressionItemHas a)
            {
            }

            public void VisitSelectorExpressionItemIs(SelectorExpressionItemIs a)
            {
            }

            public void VisitSelectorExpressionItemIsIn(SelectorExpressionItemIsIn a)
            {

                a.List = _root.GetList(a.ListName);

                if (a.List == null)
                    throw new KeyNotFoundException(a.ListName);                               

            }

            public void VisitTemplateSelector(TemplateSelector a)
            {
                a.SelectorExpression.Accept(this);
            }

            public void VisitTemplateSelectorExpression(TemplateSelectorExpression a)
            {
                a.Filter.Accept(this);
            }

            public void VisitTemplateSetting(TemplateSetting a)
            {
                throw new NotImplementedException();
            }

            public void VisitGammarTermDeclaration(GrammarConfigTermDeclaration a)
            {


            }

            public void VisitRuleTerm(GrammarRuleTermConfig a)
            {

            }

            public void VisitConstant(ConstantStringConfig a)
            {
                throw new NotImplementedException();
            }

            private readonly GrammarSpec _root;


        }


    }

}
