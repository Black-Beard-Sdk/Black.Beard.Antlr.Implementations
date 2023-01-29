using Antlr4.Runtime;
using System.Data;
using System.Diagnostics;
using System.Text;
using Ude.Core;

namespace Bb.Asts
{


    [DebuggerDisplay("{Value}")]
    public class AstAtom : AstBase
    {

        public AstAtom(ParserRuleContext ctx, AstBase value)
            : base(ctx)
        {
            this.Value = value;
        }

        public AstBase Value { get; }

        public override bool IsTerminal { get => Value.IsTerminal; }
        public override bool IsRuleReference { get => Value.IsRuleReference; }

        public override bool ContainsOneRule => Value.ContainsOneRule;
        public override bool ContainsOneTerminal => Value.ContainsOneTerminal;

        public override bool ContainsOnlyRuleReferences
        {
            get
            {
                return Value.ContainsOnlyRuleReferences;
            }
        }

        public override bool ContainsOnlyTerminals
        {
            get
            {
                return Value.ContainsOnlyTerminals;
            }
        }

        public override bool ContainsTerminals
        {
            get
            {
                return Value.ContainsTerminals;
            }
        }

        public bool Dot { get; internal set; }

        public OccurenceEnum Occurence { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAtom(this);
        }

        public override AstTerminalText GetTerminal()
        {
            return Value?.GetTerminal();
        }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            foreach (var item in Value.GetTerminals())
                if (item != null)
                    yield return item;

        }

        public override void ToString(Writer wrt)
        {
            Value.ToString(wrt);
            WriteOccurence(wrt, Occurence);
        }

    }


}
