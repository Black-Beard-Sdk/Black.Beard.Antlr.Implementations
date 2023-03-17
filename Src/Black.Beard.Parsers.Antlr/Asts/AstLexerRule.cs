using Antlr4.Runtime;
using Bb.ParsersConfiguration.Ast;

namespace Bb.Asts
{
    public class AstLexerRule : AstRuleBase
    {


        public AstLexerRule(ParserRuleContext ctx
            , AstTerminalText ruleName
            , AstLexerAlternativeList value
            ) : base(ctx, new IdentifierConfig(ruleName.ToString()))
        {

            this.Value = value;

            if (value.Count == 1)
            {

                var v = value[0];

                if (v.Count == 1)
                {

                    var tyxt = v.Rule.ToString();

                    if (tyxt.StartsWith("'") == tyxt.EndsWith("'"))
                        this.TerminalKind = TokenTypeEnum.Constant;

                }

            }

        }

        public AstLexerAlternativeList Value { get; }


        public AstRuleAltList Alternatives { get; }

        public bool CanBeRemoved { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerRule(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerRule(this);
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

        public GrammarConfigTermDeclaration Configuration { get; internal set; }

        public int Index { get; internal set; }
        public AstOptionList? Options { get; internal set; }
        public bool IsFragment { get; internal set; }


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

            if (IsFragment)
                wrt.Append("fragment ");

            wrt.Append(Name);

            if (strategy.ReturnLineAfterItems)
                wrt.AppendEndLine();

            using (wrt.Indent(strategy))
            {
                wrt.Append(" : ");
                if (Alternatives != null)
                    Alternatives.ToString(wrt);
                else
                {
                    Value.ToString(wrt);
                }
            }

        }



    }


}
