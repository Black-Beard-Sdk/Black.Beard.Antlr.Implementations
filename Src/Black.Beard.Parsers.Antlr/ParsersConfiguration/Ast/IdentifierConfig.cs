using Antlr4.Runtime;

namespace Bb.ParsersConfiguration.Ast
{

    public class IdentifierConfig : AntlrConfigAstBase
    {

        public IdentifierConfig(ParserRuleContext ctx)
            : base(ctx)
        {
            this.Text = ctx.GetText().Trim();
            if (Text.StartsWith(':'))
                this.Text = this.Text.TrimStart(':').Trim();

        }

        public string Text { get; }

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
