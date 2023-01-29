using Antlr4.Runtime;
using System.Text;

namespace Bb.Asts
{

    public class AstElementList : AstListBase<AstBase>
    {

        public AstElementList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {
            _charSplit = "  ";
        }

        public AstElementList(ParserRuleContext ctx)
            : base(ctx)
        {

        }


        public AstElementOptionList Options { get; set; }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElementList(this);
        }
      
        
       
    }


}
