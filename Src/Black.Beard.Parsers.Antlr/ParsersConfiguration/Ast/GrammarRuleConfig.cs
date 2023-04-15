using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;
using System.Data;

namespace Bb.ParsersConfiguration.Ast
{


    public class GrammarRuleConfig : AntlrConfigAstBase
    {

        public GrammarRuleConfig(ParserRuleContext ctx, bool generate, RuleTuneInherit inheritClass, TemplateSetting templateSetting, CalculatedTemplateSetting calculatedTemplateSetting, CalculatedRuleTuneInherit calculatedRuleTuneInherit)
            : base(ctx)
        {
            this.Generate = generate;

            this._inherit = inheritClass;
            this._calculatedInherit = calculatedRuleTuneInherit;

            this.TemplateSetting = templateSetting;
            this.CalculatedTemplateSetting = calculatedTemplateSetting;

        }

        public GrammarRuleConfig(Position position, bool generate, RuleTuneInherit inheritClass, TemplateSetting? templateSetting, CalculatedTemplateSetting calculatedTemplateSetting, CalculatedRuleTuneInherit calculatedRuleTuneInherit)
            : base(position)
        {
            this.Generate = generate;

            this._inherit = inheritClass;
            this._calculatedInherit = calculatedRuleTuneInherit;

            this.TemplateSetting = templateSetting;
            this.CalculatedTemplateSetting = calculatedTemplateSetting;

        }

        public bool Generate { get; private set; }

        public RuleTuneInherit Inherit
        {
            get => _inherit; 
            set 
            {
                _inherit = value; 
            }  
        }

        public CalculatedRuleTuneInherit CalculatedInherit
        {
            get => _calculatedInherit;
            set
            {
                _calculatedInherit = value;
            }
        }

        public TemplateSetting? TemplateSetting { get; }

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


            writer.Append("INHERIT ");
            if (Inherit != null)
                Inherit.ToString(writer);

            writer.AppendEndLine();
            writer.Append("CALCULATED INHERIT ");
            if (CalculatedInherit != null)
                CalculatedInherit.ToString(writer);
            

            writer.AppendEndLine();
            TemplateSetting.ToString(writer);

            writer.AppendEndLine();
            CalculatedTemplateSetting?.ToString(writer);
            
            writer.AppendEndLine();

        }

        private RuleTuneInherit _inherit;
        private CalculatedRuleTuneInherit _calculatedInherit;

    }

}
