using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;
using System.Data;

namespace Bb.ParsersConfiguration.Ast
{
    public class GrammarRuleConfig : AntlrConfigAstBase
    {

        public GrammarRuleConfig(ParserRuleContext ctx, bool generate, TemplateSetting templateSetting, CalculatedTemplateSetting calculatedTemplateSetting)
            : base(ctx)
        {
            this.Generate = generate;
            this.TemplateSetting = templateSetting;
            this.CalculatedTemplateSetting = calculatedTemplateSetting;
        }

        public GrammarRuleConfig(Position position, bool generate, TemplateSetting templateSetting, CalculatedTemplateSetting calculatedTemplateSetting)
            : base(position)
        {
            this.Generate = generate;
            this.TemplateSetting = templateSetting;
            this.CalculatedTemplateSetting = calculatedTemplateSetting;
        }


        public bool Generate { get; set; }

        public TemplateSetting TemplateSetting { get; }

        public CalculatedTemplateSetting CalculatedTemplateSetting { get; set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitRule(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitRule(this);
        }

        public override void ToString(Writer writer)
        {

            if (Generate)
                writer.AppendEndLine("GENERATE");
            else
                writer.AppendEndLine("NO GENERATE");
            
            TemplateSetting.ToString(writer);

            writer.AppendEndLine();
            CalculatedTemplateSetting?.ToString(writer);
            writer.AppendEndLine();

        }

    }

}
