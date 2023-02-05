using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{
    public class TemplateSelector : AntlrConfigAstBase
    {

        public TemplateSelector(ParserRuleContext ctx)
            : base(ctx)
        {

        }


        public TemplateSetting Settings { get; set; }
        
        public TemplateSelectorExpression SelectorExpression { get; set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitTemplateSelector(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitTemplateSelector(this);
        }


        /// <summary>
        /// SET TEMPLATE identifier additional_settings WHEN template_selector_expression SEMI.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void ToString(Writer writer)
        {

            writer.Append("SELECT ");

            if (this.Settings != null)
                this.Settings.ToString(writer);

            writer.TrimEnd(' ');

            writer.Append(" WHEN ");
            using (writer.Indent())
            {
                writer.AppendEndLine();
                this.SelectorExpression?.ToString(writer);
                writer.AppendEndLine();
                writer.AppendEndLine(";");
            }
            writer.AppendEndLine();

        }

    }

}

