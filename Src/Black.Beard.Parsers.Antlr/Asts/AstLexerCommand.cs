using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("{Name} = {Value}")]
    public class AstLexerCommand : AstBase
    {

        public AstLexerCommand(ParserRuleContext ctx, string name, string value)
            : base(ctx)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override void ToString(Writer writer)
        {
            
            writer.Append(" -> ");
            writer.Append(Name);

            if (Value != null)
            {
                writer.Append("(");
                writer.Append(Value);
                writer.Append(")");

            }

        }



        //public override bool ContainsTerminals { get => this.Value?.ContainsTerminals ?? false; }
        //public override bool ContainsRules { get => this.Value?.ContainsRules ?? false; }
        //public override bool ContainsBlocks { get => this.Value?.ContainsBlocks ?? false; }
        //public override bool ContainsAlternatives { get => this.Value?.ContainsAlternatives ?? false; }



        //public override bool ContainsOneTerminal { get => this.Value?.ContainsOneTerminal ?? false; }
        //public override bool ContainsOneRule { get => this.Value?.ContainsOneRule ?? false; }
        //public override bool ContainsOneBlock { get => this.Value?.ContainsOneBlock ?? false; }
        //public override bool ContainsOneAlternative { get => this.Value?.ContainsOneAlternative ?? false; }



        //public override bool ContainsOnlyTerminals { get => this.Value?.ContainsOnlyTerminals ?? false; }
        //public override bool ContainsOnlyRules { get => this.Value?.ContainsOnlyRules ?? false; }
        //public override bool ContainsOnlyBlocks { get => this.Value?.ContainsOnlyBlocks ?? false; }
        //public override bool ContainsOnlyAlternatives { get => this.Value?.ContainsOnlyAlternatives ?? false; }





        //public override IEnumerable<AstRuleRef> GetRules()
        //{
        //    return this.Value?.GetRules();
        //}
        //public override IEnumerable<AstBlock> GetBlocks()
        //{
        //    return this.Value?.GetBlocks();
        //}
        //public override IEnumerable<AstTerminalText> GetTerminals()
        //{
        //    return this.Value?.GetTerminals();
        //}
        //public override IEnumerable<AstAlternative> GetAlternatives()
        //{
        //    return this.Value?.GetAlternatives();
        //}

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerCommand(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerCommand(this);
        }





    }

}
