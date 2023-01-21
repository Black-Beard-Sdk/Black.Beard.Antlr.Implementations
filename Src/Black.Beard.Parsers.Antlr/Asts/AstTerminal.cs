using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    [DebuggerDisplay("{Value}")]
    public class AstTerminal : AstBase
    {

        public AstTerminal(ParserRuleContext ctx, AstIdentifier value, AstElementOptionList? options)
            : base(ctx)
        {
            this.Value = value;
            this.Options = options;
        }

        
        public AstIdentifier Value { get; }

        public AstElementOptionList? Options { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitTerminal(this);
        }

    }


}
