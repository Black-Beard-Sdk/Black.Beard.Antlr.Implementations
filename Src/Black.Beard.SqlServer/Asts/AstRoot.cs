using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Bb.Parsers;
using Bb.Asts;

namespace Bb.SqlServer.Asts
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


        protected static bool Eval(List<AstRoot> list, int index, Type type, bool optional, bool any)
        {

            AstRoot item = null;

            if (index < list.Count)
                item = list[index];

            if (item == null)
                return optional;

            var type2 = item.GetType();
            return type.IsAssignableFrom(type2);

        }

    }


}
