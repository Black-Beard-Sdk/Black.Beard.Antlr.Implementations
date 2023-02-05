using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{
    public class TemplateSelectorExpression : SelectorExpressionItem
    {

        public TemplateSelectorExpression(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public SelectorExpressionItem Filter { get; internal set; }

        /// <summary>
        /// Accepts the specified visitor.
        /// : template_selector_expression_item ((OR|AND) template_selector_expression)?
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitTemplateSelectorExpression(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitTemplateSelectorExpression(this);
        }

        public override void ToString(Writer writer)
        {
            Filter.ToString(writer);
        }

    }

}

