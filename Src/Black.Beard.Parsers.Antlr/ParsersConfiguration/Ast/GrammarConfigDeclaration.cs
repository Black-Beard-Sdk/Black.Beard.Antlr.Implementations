using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class GrammarConfigDeclaration : AntlrConfigAstBase
    {

        public GrammarConfigDeclaration(ParserRuleContext ctx, string ruleName, GrammarRuleConfig template)
            : base(ctx)
        {
            this.RuleName = ruleName;
            this.Config = template;
        }

        public GrammarConfigDeclaration(Position position, string ruleName, GrammarRuleConfig template)
            : base(position)
        {
            this.RuleName = ruleName;
            this.Config = template;
        }

        public string RuleName { get; }

        public GrammarRuleConfig Config { get; }

        public AstRule Rule { get; internal set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitGamarDeclaration(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitGamarDeclaration(this);
        }

        public override void ToString(Writer writer)
        {
            
            if (Rule != null)
            {

                writer.Append("RULE ");

                writer.Append(this.RuleName);
                writer.AppendEndLine(" ");

                writer.Append("/* ");
                Rule.ToString(writer);
                writer.TrimEnd();
                writer.AppendEndLine();
                writer.AppendEndLine("  */ ");

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
