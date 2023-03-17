using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{

    public class IdentifierConfig : AntlrConfigAstBase
    {

        public IdentifierConfig(ITerminalNode ctx)
            : base(ctx)
        {
            this.Text = ctx.GetText().Trim();

            //if (Text.StartsWith(':'))
            //    this.Text = this.Text.TrimStart(':').Trim();

            if (this.Text.StartsWith(@"""") && this.Text.EndsWith(@""""))
            {
                this.Text = this.Text.Trim('"');
                this.Enquoted = true;
            }
            if (Text == "ANY")
            {

            }

        }

        public IdentifierConfig(ParserRuleContext ctx)
            : base(ctx)
        {
            this.Text = ctx.GetText().Trim();

            //if (Text.StartsWith(':'))
            //    this.Text = this.Text.TrimStart(':').Trim();

            if (this.Text.StartsWith(@"""") && this.Text.EndsWith(@""""))
            {
                this.Text = this.Text.Trim('"');
                this.Enquoted = true;
            }

        }

        public IdentifierConfig(string text)
            : this(Position.Default, text)
        {

        }

        public IdentifierConfig(Position position, string text)
            : base(position)
        {
            this.Text = text;
            
            //if (Text.StartsWith(':'))
            //    this.Text = this.Text.TrimStart(':').Trim();

            if (this.Text.StartsWith(@"""") && this.Text.EndsWith(@""""))
            {
                this.Text = this.Text.Trim('"');
                this.Enquoted = true;
            }

        }


        public string Text { get; }

        public bool Enquoted { get; }

        public override void ToString(Writer writer)
        {
            if (Enquoted)
            {
                writer.Append(@"""");
                writer.Append(this.Text);
                writer.Append(@"""");

            }
            else
                writer.Append(this.Text);
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitIdentifier(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitIdentifier(this);
        }

    }

}
