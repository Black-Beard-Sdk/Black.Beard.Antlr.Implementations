using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Diagnostics;

namespace Bb.Asts
{


    [DebuggerDisplay("{Text}")]
    public class AstTerminalText : AstTerminal
    {

        public AstTerminalText(ITerminalNode n, string text)
            : base(n)
        {
            this.Text = text;
        }

        public AstTerminalText(ParserRuleContext ctx, string text)
            : base(ctx)
        {
            this.Text = text;
        }

        public string Text { get; }

        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitTerminalText(this);
        }

    }


}
