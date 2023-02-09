using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{


    [DebuggerDisplay("{Declaration}")]
    public class AstGrammarSpec : AstBase
    {

        public AstGrammarSpec(ParserRuleContext ctx, AstGrammarDecl declaration)
            : base(ctx)
        {
            this.Declaration = declaration;
        }

        public AstGrammarDecl Declaration { get; internal set; }
        
        public AstPrequelConstructList Prequels { get; internal set; }
        
        public AstRulesList Rules { get; internal set; }
        public AstModeSpecList Modes { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitGrammarSpec(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitGrammarSpec(this);
        }


    }


}
