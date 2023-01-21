using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
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

        public string Type { get; }

        public string Text { get; }

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
