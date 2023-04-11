using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Bb.Parsers;

namespace Bb.Asts.TSql
{

    public abstract class AstRoot : AstBase<IAstTSqlVisitor>
    {

        public AstRoot(ParserRuleContext ctx)
            : this(new Position(ctx))
        {

        }

        public AstRoot(ITerminalNode n)
            : this(new Position(n.Symbol, n.Symbol))
        {

        }

        public AstRoot(Position position)
            : base(position)
        {

        }


        protected static bool Eval(AstRoot item, Type type, bool optional, bool any)
        {

            if (item == null)
                return optional;

            return item.GetType() == type;

        }

    }


}
