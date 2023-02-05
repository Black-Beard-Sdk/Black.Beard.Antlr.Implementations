using Bb;
using Bb.Asts;
using Bb.Configurations;
using Bb.Generators;
using Bb.ParserConfigurations.Antlr;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
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

                ctx.AntlrParserRootName = Path.GetFileNameWithoutExtension(antlrParser.Name) + ".";
                this._folder = antlrParser.Directory.FullName;

                var sb = new StringBuilder(antlrParser.LoadFromFile());
                var parser = ScriptParser.ParseString(sb, antlrParser.FullName);
                var ast = (AstGrammarSpec)parser.Visit(new ScriptAntlrVisitor());

                var configFile = Path.Combine(this._folder, ctx.AntlrParserRootName + "conf");
                if (File.Exists(configFile))
                {
                    var sb2 = new StringBuilder(configFile.LoadFromFile());
                    var config = ScriptConfigParser.ParseString(sb2, configFile);
                    _astConfig = (GrammarSpec)config.Visit(new ScriptAntlrConfigVisitor());
                }
                else
                    _astConfig = new GrammarSpec(Position.Default);

                _astConfig.Append(ast);

                foreach (var item in ast.Rules)
                {
                    var conf = ctx.Configuration.GetConfiguration(item);
                    if (conf.Strategy != "_")
                        item.Configuration.Config.TemplateSetting.TemplateName = conf.Strategy;
                    item.Configuration.Config.Generate = conf.Generate;
                }

                var visitor2 = new ParentVisitor();
                visitor2.Visit(ast);

                var visitor3 = new CodeVisitor();
                visitor3.Visit(ast);

                Generate(ctx, ast);

                _astConfig.Save(configFile);

            }

        }



        private string TemplateSelector(AstRule ast, Context context)
        {

            if (!string.IsNullOrEmpty(ast.Strategy))
                return ast.Strategy;

            var conf = ast.Configuration.Config;


            var txt = _astConfig.Evaluate(ast);
            if (!string.IsNullOrEmpty(txt))
            {
                conf.CalculatedTemplateSetting = new CalculatedTemplateSetting
                (
                    Position.Default,
                    new TemplateSetting(Position.Default, txt)
                );
            }
            else
            {
                conf.CalculatedTemplateSetting = new CalculatedTemplateSetting
                (
                    Position.Default,
                    new TemplateSetting(Position.Default, TemplateSelectorCompute(ast, context))
                );
            }

            if (conf.TemplateSetting != null && !string.IsNullOrEmpty(conf.TemplateSetting.TemplateName))
                ast.Strategy = conf.TemplateSetting.TemplateName;
            else
                ast.Strategy = conf.CalculatedTemplateSetting.Setting.TemplateName;

            return ast.Strategy;

        }

        private static string TemplateSelectorCompute(AstRule ast, Context context)
        {

            if (ast.Name == "throw_statement")
            {

            }


            if (ast.Alternatives.Count == 1)
            {

                var r = ast.Alternatives[0].Rule.Rule;
                if (r.Count == 1)
                {
                    var o = r[0];
                    switch (o.Type)
                    {

                        case "AstAtom":

                            var oc = o.ResolveOccurence();

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
                                    if (i.IsTerminal)
                                        return "ClassTerminalAlias";
                                    break;

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


            if (ast.OutputContainsAlwayOneTerminal
                  && ast.OutputContainsAlwayOneItem
                  && ast.GetTerminals().Count() > 1
                  && !ast.GetTerminals().Where(c => context.TerminalsToExcludes.Contains(c.Text)).Any() // No enum with charset not letter
                  && !ast.GetTerminals().Where(c => context.Identifiers.Contains(c.Text)).Any()         // No constants
               )
                return "ClassEnum";

            if (ast.ContainsJustOneAlternative)
            {

                var itemRules = ast.GetRules().GroupBy(c => c.ResolveName()).ToList();
                if (itemRules.Count == 1)
                {

                    var itemTerms = ast.GetTerminals().GroupBy(c => c.ResolveName()).ToList();

                    if (itemTerms.Count == 0)
                    {

                        var i = itemRules[0].ToList();

                        if (i.Count == 1)
                            if ((int)i[0].ResolveOccurence() <= (int)OccurenceEnum.OneOptional)
                                if (i[0].ResolveName() == "id_")
                                    return "ClassId";

                        if (i.Count > 1)
                            return "ClassList";

                        foreach (var item in i)
                            if ((int)item.ResolveOccurence() >= (int)OccurenceEnum.OneOrMore)
                                return "ClassList";

                    }
                    else if (itemTerms.Count == 1)
                    {
                        var o = itemTerms[0].First();
                        var splitChar = new HashSet<string>() { "COMMA" };
                        if (splitChar.Contains(o.ResolveName()))
                        {
                            var oc = ast.GetRules().First().ResolveOccurence();
                            if (oc > OccurenceEnum.OneOptional)
                                return "ClassList";
                        }
                    }
                    else
                    {

                    }
                }



                if (ast.ContainsOnlyTerminals
                      && ast.OutputContainsAlwayOneItem
                      && ast.GetTerminals().Count() > 1
                      && !ast.GetTerminals().Where(c => context.TerminalsToExcludes.Contains(c.Text)).Any()
                      && !ast.GetTerminals().Where(c => context.Identifiers.Contains(c.Text)).Any()
                   )
                    return "ClassEnum";

                foreach (var item in ast.GetListAlternatives())
                    if (item.Where(c => c.IsRule).Any())
                        return "ClassWithProperties";

            }

            return "_";

        }

        private static string GetInherit(AstRule ast, Context context)
        {
            switch (context.Strategy)
            {

                case "_":
                    break;

                case "ClassTerminalAlias":
                    return "AstTerminal<string>";

                case "ClassEnum":
                    return "AstTerminal<Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum>";

                case "ClassList":
                    var astChild = ast.GetRules().FirstOrDefault();
                    return "AstRuleList<Ast" + CodeHelper.FormatCsharp(astChild.Identifier.Text) + ">";


                case "ClassId":
                    return "AstRule";


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

        private void Generate(Context ctx, AstBase ast)
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
                                 .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum")
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
                          .Using("Antlr4.Runtime.Tree")
                          .Using("Bb.Parsers")

                          .CreateTypeFrom<AstRule>((ast, type) =>
                          {

                              type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                                  .GenerateIf(() => Generate(ast, ctx))
                                  .Comment(() => ast.ToString())
                                  .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name))
                                  .Inherit(() => GetInherit(ast, ctx))


                                  .CtorWhen(() => ctx.Strategy == "_", (f) =>
                                  {
                                      f.Argument(() => "ITerminalNode", "t")
                                       .Argument(() => "List<AstRoot>", "list")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("t", "list");

                                  })
                                  .CtorWhen(() => ctx.Strategy == "_", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Argument(() => "List<AstRoot>", "list")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx", "list");

                                  })
                                  .CtorWhen(() => ctx.Strategy == "_", (f) =>
                                  {
                                      f.Argument(() => "Position", "p")
                                       .Argument(() => "List<AstRoot>", "list")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("p", "list");

                                  })


                                  .CtorWhen(() => ctx.Strategy == "ClassEnum", (f) =>
                                  {
                                      f.Argument(() => "ITerminalNode", "t")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("t".Var(), ("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Call("GetValue", "value".Var()));
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassEnum", (f) =>
                                  {
                                      f.Argument(() => "ITerminalNode", "t")
                                       .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum", "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("t".Var(), "value".Var());
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassEnum", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx".Var(), ("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Call("GetValue", "value".Var()));
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassEnum", (f) =>
                                  {
                                      f.Argument(() => "Position", "p")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("p".Var(), ("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Call("GetValue", "value".Var()));
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassEnum", (f) =>
                                  {
                                      f.Argument(() => "Position", "p")
                                       .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum", "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("p".Var(), "value".Var());
                                  })


                                  .CtorWhen(() => ctx.Strategy == "ClassTerminalAlias", (f) =>
                                  {
                                      f.Argument(() => "ITerminalNode", "t")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("t".Var(), "value".Var());
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassTerminalAlias", (f) =>
                                  {
                                      f.Argument(() => "ParserRuleContext", "ctx")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("ctx".Var(), "ctx".Var().Call("GetText"));
                                  })
                                  .CtorWhen(() => ctx.Strategy == "ClassTerminalAlias", (f) =>
                                  {
                                      f.Argument(() => "Position", "t")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("t".Var(), "value".Var());
                                  })

                                  .CtorWhen(() => ctx.Strategy == "ClassWithProperties", (f) =>
                                  {
                                      f.Argument(() => "Position", "p")
                                       .Argument(() => "List<AstRoot>", "list")
                                       .Attribute(MemberAttributes.Public)
                                       .CallBase("p", "null")
                                       .Body(b =>
                                       {

                                           b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                           {

                                               foreach (var item in ast.GetListAlternatives())
                                               {
                                                   foreach (var item2 in item.Where(c => c.IsRule).ToList())
                                                   {

                                                       var name = item2.ResolveName();

                                                       var type = "Ast" + CodeHelper.FormatCsharp(name);
                                                       var ty = new CodeTypeReference(type);
                                                       stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                                       {
                                                           t.Assign(CodeHelper.This().Field(CodeHelper.FormatCsharpField(name)), CodeHelper.Var("enumerator.Current").Cast(ty));
                                                       });
                                                   }

                                               }

                                           });

                                       })
                                       ;
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

                                               foreach (var item in ast.GetListAlternatives())
                                               {
                                                   foreach (var item2 in item.Where(c => c.IsRule).ToList())
                                                   {

                                                       var name = item2.ResolveName();

                                                       var type = "Ast" + CodeHelper.FormatCsharp(name);
                                                       var ty = new CodeTypeReference(type);
                                                       stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                                       {
                                                           t.Assign(CodeHelper.This().Field(CodeHelper.FormatCsharpField(name)), CodeHelper.Var("enumerator.Current").Cast(ty));
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
                                               "Visit" + CodeHelper.FormatCsharp(ast.Name),
                                               CodeHelper.This()
                                           );
                                       });
                                  })
                                  .Method(() => ctx.Strategy == "ClassEnum", method =>
                                  {
                                      method
                                       .Name(g => "GetValue")
                                       .Argument(() => typeof(string), "value")
                                       .Attribute(MemberAttributes.Family | MemberAttributes.Static)
                                       .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum")
                                       .Body(b =>
                                       {
                                           string typeEnum = "Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum";

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
                                          foreach (var item in ast.GetListAlternatives())
                                              foreach (AstBase item2 in item.Where(c => c.IsRule).ToList())
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
                                          .HasSet(false)
                                          ;
                                      }
                                  )

                                  .FieldsWhen(
                                      () => ctx.Strategy == "ClassWithProperties",
                                      () =>
                                      {
                                          List<object> _properties = new List<object>();
                                          foreach (var item in ast.GetListAlternatives())
                                              foreach (AstBase item2 in item.Where(c => c.IsRule).ToList())
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
                                    m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name))
                                     .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name), "a")
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
                                  .Method(() => ctx.Strategy == "_", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name))
                                       .Argument(() => ctx.AntlrParserRootName + CodeHelper.FormatCamelUpercase(ast.Name) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.DeclareAndCreate("list", "List<AstRoot>".AsType());
                                           b.Statements.ForEach("IParseTree".AsType(), "item", "context.children", stm =>
                                           {
                                               //var v1 = ("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType();
                                               var v1 = "AstRoot".AsType();
                                               stm.DeclareAndInitialize("acceptResult", v1, "item".Var().Call("Accept", CodeHelper.This()));
                                               stm.If("acceptResult".Var().IsNotEqual(CodeHelper.Null()), s =>
                                               {
                                                   s.Call("list".Var(), "Add", "acceptResult".Var());
                                               }
                                               );

                                           });
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Create("context".Var(), "list".Var()));

                                       });
                                  })
                                  .Method(() => ctx.Strategy == "ClassEnum", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Create("context".Var(), "context".Var().Call("GetText")));
                                       });
                                  })


                                  .Method(() => ctx.Strategy == "ClassTerminalAlias", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Create("context".Var()));
                                       });
                                  })


                                  .Method(() => ctx.Strategy == "ClassList", m =>
                                  {
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Body(b =>
                                       {

                                           var astChild = ast.GetRules().FirstOrDefault();
                                           var t = "TSqlParser." + CodeHelper.FormatCamelUpercase(astChild.Identifier.Text) + "Context";
                                           var t1 = t.AsType();
                                           t1.ArrayRank = 1;
                                           b.Statements.DeclareAndInitialize("source", t1, "context".Var().Call(astChild.ResolveName()));

                                           var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType();
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
                                      m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name))
                                       .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name) + "Context", "context")
                                       .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                       .Return(() => "AstRoot")
                                       .Comment(() => ast.ToString())
                                       .Body(b =>
                                       {
                                           b.Statements.DeclareAndCreate("list", "List<AstRoot>".AsType());
                                           b.Statements.ForEach("IParseTree".AsType(), "item", "context.children", stm =>
                                           {
                                               // var v1 = ("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType();
                                               var v1 = "AstRoot".AsType();
                                               stm.DeclareAndInitialize("acceptResult", v1, "item".Var().Call("Accept", CodeHelper.This()));
                                               stm.If("acceptResult".Var().IsNotEqual(CodeHelper.Null()), s =>
                                               {
                                                   s.Call("list".Var(), "Add", "acceptResult".Var());
                                               }
                                               );

                                           });
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Create("context".Var(), "list".Var()));

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
        private GrammarSpec _astConfig;
    }

}
