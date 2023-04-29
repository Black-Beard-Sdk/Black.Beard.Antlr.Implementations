using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generate.ModelsScripts
{


    public class ScriptEnums : ScriptBase
    {


        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstTerminalKeyword", ast, context);
        }

        public override string StrategyTemplateKey => "ClassEnum";


        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {

                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree");

                    ns.CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), ast =>
                    {
                        ctx.Variables["combinaisons"] = ast.ResolveAllCombinations();
                    }, (ast, type) =>
                    {
                        type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum")
                            .Attribute(TypeAttributes.Public)
                            .IsEnum()
                            .Documentation(e =>
                            {
                                e.Summary(() => ast.ToString());
                            })
                            .Fields(() => ast.ResolveAllCombinations(), (m, ast2) =>
                            {
                                m.Name(f =>
                                {
                                    try
                                    {
                                        var tree = (f as AlternativeTreeRuleItem).Item;
                                        var text = tree.GetMethodNameForClassEnum();
                                        return text;
                                    }
                                    catch (UnexpectedException e)
                                    {
                                        Console.WriteLine($"the rule {ast.Name} can't be classified in ClassEnum because {e.Message} is not terminal constant");
                                    }
                                    return string.Empty;
                                });
                                m.Documentation(d =>
                                {
                                    d.Summary(() => $"{ast.Name} : ");
                                });
                                ;
                            })
                            ;
                    });

                    ns.CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), ast =>
                    {
                        ctx.Variables["combinaisons"] = ast.ResolveAllCombinations();

                    }, (ast, type) =>
                    {

                        type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                            .Attribute(TypeAttributes.Public)
                            .Inherit(() => GetInherit(ast, ctx))
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

                            .Field(field =>
                            {

                                field.Name("_ruleValue")
                                .Type(typeof(string))
                                .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                .Value((a) =>
                                {
                                    return ast.Alternatives.ToString();
                                })
                                ;
                            })
                            .Field(field =>
                            {
                                field.Name("_ruleName")
                                     .Type(typeof(string))
                                     .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                     .Value((a) =>
                                     {
                                         return ast.Name.Text;
                                     });
                            })
                            .Property(property =>
                            {
                                property.Name((a) => "RuleName")
                                        .Type(() => typeof(string))
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Get((a) => a.Return("_ruleName".Var()))
                                        .HasSet(false)
                                        ;
                            })
                            .Property(property =>
                            {
                                property.Name((a) => "RuleValue")
                                        .Type(() => typeof(string))
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Get((a) => a.Return("_ruleValue".Var()))
                                        .HasSet(false)
                                        ;
                            })

                            .Field(field =>
                            {
                                field.Name("_isTerminal")
                                     .Type(typeof(bool))
                                     .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                     .Value((a) =>
                                     {

                                         var items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                                         foreach (var item in items)
                                         {
                                             foreach (var item1 in item.Item)
                                             {
                                                 var oo = item1.Origin.Select(c => c.Type == nameof(AstRuleRef)).FirstOrDefault();
                                                 if (oo != null)
                                                     return CodeHelper.AsConstant(false);
                                             }
                                         }

                                         return CodeHelper.AsConstant(true);
                                     });
                            })

                            .Property(property =>
                            {
                                property.Name((a) => "IsTerminal")
                                        .Type(() => typeof(bool))
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Get((a) => a.Return("_isTerminal".Var()))
                                        .HasSet(false)
                                        ;
                            })

                            .Method(method =>
                            {
                                var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                method.Name(g => "Null")
                                      .Return(() => type + "?")
                                      .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                      .Body(b => b.Statements.Return(CodeHelper.Null()))
                                 ;

                            })

                            .Method(method =>
                            {
                                var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                method.Name(g => type)
                                      .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum", "value")
                                      .Return(() => "implicit operator")
                                      .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                      .Body(b =>
                                      {

                                          var typeName = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                          var t = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum";
                                          var items = ast.ResolveAllCombinations();

                                          for (int i = 1; i < items.Count; i++)
                                          {
                                              var k = items[i];
                                              var j = k.Item;
                                              var text = j.GetMethodNameForClassEnum();
                                              b.Statements.If("value".Var().IsEqual(CodeHelper.Field(t.AsType(), text)), t =>
                                              {
                                                  t.Return(CodeHelper.Call(typeName.AsType(), text));
                                              });

                                          }

                                          var j2 = items[0].Item;
                                          var text2 = j2.GetMethodNameForClassEnum();
                                          b.Statements.Return(CodeHelper.Call(typeName.AsType(), text2));

                                      })
                                 ;

                            })

                            .Method(method =>
                            {
                                var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                method.Name(g => "ToString")
                                      .Argument("Writer", "writer")
                                      .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                      .Body(b =>
                                      {

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
