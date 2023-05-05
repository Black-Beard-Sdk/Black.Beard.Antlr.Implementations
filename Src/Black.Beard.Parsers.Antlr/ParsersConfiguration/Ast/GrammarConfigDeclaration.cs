using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{

    public class GrammarConfigDeclaration : GrammarConfigBaseDeclaration
    {

        public GrammarConfigDeclaration(ParserRuleContext ctx, RuleTuneInherit ruleName, GrammarRuleConfig template)
            : base(ctx, ruleName)
        {
            this.Config = template;
        }

        public GrammarConfigDeclaration(Position position, RuleTuneInherit ruleName, GrammarRuleConfig template)
            : base(position, ruleName)
        {
            this.Config = template;
        }


        public GrammarRuleConfig Config { get; }

        public AstRule Rule { get; internal set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitGamarDeclaration(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitGammarDeclaration(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {

            if (Rule != null)
            {

                writer.Append("RULE ");

                writer.ToString(this.RuleName);
                writer.AppendEndLine(" ");

                writer.Append("//** ");
                writer.ToString(Rule);
                writer.TrimEnd();
                writer.AppendEndLine();
                writer.AppendEndLine("  **// ");

                using (writer.Indent())
                {
                    writer.ToString(Config);
                }

                writer.AppendEndLine(";");
                writer.AppendEndLine();

            }

            return true;

        }

    }

}

