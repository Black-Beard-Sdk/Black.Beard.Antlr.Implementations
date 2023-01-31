using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Value}")]
    public abstract class AstTerminal<T> : AstRoot
    {

        public AstTerminal(ITerminalNode t, T value)
            : base(t)
        {
            this.Value = value;
        }

        public AstTerminal(ParserRuleContext ctx, T value)
            : base(ctx)
        {
            this.Value = value;
        }

        public AstTerminal(Position p, T value)
            : base(p)
        {
            this.Value = value;
        }


        public T Value { get; protected set; }


        public override string ToString()
        {
            return this.Value?.ToString() ?? string.Empty;
        }

    }




}
