using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.SqlServer.Asts
{
    public class AstTerminalDecimal : AstTerminal<decimal>
    {


        public AstTerminalDecimal(ITerminalNode t, string value)
            : base(t, ConvertValue(value))
        {

        }

        public AstTerminalDecimal(ITerminalNode t, decimal value)
            : base(t, value)
        {

        }



        public AstTerminalDecimal(ParserRuleContext ctx, string value)
            : base(ctx, ConvertValue(value))
        {

        }

        public AstTerminalDecimal(ParserRuleContext ctx, decimal value)
            : base(ctx, value)
        {

        }



        public AstTerminalDecimal(Position p, string value)
            : base(p, ConvertValue(value))
        {

        }

        public AstTerminalDecimal(Position p, decimal value)
            : base(p, value)
        {

        }



        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitTerminalDecimal(this);
        }


        public static implicit operator AstTerminalDecimal(decimal value)
        {
            return new AstTerminalDecimal(Position.Default, value);
        }


        private static decimal ConvertValue(string value)
        {
            return Convert.ToDecimal(value);
        }

    }

}
