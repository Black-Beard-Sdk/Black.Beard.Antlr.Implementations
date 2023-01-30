using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Bb.Parsers;

namespace Bb.Asts
{


    public abstract class AstTerminalEnum<T> :  AstTerminal
        where T : Enum
    {

        public AstTerminalEnum(ParserRuleContext ctx, string value)
            : this(new Position(ctx), value)
        {

        }

        public AstTerminalEnum(ParserRuleContext ctx, T value)
            : this(new Position(ctx), value)
        {


        }

        public AstTerminalEnum(ITerminalNode n)
            : this(new Position(n.Symbol, n.Symbol), n.GetText())
        {

        }

        public AstTerminalEnum(Position position, string value) : base(position)
        {

            Value = GetValue(value);

        }

        public AstTerminalEnum(Position position, T value) : base(position)
        {

            Value = value;

        }

        protected abstract T GetValue(string value);

        public T Value { get; set; }

    }


}
