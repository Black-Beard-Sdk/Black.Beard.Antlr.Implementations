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
            return GetInherit_Impl("AstTerminalKeyword", ast, context);
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

                    ns.CreateTypeFrom<AstRule>(ast => Generate(ast, context), null, (ast, type) =>
                    {

                        type.AddTemplateSelector(() => TemplateSelector(ast, context))
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                            .Attribute(TypeAttributes.Public)
                            .Inherit(() => GetInherit(ast, context))
                            .Documentation(e =>
                            {
                                e.Summary(() => ast.ToString());
                            })

                            .Ctor((f) =>
                            {
                                f.Argument(() => "ITerminalNode", "t")
                                 .Argument(() => typeof(string), "value")
                                 .Attribute(MemberAttributes.Public)
                                 .CallBase("t".Var(), "value".Var())
                                 ;
                            })

                            .Ctor((f) =>
                            {
                                f.Attribute(MemberAttributes.Public)
                                 .Argument(() => "ParserRuleContext", "ctx")
                                 .Argument(() => typeof(string), "value")
                                 .CallBase("ctx".Var(), "value".Var())
                                 ;
                            })

                            .Ctor((f) =>
                            {
                                f.Argument(() => "Position", "position")
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
                                        var tree = (n as AlternativeTreeRuleItem).Item;
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
                                    var text = (ast2 as AlternativeTreeRuleItem).Item.GetTerminalText();
                                    var value = (ast2 as AlternativeTreeRuleItem).Item.GetTerminalValue();
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
                                        var tree = (n as AlternativeTreeRuleItem).Item;
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
                                    var text = (ast2 as AlternativeTreeRuleItem).Item.GetTerminalText();
                                    var value = (ast2 as AlternativeTreeRuleItem).Item.GetTerminalValue();
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

                    ns.CreateTypeFrom<AstLexerRule>(ast => !ast.IsFragment && ast.Configuration.Config.Kind == TokenTypeEnum.Constant, null, (ast, type) =>
                    {

                        type.AddTemplateSelector(() => "_")
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
