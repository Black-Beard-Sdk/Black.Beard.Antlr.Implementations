using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{


    [DebuggerDisplay("{Type} {Name}")]
    public class AstGrammarDecl : AstBase
    {

        public AstGrammarDecl(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public GrammarType Type { get; internal set; }

        public AstIdentifier Name { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitGrammerDecl(this);
        }

    }


}
