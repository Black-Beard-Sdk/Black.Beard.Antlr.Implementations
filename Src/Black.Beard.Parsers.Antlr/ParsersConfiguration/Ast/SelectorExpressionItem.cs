using Antlr4.Runtime;

namespace Bb.ParsersConfiguration.Ast
{

    public abstract class SelectorExpressionItem : AntlrConfigAstBase
    {

        public SelectorExpressionItem(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        //public override void Accept(IAstConfigBaseVisitor visitor)
        //{
        //    visitor.VisitSelectorExpression(this);
        //}

        //public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        //{
        //    return visitor.VisitSelectorExpression(this);
        //}


    }

}

