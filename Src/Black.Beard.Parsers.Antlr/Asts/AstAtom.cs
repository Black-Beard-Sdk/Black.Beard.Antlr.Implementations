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
        public override bool IsRule { get => Value.IsRule; }

        public override bool ContainsOneRule => Value.ContainsOneRule;
        public override bool ContainsOneTerminal => Value.ContainsOneTerminal;

        public override bool ContainsOnlyRules
        {
            get
            {
                return Value.ContainsOnlyRules;
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

        public override string ResolveName()
        {
            return Value.ResolveName();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAtom(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitAtom(this);
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

        public override IEnumerable<AstRuleRef> GetRules()
        {
            foreach (var item in Value.GetRules())
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
