using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class GrammarConfigTermDeclaration : GrammarConfigBaseDeclaration
    {

        public GrammarConfigTermDeclaration(ParserRuleContext ctx, RuleTuneInherit ruleName, GrammarRuleTermConfig template)
            : base(ctx, ruleName)
        {
            this.Config = template;
        }

        public GrammarConfigTermDeclaration(Position position, RuleTuneInherit ruleName, GrammarRuleTermConfig template)
            : base(position, ruleName)
        {
            this.Config = template;
        }

        public GrammarRuleTermConfig Config { get; }

        public AstLexerRule Rule 
        {
            get => _rule;
            internal set
            {
                _rule = value;

                if (_rule != null
                    && this.Config.Kind == TokenTypeEnum.Other
                    && _rule.TerminalKind == TokenTypeEnum.Constant
                    )
                {
                    this.Config.Kind = _rule.TerminalKind;
                }

            }
        }

        private AstLexerRule _rule;

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitGammarTermDeclaration(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitGammarDeclaration(this);
        }

        public override void ToString(Writer writer)
        {
            
            if (Rule != null)
            {

                writer.Append("TERM ");

                this.RuleName.ToString(writer);
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

