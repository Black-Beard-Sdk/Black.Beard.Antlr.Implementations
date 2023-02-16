using Antlr4.Runtime;
using Bb.Generators;
using System.Data;
using System.Diagnostics;
using System.Text;
using Ude.Core;

namespace Bb.Asts
{

    [DebuggerDisplay("{ValueStart} .. {ValueEnd}")]
    public class AstRange : AstBase
    {

        public AstRange(ParserRuleContext ctx, AstTerminalText valueStart, AstTerminalText valueEnd)
            : base(ctx)
        {
            this.ValueStart = valueStart;
            this.ValueEnd = valueEnd;
        }

        public AstTerminalText ValueStart { get; }

        public AstTerminalText ValueEnd { get; }

        public override bool ContainsOnlyTerminals
        {
            get
            {
                return true; // ValueStart.ContainsOnlyTerminals;
            }
        }

        public override bool ContainsTerminals
        {
            get
            {
                return true; //ValueStart.ContainsTerminals;
            }
        }

        public Occurence Occurence { get; internal set; }

        public override string ResolveName()
        {
            return ValueStart.ResolveName();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRange(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitRange(this);
        }


        public override AstTerminalText GetTerminal()
        {
            return ValueStart?.GetTerminal();
        }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            yield return ValueStart;
            yield return ValueEnd;
        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            yield break;
        }


        public override void ToString(Writer wrt)
        {
            ValueStart.ToString(wrt);
            wrt.Append(" .. ");
            ValueEnd.ToString(wrt);
        }

    }



    [DebuggerDisplay("{Value}")]
    public class AstAtom : AstBase
    {

        public AstAtom(ParserRuleContext ctx, AstBase value)
            : base(ctx)
        {
            this.Value = value;
            if (value == null)
            {

            }
        }

        public AstBase Value { get; }

        public override bool IsTerminal { get => Value.IsTerminal; }

        public bool IsConstant => (Value as AstTerminal)?.IsConstant ?? false;

        public bool IsKeyword => (Value as AstTerminal)?.IsKeyword ?? false;

        public override bool IsRule { get => Value.IsRule; }

        public override bool ContainsRules => Value.ContainsRules;
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

        public Occurence Occurence { get; internal set; }

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
            Value?.ToString(wrt);
            WriteOccurence(wrt, Occurence);
        }

    }


}
