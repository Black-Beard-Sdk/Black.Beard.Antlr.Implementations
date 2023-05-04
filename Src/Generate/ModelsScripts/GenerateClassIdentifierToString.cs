using Bb.Asts;
using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{


    public class GenerateClassIdentifierToString : GenerateCodeFromAst
    {

        private GenerateClassIdentifierToString(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {

        }

        public static CodeExpression Generate(AstRule ast, CodeStatementCollection code)
        {
            var strategy = GenerateCodeFromAst.GetStrategy(ast);
            var visitor = new GenerateClassIdentifierToString(code, strategy);

            var result = ast.Accept(visitor);

            return result;
        }

        public override CodeExpression VisitRule(AstRule a)
        {

            using (var current = Stack())
            {

                current.Current.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());

                current.Current.DeclareAndInitialize("strategy", "var".AsType(), "writer".Var().Property("Strategy").Call("GetStrategy", "_ruleName".Var()));

                current.Current.Using("blockStrategy", "writer".Var().Call("Apply", "strategy".Var()), j =>
                {
                    using (var current = Stack(j))
                    {
                        a.Alternatives.Accept(this);
                    }
                });



            }

            return null;

        }

        public override CodeExpression VisitRuleAltList(AstRuleAltList a)
        {

            using (var current = Stack())
            {

                foreach (AstLabeledAlt item in a)
                {
                    item.Accept(this);
                }

            }

            return null;

        }

        public override CodeExpression VisitLabeledAlt(AstLabeledAlt a)
        {

            CodeExpression result = null;

            using (var current = Stack())
            {

                if (a.Name != null)
                {

                }

                result = a.Rule.Accept(this);

                if (a.Name != null)
                {
                    Pause();
                }


            }

            return result;

        }

        public override CodeExpression VisitAlternative(AstAlternative a)
        {

            using (var current = Stack())
            {

                a.Rule.Accept(this);

                if (a.Options != null)
                {
                    Pause();
                }

            }

            return null;

        }

        public override CodeExpression VisitAlternativeList(AstAlternativeList a)
        {

            using (var current = Stack())
            {

                foreach (AstAlternative item in a)
                {
                    item.Accept(this);
                }

            }

            return null;

        }

        public override CodeExpression VisitElementList(AstElementList a)
        {

            CodeExpression result = null;

            using (var current = Stack())
            {

                foreach (var item in a)
                {

                    var code = item.Accept(this);
                    if (code != null)
                    {
                    }
                }

            }

            return result;

        }

        public override CodeExpression VisitAtom(AstAtom a)
        {

            using (var current = Stack())
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
            }

            return null;

        }

        public override CodeExpression VisitBlock(AstBlock a)
        {

            CodeExpression result = null;

            using (var current = Stack())
            {
                a.AlternativeList.Accept(this);
            }

            return result;

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

            var i = CodeHelper.Call("Value".Var(), "ToString", "writer".Var());
            current.Current.Add(i);

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
                current.Current.Add(i);
            }
            else if (a.Link.TerminalKind == TokenTypeEnum.Identifier)
            {
                var i = CodeHelper.Call("Value".Var(), "ToString", "writer".Var());
                current.Current.Add(i);
            }
            else
            {

            }

            return null;
        }


    }

}
