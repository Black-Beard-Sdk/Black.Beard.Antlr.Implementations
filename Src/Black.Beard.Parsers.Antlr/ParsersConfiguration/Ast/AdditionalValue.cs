using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{


    public class AdditionalValue : AntlrConfigAstBase
    {

        public AdditionalValue(ParserRuleContext ctx, IdentifierConfig key, string value)
            : base(ctx)
        {
            this.Key = key.Text;
            this._value = value;
        }

        public AdditionalValue(ParserRuleContext ctx, string key, string value)
            : base(ctx)
        {

            this.Key = key;
            this._value = value;

        }

        public string Key { get; }




        internal string Get()
        {
            return _value;
        }

        public override void ToString(Writer writer)
        {

            writer.Append(Key);
            writer.Append(" : ");
            writer.Append(_value);

        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitAdditionalValue(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitAdditionalValue(this);
            
        }

        private readonly string _value;

    }

  
}

