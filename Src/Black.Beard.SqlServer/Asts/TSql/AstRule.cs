using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.Asts.TSql
{


    public abstract class AstRule : AstRoot
    {

        public AstRule(ITerminalNode t/*, List<AstRoot> list*/)
            : base(t)
        {
            //_list = new List<AstRoot>();
            //if (list != null)
            //    _list.AddRange(list);
        }

        public AstRule(ParserRuleContext ctx/*, List<AstRoot> list*/)
            : base(ctx)
        {
            //_list = new List<AstRoot>();
            //if (list != null)
            //    _list.AddRange(list);
        }

        public AstRule(Position position/*, List<AstRoot> list*/)
            : base(position)
        {
            //_list = new List<AstRoot>();
            //if (list != null)
            //    _list.AddRange(list);
        }

        //protected List<AstRoot> _list;

    }


}
