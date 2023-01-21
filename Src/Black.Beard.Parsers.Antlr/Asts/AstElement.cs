using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Child}")]

    public class AstElement : AstBase
    {

        public AstElement(ParserRuleContext ctx, AstBase child)
            : base(ctx)
        {
            this.Child = child;
        }

        public AstBase Child { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElement(this);
        }

    }


}
