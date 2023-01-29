using Antlr4.Runtime;
using Bb.Parsers.Antlr;
using System.Diagnostics;
using System.Text;

namespace Bb.Asts
{


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

        public bool CanBeRemoved { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRule(this);
        }

        public override bool ContainsOnlyRuleReferences
        {
            get
            {
                return this.RuleBlock?.ContainsOnlyRuleReferences ?? false;
            }
        }

        public override bool ContainsOnlyTerminals
        {
            get
            {
                return this.RuleBlock?.ContainsOnlyTerminals ?? false;
            }
        }

        public bool OutputContainsAlwayOneItem { get => RuleBlock?.OutputContainsAlwayOneItem ?? false; }
        
        public bool OutputContainsAlwayOneRule { get => RuleBlock?.OutputContainsAlwayOneRule ?? false; }

        public bool OutputContainsAlwayOneTerminal { get => RuleBlock?.OutputContainsAlwayOneTerminal ?? false; }
        
        public bool ContainsJustOneAlternative { get => RuleBlock?.ContainsJustOneAlternative ?? false; }


        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.RuleBlock.GetTerminals();
        }

        public override void ToString(Writer wrt)
        {

            var strategy = wrt.Strategy.GetStrategyFrom(this);

            RuleName.ToString(wrt);

            if (strategy.ReturnLineAfterItems)
                wrt.AppendEndLine();

            using (wrt.Indent(strategy))
            {
                wrt.Append(" : ");

                RuleBlock.ToString(wrt);
            }
        }



    }


}
