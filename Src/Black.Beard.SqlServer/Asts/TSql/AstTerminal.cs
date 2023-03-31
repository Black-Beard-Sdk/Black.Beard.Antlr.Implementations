using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using System.Diagnostics;

namespace Bb.Asts.TSql
{

    [DebuggerDisplay("{Value}")]
    public abstract class AstTerminal<T> : AstRoot
    {

        public AstTerminal(ITerminalNode t, T value)
            : base(t)
        {
            Value = value;
        }

        public AstTerminal(ParserRuleContext ctx, T value)
            : base(ctx)
        {
            Value = value;
        }

        public AstTerminal(Position p, T value)
            : base(p)
        {
            Value = value;
        }


        public T Value { get; protected set; }


        public override string ToString()
        {
            return Value?.ToString() ?? string.Empty;
        }

    }


}
