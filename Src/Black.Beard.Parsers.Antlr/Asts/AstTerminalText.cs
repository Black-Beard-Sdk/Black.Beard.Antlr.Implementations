using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Generators;
using Bb.Parsers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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

        public override bool ToString(Writer wrt, StrategySerializationItem strategy)
        {

            if (Enquoted)
                wrt.Append("'");

            wrt.Append(this.Text?.Trim());

            if (Enquoted)
                wrt.Append("'");

            return true;

        }

        public override string ResolveName()
        {
            return Text;
        }

        public bool Enquoted { get; private set; }

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

        public string Text { get; private set; }

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


        private static bool EvaluateIsDynamic(string text)
        {
            string pattern = @"\w-|-\w";
            string input = @"";
            RegexOptions options = RegexOptions.IgnoreCase;

            return Regex.IsMatch(text, pattern, options);

        }

        private void Evaluate()
        {
            if (!string.IsNullOrEmpty(Text))
            {

                if (Text.StartsWith("[") && Text.EndsWith("]") && Text.Length > 1)
                {

                    var txt = this.Text.Substring(1, this.Text.Length - 2).ToLower();
                    
                    if (EvaluateIsDynamic(txt))
                        this.TerminalKind = TokenTypeEnum.Identifier;

                }

                else if (Text.StartsWith("'") && Text.EndsWith("'") && Text.Length > 1)
                {
                    this.Text = this.Text.Substring(1, this.Text.Length - 2);
                    this.Enquoted = true;

                    if (IsLetters(this.Text))
                        this.TerminalKind = TokenTypeEnum.Constant;
                    else
                    {


                    }

                }
                
                else
                {

                    if (int.TryParse(this.Text, out var i))
                        this.TerminalKind = TokenTypeEnum.Int;

                    else if (decimal.TryParse(this.Text, out var j))
                        this.TerminalKind = TokenTypeEnum.Real;

                }

            }
        }

        private bool IsLetters(string txt)
        {

            foreach (var item in txt)
                if (!char.IsLetter(item) && item != '_')
                    return false;

            return true;

        }

    }


}

