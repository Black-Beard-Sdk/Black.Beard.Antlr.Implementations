using Antlr4.Runtime;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public abstract class GrammarConfigBaseDeclaration : AntlrConfigAstBase
    {

        public GrammarConfigBaseDeclaration(ParserRuleContext ctx, IdentifierConfig ruleName)
            : base(ctx)
        {
            this.RuleName = ruleName;
        }

        public GrammarConfigBaseDeclaration(Position position, IdentifierConfig ruleName)
            : base(position)
        {
            this.RuleName = ruleName;
        }

        public IdentifierConfig RuleName { get; }


    }

}

