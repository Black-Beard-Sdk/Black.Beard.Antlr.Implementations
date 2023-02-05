using Antlr4.Runtime;
using Bb.Configurations;
using Bb.Parsers.Antlr;
using Bb.ParsersConfiguration.Ast;
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
            this._ruleName = ruleName;
            this.Rule = rule;
            this.Return = returnRule;
            this.ThrowsSpec = throwsSpec;
            this.Local = localSpec;
            this.Prequels = prequels;
            this.Alternatives = ruleBlock;
            this.ExceptionGroup = exceptionGroup;
        }

        public AstRuleModifierList Modifiers { get; }

        private readonly AstIdentifier _ruleName;
        public AstIdentifier RuleName => _ruleName;

        public string Name => _ruleName.Text; 

        public AstRuleAltList Alternatives { get; }

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



        public override bool ContainsOnlyRules
        {
            get
            {
                return this.Alternatives?.ContainsOnlyRules ?? false;
            }
        }
        public override bool ContainsOnlyTerminals
        {
            get
            {
                return this.Alternatives?.ContainsOnlyTerminals ?? false;
            }
        }
        public override bool ContainsOnlyBlocks
        {
            get
            {
                return this.Alternatives?.ContainsOnlyBlocks ?? false;
            }
        }
        public override bool ContainsOnlyAlternatives
        {
            get
            {
                return true;
            }
        }




        public override bool ContainsOneRule
        {
            get
            {
                return this.Alternatives?.ContainsOneRule ?? false;
            }
        }
        public override bool ContainsOneTerminal
        {
            get
            {
                return this.Alternatives?.ContainsOneTerminal ?? false;
            }
        }
        public override bool ContainsOneBlock
        {
            get
            {
                return this.Alternatives?.ContainsOneBlock ?? false;
            }
        }
        public override bool ContainsOneAlternative
        {
            get
            {
                return true;
            }
        }




        public override bool ContainsTerminals
        {
            get
            {
                return this.Alternatives?.ContainsTerminals ?? false;
            }
        }
        public override bool ContainsRules
        {
            get
            {
                return this.Alternatives?.ContainsRules ?? false;
            }
        }
        public override bool ContainsBlocks
        {
            get
            {
                return this.Alternatives?.ContainsBlocks ?? false;
            }
        }
        public override bool ContainsAlternatives
        {
            get
            {
                return this.Alternatives?.ContainsAlternatives ?? false;
            }
        }



     


        public bool OutputContainsAlwayOneItem { get => Alternatives?.OutputContainsAlwayOneItem ?? false; }
        
        public bool OutputContainsAlwayOneRule { get => Alternatives?.OutputContainsAlwayOneRule ?? false; }

        public bool OutputContainsAlwayOneTerminal { get => Alternatives?.OutputContainsAlwayOneTerminal ?? false; }
        
        public bool ContainsJustOneAlternative { get => Alternatives?.ContainsJustOneAlternative ?? false; }



        public string Strategy { get; set; }

        public GrammarConfigDeclaration Configuration { get; internal set; }


        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            return this.Alternatives.GetTerminals();
        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            return this.Alternatives.GetRules();
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            return this.Alternatives.GetBlocks();
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            return this.Alternatives.GetAlternatives();
        }




        public IEnumerable<IEnumerable<AstBase>> GetListAlternatives()
        {
            return this.Alternatives.GetListAlternatives();
        }

        public override void ToString(Writer wrt)
        {

            var strategy = wrt.Strategy.GetStrategyFrom(this);

            _ruleName.ToString(wrt);

            if (strategy.ReturnLineAfterItems)
                wrt.AppendEndLine();

            using (wrt.Indent(strategy))
            {
                wrt.Append(" : ");

                Alternatives.ToString(wrt);
            }
        }



    }


}
