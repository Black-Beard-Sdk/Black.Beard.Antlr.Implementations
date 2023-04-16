using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using System.Diagnostics;

namespace Bb.SqlServer.Asts
{

    public class AstTerminalIdentifier : AstBnfRule
    {

        public AstTerminalIdentifier(ITerminalNode n, params AstRoot[] items)
            : base(n)
        {

        }

        public AstTerminalIdentifier(ParserRuleContext ctx, params AstRoot[] items)
            : base(ctx)
        {

        }

        public AstTerminalIdentifier(Position t, params AstRoot[] items) 
            :base(t)
        {

        }

        public void Add(AstRoot item)
        {
            //if (item is AstTerminal<string> a)
            //    base.Add(a);

            //else if (item is AstRuleList<AstTerminal<string>> b)
            //{
            //    foreach (var item2 in b)
            //        base.Add(item2);
            //}
            //else
            //{

            //}

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
