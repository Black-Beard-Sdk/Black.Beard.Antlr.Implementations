using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class CalculatedRuleTuneInherit : AntlrConfigAstBase
    {
            
        public CalculatedRuleTuneInherit(ParserRuleContext ctx, RuleTuneInherit ruleTuneInherit)
            : base(ctx)
        {
         
            RuleTuneInherit = ruleTuneInherit;

        }

        public CalculatedRuleTuneInherit(string ruleTuneInherit)
            : this(new RuleTuneInherit(ruleTuneInherit))
        {

        }


        public CalculatedRuleTuneInherit(RuleTuneInherit ruleTuneInherit)
            : base(Position.Default)
        {
            RuleTuneInherit = ruleTuneInherit;
        }


        public RuleTuneInherit RuleTuneInherit { get; }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            return writer.ToString(RuleTuneInherit);
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitCalculatedTuneInherit(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitCalculatedTuneInherit(this);
        }

    }

}
