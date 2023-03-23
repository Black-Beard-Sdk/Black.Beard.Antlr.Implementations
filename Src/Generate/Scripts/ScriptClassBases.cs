﻿using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Generate.Scripts
{

    public class ScriptClassBases : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstRule\"");

            return config.Inherit.Text;

        }

        public override string StrategyTemplateKey => "ClassBase";

        protected override bool Generate(AstRule ast, Context context)
        {
            var result = !ast.Configuration.Config.Generate;
            return result;
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")

                .CreateTypeFrom<AstRule>((ast, type) =>
                {

                    var item =
                    type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                        .GenerateIf(() => Generate(ast, ctx))
                        .Attribute(TypeAttributes.Abstract | TypeAttributes.Public)
                        .Documentation(c => c.Summary(() => ast.ToString()))
                        .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                        .Inherit(() => GetInherit(ast, ctx))

                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ITerminalNode", "t")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("t", "list");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ParserRuleContext", "ctx")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("ctx", "list");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "Position", "p")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("p", "list");
                               })

                        .Make(t =>
                        {

                            var r = ast.Root();

                            HashSet<string> _h = new HashSet<string>();
                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                            var alternatives = ast.GetAlternatives(ctx);

                            foreach (var alt in alternatives)
                            {

                                var n1 = CodeHelper.FormatCsharp(alt.Name);
                                var n2 = "Ast" + n1;
                                var t1 = n2.AsType();

                                StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                List<string> arguments = new List<string>();

                                var method = new CodeMemberMethod()
                                {
                                    Name = n1,
                                    ReturnType = t1,
                                    Attributes = MemberAttributes.Public | MemberAttributes.Static,
                                };

                                method.Comments.Add(new CodeCommentStatement("<summary>", true));
                                method.Comments.Add(new CodeCommentStatement($"{alt.Name} : ", true));
                                method.Comments.Add(new CodeCommentStatement(alt.GenerateDoc(ctx), true));
                                method.Comments.Add(new CodeCommentStatement("</summary>", true));

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

                                if (alt.Count > 0)
                                    foreach (var itemAlt in alt)
                                        act(itemAlt);
                                else
                                    act(alt);

                                if (method.Parameters.Count > 0)
                                {

                                    var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                    if (_h.Add(noDuplicateKey))
                                    {
                                        List<CodeExpression> args = new List<CodeExpression>(arguments.Count);
                                        foreach (var itemArg in arguments)
                                            args.Add(itemArg.Var());

                                        methods.Add(method);
                                        var ret = CodeHelper.Call(t1, n1, args.ToArray());
                                        method.Statements.Return(ret);

                                    }

                                }

                            }

                            foreach (var item in methods)
                            {
                                t.Members.Add(item);
                            }

                        })

                        ;




                })

                .CreateTypeFrom<AstLabeledAlt>((ast, type) =>
                       {
                           type.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text))
                               .Inherit(() => "AstRule")
                               .Documentation(c => c.Summary(() => ast.ToString()))

                               .Ctor((f) =>
                               {
                                   f.Argument(() => "ParserRuleContext", "ctx")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("ctx", "list");
                               })
                               .Method(method =>
                               {
                                   method
                                    .Name(g => "Accept")
                                    .Argument("IAstTSqlVisitor", "visitor")
                                    .Attribute(MemberAttributes.Override | MemberAttributes.Public)
                                    .Body(b =>
                                    {
                                        b.Statements.Call
                                        (
                                            CodeHelper.Var("visitor"),
                                            "Visit" + CodeHelper.FormatCamelUpercase(ast.Identifier.Text),
                                            CodeHelper.This()
                                        );
                                    });
                               });
                       });

                });

            });

        }

    }

    public static class ScriptExtension
    {

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
