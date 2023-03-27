using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;

namespace Generate.Scripts
{
    public static class ScriptExtension
    {

        public static string Type(this AstBase ast)
        {

            switch (ast.TerminalKind)
            {

                case TokenTypeEnum.Hexa:
                    break;

                case TokenTypeEnum.Boolean:
                    return nameof(Boolean);

                case TokenTypeEnum.Decimal:
                    return nameof(Decimal);

                case TokenTypeEnum.Int:
                    return nameof(Int64);

                case TokenTypeEnum.Real:
                    return nameof(Double);

                case TokenTypeEnum.Binary:
                case TokenTypeEnum.Identifier:
                case TokenTypeEnum.String:
                case TokenTypeEnum.Pattern:
                    return nameof(String);

                case TokenTypeEnum.Comment:
                case TokenTypeEnum.Other:
                case TokenTypeEnum.Operator:
                case TokenTypeEnum.Constant:
                case TokenTypeEnum.Ponctuation:
                default:
                    break;
            }

            string _name = (ast as AstBase).ResolveName();

            if (_name == "STRING")
            {

            }

            var result = "Ast" + CodeHelper.FormatCsharp(_name);
            return result;
        }

        public static string GetPropertyName(this AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Left.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Left.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = CodeHelper.FormatCsharp(_name);

            return result;
        }

        public static string GetFieldName(this AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Left.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Left.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = CodeHelper.FormatCsharpField(_name);

            return result;
        }

        public static string GetParameterdName(this AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Left.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Left.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = CodeHelper.FormatCsharpArgument(_name);

            return result;
        }

        public static List<object> GetProperties(this AstBase ast)
        {

            var items = ast.GetAllItems()
            .KeepTerminal(c =>
            {

                if (c.TerminalKind == TokenTypeEnum.Comment)
                    return false;

                if (c.TerminalKind == TokenTypeEnum.Operator)
                    return false;

                if (c.TerminalKind == TokenTypeEnum.Ponctuation)
                    return false;

                if (c.TerminalKind == TokenTypeEnum.Constant)
                    return false;

                return true;

            });

            HashSet<string> names = new HashSet<string>();
            List<object> _properties = new List<object>();
            foreach (var item in items)
                if (names.Add(GetPropertyName(item)))
                    _properties.Add(item);

            return _properties;

        }


        public static string GenerateDoc(this TreeRuleItem alt, Context ctx)
        {

            var txt = alt.ToString();
            var items = txt.Split(" ");

            foreach (var item in items)
            {
                if (IsUppercase(item))
                {
                    var o = ctx.RootAst.Rules.ResolveByName(item) as AstLexerRule;
                    if (o != null && o.Configuration.Config.Kind == TokenTypeEnum.Ponctuation)
                    {
                        txt = txt.Replace(item, o.Value.ToString().Trim('\''));
                    }
                }

            }

            return "   " + txt;

        }

        public static bool IsUppercase(this string text)
        {

            foreach (var item in text)
                if (char.IsLower(item))
                    return false;

            return true;

        }

        public static IEnumerable<TreeRuleItem> GetAlternatives(this AstRule ast, Context ctx)
        {

            List<TreeRuleItem> _results = new List<TreeRuleItem>();

            foreach (var alternative in ast.Alternatives)
            {

                var rule = alternative.GetRules().FirstOrDefault();
                if (rule != null)
                {

                    var ru1 = ctx.RootAst.Rules.ResolveByName(rule.Identifier.Text) as AstRule;

                    if (ru1 != null)
                    {
                        if (ru1.Configuration.Config.Generate)
                        {

                            List<TreeRuleItem> allCombinations = ru1.ResolveAllCombinations();
                            foreach (var item in allCombinations)
                                _results.Add(item);
                        }
                        else
                        {
                            var res = GetAlternatives(ru1, ctx);
                            _results.AddRange(res);
                        }
                    }

                }

            }

            return _results;

        }

        public static string ResolveKey(this TreeRuleItem item)
        {

            if (!string.IsNullOrEmpty(item.Name))
                return item.Name;

            return null;

        }




    }


}
