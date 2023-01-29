using Antlr4.Runtime;
using System.Text;

namespace Bb.Asts
{

    public class AstAlternative : AstBase
    {

        public AstAlternative(ParserRuleContext ctx, AstElementList rule, AstElementOptionList options)
            : base(ctx)
        {
            this.Rule = rule;
            this.Options = options;
        }

        public int Count { get => Rule.Count; }

        public AstElementList Rule { get; }

        public AstElementOptionList Options { get; }

        public override bool ContainsOneTerminal
        {
            get
            {
                return Rule?.ContainsOneTerminal ?? false;
            }
        }

        public override bool ContainsOneRule
        {
            get
            {
                return this.Rule?.ContainsOneRule ?? false;
            }
        }
        public override bool ContainsOnlyTerminals
        {
            get
            {
                return this.Rule?.ContainsOnlyTerminals ?? false;
            }
        }

        public override bool ContainsTerminals
        {
            get
            {
                return this.Rule?.ContainsTerminals ?? false;
            }
        }

        public override bool ContainsOnlyRuleReferences
        {
            get
            {
                return this.Rule?.ContainsOnlyRuleReferences ?? false;
            }
        }
        

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAlternative(this);
        }

        public override void ToString(Writer wrt)
        {

            if (Options != null)
                Options.ToString(wrt);

            Rule.ToString(wrt);

        }
    }


}
