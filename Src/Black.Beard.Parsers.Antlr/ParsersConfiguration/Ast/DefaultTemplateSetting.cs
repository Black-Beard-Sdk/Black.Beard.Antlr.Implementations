using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{

    public class DefaultTemplateSetting : AntlrConfigAstBase
    {

        public DefaultTemplateSetting(ParserRuleContext ctx, TemplateSetting setting)
            : base(ctx)
        {
            this.Setting = setting;
        }

        public DefaultTemplateSetting(Position position, TemplateSetting setting)
            : base(position)
        {
            this.Setting = setting;
        }


        public TemplateSetting Setting { get; private set; }


        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitDefaultTemplateSetting(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitDefaultTemplateSetting(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            writer.Append("DEFAULT ");
            writer.ToString(Setting);
            writer.AppendEndLine();
            writer.AppendEndLine(";");
            writer.AppendEndLine();
            return true;
        }

        internal void Merge(DefaultTemplateSetting t1)
        {

            if (this.Setting != null)
                this.Setting.Merge(t1.Setting);
            else
                this.Setting = t1.Setting;

        }

    }


}

