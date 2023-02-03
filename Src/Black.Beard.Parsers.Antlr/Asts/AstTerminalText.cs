using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Text}")]
    public class AstTerminalText : AstBase
    {

        public AstTerminalText(ITerminalNode n, string type, string text) 
            : base(n)
        {
            this.Type = type;
            this.Text = text;
        }

        public AstTerminalText(ParserRuleContext ctx, string type, string text) 
            : base(ctx)
        {
            this.Type = type;
            this.Text = text;
        }

        public override void ToString(Writer wrt)
        {
            wrt.Append(this.Text.Trim());
        }

        public string Type { get; }

        public string Text { get; }

        public override bool ContainsOnlyRules  => false; 
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

    }


}



namespace Bb
{

}
