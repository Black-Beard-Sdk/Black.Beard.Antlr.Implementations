using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("~{Value}")]
    public class AstNot : AstBase
    {

        public AstNot(ParserRuleContext ctx, AstBase value)
            : base(ctx)
        {
            this.Rule = value;
        }

        public AstBase Rule { get; }

        public override bool IsTerminal { get => Rule.IsTerminal; }
        public override bool IsRule { get => Rule.IsRule; }

        public override bool ContainsOneRule => Rule.ContainsOneRule;
        public override bool ContainsOneTerminal => Rule.ContainsOneTerminal;

        public override bool ContainsOnlyRules
        {
            get
            {
                return Rule.ContainsOnlyRules;
            }
        }

        public override bool ContainsOnlyTerminals
        {
            get
            {
                return Rule.ContainsOnlyTerminals;
            }
        }

        public override bool ContainsTerminals
        {
            get
            {
                return Rule.ContainsTerminals;
            }
        }

        public Occurence Occurence { get; internal set; }

        public override string ResolveName()
        {
            return Rule.ResolveName();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitNot(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitNot(this);
        }


        public override AstTerminalText GetTerminal()
        {
            return Rule?.GetTerminal();
        }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            foreach (var item in Rule.GetTerminals())
                if (item != null)
                    yield return item;

        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            foreach (var item in Rule.GetRules())
                if (item != null)
                    yield return item;
        }


        public override bool ToString(Writer wrt, StrategySerializationItem strategy)
        {
            wrt.Append("~");
            wrt.ToString(Rule);
            return true;
        }

    }


}
