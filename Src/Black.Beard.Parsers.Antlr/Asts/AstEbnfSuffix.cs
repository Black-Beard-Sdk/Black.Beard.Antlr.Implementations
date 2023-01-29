using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("{Value}")]
    public class AstEbnfSuffix : AstBase
    {

        public AstEbnfSuffix(ParserRuleContext ctx)
            : base(ctx)
        {
            
        }

        public OccurenceEnum Occurence { get; set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAstEbnfSuffix(this);
        }

    }


}
