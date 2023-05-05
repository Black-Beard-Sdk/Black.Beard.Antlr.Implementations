using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{

    public class TemplateSetting : AntlrConfigAstBase
    {

        public TemplateSetting(ParserRuleContext ctx, string templateName)
            : base(ctx)
        {
            this.TemplateName = templateName;
        }

        public TemplateSetting(Position position, string templateName)
            : base(position)
        {
            this.TemplateName = templateName;
        }


        public string TemplateName { get; }

        public AdditionalValues AddtionnalSettings { get; set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitTemplateSetting(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitTemplateSetting(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            writer.Append("TEMPLATE ");
            if (!string.IsNullOrEmpty(TemplateName))
                writer.Append(TemplateName);

            if (AddtionnalSettings != null)
            {

                writer.Append(" ");

                writer.ToString(AddtionnalSettings);

            }

            return true;

        }

        internal void Merge(TemplateSetting setting)
        {
            if (this.AddtionnalSettings != null)
                this.AddtionnalSettings.Merge(setting.AddtionnalSettings);
            else
                this.AddtionnalSettings = setting.AddtionnalSettings;
        }



    }


}

