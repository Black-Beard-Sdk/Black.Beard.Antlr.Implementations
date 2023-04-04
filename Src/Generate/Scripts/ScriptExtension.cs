using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

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


        public static IEnumerable<TreeRuleItem> GetAlternativesWithOnlyRules(this AstRule ast, Context ctx)
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
                            var res = GetAlternativesWithOnlyRules(ru1, ctx);
                            _results.AddRange(res);
                        }
                    }

                }

            }

            return _results;

        }

        public static IEnumerable<TreeRuleItem> GetAlternativesForTerminalsClass(this AstRule ast, Context ctx)
        {

            List<TreeRuleItem> _results = new List<TreeRuleItem>();

            foreach (var alternative in ast.Alternatives)
                _results.AddRange( alternative.ResolveAllCombinations());

            return _results;

        }

        public static IEnumerable<TreeRuleItem> GetAlternatives(this AstLabeledAlt ast, Context ctx)
        {

            List<TreeRuleItem> _results = new List<TreeRuleItem>();

            var rule = ast.GetRules().FirstOrDefault();
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
                        var res = GetAlternativesWithOnlyRules(ru1, ctx);
                        _results.AddRange(res);
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

        public static string GetMethodNameForClassEnum(this TreeRuleItem item)
        {
            return TreeRuleNameResolver.Get(item);
        }

        public static string GetTerminalText(this TreeRuleItem item)
        {
            StringBuilder sb = new StringBuilder();

            var i = TreeRuleGetITems.Get(item);
            foreach (var j in i)
            {
                if (j is AstTerminalText t)
                {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    var l = t.Link as AstLexerRule;

                    sb.Append(l.Name);
                }
                else
                {

                }
            }

            return sb.ToString();

        }

        public static string GetTerminalValue(this TreeRuleItem item)
        {
            StringBuilder sb = new StringBuilder();

            var i = TreeRuleGetITems.Get(item);
            foreach (var j in i)
            {
                if (j is AstTerminalText t)
                {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    var l = t.Link as AstLexerRule;

                    if (l.TerminalKind == TokenTypeEnum.Constant)
                        sb.Append(l.Value.ToString().Trim('\''));
                    else
                        sb.Append(l.Value);
                }
                else
                {

                }
            }

            return sb.ToString();

        }

        public static CodeMemberMethod AsMethod(this string name, CodeTypeReference type, MemberAttributes attributes)
        {
            var method = new CodeMemberMethod()
            {
                Name = name,
                ReturnType = type,
                Attributes = attributes,
            };

            return method;

        }

        public static CodeMemberMethod BuildDocumentation(this CodeMemberMethod method, TreeRuleItem item, Context ctx)
        {

            method.Comments.Add(new CodeCommentStatement("<summary>", true));
            method.Comments.Add(new CodeCommentStatement($"{item.Name} : ", true));
            method.Comments.Add(new CodeCommentStatement(item.GenerateDoc(ctx), true));
            method.Comments.Add(new CodeCommentStatement("</summary>", true));

            return method;

        }

        public static void BuildStaticMethod(this TreeRuleItem itemAst, AstRule ast, CodeMemberMethod method, List<string> arguments)
        {

            StringBuilder uniqeConstraintKeyMethod = new StringBuilder();

            Action<TreeRuleItem> act = itemAst =>
            {

                string name = null;
                CodeTypeReference argumentTypeName = null;
                string varName = null;

                var itemResult = ast.ResolveByName(itemAst.ResolveKey());
                if (itemResult != null && itemResult is AstRule r1 && r1?.Configuration != null)
                {
                    name = "Ast" + CodeHelper.FormatCsharp(itemAst.Name);
                    if (string.IsNullOrEmpty(itemAst.Label))
                        varName = CodeHelper.FormatCsharpArgument(itemAst.Name);
                    else
                        varName = CodeHelper.FormatCsharpArgument(itemAst.Label);

                }
                else if (itemResult != null && itemResult is AstLexerRule r2 && r2?.Configuration != null)
                {

                    switch (r2.Configuration.Config.Kind)
                    {
                        case TokenTypeEnum.Pattern:
                        case TokenTypeEnum.String:
                        case TokenTypeEnum.Identifier:
                            name = nameof(String);
                            varName = "txt";
                            break;
                        case TokenTypeEnum.Boolean:
                            name = nameof(Boolean);
                            varName = "boolean";
                            break;
                        case TokenTypeEnum.Decimal:
                            name = nameof(Decimal);
                            varName = "_decimal";
                            break;
                        case TokenTypeEnum.Int:
                            name = nameof(Int64);
                            varName = "integer";
                            break;
                        case TokenTypeEnum.Real:
                            name = nameof(Double);
                            varName = "real";
                            break;
                        case TokenTypeEnum.Hexa:
                            name = "";
                            varName = "";
                            break;
                        case TokenTypeEnum.Binary:
                            name = "Object";
                            varName = "_binary";
                            break;

                        case TokenTypeEnum.Operator:
                        case TokenTypeEnum.Ponctuation:
                        case TokenTypeEnum.Other:
                        case TokenTypeEnum.Comment:
                        case TokenTypeEnum.Constant:
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(itemAst.Label))
                        varName = CodeHelper.FormatCsharpArgument(itemAst.Label);

                }

                if (name != null)
                {
                    argumentTypeName = new CodeTypeReference(name);
                    if (itemAst.Occurence.Value == OccurenceEnum.Any)
                        argumentTypeName = new CodeTypeReference(typeof(IEnumerable<>).Name, argumentTypeName);

                    method.Parameters.Add(new CodeParameterDeclarationExpression(argumentTypeName, varName));
                    uniqeConstraintKeyMethod.Append(name);
                    arguments.Add(varName);
                }

            };

        }


    }

    public class TreeRuleGetITems : TreeRuleItemVisitor
    {

        private TreeRuleGetITems()
        {
            this._items = new List<AstBase>();
            this._stack = new Stack<TreeRuleItem>();

        }

        public override void Visit(TreeRuleItem i)
        {

            if (_stack.Contains(i))
                return;

            _stack.Push(i);

            if (i.Count == 0)
                i.Accept(this);
            else
                foreach (var j in i)
                    j.Accept(this);

            _stack.Pop();

        }

        public static List<AstBase> Get(TreeRuleItem item)
        {
            var visitor = new TreeRuleGetITems();
            visitor.Visit(item);
            return visitor._items;
        }

        public override void VisitAlternative(TreeRuleItem i)
        {
            foreach (var item in i)
            {
                item.Accept(this);
            }
        }

        public override void VisitBlock(TreeRuleItem i)
        {
            foreach (var item in i)
            {
                item.Accept(this);
            }
        }

        public override void VisitRange(TreeRuleItem i)
        {
            foreach (var item in i)
            {
                item.Accept(this);
            }
        }

        public override void VisitRuleRef(TreeRuleItem i)
        {
            var t = i.Origin;
            _items.Add(t);
        }

        public override void VisitTerminal(TreeRuleItem i)
        {
            var t = i.Origin;
            _items.Add(t);
        }

        private readonly List<AstBase> _items;
        private readonly Stack<TreeRuleItem> _stack;
    }

    public class TreeRuleNameResolver : TreeRuleItemVisitor
    {

        private TreeRuleNameResolver()
        {

            this._sb = new StringBuilder();
            this._stack = new Stack<TreeRuleItem>();
        }

        public override void Visit(TreeRuleItem i)
        {

            if (_stack.Contains(i))
                return;

            _stack.Push(i);

            if (i.Count > 0)
                foreach (var item in i)
                    item.Accept(this);

            else
                i.Accept(this);

            _stack.Pop();

        }

        public static string Get(TreeRuleItem item)
        {
            var visitor = new TreeRuleNameResolver();
            visitor.Visit(item);
            return visitor._sb.ToString();
        }

        public override void VisitAlternative(TreeRuleItem i)
        {
            foreach (var item in i)
            {
                item.Accept(this);
            }
        }

        public override void VisitBlock(TreeRuleItem i)
        {
            foreach (var item in i)
            {
                item.Accept(this);
            }
        }

        public override void VisitRange(TreeRuleItem i)
        {
            foreach (var item in i)
            {
                item.Accept(this);
            }
        }

        public override void VisitRuleRef(TreeRuleItem i)
        {
            throw new UnexpectedException(i.ToString());
        }

        public override void VisitTerminal(TreeRuleItem i)
        {
            var t = i.Origin as AstTerminalText;
            _sb.Append(CodeHelper.FormatCsharp(t.Text.ToLower()));
        }

        private readonly StringBuilder _sb;
        private readonly Stack<TreeRuleItem> _stack;
    }

}
