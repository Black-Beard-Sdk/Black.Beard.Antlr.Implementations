using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{

    public class SelectorExpressionItemHas : SelectorExpressionItem
    {

        public SelectorExpressionItemHas(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public FilterExpressionEnum FilterCount { get; internal set; }

        public FilterExpressionTargetEnum Target { get; internal set; }
        public bool IsOutput { get; internal set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitSelectorExpressionItemHas(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitSelectorExpressionItemHas(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {

            if (writer.EndBy("\t"))
                writer.Append("  ");

            writer.Append("RULE HAS ");

            switch (FilterCount)
            {

                case FilterExpressionEnum.One:
                    writer.Append("ONE ");
                    break;

                case FilterExpressionEnum.Only:
                    writer.Append("ONLY ");
                    break;

                case FilterExpressionEnum.Any:
                    writer.Append("ANY ");
                    break;

                case FilterExpressionEnum.Many:
                    writer.Append("MANY ");
                    break;

                case FilterExpressionEnum.No:
                    writer.Append("NONE ");
                    break;

                case FilterExpressionEnum._Undefined:
                default:
                    break;
            }

            if (IsOutput)
                writer.Append("OUTPUT ");

            switch (Target)
            {

                case FilterExpressionTargetEnum.Block:
                    writer.Append("BLOCK ");
                    break;

                case FilterExpressionTargetEnum.Rule:
                    writer.Append("RULE ");
                    break;
                case FilterExpressionTargetEnum.Term:
                    writer.Append("TERM ");
                    break;
                case FilterExpressionTargetEnum.Alternative:
                    writer.Append("ALTERNATIVE ");
                    break;

                default:
                    break;

            }

            return true;

        }

    }


}

