using Antlr4.Runtime;

namespace Bb.Asts
{

    public class AstBlock : AstBase
    {

        public AstBlock(ParserRuleContext ctx, AstOptionList? optionList, AstRuleAction? ruleAction, AstAlternativeList? alternativeList)
            : base(ctx)
        {
            OpptionList = optionList;
            RuleAction = ruleAction;
            AlternativeList = alternativeList;
        }

        public AstOptionList? OpptionList { get; }

        public AstRuleAction? RuleAction { get; }

        public AstAlternativeList? AlternativeList { get; }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitBlock(this);
        }

    }


}
