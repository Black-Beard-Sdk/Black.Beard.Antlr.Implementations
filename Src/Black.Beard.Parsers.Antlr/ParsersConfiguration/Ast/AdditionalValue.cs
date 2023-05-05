using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{


    public class AdditionalValue : AntlrConfigAstBase
    {

        public AdditionalValue(ParserRuleContext ctx, RuleTuneInherit key, RuleTuneInherit value)
            : base(ctx)
        {
            this.Key = key.Text;
            this._value = value;
        }

        public AdditionalValue(ParserRuleContext ctx, string key, RuleTuneInherit value)
            : base(ctx)
        {

            this.Key = key;
            this._value = value;

        }

        public string Key { get; }




        internal string Get()
        {
            return _value.Text;
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            writer.Append(Key);
            writer.Append(" : ");
            writer.ToString(_value);
            return true;
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitAdditionalValue(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitAdditionalValue(this);
            
        }

        private readonly RuleTuneInherit _value;

    }

  
}

