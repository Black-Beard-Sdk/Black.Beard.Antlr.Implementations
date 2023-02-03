using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;
using System.Data;

namespace Bb.ParsersConfiguration.Antlr
{
    public class GrammarRuleConfig : AntlrConfigAstBase
    {

        public GrammarRuleConfig(ParserRuleContext ctx, bool generate, string templateName)
            : base(ctx)
        {
            this.Generate = generate;
            this.TemplateName = templateName;
        }

        public GrammarRuleConfig(Position position, bool generate, string templateName)
            : base(position)
        {
            this.Generate = generate;
            this.TemplateName = templateName;
        }

        public bool Generate { get; set; }

        public string TemplateName { get; set; }

        public string CalculatedTemplateName { get; set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitTemplate(this);
        }

        public override void ToString(Writer writer)
        {

            if (Generate)
                writer.AppendEndLine("GENERATE");
            else
                writer.AppendEndLine("NO GENERATE");
            
            writer.Append("TEMPLATE : ");
            
            if (!string.IsNullOrEmpty(TemplateName))
                writer.Append(this.TemplateName);
            writer.AppendEndLine();

            writer.Append("CALCULATED TEMPLATE : ");
            writer.Append(CalculatedTemplateName);
            writer.AppendEndLine();

        }

    }

}
