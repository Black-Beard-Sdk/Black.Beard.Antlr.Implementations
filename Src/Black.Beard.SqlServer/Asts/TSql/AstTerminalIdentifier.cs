using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using System.Diagnostics;

namespace Bb.Asts.TSql
{

    public class AstTerminalIdentifier : AstRuleList<AstTerminal<string>>
    {

        public AstTerminalIdentifier(ITerminalNode n)
            : base(n)
        {

        }

        public AstTerminalIdentifier(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstTerminalIdentifier(Position t) :
                base(t)
        {

        }

        public void Add(AstRoot item)
        {
            if (item is AstTerminal<string> a)
                base.Add(a);

            else if (item is AstRuleList<AstTerminal<string>> b)
            {
                foreach (var item2 in b)
                    base.Add(item2);
            }
            else
            {

            }

        }

        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIdentifier(this);
        }

        private List<AstTerminal<string>> _list = new List<AstTerminal<string>>();

    }


}
