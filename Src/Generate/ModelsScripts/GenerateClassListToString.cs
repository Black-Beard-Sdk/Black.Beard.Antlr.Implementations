using Bb.Asts;
using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{

    public class GenerateClassListToString : GenerateCodeFromAst
    {

        private GenerateClassListToString(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {

        }

        public static CodeExpression Generate(AstRule ast, CodeStatementCollection code)
        {
            var strategy = GenerateCodeFromAst.GetStrategy(ast);
            var visitor = new GenerateClassListToString(code, strategy);

            var result = ast.Accept(visitor);

            return result;
        }

        public override CodeExpression VisitRule(AstRule a)
        {

            using (var current = Stack(a, null))
            {
                current.Code.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());
                current.Code.If(CodeHelper.This().Property("Count").GreaterThan((0).AsConstant()),
                i =>
                {
                    using (var current = Stack(a, i))
                    {
                        current.Code.DeclareAndInitialize("index", typeof(int).AsType(), (0).AsConstant());
                        a.Alternatives.Accept(this);
                    }
                });
                current.Code.Return(CodeHelper.AsConstant(true));

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

            using (var current = Stack(a, null))
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

            using (var current = Stack(a, null))
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

            using (var current = Stack(a, null))
            {

                if (a.Occurence.Value == OccurenceEnum.Any)
                {

                    var item = CodeHelper.This().Indexer("index".Var());
                    current.SetData("source", item);

                    if (a.Value.Type == "AstTerminal")
                    {


                        if (!a.Occurence.Optional)
                        {
                            a.Value.Accept(this);
                        }
                        if (a.Value.TerminalKind == TokenTypeEnum.Ponctuation)
                        {
                            current.Code.If("strategy".Var().Property("ReturnLineAfterItems"), t =>
                            {
                                using (var current = Stack(a, t))
                                {
                                    current.Code.Add("writer".Var().Call("AppendEndLine"));
                                }
                            });
                        }

                    }
                    else
                    {

                        a.Value.Accept(this);

                        var left = "index".Var();
                        var right = CodeHelper.This().Property("Count");

                        var m = CodeHelper.For("index".Var().Assign((1).AsConstant()), left.LessThan(right), "index".PostIncrement().AsStatement(), j =>
                        {
                            using (var current = Stack(a, j))
                            {
                                a.Value.Accept(this);
                            }
                        });
                        current.Code.Add(m);
                    }

                }
                else
                {

                    var item = CodeHelper.This().Indexer("index".Var());
                    current.SetData("source", item);

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

            var value = 0;
            value = 1;

            CodeExpression result = null;

            using (var current = Stack(a, null))
            {
                current.Code.If(CodeHelper.This().Property("Count").GreaterThan((value).AsConstant()),
                i =>
                {
                    using (var current = Stack(a, i))
                    {

                        if (a.Occurence.Value == OccurenceEnum.Any)
                        {

                            var left = "index".Var();
                            var right = CodeHelper.This().Property("Count");

                            var m = CodeHelper.For("index".Var().Assign((1).AsConstant()), left.LessThan(right), "index".PostIncrement().AsStatement(),
                            j =>
                            {

                                using (var current = Stack(a, j))
                                {
                                    var item = CodeHelper.This().Indexer("index".Var());
                                    current.SetData("source", item);
                                    a.AlternativeList.Accept(this);
                                }
                            });
                            current.Code.Add(m);

                        }

                        else
                        {
                            a.AlternativeList.Accept(this);
                        }

                    }
                });

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

            CodeExpression v = current.GetRecursiveData("source") as CodeExpression;
            if (v == null)
                v = "item".Var();

            var i = CodeHelper.Call("writer".Var(), "ToString", v);
            current.Code.Add(i);

            return null;

        }

        public override CodeExpression VisitTerminal(AstTerminal a)
        {
            var current = CurrentBlock;

            string name = a.Value.Text;
            CodeExpression value = "Constants".AsType().Field(name);

            var i = CodeHelper.Call("writer".Var(), "Append", value);
            current.Code.Add(i);

            return null;
        }


    }

}
