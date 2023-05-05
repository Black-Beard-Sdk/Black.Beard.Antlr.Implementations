using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.ModelsScripts
{

    public class GenerateToStringForClassProperties : GenerateCodeFromTreeRuleItem
    {

        public GenerateToStringForClassProperties(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {


        }

        public static CodeExpression Generate(AstRule ast, AlternativeTreeRuleItem item, CodeStatementCollection code)
        {
            var strategy = GenerateCodeFromTreeRuleItem.GetStrategy(ast);
            var visitor = new GenerateToStringForClassProperties(code, strategy);

            item.Item.Accept(visitor);

            return null;

        }

    }


    public class GenerateCodeFromTreeRuleItem : TreeRuleItemVisitor
    {

        public GenerateCodeFromTreeRuleItem(CodeStatementCollection code, string strategy)
        {
            this._code = new CodeBlock(code);
            this.Strategy = strategy;
        }

        public string Strategy { get; }


        protected static string GetStrategy(AstRule ast)
        {
            var config = ast.Configuration.Config;
            var strategy = config.TemplateSetting?.TemplateName ?? config.CalculatedTemplateSetting?.Setting.TemplateName;
            return strategy;
        }


        protected LevelBloc CurrentBlock => _code.CurrentBlock;
        protected LevelBloc Stack(CodeStatementCollection c = null) => _code.Stack(c);
        private CodeBlock _code;


    }


}
