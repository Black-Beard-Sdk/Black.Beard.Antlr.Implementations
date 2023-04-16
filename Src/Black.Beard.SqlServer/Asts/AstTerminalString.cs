using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.SqlServer.Asts
{


    public class AstTerminalString : AstTerminal<string>
    {

        public AstTerminalString(ITerminalNode t, string value)
            : base(t, value)
        {

        }

        public AstTerminalString(ParserRuleContext ctx, string value)
            : base(ctx, value)
        {

        }

        public AstTerminalString(Position p, string value)
            : base(p, value)
        {

        }

        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitTerminalString(this);
        }

        public static implicit operator AstTerminalString(string value)
        {
            return new AstTerminalString(Position.Default, value);
        }



    }

}
