using Bb.Asts;
using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{

    public class GenerateClassTerminalToString : GenerateCodeFromAst
    {

        private GenerateClassTerminalToString(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {
        }

        public static CodeExpression Generate(AstRule ast, CodeStatementCollection code)
        {
            var strategy = GenerateCodeFromAst.GetStrategy(ast);
            var visitor = new GenerateClassTerminalToString(code, strategy);
            var result = ast.Accept(visitor);
            return result;
        }

        public override CodeExpression VisitRule(AstRule a)
        {
            using(var current = Stack(a, null))
            {
                current.Code.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());
                a.Alternatives.Accept(this);
            }
            return null;
        }

        public override CodeExpression VisitRuleAltList(AstRuleAltList a)
        {

            foreach (AstLabeledAlt item in a)
            {
                item.Accept(this);
            }

            return null;

        }

        public override CodeExpression VisitLabeledAlt(AstLabeledAlt a)
        {

            CodeExpression result = null;

            if (a.Name != null)
            {

            }

            result = a.Rule.Accept(this);

            if (a.Name != null)
            {
                Pause();
            }

            return result;

        }

        public override CodeExpression VisitAlternative(AstAlternative a)
        {

            a.Rule.Accept(this);

            if (a.Options != null)
            {
                Pause();
            }

            return null;

        }

        public override CodeExpression VisitAlternativeList(AstAlternativeList a)
        {

            foreach (AstAlternative item in a)
            {
                item.Accept(this);
            }

            return null;

        }

        public override CodeExpression VisitElementList(AstElementList a)
        {

            foreach (var item in a)
            {

                var code = item.Accept(this);
                if (code != null)
                {
                }
            }

            return null;

        }

        public override CodeExpression VisitAtom(AstAtom a)
        {

            if (a.Occurence.Value == OccurenceEnum.Any)
            {

            }
            else
            {

                if (a.Occurence.Optional)
                {

                }

                var result = a.Value.Accept(this);

            }

            return null;

        }

        public override CodeExpression VisitBlock(AstBlock a)
        {
            a.AlternativeList.Accept(this);
            return null;
        }


        public override CodeExpression VisitLabeledElement(AstLabeledElement a)
        {

            if (a.Name != null)
            {


            }

            a.Rule.Accept(this);

            return null;
        }



        public override CodeExpression VisitRuleRef(AstRuleRef a)
        {

            var current = CurrentBlock;

            var i = CodeHelper.Call("writer".Var(), "ToString", "Value".Var());
            current.Code.Add(i);

            return null;

        }

        public override CodeExpression VisitTerminal(AstTerminal a)
        {
            var current = CurrentBlock;

            if (a.Link.TerminalKind == TokenTypeEnum.Constant)
            {

                string name = a.Value.Text;
                CodeExpression value = "Constants".AsType().Field(name);

                var i = CodeHelper.Call("writer".Var(), "Append", value);
                current.Code.Add(i);

            }
            else if (a.Link.TerminalKind == TokenTypeEnum.Identifier)
            {
                //var i = CodeHelper.Call("Value".Var(), "ToString", "writer".Var());
                var i = CodeHelper.Call("writer".Var(), "Append", "Value".Var());
                current.Code.Add(i);
            }
            else
            {

            }

            return null;
        }

    }

}
