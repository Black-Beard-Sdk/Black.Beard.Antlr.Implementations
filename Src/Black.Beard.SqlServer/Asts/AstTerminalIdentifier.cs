using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Diagnostics;

namespace Bb.Asts
{

    public class AstTerminalIdentifier : AstTerminal<String>
    {

        public AstTerminalIdentifier(ITerminalNode n, string text, string type)
            : base(n, text)
        {
            this.Type = type;
        }

        public AstTerminalIdentifier(ParserRuleContext ctx, string text, string type)
            : base(ctx, text)
        {        
            this.Type = type;
        }


        public AstTerminalIdentifier Child { get; private set; }

        public string Type { get; }

        public void Add(AstTerminalIdentifier identifier)
        {
            if (this.Child == null)
                this.Child = identifier;
            else
                this.Child.Add(identifier);
        }

        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIdentifier(this);
        }

    }


}
