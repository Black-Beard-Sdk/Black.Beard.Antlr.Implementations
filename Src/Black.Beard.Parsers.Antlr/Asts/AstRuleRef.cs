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

        public override bool IsRuleReference { get => true; }

        public override string ResolveName()
        {
            return Identifier.Text;
        }

        public override IEnumerable<AstRuleRef> GetRules()
        {
            yield return this;
        }

        public override bool ContainsOnlyRuleReferences { get => true; }

        public override bool ContainsOnlyTerminals { get => false; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRuleRef(this);
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
