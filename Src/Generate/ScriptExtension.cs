using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Security.Principal;
using System.Text;

namespace Generate
{
    public static class ScriptExtension
    {


        public static string GetTemplate(this AstBase ast)
        {

            if (ast != null)
            {
                if (ast is AstRule r)
                {
                    var c = r?.Configuration?.Config;
                    if (c != null)
                    {
                        if (c.TemplateSetting != null)
                            return c.TemplateSetting.TemplateName;
                        else if (c.CalculatedTemplateSetting != null)
                            return c.CalculatedTemplateSetting.Setting.TemplateName;
                    }
                }

            }



            return string.Empty;

        }

        public static string Type(this AstBase item)
        {

            var ast = item;

            if (item.Type == "TOKEN_REF")
            {
                var link = item.Link;
                if (link != null)
                {
                    var value1 = link.TerminalKind.Type(false);
                    if (value1 != null)
                    {
                        var occurence1 = ast.ResolveOccurence();
                        if (occurence1 != null && occurence1.Value == OccurenceEnum.Any)
                            value1 = "IEnumerable<" + value1 + ">";
                        return value1 + (occurence1 != null && occurence1.Optional ? "?" : string.Empty);
                    }
                }
            }

            if (item is AstElementList a)
            {
                if (a.Count == 1)
                    ast = a[0];
            }


            var value = ast.TerminalKind.Type(false);

            if (value != null)
                return value;

            string _name = (ast as AstBase).ResolveName();

            var result = "Ast" + _name.FormatCsharp();

            var occurence = ast.ResolveOccurence();
            if (occurence != null && occurence.Value == OccurenceEnum.Any)
                result = "IEnumerable<" + result + ">";

            return result + (occurence != null && occurence.Optional ? "?" : string.Empty);

        }

        public static string Type(this TreeRuleItem ast)
        {

            string suffix = string.Empty;
            if (ast.Occurence.Optional)
                suffix = "?";

            if (ast.IsTerminal)
            {

                var link = ast.Origin.Link;

                var value = link.TerminalKind.Type(ast.Occurence.Optional);

                if (value != null)
                    return value + suffix;

            }

            string _name = ast.Name;

            var result = "Ast" + _name.FormatCsharp();

            if (ast.Occurence.Value == OccurenceEnum.Any)
                result = "IEnumerable<" + result + ">";

            return result + suffix;

        }

        public static string? Type(this TokenTypeEnum self, bool optional)
        {

            string suffix = optional ? "?" : string.Empty;

            switch (self)
            {

                case TokenTypeEnum.Hexa:
                    break;

                case TokenTypeEnum.Boolean:
                    return nameof(Boolean) + suffix;

                case TokenTypeEnum.Decimal:
                    return nameof(Decimal) + suffix;

                case TokenTypeEnum.Int:
                    return nameof(Int64) + suffix;

                case TokenTypeEnum.Real:
                    return nameof(Double) + suffix;

                case TokenTypeEnum.Binary:
                case TokenTypeEnum.Identifier:
                case TokenTypeEnum.String:
                case TokenTypeEnum.Pattern:
                    return nameof(String) + suffix;

                case TokenTypeEnum.Comment:
                case TokenTypeEnum.Other:
                case TokenTypeEnum.Operator:
                case TokenTypeEnum.Constant:
                case TokenTypeEnum.Ponctuation:
                default:
                    break;
            }

            return null;

        }

        public static string GetPropertyName(this AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Name.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Name.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = _name.FormatCsharp();

            return result;
        }

        public static string GetPropertyName(this TreeRuleItem ast)
        {
            return (ast.Label ?? ast.Name).FormatCsharp();
        }

        public static string GetFieldName(this AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Name.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Name.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = _name.FormatCsharpField();

            return result;
        }

        public static string GetFieldName(this TreeRuleItem ast)
        {
            return (ast.Label ?? ast.Name).FormatCsharpField();
        }

        public static string GetParameterdName(this AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Name.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Name.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = _name.FormatCsharpArgument();

            return result;
        }

        public static string GetParameterdName(this TreeRuleItem ast)
        {
            return (ast.Label ?? ast.Name).FormatCsharpArgument();
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
                if (names.Add(item.GetPropertyName()))
                    _properties.Add(item);

            return _properties;

        }

        public static string GenerateDoc(this TreeRuleItem alt, Context ctx)
        {

            var txt = alt.ToString();
            var items = txt.Split(" ");

            foreach (var item in items)
            {
                if (item.IsUppercase())
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

        public static bool ContainsTerminalIdentifier(this AstBase ast)
        {

            if (ast.TerminalKind == TokenTypeEnum.Identifier)
                return true;

            if (ast != ast.Link
                && ast.Link != null
                && ast.Link.ContainsTerminalIdentifier())
                return true;

            foreach (var item in ast.GetAllItems())
                if (item.ContainsTerminalIdentifier())
                    return true;

            return false;

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

        public static CodeMemberMethod BuildDocumentation(this CodeMemberMethod method, string name, TreeRuleItem item, Context ctx)
        {

            method.Comments.Add(new CodeCommentStatement("<summary>", true));
            method.Comments.Add(new CodeCommentStatement($"{name} : ", true));
            method.Comments.Add(new CodeCommentStatement(item.GenerateDoc(ctx), true));
            method.Comments.Add(new CodeCommentStatement("</summary>", true));

            return method;

        }

        public static void BuildStaticMethod(this TreeRuleItem itemAst, AstRule ast, CodeMemberMethod method, List<string> arguments, StringBuilder uniqeConstraintKeyMethod)
        {

            var dic = new HashSet<string>();
            foreach (CodeParameterDeclarationExpression parameter in method.Parameters)
                dic.Add(parameter.Name);

            Action<TreeRuleItem> act = itemAst =>
            {

                string name = null;
                string varName = null;

                var itemResult = ast.ResolveByName(itemAst.ResolveKey());
                if (itemResult != null && itemResult is AstRule r1 && r1?.Configuration != null)
                {
                    name = itemAst.Type(); // "Ast" + CodeHelper.FormatCsharp(itemAst.Name);
                    if (string.IsNullOrEmpty(itemAst.Label))
                        varName = itemAst.Name.FormatCsharpArgument();
                    else
                        varName = itemAst.Label.FormatCsharpArgument();

                }
                else if (itemResult != null && itemResult is AstLexerRule r2 && r2?.Configuration != null)
                {

                    name = itemAst.Type();
                    if (!string.IsNullOrEmpty(itemAst.Label))
                        varName = itemAst.Label.FormatCsharpArgument();
                    else
                        varName = r2.Configuration.Config.GetvariableName(dic);
                }

                if (name != null)
                {
                    var argumentTypeName = new CodeTypeReference(name);
                    method.Parameters.Add(new CodeParameterDeclarationExpression(argumentTypeName, varName));
                    uniqeConstraintKeyMethod.Append(name.Trim('?'));
                    arguments.Add(varName);
                }

            };

            if (itemAst.WhereRuleOrIdentifiers())
                act(itemAst);

        }

        public static string GetvariableName(this GrammarRuleTermConfig item, HashSet<string> dic)
        {

            string result = "var";

            switch (item.Kind)
            {
                case TokenTypeEnum.Pattern:
                    result = item.ExtendedPattern.Text;
                    break;

                case TokenTypeEnum.String:
                case TokenTypeEnum.Identifier:
                    result = "txt";
                    break;

                case TokenTypeEnum.Boolean:
                    result = "boolean";
                    break;

                case TokenTypeEnum.Decimal:
                    result = "_decimal";
                    break;

                case TokenTypeEnum.Int:
                    result = "integer";
                    break;

                case TokenTypeEnum.Real:
                    result = "real";
                    break;

                case TokenTypeEnum.Hexa:
                    result = "data";
                    break;

                case TokenTypeEnum.Binary:
                    result = "_binary";
                    break;

                case TokenTypeEnum.Operator:
                case TokenTypeEnum.Ponctuation:
                case TokenTypeEnum.Other:
                case TokenTypeEnum.Comment:
                case TokenTypeEnum.Constant:
                default:
                    break;
            }

            int count = 0;
            var p = result;
            while (dic.Contains(p))
            {
                p = result + count++;
            }

            return p;

        }


    }

    public class TreeRuleGetITems : TreeRuleItemVisitor
    {

        private TreeRuleGetITems()
        {
            _items = new List<AstBase>();
            _stack = new Stack<TreeRuleItem>();

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

            _sb = new StringBuilder();
            _stack = new Stack<TreeRuleItem>();
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
            _sb.Append(t.Text.ToLower().FormatCsharp());
        }

        private readonly StringBuilder _sb;
        private readonly Stack<TreeRuleItem> _stack;
    }

}
