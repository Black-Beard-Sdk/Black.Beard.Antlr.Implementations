using Antlr4.Runtime;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public abstract class GrammarConfigBaseDeclaration : AntlrConfigAstBase
    {

        public GrammarConfigBaseDeclaration(ParserRuleContext ctx, RuleTuneInherit ruleName)
            : base(ctx)
        {
            this.RuleName = ruleName;
        }

        public GrammarConfigBaseDeclaration(Position position, RuleTuneInherit ruleName)
            : base(position)
        {
            this.RuleName = ruleName;
        }

        public RuleTuneInherit RuleName { get; }


    }

}

