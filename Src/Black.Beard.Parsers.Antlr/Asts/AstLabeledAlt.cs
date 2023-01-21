using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("{Rule} = {Identifier}")]
    public class AstLabeledAlt : AstBase
    {

        public AstLabeledAlt(ParserRuleContext ctx, AstAlternative rule, AstIdentifier? identifier = null)
            : base(ctx)
        {
            this.Rule = rule;
            this.Identifier = identifier;
        }

        public AstAlternative Rule { get; }
        
        public AstIdentifier Identifier { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLabeledAlt(this);
        }

    }


}
