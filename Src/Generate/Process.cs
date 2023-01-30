using Bb;
using Bb.Asts;
using Bb.Configurations;
using Bb.Generators;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generate
{

    public class Process
    {

        public void Run(FileInfo antlrParser, Context ctx)
        {

            if (antlrParser.Exists)
            {

                this._folder = antlrParser.Directory.FullName;

                var sb = new StringBuilder(antlrParser.LoadFromFile());

                var parser = ScriptParser.ParseString(sb, antlrParser.FullName);


                var visitor = new ScriptAntlrVisitor(parser.Parser, new Diagnostics(), this._folder);
                var ast = parser.Visit(visitor);


                var visitor2 = new ParentVisitor();
                visitor2.Visit(ast);


                var visitor3 = new CodeVisitor();
                visitor3.Visit(ast);

                Generate(ctx, ast);

            }

        }


        private static string TemplateSelector(AstRule ast, Context context)
        {

            if (ast.RuleName.Text == "binary_content_nexts")
            {
                if (ast.RuleBlock.Count == 1)
                {
                    var r = ast.RuleBlock[0].Rule.Rule;
                    if (r.Count == 1)
                    {
                        var o = r[0];
                        switch (o.Type)
                        {

                            case "AstAtom":
                                var i = o as AstAtom;
                                var p = i.Occurence;
                                switch (p)
                                {
                                    case OccurenceEnum.OneOrMore:
                                    case OccurenceEnum.OneOrMoreOptional:
                                    case OccurenceEnum.Any:
                                    case OccurenceEnum.AnyOptional:
                                        return "ClassList";

                                    case OccurenceEnum.One:
                                    case OccurenceEnum.OneOptional:
                                    default:
                                        break;
                                }

                                break;

                            case "AstlabeledElement":
                                break;

                            case "AstBlock":
                                break;

                            case "AstArgActionBlock":
                                break;

                            default:
                                break;
                        }

                    }
                    else
                    {

                    }

                }
            }

            //if (!string.IsNullOrEmpty(context.CurrentConfiguration.Strategy))
            //    return context.CurrentConfiguration.Strategy;


            if (ast.OutputContainsAlwayOneTerminal
                  && ast.OutputContainsAlwayOneItem
                  && ast.GetTerminals().Count() > 1
                  && !ast.GetTerminals().Where(c => context.TerminalsToExcludes.Contains(c.Text)).Any()
                  && !ast.GetTerminals().Where(c => context.Identifiers.Contains(c.Text)).Any()
               )
                return "ClassEnum";

            if (ast.ContainsJustOneAlternative)
            {

                if (ast.ContainsOnlyTerminals
                      && ast.OutputContainsAlwayOneItem
                      && ast.GetTerminals().Count() > 1
                      && !ast.GetTerminals().Where(c => context.TerminalsToExcludes.Contains(c.Text)).Any()
                      && !ast.GetTerminals().Where(c => context.Identifiers.Contains(c.Text)).Any()
                   )
                    return "ClassEnum";

                bool t = false;
                foreach (var item in ast.GetAlternatives())
                    foreach (var item2 in item.Where(c => c.IsRuleReference).ToList())
                    {
                        t = true;
                        continue;
                    }

                if (t)
                    return "ClassWithProperties";

            }

            return string.Empty;

        }

        private static string GetInherit(AstRule ast, Context context)
        {
            switch (context.Strategy)
            {
                case "ClassEnum":
                    return "AstTerminalEnum<Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text) + "Enum>";

                case "ClassList":
                    var astChild = ast.GetRules().FirstOrDefault();
                    return "AstRuleList<Ast" + CodeHelper.FormatCsharp(astChild.Identifier.Text) + ">";

                default:
                    break;
            }

            return "AstRule";

        }

        private static bool Generate(AstRule ast, Context context)
        {

            //if (ast.OutputContainsAlwayOneRule)
            //    return false;

            return true;
        }

        private static void Generate(Context ctx, AstBase ast)
        {

            var visitor4 = new CodeGeneratorVisitor(ctx)


                .Add("Enums", template =>
                {

                    template.Namespace("Bb.Asts", ns =>
                    {

                        ns.CreateTypeFrom<AstRule>((ast, type) =>
                         {

                             type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                                 .GenerateIf(() => ctx.Strategy == "ClassEnum")
                                 .IsEnum()
                                 .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text) + "Enum")
                                 .Attribute(MemberAttributes.Public)
                                 .Field((field) =>
                                 {
                                     field.Name(n => "_undefined");
                                 })
                                 .Fields(() => ast.GetTerminals(), (field, ast2) =>
                                 {

                                     field.Name(n => CodeHelper.FormatCsharp((ast2 as AstTerminalText).Text.ToLower()))
                                     ;

                                 });

                         });


                    });
                })

                .Add("models", template =>
                {

                    template.Namespace("Bb.Asts", ns =>
                    {
                        ns.Using("System")
                          .Using("Antlr4.Runtime")
                          .Using("System.Collections")
                          .CreateTypeFrom<AstRule>((ast, type) =>
                          {

                              type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                                  .GenerateIf(() => Generate(ast, ctx))
                                  .Comment(() => ast.ToString())
                                  .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text))
                                  .Inherit(() => GetInherit(ast, ctx))
                                  .CtorWhen(() => ctx.Strategy == string.Empty || ctx.Strategy == "Default", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Argument(() => "List<AstRoot>", "list")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx", "list");

                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassEnum", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx", "value");
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassWithProperties", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Argument(() => "List<AstRoot>", "list")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx", "null")
                                       .Body(b =>
                                       {

                                           b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                           {

                                               foreach (var item in ast.GetAlternatives())
                                               {
                                                   foreach (var item2 in item.Where(c => c.IsRuleReference).ToList())
                                                   {

                                                       var name = item2.ResolveName();

                                                       var type = "Ast" + CodeHelper.FormatCsharp(name);
                                                       var ty = new CodeTypeReference(type);
                                                       stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                                       {
                                                           t.Assign(CodeHelper.This().Property(CodeHelper.FormatCsharp(CodeHelper.FormatCsharp(name))), CodeHelper.Var("enumerator.Current").Cast(ty));
                                                       });
                                                   }

                                               }

                                           });

                                       })
                                       ;
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassList", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx");
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassList", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Argument(() => typeof(int), "capacity")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx", "capacity");
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
                                               "Visit" + CodeHelper.FormatCsharp(ast.RuleName.Text),
                                               CodeHelper.This()
                                           );
                                       });
                                  })
                                  .Method(() => ctx.Strategy == "ClassEnum", method =>
                                  {
                                      method
                                       .Name(g => "GetValue")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Override | MemberAttributes.Family)
                                       .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text) + "Enum")
                                       .Body(b =>
                                       {
                                           string typeEnum = "Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text) + "Enum";

                                           var items = ast.GetTerminals().ToList();
                                           foreach (AstTerminalText text in items)
                                           {
                                               var test = "value".Var().IsEqual(text.Text.AsConstant());
                                               b.Statements.If(test, t =>
                                               {
                                                   t.Return(typeEnum.AsType().Field(CodeHelper.FormatCsharp(text.Text.ToLower())));
                                               });
                                           }

                                           b.Statements.Return(typeEnum.AsType().Field("_undefined"));

                                       });
                                  })

                                  .PropertiesWhen(
                                      () => ctx.Strategy == "ClassWithProperties",
                                      () =>
                                      {
                                          List<object> _properties = new List<object>();
                                          foreach (var item in ast.GetAlternatives())
                                              foreach (AstBase item2 in item.Where(c => c.IsRuleReference).ToList())
                                                  _properties.Add(item2);
                                          return _properties;
                                      },
                                      (property, model) =>
                                      {
                                          property.Name(m => CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                          .Type(() => "Ast" + CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                          .Attribute(MemberAttributes.Public)
                                          .Get((stm) =>
                                          {
                                              stm.Return(CodeHelper.This().Field(CodeHelper.FormatCsharpField((model as AstBase).ResolveName())));
                                          })
                                          .Set((stm) =>
                                          {
                                              stm.Assign(CodeHelper.This().Field(CodeHelper.FormatCsharpField((model as AstBase).ResolveName())), new CodePropertySetValueReferenceExpression());
                                          })
                                          ;
                                      }
                                  )

                                  .FieldsWhen(
                                      () => ctx.Strategy == "ClassWithProperties",
                                      () =>
                                      {
                                          List<object> _properties = new List<object>();
                                          foreach (var item in ast.GetAlternatives())
                                              foreach (AstBase item2 in item.Where(c => c.IsRuleReference).ToList())
                                                  _properties.Add(item2);
                                          return _properties;
                                      },
                                      (field, model) =>
                                      {
                                          field.Name(m => CodeHelper.FormatCsharpField((model as AstBase).ResolveName()))
                                          .Type(() => "Ast" + CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                          .Attribute(MemberAttributes.Private)
                                          ;
                                      }
                                  )

                                  ;
                          })
                          .CreateTypeFrom<AstLabeledAlt>((ast, type) =>
                          {
                              type.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text))
                                  .Inherit(() => "AstRule")
                                  .Comment(() => ast.ToString())

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
                })



                .Add("IAstTSqlVisitor1", template =>
                {
                    template.Namespace("Bb.Asts", ns =>
                    {
                        ns.Using("System")
                        .CreateOneType<AstRule>((ast, type) =>
                        {
                            type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                                .GenerateIf(() => Generate(ast, ctx))
                                .Name(() => "IAstTSqlVisitor")
                                .IsInterface()
                                .Method(m =>
                                {
                                    m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.RuleName.Text))
                                     .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text), "a")
                                    ;
                                });
                        });
                    });
                })

                .Add("IAstTSqlVisitor2", template =>
                {
                    template.Namespace("Bb.Asts", ns =>
                    {
                        ns.Using("System")
                          .CreateOneType<AstLabeledAlt>((ast, type) =>
                          {
                              type.Name(() => "IAstTSqlVisitor")
                              .IsInterface()
                              .Method(m =>
                              {
                                  m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Identifier.Text))
                                   .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text), "a")
                                  ;
                              });
                          });
                    });
                })



                .Add("ScriptTSqlVisitor1", template =>
                {
                    template.Namespace("Bb.Parsers", ns =>
                    {
                        ns.Using("System",
                            "Bb.Parsers.Tsql",
                            "Bb.Asts",
                            "Antlr4.Runtime.Misc",
                            "Antlr4.Runtime.Tree",
                            "System.Collections"
                          )
                          .CreateOneType<AstRule>((ast, type) =>
                          {
                              type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                                  .GenerateIf(() => Generate(ast, ctx))
                                  .Name(() => "ScriptTSqlVisitor")
                                  .Method(() => ctx.Strategy == "" || ctx.Strategy == "Default", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.RuleName.Text))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.RuleName.Text) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.DeclareAndCreate("list", "List<AstRoot>".AsType());
                                           b.Statements.ForEach("IParseTree".AsType(), "item", "context.children", stm =>
                                           {
                                               //var v1 = ("Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text)).AsType();
                                               var v1 = "AstRoot".AsType();
                                               stm.DeclareAndInitialize("acceptResult", v1, "item".Var().Call("Accept", CodeHelper.This()));
                                               stm.If("acceptResult".Var().IsNotEqual(CodeHelper.Null()), s =>
                                               {
                                                   s.Call("list".Var(), "Add", "acceptResult".Var());
                                               }
                                               );

                                           });
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text)).AsType().Create("context".Var(), "list".Var()));

                                       });
                                  })
                                  .Method(() => ctx.Strategy == "ClassEnum", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.RuleName.Text))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.RuleName.Text) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text)).AsType().Create("context".Var(), "context".Var().Call("GetText")));
                                       });
                                  })
                                  .Method(() => ctx.Strategy == "ClassList", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.RuleName.Text))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.RuleName.Text) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Body(b =>
                                       {

                                           var astChild = ast.GetRules().FirstOrDefault();
                                           var t = "TSqlParser." + CodeHelper.FormatCamelUpercase(astChild.Identifier.Text) + "Context";
                                           var t1 = t.AsType();
                                           t1.ArrayRank = 1;
                                           b.Statements.DeclareAndInitialize("source", t1, "context".Var().Call(astChild.ResolveName()));

                                           var type = ("Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text)).AsType();
                                           b.Statements.DeclareAndInitialize("list", type, type.Create("context".Var(), "source".Var().Field("Length")));
                                           b.Statements.ForEach(t.AsType(), "item", "source", stm =>
                                           {
                                               var v1 = ("Ast" + CodeHelper.FormatCsharp(astChild.Identifier.Text)).AsType();
                                               stm.DeclareAndInitialize("acceptResult", v1, "item".Var().Call("Accept", CodeHelper.This()).Cast(v1));
                                               stm.If("acceptResult".Var().IsNotEqual(CodeHelper.Null()), s =>
                                               {
                                                   s.Call("list".Var(), "Add", "acceptResult".Var());
                                               }
                                               );
                                           });
                                           b.Statements.Return("list".Var());

                                       });
                                  })
                                  .Method(() => ctx.Strategy == "ClassWithProperties", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.RuleName.Text))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.RuleName.Text) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.DeclareAndCreate("list", "List<AstRoot>".AsType());
                                           b.Statements.ForEach("IParseTree".AsType(), "item", "context.children", stm =>
                                           {
                                               // var v1 = ("Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text)).AsType();
                                               var v1 = "AstRoot".AsType();
                                               stm.DeclareAndInitialize("acceptResult", v1, "item".Var().Call("Accept", CodeHelper.This()));
                                               stm.If("acceptResult".Var().IsNotEqual(CodeHelper.Null()), s =>
                                               {
                                                   s.Call("list".Var(), "Add", "acceptResult".Var());
                                               }
                                               );

                                           });
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.RuleName.Text)).AsType().Create("context".Var(), "list".Var()));

                                       });
                                  })
                                  ;
                          });
                    });
                })

                .Add("ScriptTSqlVisitor2", template =>
                {
                    template.Namespace("Bb.Parsers", ns =>
                    {
                        ns.Using("System",
                            "Bb.Parsers.Tsql",
                            "Bb.Asts",
                            "Antlr4.Runtime.Misc",
                            "Antlr4.Runtime.Tree"
                          )

                          .CreateOneType<AstLabeledAlt>((ast, type) =>
                          {
                              type.Name(() => "ScriptTSqlVisitor")
                              .Method(m =>
                              {

                                  m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Identifier.Text))
                                   .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Identifier.Text) + "Context", "context")
                                   .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                   .Return(() => "AstRoot")
                                   .Body(b =>
                                   {
                                       b.Statements.DeclareAndCreate("list", "List<AstRoot>".AsType());
                                       b.Statements.ForEach("IParseTree".AsType(), "item", "context.children", stm =>
                                       {
                                           stm.Call("list".Var(), "Add", CodeHelper.Var("enumerator.Current").Call("Accept", CodeHelper.This()));
                                       });

                                       b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text)).AsType().Create("context".Var(), "list".Var()));

                                   })

                                   ;
                              });
                          });
                    });
                })
                ;

            visitor4.Visit(ast);
        }

        private string _folder;

    }

}
