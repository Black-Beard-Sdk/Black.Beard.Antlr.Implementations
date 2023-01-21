using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Identifier}")]
    public class AstRuleRef : AstBase
    {

        public AstRuleRef(ParserRuleContext ctx, AstIdentifier identifier)
            : base(ctx)
        {
            this.Identifier = identifier;
        }

        public AstIdentifier Identifier { get; }

        public AstBase Action { get; internal set; }
        
        public AstBase Option { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleRef(this);
        }

    }


}
