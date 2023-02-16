using Antlr4.Runtime;

namespace Bb.Asts
{
    public class AstLexerAlternative : AstBase
    {

        public AstLexerAlternative(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public int Count { get => Rule.Count; }


        public AstLexerElementList Rule { get; internal set; }

        public bool IsConstant { get => Rule.IsConstant; }
        
        public bool IsKeyword { get => Rule.IsConstant; }

        public AstLexerCommandList Commands { get; internal set; }



        public override bool ContainsRules => this.Rule?.ContainsRules ?? false;
        public override bool ContainsTerminals => this.Rule?.ContainsTerminals ?? false;
        public override bool ContainsBlocks => this.Rule?.ContainsBlocks ?? false;
        public override bool ContainsAlternatives => this.Rule?.ContainsAlternatives ?? false;



        public override bool ContainsOneRule
        {
            get
            {
                return this.Rule?.ContainsOneRule ?? false;
            }
        }
        public override bool ContainsOneTerminal
        {
            get
            {
                return Rule?.ContainsOneTerminal ?? false;
            }
        }
        public override bool ContainsOneAlternative
        {
            get
            {
                return Rule?.ContainsOneAlternative ?? false;
            }
        }
        public override bool ContainsOneBlock
        {
            get
            {
                return Rule?.ContainsOneBlock ?? false;
            }
        }



        public override bool ContainsOnlyRules => Rule.ContainsOnlyRules;
        public override bool ContainsOnlyBlocks => Rule.ContainsOnlyBlocks;
        public override bool ContainsOnlyAlternatives => Rule.ContainsOnlyAlternatives;
        public override bool ContainsOnlyTerminals => this.Rule?.ContainsOnlyTerminals ?? false;


        public override IEnumerable<AstRuleRef> GetRules()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetRules())
                        yield return t;
        }
        public override IEnumerable<AstBlock> GetBlocks()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetBlocks())
                        yield return t;
        }
        public override IEnumerable<AstTerminalText> GetTerminals()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetTerminals())
                        yield return t;
        }
        public override IEnumerable<AstAlternative> GetAlternatives()
        {
            foreach (AstBase item in this.Rule)
                if (item != null)
                    foreach (var t in item.GetAlternatives())
                        yield return t;
        }






        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLexerAlternative(this);
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLexerAlternative(this);
        }

        public override void ToString(Writer wrt)
        {
            Rule.ToString(wrt);
        }

    }


}
