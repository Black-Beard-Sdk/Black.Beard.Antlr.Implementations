using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Value}")]
    public class AstAtom : AstBase
    {

        public AstAtom(ParserRuleContext ctx, AstBase value)
            : base(ctx)
        {
            this.Value = value;
        }

        public AstBase Value { get; }
        public bool Dot { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAtom(this);
        }

    }


}
