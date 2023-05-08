using Bb.Asts;
using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{
    public class GenerateClassDefaultToString : GenerateCodeFromAst
    {

        private GenerateClassDefaultToString(CodeStatementCollection code, string strategy)
            : base(code, strategy)
        {

        }

        public static CodeExpression Generate(AstRule ast, CodeStatementCollection code)
        {
            var strategy = GenerateCodeFromAst.GetStrategy(ast);
            var visitor = new GenerateClassDefaultToString(code, strategy);

            var result = ast.Accept(visitor);

            return result;
        }

        public override CodeExpression VisitRule(AstRule a)
        {

            using (var current = Stack(a, null))
            {
                current.Code.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());
                a.Alternatives.Accept(this);
                current.Code.Return(CodeHelper.AsConstant(true));
            }
            return null;
        }

        public override CodeExpression VisitRuleAltList(AstRuleAltList a)
        {

            foreach (AstLabeledAlt item in a)
                item.Accept(this);

            return null;

        }

        public override CodeExpression VisitLabeledAlt(AstLabeledAlt a)
        {

            if (a.Name != null)
            {

            }

            a.Rule.Accept(this);

            if (a.Name != null)
            {
                Pause();
            }

            return null;

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
                    a.Value.Accept(this);
                }
                else
                {

                    a.Value.Accept(this);

                }

            }

            return null;

        }

        public override CodeExpression VisitBlock(AstBlock a)
        {

            if (a.Occurence.Optional)
            {

                var current1 = CurrentBlock;
                var variableTest = CurrentBlock.CreateVariable("test", typeof(bool));
                var marker = current1.GetMarker();

                current1.Code.If(variableTest.Name.Var(), t =>
                {

                    using (var current2 = Stack(a, t))
                    {

                        if (a.Occurence.Optional)
                        {
                            current2.SetData("InOptionalBloc", true);
                            current2.SetData("optionalTests", new List<CodeExpression>());
                        }

                        a.AlternativeList.Accept(this);
                        var c = current2.GetData<CodeExpression>("testOptional");
                        var s = CodeHelper.DeclareAndInitialize(variableTest.Name, variableTest.Type, c);
                        marker.Append(s);

                    }

                });
            }
            else
                a.AlternativeList.Accept(this);

            return null;

        }

        public override CodeExpression VisitRuleRef(AstRuleRef a)
        {

            var s = a.GetFieldName();
            CodeExpression i = CodeHelper.Call("writer".Var(), "ToString", s.Var());

            var current = CurrentBlock;

            var c = current.GetData<bool>("InOptionalBloc");
            if (c)
            {
                var c1 = current.GetData<List<CodeExpression>>("optionalTests");
                c1.Add(s.Var().IsNotEqual(CodeHelper.Null()));
            }


            var b = a.Ancestor<AstBlock>();
            if (b != null && b.Occurence.Optional)
            {
                
                var d = GetDataFor(b);
                if (!d.DataExist("testOptional"))
                    
            }

            current.Code.Add(i);

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

        public override CodeExpression VisitTerminal(AstTerminal a)
        {
            var current = CurrentBlock;

            if (a.Link.TerminalKind == TokenTypeEnum.Identifier)
            {
                var i = CodeHelper.Call("writer".Var(), "ToString", "Value".Var());
                current.Code.Add(i);
            }
            else
            {

                string name = a.Value.Text;
                CodeExpression value = "Constants".AsType().Field(name);

                var i = CodeHelper.Call("writer".Var(), "Append", value);
                current.Code.Add(i);
            }

            return null;
        }


    }

}
