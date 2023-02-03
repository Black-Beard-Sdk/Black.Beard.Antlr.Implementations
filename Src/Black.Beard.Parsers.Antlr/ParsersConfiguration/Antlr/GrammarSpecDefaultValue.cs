using Antlr4.Runtime;

namespace Bb.ParsersConfiguration.Antlr
{
    public class GrammarSpecDefaultValue : AntlrConfigAstBase
    {

        public GrammarSpecDefaultValue(ParserRuleContext ctx, IdentifierConfig key, string value)
            : base(ctx)
        {

            this.Key = key.Text;
            this._value = value;

        }

        public string Key { get; }


        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitDefaultValue(this);
        }

        internal string Get()
        {
            return _value;
        }

        private readonly string _value;

    }

}
