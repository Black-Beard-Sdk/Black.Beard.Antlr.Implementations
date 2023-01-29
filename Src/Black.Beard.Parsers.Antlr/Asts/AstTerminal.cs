using Antlr4.Runtime;
using System.Diagnostics;
using System.Text;

namespace Bb.Asts
{

    [DebuggerDisplay("{Value}")]
    public class AstTerminal : AstBase
    {

        public AstTerminal(ParserRuleContext ctx, AstIdentifier value, AstElementOptionList? options)
            : base(ctx)
        {
            this.Options = options;
            this.Value = value;
        }

        public AstElementOptionList? Options { get; }

        public AstIdentifier Value { get; }

        public override AstTerminalText GetTerminal() { return Value; }

        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            yield return Value;
        }

        public override bool ContainsOnlyRuleReferences => false; 
        public override bool ContainsOnlyTerminals => true; 
        public override bool ContainsTerminals => true;
        public override bool ContainsOneRule => false;
        public override bool ContainsOneTerminal => true;



        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitTerminal(this);
        }

        public override void ToString(Writer wrt)
        {

            if (Options != null)
            {

            }

            Value.ToString(wrt);

        }

    }


}
