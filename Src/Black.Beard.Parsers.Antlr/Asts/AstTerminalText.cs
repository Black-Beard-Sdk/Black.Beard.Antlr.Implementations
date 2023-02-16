using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Generators;
using Bb.Parsers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Text}")]
    public class AstTerminalText : AstBase
    {

        public static AstTerminalText EmptyRule(ParserRuleContext ctx) => new AstTerminalText(ctx, "Empty", string.Empty);
        public static AstTerminalText EmptyRule() => new AstTerminalText(Position.Default, "Empty", string.Empty);

        public AstTerminalText(ITerminalNode n, string type, string text)
            : base(n)
        {

            this.Type = type;
            this.Text = text;

            Evaluate();

        }

        public AstTerminalText(ParserRuleContext ctx, string type, string text)
            : base(ctx)
        {
            Evaluate();
        }

        public AstTerminalText(Position position, string type, string text)
            : base(position)
        {
            this.Type = type;
            this.Text = text;
            Evaluate();
        }

        public override void ToString(Writer wrt)
        {
            wrt.Append(this.Text?.Trim());
        }

        public override string ResolveName()
        {
            return Text;
        }

        public bool IsLetter()
        {
            var o = this.Text.Trim('\'');
            for (int i = 0; i < o.Length; i++)
            {
                var c = o[i];
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public override string Type { get; }

        public string Text { get; }
        public bool IsConstant { get; protected set; }
        public bool IsKeyword { get; protected set; }

        public override bool ContainsOnlyRules => false;
        public override bool ContainsOneRule => false;
        public override bool ContainsOneTerminal => true;

        public override bool ContainsTerminals => true;

        public override AstTerminalText GetTerminal()
        {
            return this;
        }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            yield return this;
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitTerminalText(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitTerminalText(this);
        }


        private void Evaluate()
        {
            if (!string.IsNullOrEmpty(Text))
            {

                this.IsConstant
                    = this.Text.StartsWith("'")
                    && this.Text.EndsWith("'");

                this.IsKeyword
                    = this.IsConstant
                    && this.IsLetter();

                if (!this.IsConstant)
                    this.IsConstant = int.TryParse(this.Text, out var i);

                else if (!this.IsConstant)
                    this.IsConstant = decimal.TryParse(this.Text, out var i);
            }
        }

    }


}

