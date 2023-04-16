using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.SqlServer.Asts
{


    public class AstTerminalDouble : AstTerminal<double>
    {


        public AstTerminalDouble(ITerminalNode t, string value)
            : base(t, ConvertValue(value))
        {
 
        }
               
        public AstTerminalDouble(ITerminalNode t, double value)
            : base(t, value)
        {
    
        }



        public AstTerminalDouble(ParserRuleContext ctx, string value)
            : base(ctx, ConvertValue(value))
        {

        }

        public AstTerminalDouble(ParserRuleContext ctx, double value)
            : base(ctx, value)
        {
          
        }



        public AstTerminalDouble(Position p, string value)
            : base(p, ConvertValue(value))
        {

        }

        public AstTerminalDouble(Position p, double value)
            : base(p, value)
        {

        }



        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitTerminalFloat(this);
        }


        public static implicit operator AstTerminalDouble(double value)
        {
            return new AstTerminalDouble(Position.Default, value);
        }


        private static double ConvertValue(string value)
        {
            return Convert.ToDouble(value);
        }

    }

}
