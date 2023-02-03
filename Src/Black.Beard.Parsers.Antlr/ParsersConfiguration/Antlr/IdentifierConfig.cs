using Antlr4.Runtime;

namespace Bb.ParsersConfiguration.Antlr
{
    public class IdentifierConfig : AntlrConfigAstBase
    {

        public IdentifierConfig(ParserRuleContext ctx)
            : base(ctx)
        {
            this.Text = ctx.GetText();
        }

        public string Text { get; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitIdentifier(this);
        }

    }

}
