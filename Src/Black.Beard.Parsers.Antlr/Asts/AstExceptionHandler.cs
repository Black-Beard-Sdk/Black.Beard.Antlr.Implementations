using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstExceptionHandler : AstBase
    {

        public AstExceptionHandler(ParserRuleContext ctx, AstArgActionBlock argActionBlock, AstActionBlock actionBlock)
            : base(ctx)
        {
            this.ArgActionBlock = argActionBlock;
            this.ActionBlock = actionBlock;
        }

        public AstArgActionBlock ArgActionBlock { get; }
        public AstActionBlock ActionBlock { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitExceptionHandler(this);
        }

    }


}
