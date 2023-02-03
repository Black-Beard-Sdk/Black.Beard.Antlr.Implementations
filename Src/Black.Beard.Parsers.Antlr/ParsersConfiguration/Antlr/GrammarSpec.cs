using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Antlr
{
    public class GrammarSpec : AntlrConfigAstBase
    {


        public GrammarSpec(ParserRuleContext ctx)
            : base(ctx)
        {
            this._list = new Dictionary<string, GrammarConfigDeclaration>();
            this.Defaults = new GrammarSpecDefaultValues(Position.Default);
        }

        public GrammarSpec(Position position)
            : base(position)
        {
            this._list = new Dictionary<string, GrammarConfigDeclaration>();
            this.Defaults = new GrammarSpecDefaultValues(Position.Default);
        }


        public GrammarSpec Append(AstGrammarSpec ast)
        {

            foreach (AstRule rule in ast.Rules)
                this.Append(rule);

            return this;

        }

        private void Append(AstRule rule)
        {

            var name = rule.RuleName.Text;
            if (!this._list.TryGetValue(name, out GrammarConfigDeclaration grammar))
                this._list.Add(name,
                    grammar = new GrammarConfigDeclaration(Position.Default, name,
                        new GrammarRuleConfig(Position.Default, true, this.Defaults.Template))
                    );

            grammar.Rule = rule;
            rule.Configuration = grammar;

        }


        protected override SerializationStrategy StrategySerialization()
        {
            return new StrategyConfiguration().GetStrategy();
        }

        public void Save(string filename)
        {

            if (File.Exists(filename))
                File.Delete(filename);

            File.AppendAllText(filename, this.ToString());

        }


        public GrammarSpec Add(GrammarConfigDeclaration grammar)
        {
            this._list.Add(grammar.RuleName, grammar);
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
            this._list.Remove(grammar.RuleName);
            return this;
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitGrammarSpec(this);
        }

        public override void ToString(Writer writer)
        {

            Defaults.ToString(writer);

            foreach (var item in _list)
                item.Value.ToString(writer);

        }


        private readonly Dictionary<string, GrammarConfigDeclaration> _list;

        public GrammarSpecDefaultValues Defaults { get; internal set; }

    }

}
