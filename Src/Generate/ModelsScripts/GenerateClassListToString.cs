using Bb.Asts;
using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{


    public class GenerateClassIdentifierToString : GenerateCode
    {

        public GenerateClassIdentifierToString(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {

        }

        public static CodeExpression Generate(AstRule ast, CodeStatementCollection code)
        {
            var strategy = GenerateCode.GetStrategy(ast);
            var visitor = new GenerateClassListToString(code, strategy);

            var result = ast.Accept(visitor);

            return result;
        }

        public override CodeExpression VisitRule(AstRule a)
        {
            //             Writer.EnsureEndBy(' ');
            using (var current = Stack())
            {

                current.Current.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());

                switch (this.Strategy)
                {

                    case "ClassList":
                        current.Current.If(CodeHelper.This().Property("Count").GreaterThan((0).AsConstant()),
                        i =>
                        {
                            using (var current = Stack(i))
                            {
                                current.Current.DeclareAndInitialize("index", typeof(int).AsType(), (0).AsConstant());
                                a.Alternatives.Accept(this);
                            }
                        });

                        break;

                    default:
                        a.Alternatives.Accept(this);
                        break;
                }

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
                    Stop();
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
                    Stop();
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

                    var item = CodeHelper.This().Indexer("index".Var());
                    current.SetData("source", item);

                    if (a.Value.Type == "AstTerminal")
                    {
                        if (!a.Occurence.Optional)
                            a.Value.Accept(this);
                    }
                    else
                    {

                        a.Value.Accept(this);

                        var left = "index".Var();
                        var right = CodeHelper.This().Property("Count");

                        var m = CodeHelper.For("index".Var().Assign((1).AsConstant()), left.LessThan(right), "index".PostIncrement().AsStatement(), j =>
                        {
                            using (var current = Stack(j))
                            {
                                a.Value.Accept(this);
                            }
                        });
                        current.Current.Add(m);
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
            if (Strategy == "ClassList")
                value = 1;

            CodeExpression result = null;

            using (var current = Stack())
            {
                current.Current.If(CodeHelper.This().Property("Count").GreaterThan((value).AsConstant()),
                i =>
                {
                    using (var current = Stack(i))
                    {

                        if (a.Occurence.Value == OccurenceEnum.Any)
                        {

                            var left = "index".Var();
                            var right = CodeHelper.This().Property("Count");

                            var m = CodeHelper.For("index".Var().Assign((1).AsConstant()), left.LessThan(right), "index".PostIncrement().AsStatement(),
                            j =>
                            {

                                using (var current = Stack(j))
                                {
                                    var item = CodeHelper.This().Indexer("index".Var());
                                    current.SetData("source", item);
                                    a.AlternativeList.Accept(this);
                                }
                            });
                            current.Current.Add(m);

                        }

                        else
                        {

                            //if (a.Occurence.Optional)
                            //{
                            //    Stop();
                            //}

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

            CodeExpression v = current.GetData("source") as CodeExpression;
            if (v == null)
                v = "item".Var();

            var i = CodeHelper.Call(v, "ToString", "writer".Var());
            current.Current.Add(i);

            return null;

        }

        public override CodeExpression VisitTerminal(AstTerminal a)
        {
            var current = CurrentBlock;

            string name = a.Value.Text;
            CodeExpression value = "Constants".AsType().Field(name);

            var i = CodeHelper.Call("writer".Var(), "Append", value);
            current.Current.Add(i);

            return null;
        }


    }


    public class GenerateClassListToString : GenerateCode
    {

        public GenerateClassListToString(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {

        }

        public static CodeExpression Generate(AstRule ast, CodeStatementCollection code)
        {
            var strategy = GenerateCode.GetStrategy(ast);
            var visitor = new GenerateClassListToString(code, strategy);

            var result = ast.Accept(visitor);

            return result;
        }

        public override CodeExpression VisitRule(AstRule a)
        {
            //             Writer.EnsureEndBy(' ');
            using (var current = Stack())
            {

                current.Current.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());

                switch (this.Strategy)
                {

                    case "ClassList":
                        current.Current.If(CodeHelper.This().Property("Count").GreaterThan((0).AsConstant()),
                        i =>
                        {
                            using (var current = Stack(i))
                            {
                                current.Current.DeclareAndInitialize("index", typeof(int).AsType(), (0).AsConstant());
                                a.Alternatives.Accept(this);
                            }
                        });

                        break;

                    default:
                        a.Alternatives.Accept(this);
                        break;
                }

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
                    Stop();
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
                    Stop();
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

                    var item = CodeHelper.This().Indexer("index".Var());
                    current.SetData("source", item);

                    if (a.Value.Type == "AstTerminal")
                    {
                        if (!a.Occurence.Optional)
                            a.Value.Accept(this);
                    }
                    else
                    {

                        a.Value.Accept(this);

                        var left = "index".Var();
                        var right = CodeHelper.This().Property("Count");

                        var m = CodeHelper.For("index".Var().Assign((1).AsConstant()), left.LessThan(right), "index".PostIncrement().AsStatement(), j =>
                        {
                            using (var current = Stack(j))
                            {
                                a.Value.Accept(this);
                            }
                        });
                        current.Current.Add(m);
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
            if (Strategy == "ClassList")
                value = 1;

            CodeExpression result = null;

            using (var current = Stack())
            {
                current.Current.If(CodeHelper.This().Property("Count").GreaterThan((value).AsConstant()),
                i =>
                {
                    using (var current = Stack(i))
                    {

                        if (a.Occurence.Value == OccurenceEnum.Any)
                        {

                            var left = "index".Var();
                            var right = CodeHelper.This().Property("Count");

                            var m = CodeHelper.For("index".Var().Assign((1).AsConstant()), left.LessThan(right), "index".PostIncrement().AsStatement(),
                            j =>
                            {

                                using (var current = Stack(j))
                                {
                                    var item = CodeHelper.This().Indexer("index".Var());
                                    current.SetData("source", item);
                                    a.AlternativeList.Accept(this);
                                }
                            });
                            current.Current.Add(m);

                        }

                        else
                        {

                            //if (a.Occurence.Optional)
                            //{
                            //    Stop();
                            //}

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

            CodeExpression v = current.GetData("source") as CodeExpression;
            if (v == null)
                v = "item".Var();

            var i = CodeHelper.Call(v, "ToString", "writer".Var());
            current.Current.Add(i);

            return null;

        }

        public override CodeExpression VisitTerminal(AstTerminal a)
        {
            var current = CurrentBlock;

            string name = a.Value.Text;
            CodeExpression value = "Constants".AsType().Field(name);

            var i = CodeHelper.Call("writer".Var(), "Append", value);
            current.Current.Add(i);

            return null;
        }


    }

}
