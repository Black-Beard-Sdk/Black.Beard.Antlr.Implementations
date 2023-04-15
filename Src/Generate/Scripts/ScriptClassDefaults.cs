using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace Generate.Scripts
{

    public class ScriptClassDefaults : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstBnfRule", ast, context);
        }

        public override string StrategyTemplateKey => "_";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")

                .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), ast =>
                {
                    ctx.Variables["combinaisons"] = ast.ResolveAllCombinations();

                }, (ast, type) =>
                {

                    var item =
                    type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                        .Documentation(c => c.Summary(() => ast.ToString()))
                        .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                        .Attribute(ctx.Variables.Get<List<TreeRuleItem>>("combinaisons").Count == 1 ? System.Reflection.TypeAttributes.Public : System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Abstract)
                        .Inherit(() => GetInherit(ast, ctx))

                        .Ctor((f) =>
                               {

                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ParserRuleContext", "ctx")
                                    .CallBase("ctx");

                                   var alternatives = ctx.Variables.Get<List<TreeRuleItem>>("combinaisons");
                                   if (alternatives.Count == 1)
                                       f.Argument(() => "List<AstRoot>", "list");

                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "Position", "p")
                                    .CallBase("p");

                                   var alternatives = ctx.Variables.Get<List<TreeRuleItem>>("combinaisons");
                                   if (alternatives.Count == 1)
                                       f.Argument(() => "List<AstRoot>", "list");


                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("Position.Default");
                               })



                        .MethodWhen(() => ctx.Variables.Get<List<TreeRuleItem>>("combinaisons").Count == 1, method =>
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
                                     "Visit" + CodeHelper.FormatCsharp(ast.Name.Text),
                                     CodeHelper.This()
                                 );
                             });
                        })

                        .MethodWhen(() => ctx.Variables.Get<List<TreeRuleItem>>("combinaisons").Count > 1, method =>
                        {
                            method
                             .Name(g => "Create")
                             .Argument("ParserRuleContext", "ctx")
                             .Argument("List<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                             .Body(b =>
                             {

                                 b.Statements.DeclareAndInitialize("index", typeof(int).AsType(), CodeHelper.Call(("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType()
                                     , "Resolve", "list".Var()));

                                 List<TreeRuleItem> items = ctx.Variables.Get<List<TreeRuleItem>>("combinaisons");
                                 int j = 1;
                                 foreach (var alternative in items)
                                 {
                                     int i = 0;
                                     List<CodeExpression> args = new List<CodeExpression>()
                                     {
                                         "ctx".Var()
                                     };
                                     if (alternative.Count == 0)
                                     {
                                         if (alternative.WhereRuleOrIdentifiers())
                                             args.Add("list".Indexer(i.AsConstant()).Cast(alternative.Type().AsType()));
                                     }
                                     else
                                     {
                                         foreach (var item in alternative)
                                             if (item.WhereRuleOrIdentifiers())
                                             {
                                                 args.Add("list".Indexer(i.AsConstant()).Cast(item.Type().AsType()));
                                                 i++;
                                             }
                                     }


                                     b.Statements.If("index".Var().IsEqual((i + 1).AsConstant()), _t =>
                                     {
                                         string typename = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                         var t = typename + "." + typename + (j).ToString();
                                         _t.Return(CodeHelper.Create(t.AsType(), args.ToArray()));
                                     });
                                     j++;

                                 }
                                 b.Statements.Return(CodeHelper.Null());

                             });
                        })

                        .MethodWhen(() => ctx.Variables.Get<List<TreeRuleItem>>("combinaisons").Count > 1, method =>
                        {
                            method
                             .Name(g => "Resolve")
                             .Argument("List<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => typeof(int))
                             .Body(b =>
                             {

                                 List<List<(string, bool, bool)>> alternatives = new List<List<(string, bool, bool)>>(ast.Alternatives.Count);
                                 List<TreeRuleItem> items = ctx.Variables.Get<List<TreeRuleItem>>("combinaisons");

                                 foreach (var alternative in items)
                                 {
                                     var l = new List<(string, bool, bool)>(alternative.Count + 1);

                                     if (alternative.Count == 0)
                                     {
                                         var occurence = alternative.Occurence;
                                         if (alternative.WhereRuleOrIdentifiers())
                                         {
                                             var type = alternative.Type();
                                             l.Add((type, occurence.Optional, occurence.Value == OccurenceEnum.Any));
                                         }
                                         else
                                         {
                                             //l.Add(("null", occurence.Optional, occurence.Value == OccurenceEnum.Any));
                                         }
                                     }
                                     else
                                     {
                                         foreach (TreeRuleItem item in alternative)
                                         {
                                             var occurence = alternative.Occurence;
                                             if (item.WhereRuleOrIdentifiers())
                                             {
                                                 var type = item.Type();                                                 
                                                 l.Add((type, occurence.Optional, occurence.Value == OccurenceEnum.Any));
                                             }
                                             else
                                             {
                                                // l.Add(("object", occurence.Optional, occurence.Value == OccurenceEnum.Any));
                                             }
                                         }
                                     }
                                     if (l.Count > 0)
                                         alternatives.Add(l);

                                 }

                                 foreach (var alternative in alternatives.OrderByDescending(c => c.Count).GroupBy(c => c.Count))
                                 {

                                     var listCount = "list".Var().Property("Count");
                                     var constant = alternative.Key.AsConstant();

                                     b.Statements.If(CodeHelper.IsEqual(listCount, constant), _t =>
                                     {

                                         foreach (var rule in alternative)
                                         {

                                             var nt = _t;
                                             int i = 0;

                                             foreach (var item2 in rule)
                                             {

                                                 nt.If("AstRoot".AsType().Call("Eval",
                                                     "list".Indexer(i.AsConstant()),
                                                     item2.Item1.AsType().Typeof(),
                                                     item2.Item2.AsConstant(),
                                                     item2.Item3.AsConstant()), _t2 =>
                                                 {
                                                     nt = _t2;
                                                 });

                                                 i++;

                                             }

                                             nt.Return((alternatives.IndexOf(rule) + 1).AsConstant());

                                         }

                                     });



                                 }

                                 b.Statements.Return(CodeHelper.AsConstant(0));


                             });
                        })

                        .Field(field =>
                        {
                            field.Name("_rule")
                            .Type(typeof(string))
                            .Attribute(MemberAttributes.Family | MemberAttributes.Static)
                            .Value((a) =>
                            {
                                return ast.ToString();
                            })
                            ;
                        })

                        .Make(t =>
                        {

                            HashSet<string> _h = new HashSet<string>();
                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                            int i = 0;
                            foreach (AstLabeledAlt alternative in ast.Alternatives)
                            {

                                i++;
                                var allCombinations = alternative.ResolveAllCombinations();

                                foreach (TreeRuleItem alt in allCombinations)
                                {

                                    StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                    var name = CodeHelper.FormatCsharp(ast.Name.Text);
                                    var tname = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                    var tname2 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                    if (ast.Alternatives.Count > 1)
                                    {
                                        tname2 = tname + "." + tname + (i).ToString();
                                    }
                                    var t1 = tname.AsType();
                                    var t3 = tname2.AsType();
                                    List<string> arguments = new List<string>();

                                    var method1 = name.AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                        .BuildDocumentation(ast.Name.Text, alt, ctx)                                        
                                        ;
                                    method1.Parameters.Add(new CodeParameterDeclarationExpression("ParserRuleContext".AsType(), "ctx"));

                                    if (alt.Count > 0)
                                        foreach (var itemAlt in alt)
                                            itemAlt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);
                                    else
                                        alt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);

                                    if (method1.Parameters.Count > 0)
                                    {

                                        var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                        if (_h.Add(noDuplicateKey))
                                        {
                                            methods.Add(method1);
                                            List<CodeExpression> _argu = new List<CodeExpression>()
                                            {
                                                "ctx".Var()
                                            };

                                            foreach (var item in arguments)
                                                _argu.Add(item.Var());

                                            method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t3, _argu.ToArray()));
                                            method1.Statements.Return("result".Var());
                                        }

                                    }

                                }

                            }

                            foreach (var item in methods)
                            {
                                t.Members.Add(item);
                            }

                        })
                        ;


                    List<TreeRuleItem> alternatives = ctx.Variables.Get<List<TreeRuleItem>>("combinaisons");

                    if (alternatives.Count == 0)
                    {

                    }
                    else if (alternatives.Count > 1)
                    {
                        int i = 0;
                        foreach (var ast2 in alternatives)
                        {

                            type.CreateTypeFrom<AstBase>(ast => true, null, (ast3, type2) =>
                            {
                                type2.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + (++i).ToString())
                                .Documentation(c => c.Summary(() => ast.Name.Text + " : " + ast2.ToString()))
                                .Inherit(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))

                                .Ctor((f) =>
                                {
                                    f.Attribute(MemberAttributes.FamilyAndAssembly)
                                     .Argument(() => "ParserRuleContext", "ctx")
                                     .CallBase("ctx");

                                    if (ast.Name.Text == "goto_statement")
                                    {

                                    }

                                    if (ast2.Count == 0)
                                    {
                                        if (ast2.WhereRuleOrIdentifiers())
                                            f.Argument(() => ast2.Type(), ast2.GetParameterdName());
                                    }
                                    else
                                        foreach (TreeRuleItem item in ast2)
                                            if (item.WhereRuleOrIdentifiers())
                                                f.Argument(() => item.Type(), item.GetParameterdName());

                                    f.Body(b =>
                                    {
                                        if (ast2.Count == 0)
                                        {
                                            if (ast2.WhereRuleOrIdentifiers())
                                                b.Statements.Assign(ast2.GetFieldName().Var(), ast2.GetParameterdName().Var());
                                        }
                                        else
                                            foreach (TreeRuleItem item in ast2)
                                                if (item.WhereRuleOrIdentifiers())
                                                    b.Statements.Assign(item.GetFieldName().Var(), item.GetParameterdName().Var());
                                    });

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
                                             "Visit" + CodeHelper.FormatCsharp(ast.Name.Text),
                                             CodeHelper.This()
                                         );
                                     });
                                })

                                .Fields(() =>
                                      {
                                          List<TreeRuleItem> items = new List<TreeRuleItem>();

                                          if (ast2.Count == 0)
                                          {
                                              if (ast2.WhereRuleOrIdentifiers())
                                                  items.Add(ast2);
                                          }
                                          else
                                              foreach (TreeRuleItem item in ast2)
                                                  if (item.WhereRuleOrIdentifiers())
                                                      items.Add(item);

                                          return items;

                                      },
                                      (field, model) =>
                                      {
                                          var i = model as TreeRuleItem;
                                          if (i.WhereRuleOrIdentifiers())
                                          {
                                              field.Name(m => i.GetFieldName())
                                              .Type(() => i.Type())
                                              .Attribute(MemberAttributes.Private)
                                              ;
                                          }
                                          else
                                          {

                                          }
                                      }
                                )
                                .Properties(() =>
                                {
                                    List<TreeRuleItem> items = new List<TreeRuleItem>();

                                    if (ast2.Count == 0)
                                    {
                                        if (ast2.WhereRuleOrIdentifiers())
                                            items.Add(ast2);
                                    }
                                    else
                                        foreach (TreeRuleItem item in ast2)
                                            if (item.WhereRuleOrIdentifiers())
                                                items.Add(item);

                                    return items;
                                },
                                      (property, model) =>
                                      {
                                          var i = model as TreeRuleItem;
                                          if (i.WhereRuleOrIdentifiers())
                                          {
                                              property.Name(m => i.GetPropertyName())
                                              .Type(() => i.Type())
                                              .Attribute(MemberAttributes.Public)
                                              .Get(g => g.Return(i.GetFieldName().Var()))
                                              ;
                                          }
                                          else
                                          {

                                          }
                                      }
                                )
                                ;

                            });
                        }
                    }

                })

                .CreateTypeFrom<AstLabeledAlt>(null, null, (ast, type) =>
                {
                    type.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
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
                                     "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text),
                                     CodeHelper.This()
                                 );
                             });
                        });
                });

                });

            });

        }


    }


}
