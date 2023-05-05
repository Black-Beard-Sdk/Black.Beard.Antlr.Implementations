using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers;
using System;

namespace Bb.ParsersConfiguration.Ast
{

    public class RuleTuneInherit : AntlrConfigAstBase
    {

        public RuleTuneInherit(ITerminalNode ctx)
            : base(ctx)
        {
            this.Text = ctx.GetText().Trim();

            if (this.Text.StartsWith(@"""") && this.Text.EndsWith(@""""))
            {
                this.Text = this.Text.Trim('"');
                this.Enquoted = true;
            }

        }

        public RuleTuneInherit(ParserRuleContext ctx)
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

        public RuleTuneInherit(string text)
            : this(Position.Default, text)
        {

        }

        public RuleTuneInherit(Position position, string text)
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

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            if (Enquoted)
            {
                writer.Append(@"""");
                writer.Append(this.Text);
                writer.Append(@"""");

            }
            else
                writer.Append(this.Text);

            return true;
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitTuneInherit(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitTuneInherit(this);
        }

    }

}
