using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{
    public class SelectorExpressionItemIsIn : SelectorExpressionItem
    {

        public SelectorExpressionItemIsIn(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public bool IsNot { get; set; }

        public string ListName { get; set; }

        public ListDeclaration List { get; internal set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitSelectorExpressionItemIsIn(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitSelectorExpressionItemIsIn(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {

            if (writer.EndBy("\t"))
                writer.Append("  ");

            writer.Append("RULE IS ");
            
            if (IsNot)
                writer.Append("NOT ");

            writer.Append("IN ");

            writer.Append(ListName);

            return true;

        }

    }


}

