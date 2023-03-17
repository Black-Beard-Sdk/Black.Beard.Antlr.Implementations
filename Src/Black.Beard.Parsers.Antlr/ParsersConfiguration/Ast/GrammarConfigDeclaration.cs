using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{

    public class GrammarConfigDeclaration : GrammarConfigBaseDeclaration
    {

        public GrammarConfigDeclaration(ParserRuleContext ctx, IdentifierConfig ruleName, GrammarRuleConfig template)
            : base(ctx, ruleName)
        {
            this.Config = template;
        }

        public GrammarConfigDeclaration(Position position, IdentifierConfig ruleName, GrammarRuleConfig template)
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

        public override void ToString(Writer writer)
        {

            if (Rule != null)
            {

                writer.Append("RULE ");

                writer.Append(this.RuleName);
                writer.AppendEndLine(" ");

                writer.Append("//** ");
                Rule.ToString(writer);
                writer.TrimEnd();
                writer.AppendEndLine();
                writer.AppendEndLine("  **// ");

                using (writer.Indent())
                {
                    Config.ToString(writer);
                }

                writer.AppendEndLine(";");
                writer.AppendEndLine();

            }

        }

    }

}

