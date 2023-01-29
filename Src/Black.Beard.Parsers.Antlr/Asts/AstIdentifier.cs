using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Bb.Asts
{

    public class AstIdentifier : AstTerminalText
    {

        public AstIdentifier(ITerminalNode n, string type, string text) 
            : base(n, type, text)
        {
            
        }

        public AstIdentifier(ParserRuleContext ctx, string type, string text)
            : base(ctx, type, text)
        {


        }


        public AstIdentifier Child { get; private set; }

        public void Add(AstIdentifier iDentifier)
        {
            if (this.Child == null)
                this.Child = iDentifier;
            else
                this.Child.Add(iDentifier);
        }

    }


}
