using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Key} = {Value}")]
    public class AstOption : AstBase
    {

        public AstOption(ParserRuleContext ctx, AstIdentifier key, AstBase value)
            : base(ctx)
        {
            this.Key = key;
            this.Value = value;
        }

        public AstIdentifier Key { get; private set; }

        public AstBase Value { get; private set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitOption(this);
        }


    }


}
