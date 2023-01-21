using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Child}")]
    public class AstPrequelConstruct : AstBase
    {

        public AstPrequelConstruct(ParserRuleContext ctx, AstBase item)
            : base(ctx)
        {
            this.Child = item;
        }

        public AstBase Child { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitPrequelConstruct(this);
        }

    }


}
