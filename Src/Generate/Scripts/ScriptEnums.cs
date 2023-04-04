using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generate.Scripts
{


    public class ScriptEnums : ScriptBase
    {


        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstTerminalKeyword"; // "AstTerminal<Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum>";
        }

        public override string StrategyTemplateKey => "ClassEnum";


        protected override void ConfigureTemplate(Context context, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {

                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree");

                    ns.CreateTypeFrom<AstRule>((ast, type) =>
                    {

                        type.AddTemplateSelector(() => TemplateSelector(ast, context))
                            .GenerateIf(() => Generate(ast, context))
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                            .Attribute(TypeAttributes.Public)
                            .Inherit(() => GetInherit(ast, context))
                            .Documentation(e =>
                            {
                                e.Summary(() => ast.ToString());
                            })

                            .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                            {
                                f.Attribute(MemberAttributes.Family)
                                 .Argument(() => "ITerminalNode", "t")
                                 .Argument(() => typeof(string), "value")
                                 .Attribute(MemberAttributes.Public)
                                 .CallBase("t".Var(), "value".Var())
                                 ;
                            })

                            .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                            {
                                f.Attribute(MemberAttributes.Family)
                                 .Argument(() => "Position", "position")
                                 .Argument(() => typeof(string), "name")
                                 .Argument(() => typeof(string), "value")
                                 .Attribute(MemberAttributes.Public)
                                 .CallBase("position".Var(), "name".Var(), "value".Var());
                            })

                            .Methods(() => ast.ResolveAllCombinations(), (method, ast2) =>
                            {

                                method.Name(n =>
                                {
                                    
                                    try
                                    {
                                        var tree = n as TreeRuleItem;
                                        var text = tree.GetMethodNameForClassEnum();
                                        return text;
                                    }
                                    catch (UnexpectedException e)
                                    {
                                        Console.WriteLine($"the rule {ast.Name} can't be classified in ClassEnum because {e.Message} is not terminal constant");
                                    }

                                    return string.Empty;

                                })
                                .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                                .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                                .Body(m =>
                                {
                                    var text = (ast2 as TreeRuleItem).GetTerminalText();
                                    var value = (ast2 as TreeRuleItem).GetTerminalValue();
                                    var typeName = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    m.Statements.Return(CodeHelper.Create(typeName.AsType(), "Position.Default".Var(), text.Trim('\'').AsConstant(), value.Trim('\'').AsConstant()));

                                    m.Comments.Add(new CodeCommentStatement("<summary>", true));
                                    m.Comments.Add(new CodeCommentStatement($"{ast.Name} : {value}", true));
                                    m.Comments.Add(new CodeCommentStatement("</summary>", true));

                                })
                                ;

                            })

                            .Methods(() => ast.ResolveAllCombinations(), (method, ast2) =>
                            {

                                method.Name(n =>
                                {
                                    try
                                    {
                                        var tree = n as TreeRuleItem;
                                        var text = tree.GetMethodNameForClassEnum();
                                        return text;
                                    }
                                    catch (UnexpectedException e)
                                    {
                                        Console.WriteLine($"the rule {ast.Name} can't be classified in ClassEnum because {e.Message} is not terminal constant");
                                    }

                                    return string.Empty;

                                })
                                .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                                .Argument(() => "Position", "position")
                                .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                                .Body(m =>
                                {
                                    var text = (ast2 as TreeRuleItem).GetTerminalText();
                                    var value = (ast2 as TreeRuleItem).GetTerminalValue();
                                    var typeName = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    m.Statements.Return(CodeHelper.Create(typeName.AsType(), "position".Var(), text.Trim('\'').AsConstant(), value.Trim('\'').AsConstant()));

                                    m.Comments.Add(new CodeCommentStatement("<summary>", true));
                                    m.Comments.Add(new CodeCommentStatement($"{ast.Name} : {value}", true));
                                    m.Comments.Add(new CodeCommentStatement("</summary>", true));

                                })
                                ;

                            })
                            ;

                    })
                    ;

                    ns.CreateTypeFrom<AstLexerRule>((ast, type) =>
                    {

                        type.AddTemplateSelector(() => "_")
                            .GenerateIf(() => !ast.IsFragment && ast.Configuration.Config.Kind == TokenTypeEnum.Constant)
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                            .Attribute(TypeAttributes.Public)
                            .Inherit(() => "AstTerminalKeyword")
                            .Documentation(e =>
                            {
                                e.Summary(() => ast.ToString());
                            })

                            .Ctor((f) =>
                            {
                                f.Attribute(MemberAttributes.Family)
                                 .Argument(() => "ITerminalNode", "t")
                                 .Argument(() => typeof(string), "value")
                                 .Attribute(MemberAttributes.Public)
                                 .CallBase("t".Var(), "value".Var())
                                 ;
                            })

                            .Ctor((f) =>
                            {
                                f.Attribute(MemberAttributes.Family)
                                 .Argument(() => "Position", "position")
                                 .Argument(() => typeof(string), "name")
                                 .Argument(() => typeof(string), "value")
                                 .Attribute(MemberAttributes.Public)
                                 .CallBase("position".Var(), "name".Var(), "value".Var());
                            })

                            .Method((method) =>
                            {

                                method.Name(n =>
                                {
                                    return CodeHelper.FormatCsharp(ast.Name.Text);
                                })
                                .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                                .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                                .Body(m =>
                                {

                                    var value = ast.Value.ToString();
                                    value = value.Substring(1, value.Length - 2);

                                    var typeName = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    m.Statements.Return(CodeHelper.Create(typeName.AsType(), "Position.Default".Var(), ast.Name.Text.AsConstant(), value.AsConstant()));

                                    m.Comments.Add(new CodeCommentStatement("<summary>", true));
                                    m.Comments.Add(new CodeCommentStatement($"{ast.Name} : {value}", true));
                                    m.Comments.Add(new CodeCommentStatement("</summary>", true));

                                })
                                ;

                            })

                            ;

                    })
                    ;

                });
            });


        }

    }
}
