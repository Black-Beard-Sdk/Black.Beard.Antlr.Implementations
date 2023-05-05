using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{

    public class SelectorExpressionItemBinary : SelectorExpressionItem
    {

        public SelectorExpressionItemBinary(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public SelectorExpressionItem Left { get; internal set; }

        public SelectorExpressionOperationEnum Operator { get; internal set; }

        public SelectorExpressionItem Right { get; internal set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitSelectorExpressionItemBinary(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitSelectorExpressionItemBinary(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {

            writer.ToString(Left);
            writer.AppendEndLine();

            switch (Operator)
            {
                case SelectorExpressionOperationEnum.And:
                    writer.Append("& ");
                    break;
                case SelectorExpressionOperationEnum.Or:
                    writer.Append("| ");
                    break;
                default:
                    break;
            }

            writer.ToString(Right);

            return true;

        }

    }

}

