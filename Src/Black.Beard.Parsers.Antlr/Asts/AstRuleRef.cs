using Antlr4.Runtime;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace Bb.Asts
{

    [DebuggerDisplay("{Identifier}")]
    public class AstRuleRef : AstBase
    {

        public AstRuleRef(ParserRuleContext ctx, AstIdentifier identifier)
            : base(ctx)
        {
            this.Identifier = identifier;
        }

        public AstIdentifier Identifier { get; }

        public AstBase Action { get; internal set; }

        public AstBase Option { get; internal set; }

        public override bool IsRule { get => true; }

        public override string ResolveName()
        {
            return Identifier.Text;
        }

        public override Occurence ResolveOccurence()
        {
            if ( this.Parent is AstAtom a)
                return a.Occurence;
            return new Occurence();
        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            yield return this;
        }



        public override bool ContainsOnlyRules { get => true; }
        public override bool ContainsOnlyTerminals { get => false; }
        public override bool ContainsOnlyBlocks { get => false; }
        public override bool ContainsOnlyAlternatives { get => false; }



        public override bool ContainsOneRule { get => true; }
        public override bool ContainsOneTerminal { get => false; }
        public override bool ContainsOneBlock { get => false; }
        public override bool ContainsOneAlternative { get => false; }



        public override bool ContainsRules { get => true; }
        public override bool ContainsTerminals { get => false; }
        public override bool ContainsBlocks { get => false; }
        public override bool ContainsAlternatives { get => false; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleRef(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitRuleRef(this);
        }

        public override void ToString(Writer wrt)
        {

            if (Action != null)
                Identifier.ToString(wrt);

            if (Option != null)
                Option.ToString(wrt);

            Identifier.ToString(wrt);

        }

    }


}
