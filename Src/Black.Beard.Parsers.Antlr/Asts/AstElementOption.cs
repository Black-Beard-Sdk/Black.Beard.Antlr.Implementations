using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Key} {Value}")]
    public class AstElementOption : AstBase
    {

        public AstElementOption(ParserRuleContext ctx, AstIdentifier key)
            : base(ctx)
        {

            this.Key = key;

        }

        public AstIdentifier Key { get; }

        public AstBase Value { get; set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElmentOption(this);
        }

    }


}
