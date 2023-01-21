using Antlr4.Runtime;
using Bb.Parsers.Antlr;
using System.Diagnostics;

namespace Bb.Asts
{


    [DebuggerDisplay("{RuleName}")]
    public class AstRule : AstBase
    {

        public AstRule(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstRule(ParserRuleContext ctx
            , AstRuleModifierList? modifiers
            , AstIdentifier ruleName
            , AstArgActionBlock? rule
            , AstArgActionBlock? returnRule
            , AstIdentifierList? throwsSpec
            , AstArgActionBlock? localSpec
            , AstPrequelList? prequels
            , AstRuleAltList? ruleBlock
            , AstExceptionGroup? exceptionGroup
            ) : this(ctx)
        {
            this.Modifiers = modifiers;
            this.RuleName = ruleName;
            this.Rule = rule;
            this.Return = returnRule;
            this.ThrowsSpec = throwsSpec;
            this.Local = localSpec;
            this.Prequels = prequels;
            this.RuleBlock = ruleBlock;
            this.ExceptionGroup = exceptionGroup;
        }

        public AstRuleModifierList Modifiers { get; }

        public AstIdentifier RuleName { get; }
        public AstRuleAltList RuleBlock { get; }

        public AstArgActionBlock Rule { get; }
        public AstArgActionBlock Return { get; }
        public AstIdentifierList ThrowsSpec { get; }
        public AstArgActionBlock Local { get; }
        public AstPrequelList Prequels { get; }
        public AstExceptionGroup ExceptionGroup { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRule(this);
        }


    }


}
