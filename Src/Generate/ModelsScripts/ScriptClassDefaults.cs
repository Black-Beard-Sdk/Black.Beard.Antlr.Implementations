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
using Antlr4.Runtime.Misc;
using System.Data;

namespace Generate.ModelsScripts
{

    public class ScriptClassDefaults : ScriptBase
    {

        private HashSet<string> _keys = new HashSet<string> { "_" };

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstBnfRule", ast, context);
        }

        public override HashSet<string> StrategyTemplateKeys => _keys;

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
                        .Attribute(ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1 ? System.Reflection.TypeAttributes.Public : System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Abstract)
                        .Inherit(() => GetInherit(ast, ctx))

                        .Ctor((f) =>
                               {

                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ParserRuleContext", "ctx")
                                    .CallBase("ctx");

                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "Position", "p")
                                    .CallBase("p");

                                   var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                                   if (alternatives.Count == 1)
                                   {
                                       f.Argument(() => "AstRootList<AstRoot>", "list")

                                       .Body(b =>
                                        {

                                            var items = ast.GetProperties();

                                            b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                            {

                                                foreach (var item in items)
                                                {

                                                    var fieldName = (item as AstBase).GetFieldName();

                                                    var type = (item as AstBase).Type();
                                                    var ty = new CodeTypeReference(type);
                                                    stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                                    {
                                                        t.Assign(CodeHelper.This().Field(fieldName), CodeHelper.Var("enumerator.Current").Cast(ty));
                                                    });


                                                }

                                            });

                                        });
                                   }

                               })
                        .CtorWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, (f) =>
                        {
                            f.Attribute(MemberAttributes.FamilyAndAssembly)
                             .Argument(() => "AstRootList<AstRoot>", "list")
                             .CallBase("Position.Default")
                             .Body(b =>
                             {

                                 //if (ast.Name.Text == "exist_call")
                                 //{ }

                                 var items = ast.GetProperties();

                                 b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                 {

                                     foreach (var item in items)
                                     {

                                         var fieldName = (item as AstBase).GetFieldName();

                                         var type = (item as AstBase).Type();
                                         var ty = new CodeTypeReference(type);
                                         stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                         {
                                             t.Assign(CodeHelper.This().Field(fieldName), CodeHelper.Var("enumerator.Current").Cast(ty));
                                          });


                                     }

                                 });

                             })
                             ;
                        })

                        .CtorWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, (f) =>
                        {
                            f.Attribute(MemberAttributes.FamilyAndAssembly)
                             .Argument(() => "ParserRuleContext", "ctx")
                             .CallBase("ctx");

                            var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                            var ast2 = alternatives[0];

                            foreach (TreeRuleItem item in ast2.Item)
                                if (item.WhereRuleOrIdentifiers())
                                    f.Argument(() => item.Type(), item.GetParameterdName());

                            f.Body(b =>
                            {
                                foreach (TreeRuleItem item in ast2.Item)
                                    if (item.WhereRuleOrIdentifiers())
                                        b.Statements.Assign(item.GetFieldName().Var(), item.GetParameterdName().Var());
                            });

                        })

                        .CtorWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, (f) =>
                        {
                            f.Attribute(MemberAttributes.FamilyAndAssembly)
                             .Argument(() => "Position", "position")
                             .CallBase("position");

                            var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                            var ast2 = alternatives[0];

                            foreach (TreeRuleItem item in ast2.Item)
                                if (item.WhereRuleOrIdentifiers())
                                    f.Argument(() => item.Type(), item.GetParameterdName());

                            f.Body(b =>
                            {
                                foreach (TreeRuleItem item in ast2.Item)
                                    if (item.WhereRuleOrIdentifiers())
                                        b.Statements.Assign(item.GetFieldName().Var(), item.GetParameterdName().Var());
                            });

                        })

                        .CtorWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, (f) =>
                        {
                            f.Attribute(MemberAttributes.FamilyAndAssembly)
                             .CallBase("Position.Default");

                            var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                            var ast2 = alternatives[0];

                            foreach (TreeRuleItem item in ast2.Item)
                                if (item.WhereRuleOrIdentifiers())
                                    f.Argument(() => item.Type(), item.GetParameterdName());

                            f.Body(b =>
                            {
                                foreach (TreeRuleItem item in ast2.Item)
                                    if (item.WhereRuleOrIdentifiers())
                                        b.Statements.Assign(item.GetFieldName().Var(), item.GetParameterdName().Var());
                            });

                        })


                        .MethodWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, method =>
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

                        .MethodWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, method =>
                        {
                            method
                             .Name(g => "Create")
                             .Argument("ParserRuleContext", "ctx")
                             .Argument("AstRootList<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                             .Body(b =>
                             {

                                 AlternativeTreeRuleItemList items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                                 foreach (var alt in items)
                                 {
                                     var alternative = alt.Item;

                                     int i = 0;
                                     List<CodeExpression> args = new List<CodeExpression>()
                                     {
                                        "ctx".Var()
                                     };
                                     foreach (var item in alternative)
                                         if (item.WhereRuleOrIdentifiers())
                                         {
                                             var c = "list".Var().Call("Get", new string[] { item.Type() }, i.AsConstant());
                                             args.Add(c);
                                             i++;
                                         }

                                     string typename = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                     b.Statements.Return(CodeHelper.Create(typename.AsType(), args.ToArray()));

                                 }


                             });
                        })

                        .MethodWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, method =>
                        {
                            method
                             .Name(g => "Create")
                             .Argument("AstRootList<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                             .Body(b =>
                             {

                                 AlternativeTreeRuleItemList items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                                 foreach (var alt in items)
                                 {
                                     var alternative = alt.Item;

                                     int i = 0;
                                     List<CodeExpression> args = new List<CodeExpression>()
                                     {
                                     };
                                     foreach (var item in alternative)
                                         if (item.WhereRuleOrIdentifiers())
                                         {
                                             var c = "list".Var().Call("Get", new string[] { item.Type() }, i.AsConstant());
                                             args.Add(c);
                                             i++;
                                         }

                                     string typename = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                     b.Statements.Return(CodeHelper.Create(typename.AsType(), args.ToArray()));

                                 }


                             });
                        })


                        .MethodWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count > 1, method =>
                        {
                            method
                             .Name(g => "Create")
                             .Argument("ParserRuleContext", "ctx")
                             .Argument("AstRootList<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                             .Body(b =>
                             {

                                 b.Statements.DeclareAndInitialize("index", typeof(int).AsType(), "_ruleevaluator".Var().Call("Evaluate", "list".Var()));

                                 AlternativeTreeRuleItemList items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                                 foreach (var alt in items)
                                 {

                                     var alternative = alt.Item;
                                     List<CodeExpression> args = new List<CodeExpression>()
                                     {
                                         "ctx".Var()
                                     };
                                     int i = 0;
                                     foreach (var item in alternative)
                                     {
                                         if (item.WhereRuleOrIdentifiers())
                                         {
                                             var c = "list".Var().Call("Get", new string[] { item.Type() }, i.AsConstant());
                                             args.Add(c);
                                             i++;
                                         }
                                     }

                                     b.Statements.If("index".Var().IsEqual(alt.AlternativeIdentifier.AsConstant()), _t =>
                                     {
                                         string typename = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                         var t = typename + "." + typename + (alt.AlternativeIdentifier).ToString();
                                         _t.Return(CodeHelper.Create(t.AsType(), args.ToArray()));
                                     });


                                 }
                                 b.Statements.Return(CodeHelper.Null());

                             });
                        })

                        .MethodWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count > 1, method =>
                        {
                            method
                             .Name(g => "Create")
                             .Argument("AstRootList<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                             .Body(b =>
                             {

                                 b.Statements.DeclareAndInitialize("index", typeof(int).AsType(), "_ruleevaluator".Var().Call("Evaluate", "list".Var()));

                                 AlternativeTreeRuleItemList items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                                 foreach (var alt in items)
                                 {

                                     var alternative = alt.Item;
                                     List<CodeExpression> args = new List<CodeExpression>()
                                     {
                                        //"ctx".Var()
                                     };
                                     int i = 0;
                                     foreach (var item in alternative)
                                     {
                                         if (item.WhereRuleOrIdentifiers())
                                         {
                                             var c = "list".Var().Call("Get", new string[] { item.Type() }, i.AsConstant());
                                             args.Add(c);
                                             i++;
                                         }
                                     }


                                     b.Statements.If("index".Var().IsEqual(alt.AlternativeIdentifier.AsConstant()), _t =>
                                     {
                                         string typename = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                         var t = typename + "." + typename + (alt.AlternativeIdentifier).ToString();
                                         _t.Return(CodeHelper.Create(t.AsType(), args.ToArray()));
                                     });


                                 }
                                 b.Statements.Return(CodeHelper.Null());

                             });
                        })


                        .Field(field =>
                        {

                            field.Name("_ruleevaluator")
                            .Type(typeof(AstRuleEvaluator))
                            .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                            .Value((a) =>
                            {

                                List<List<(string, bool, bool, int, string)>> alternatives = new List<List<(string, bool, bool, int, string)>>(ast.Alternatives.Count);
                                AlternativeTreeRuleItemList items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                                foreach (var alt in items)
                                {

                                    var alternative = alt.Item;
                                    var l = new List<(string, bool, bool, int, string)>(alternative.Count + 1);


                                    foreach (TreeRuleItem item in alternative)
                                    {
                                        var occurence = item.Occurence;
                                        if (item.WhereRuleOrIdentifiers())
                                        {
                                            var type = item.Type();
                                            l.Add((type, occurence.Optional, occurence.Value == OccurenceEnum.Any, alt.AlternativeIdentifier, item.Name.Trim()));
                                        }
                                        else
                                        {
                                            // l.Add(("object", occurence.Optional, occurence.Value == OccurenceEnum.Any));
                                        }
                                    }

                                    if (l.Count > 0)
                                        alternatives.Add(l);

                                }

                                List<CodeExpression> args = new List<CodeExpression>();

                                var lst = alternatives.OrderByDescending(c => c.Count).ToList();
                                foreach (var alternative in lst)
                                {
                                    List<CodeExpression> arg2 = new List<CodeExpression>();
                                    int index = 0;
                                    foreach (var rule in alternative)
                                    {
                                        index = rule.Item4;
                                        var r = typeof(AstRuleMatcherItem).AsType()
                                        .Create
                                        (
                                            rule.Item1.AsType().Typeof(),
                                                 rule.Item2.AsConstant(),
                                                 rule.Item3.AsConstant(),
                                                 rule.Item5.AsConstant()
                                        );
                                        arg2.Add(r);
                                    }
                                    arg2.Insert(0, index.AsConstant());
                                    args.Add(typeof(AstRuleMatcherItems).AsType().Create(arg2.ToArray()));
                                }
                                var arg1 = typeof(AstRuleMatcherList).AsType().Create(args.ToArray());
                                return typeof(AstRuleEvaluator).AsType().Create(arg1);
                            })
                            ;
                        })


                        .Fields(() =>
                        {
                            var result = new AlternativeTreeRuleItemList(1);
                            var items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                            if (items.Count == 1)
                                foreach (var item in items[0].Item)
                                    result.Add(item);

                            return result;

                        },
                        (field, model) =>
                        {
                            var i = (model as AlternativeTreeRuleItem).Item;
                            if (i.WhereRuleOrIdentifiers())
                            {
                                field.Name(m => i.GetFieldName())
                                .Type(() => i.Type())
                                .Attribute(MemberAttributes.Private)
                                ;
                            }
                        })

                        .Properties(() =>
                        {
                            var result = new AlternativeTreeRuleItemList(1);
                            var items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                            if (items.Count == 1)
                                foreach (var item in items[0].Item)
                                    result.Add(item);

                            return result;

                        },
                        (property, model) =>
                        {
                            var i = (model as AlternativeTreeRuleItem).Item;
                            if (i.WhereRuleOrIdentifiers())
                            {
                                property.Name(m => i.GetPropertyName())
                                        .Type(() => i.Type())
                                        .Attribute(MemberAttributes.Public)
                                        .Get(g => g.Return(i.GetFieldName().Var()))
                                        .HasSet(false)
                                ;
                            }
                        })

                        .Field(field =>
                        {

                            field.Name("_ruleValue")
                            .Type(typeof(string))
                            .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                            .Value((a) =>
                            {
                                return ast.Alternatives.ToString().Trim();
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

                        .Make(t =>
                        {

                            HashSet<string> _h = new HashSet<string>();
                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();
                            var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                            int i = 0;
                            foreach (var alt in alternatives)
                            {
                                var alternative = alt.Item;
                                i++;

                                StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                var name = CodeHelper.FormatCsharp(ast.Name.Text);
                                var tname = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                var tname2 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                if (alternatives.Count > 1)
                                {
                                    tname2 = tname + "." + tname + (i).ToString();
                                }
                                var t1 = tname.AsType();
                                var t3 = tname2.AsType();
                                List<string> arguments = new List<string>();

                                var method1 = "New".AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                    .BuildDocumentation(ast.Name.Text, alternative, ctx)
                                    ;
                                method1.Parameters.Add(new CodeParameterDeclarationExpression("ParserRuleContext".AsType(), "ctx"));

                                if (alternative.Count > 0)
                                    foreach (var itemAlt in alternative)
                                        itemAlt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);
                                else
                                    alternative.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);

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

                                        if (alternatives.Count == 1)
                                        {
                                            method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t3, _argu.ToArray()));
                                        }
                                        else
                                        {
                                            string typename = ast.Type();
                                            var t2 = typename + "." + typename + (i).ToString();
                                            method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t2.AsType(), _argu.ToArray()));


                                        }

                                        method1.Statements.Return("result".Var());
                                    }

                                }

                            }

                            foreach (var item in methods)
                            {
                                t.Members.Add(item);
                            }
                        })

                        .Make(t =>
                        {

                            HashSet<string> _h = new HashSet<string>();
                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();
                            var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                            int i = 0;
                            foreach (var alt in alternatives)
                            {
                                var alternative = alt.Item;
                                i++;

                                StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                var name = CodeHelper.FormatCsharp(ast.Name.Text);
                                var tname = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                var tname2 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                if (alternatives.Count > 1)
                                {
                                    tname2 = tname + "." + tname + (i).ToString();
                                }
                                var t1 = tname.AsType();
                                var t3 = tname2.AsType();
                                List<string> arguments = new List<string>();

                                var method1 = "New".AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                    .BuildDocumentation(ast.Name.Text, alternative, ctx)
                                    ;

                                if (alternative.Count > 0)
                                    foreach (var itemAlt in alternative)
                                        itemAlt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);
                                else
                                    alternative.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);

                                if (method1.Parameters.Count > 0)
                                {

                                    var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                    if (_h.Add(noDuplicateKey))
                                    {
                                        methods.Add(method1);
                                        List<CodeExpression> _argu = new List<CodeExpression>();

                                        foreach (var item in arguments)
                                            _argu.Add(item.Var());

                                        if (alternatives.Count == 1)
                                        {
                                            method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t3, _argu.ToArray()));
                                        }
                                        else
                                        {
                                            string typename = ast.Type();
                                            var t2 = typename + "." + typename + (i).ToString();
                                            method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t2.AsType(), _argu.ToArray()));


                                        }

                                        method1.Statements.Return("result".Var());
                                    }

                                }

                            }

                            foreach (var item in methods)
                            {
                                t.Members.Add(item);
                            }
                        })

                        .Method(method =>
                        {
                            var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                            method
                             .Name(g => "Null")
                             .Return(() => type + "?")
                             .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                             .Body(b => b.Statements.Return(CodeHelper.Null()))
                             ;

                        })

                        .MethodWhen(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons").Count == 1, method =>
                        {
                            var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                            method.Name(g => "ToString")
                                  .Argument("Writer", "writer")
                                  .Argument("StrategySerializationItem", "strategy")
                                  .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                  .Return(() => typeof(bool))
                                  .Body(b =>
                                  {
                                      GenerateClassDefaultToString.Generate(ast, b.Statements);
                                  })
                             ;

                        })
                        ;


                    AlternativeTreeRuleItemList alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                    if (alternatives.Count == 0)
                    {

                    }
                    else if (alternatives.Count > 1)
                    {
                        int i = 0;
                        foreach (var alt2 in alternatives)
                        {

                            var alternative2 = alt2.Item;

                            type.CreateTypeFrom<AstBase>(ast => true, null, (ast3, type2) =>
                            {
                                type2.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + (++i).ToString())
                                .Documentation(c => c.Summary(() => ast.Name.Text + " : " + alternative2.ToString()))
                                .Inherit(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))

                                .Ctor((f) =>
                                {

                                    f.Attribute(MemberAttributes.FamilyAndAssembly)
                                     .Argument(() => "ParserRuleContext", "ctx")
                                     .CallBase("ctx");

                                    if (alternative2.Count == 0)
                                    {
                                        if (alternative2.WhereRuleOrIdentifiers())
                                            f.Argument(() => alternative2.Type(), alternative2.GetParameterdName());
                                    }
                                    else
                                        foreach (TreeRuleItem item in alternative2)
                                            if (item.WhereRuleOrIdentifiers())
                                                f.Argument(() => item.Type(), item.GetParameterdName());

                                    f.Body(b =>
                                    {
                                        if (alternative2.Count == 0)
                                        {
                                            if (alternative2.WhereRuleOrIdentifiers())
                                                b.Statements.Assign(alternative2.GetFieldName().Var(), alternative2.GetParameterdName().Var());
                                        }
                                        else
                                            foreach (TreeRuleItem item in alternative2)
                                                if (item.WhereRuleOrIdentifiers())
                                                    b.Statements.Assign(item.GetFieldName().Var(), item.GetParameterdName().Var());
                                    });

                                })

                                .Ctor((f) =>
                                {

                                    f.Attribute(MemberAttributes.FamilyAndAssembly)
                                     .Argument(() => "Position", "position")
                                     .CallBase("position");

                                    if (alternative2.Count == 0)
                                    {
                                        if (alternative2.WhereRuleOrIdentifiers())
                                            f.Argument(() => alternative2.Type(), alternative2.GetParameterdName());
                                    }
                                    else
                                        foreach (TreeRuleItem item in alternative2)
                                            if (item.WhereRuleOrIdentifiers())
                                                f.Argument(() => item.Type(), item.GetParameterdName());

                                    f.Body(b =>
                                    {
                                        if (alternative2.Count == 0)
                                        {
                                            if (alternative2.WhereRuleOrIdentifiers())
                                                b.Statements.Assign(alternative2.GetFieldName().Var(), alternative2.GetParameterdName().Var());
                                        }
                                        else
                                            foreach (TreeRuleItem item in alternative2)
                                                if (item.WhereRuleOrIdentifiers())
                                                    b.Statements.Assign(item.GetFieldName().Var(), item.GetParameterdName().Var());
                                    });

                                })

                                .Ctor((f) =>
                                {

                                    f.Attribute(MemberAttributes.FamilyAndAssembly)
                                     .CallBase("Position.Default");

                                    if (alternative2.Count == 0)
                                    {
                                        if (alternative2.WhereRuleOrIdentifiers())
                                            f.Argument(() => alternative2.Type(), alternative2.GetParameterdName());
                                    }
                                    else
                                        foreach (TreeRuleItem item in alternative2)
                                            if (item.WhereRuleOrIdentifiers())
                                                f.Argument(() => item.Type(), item.GetParameterdName());

                                    f.Body(b =>
                                    {
                                        if (alternative2.Count == 0)
                                        {
                                            if (alternative2.WhereRuleOrIdentifiers())
                                                b.Statements.Assign(alternative2.GetFieldName().Var(), alternative2.GetParameterdName().Var());
                                        }
                                        else
                                            foreach (TreeRuleItem item in alternative2)
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

                                .Field(field =>
                                {
                                    field.Name("_ruleName1")
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
                                            .Get((a) => a.Return("_ruleName1".Var()))
                                            .HasSet(false)
                                            ;
                                })

                                .Fields(() =>
                                      {
                                          AlternativeTreeRuleItemList items = new AlternativeTreeRuleItemList();

                                          if (alternative2.Count == 0)
                                          {
                                              if (alternative2.WhereRuleOrIdentifiers())
                                                  items.Add(alternative2);
                                          }
                                          else
                                              foreach (TreeRuleItem item in alternative2)
                                                  if (item.WhereRuleOrIdentifiers())
                                                      items.Add(item);

                                          return items;

                                      },
                                      (field, model) =>
                                      {
                                          var i = (model as AlternativeTreeRuleItem).Item;
                                          if (i.WhereRuleOrIdentifiers())
                                          {
                                              field.Name(m => i.GetFieldName())
                                              .Type(() => i.Type())
                                              .Attribute(MemberAttributes.Private)
                                              ;
                                          }
                                      }
                                )
                                .Properties(() =>
                                {
                                    AlternativeTreeRuleItemList items = new AlternativeTreeRuleItemList();

                                    if (alternative2.Count == 0)
                                    {
                                        if (alternative2.WhereRuleOrIdentifiers())
                                            items.Add(alternative2);
                                    }
                                    else
                                        foreach (TreeRuleItem item in alternative2)
                                            if (item.WhereRuleOrIdentifiers())
                                                items.Add(item);

                                    return items;
                                },
                                      (property, model) =>
                                      {
                                          var i = (model as AlternativeTreeRuleItem).Item;
                                          if (i.WhereRuleOrIdentifiers())
                                          {
                                              property.Name(m => i.GetPropertyName())
                                              .Type(() => i.Type())
                                              .Attribute(MemberAttributes.Public)
                                              .Get(g => g.Return(i.GetFieldName().Var()))
                                              .HasSet(false)
                                              ;
                                          }
                                      }
                                )

                                .Method(method =>
                                {
                                    var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                    method.Name(g => "ToString")
                                          .Argument("Writer", "writer")
                                          .Argument("StrategySerializationItem", "strategy")
                                          .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                          .Return(() => typeof(bool))
                                          .Body(b =>
                                          {
                                              // GenerateToStringForClassProperties.Generate(ast, alt2, b.Statements);
                                              b.Statements.Return(CodeHelper.AsConstant(true));

                                          })
                                     ;

                                })

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
                             .Argument(() => "AstRootList<AstRoot>", "list")
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

        private static void Generate(Context ctx, AstRule ast, CodeTypeDeclaration t, bool withCtx = false)
        {

            HashSet<string> _h = new HashSet<string>();
            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();
            var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

            int i = 0;
            foreach (var alt1 in alternatives)
            {

                var i2 = alt1.ResolveAllCombinationsWithoutOptional();
                var i3 = i2.RemoveNonDynamic();

                foreach (var alt in i3)
                {

                    var alternative = alt.Item;
                    i++;

                    StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                    var name = CodeHelper.FormatCsharp(ast.Name.Text);
                    var tname = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                    var tname2 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                    if (alternatives.Count > 1)
                    {
                        tname2 = tname + "." + tname + (i).ToString();
                    }
                    var t1 = tname.AsType();
                    var t3 = tname2.AsType();
                    List<string> arguments = new List<string>();

                    var method1 = "New".AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                        .BuildDocumentation(ast.Name.Text, alternative, ctx)
                        ;

                    if (withCtx)
                        method1.Parameters.Add(new CodeParameterDeclarationExpression("ParserRuleContext".AsType(), "ctx"));

                    if (alternative.Count > 0)
                        foreach (var itemAlt in alternative)
                            itemAlt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);
                    else
                        alternative.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);

                    if (method1.Parameters.Count > 0)
                    {

                        var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                        if (_h.Add(noDuplicateKey))
                        {

                            methods.Add(method1);
                            List<CodeExpression> _argu = new List<CodeExpression>();

                            if (withCtx)
                            {
                                _argu.Add("ctx".Var());
                            }

                            foreach (var item in arguments)
                                _argu.Add(item.Var());

                            if (alternatives.Count == 1)
                            {
                                method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t3, _argu.ToArray()));
                            }
                            else
                            {
                                string typename = ast.Type();
                                var t2 = typename + "." + typename + (i).ToString();
                                method1.Statements.Add(CodeHelper.DeclareAndCreate("result", t2.AsType(), _argu.ToArray()));
                            }

                            method1.Statements.Return("result".Var());

                        }

                    }

                }

            }

            foreach (var item in methods)
                if (!CodedomHelper.MemberExists(t.Members, item))
                    t.Members.Add(item);

        }



    }


}
