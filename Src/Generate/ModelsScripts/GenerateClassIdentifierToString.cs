using Bb.Asts;
using Bb.Generators;
using System.CodeDom;
using System.Transactions;

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

            using (var current = Stack(a))
            {

                current.Code.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());
                a.Alternatives.Accept(this);
                current.Code.Return(CodeHelper.AsConstant(true));
            }

            return null;

        }

        public override CodeExpression VisitRuleAltList(AstRuleAltList a)
        {

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
            {

                if (a.Occurence.Value == OccurenceEnum.Any)
                {

                }
                else
                {

                    if (a.Occurence.Optional)
                    {

                    }
                    else
                    {

                        a.Value.Accept(this);

                    }

                }
            }

            return null;

        }

        public override CodeExpression VisitBlock(AstBlock a)
        {

            using (var current = Stack(a))
            {
                a.AlternativeList.Accept(this);
            }

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
            var s = a.GetFieldName();
            var b = a.Ancestor<AstBlock>();
            if (b != null)
            {
                if (b.Occurence.Optional)
                {

                    var d = GetDataFor(b);


                }
            }

            var i = CodeHelper.Call("writer".Var(), "ToString", s.Var());
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
                var i = CodeHelper.Call("writer".Var(), "ToString", "Value".Var());
                current.Code.Add(i);
            }
            else
            {

            }

            return null;
        }


    }


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

            using (var current = Stack(a))
            {
                current.Code.Call("writer".Var(), "EnsureEndBy", ' '.AsConstant());
                a.Alternatives.Accept(this);
            }

            return null;

        }

        public override CodeExpression VisitRuleAltList(AstRuleAltList a)
        {

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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

            using (var current = Stack(a))
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
                var i = CodeHelper.Call("writer".Var(), "ToString", "Value".Var());
                current.Code.Add(i);
            }
            else
            {

            }

            return null;
        }


    }

}
