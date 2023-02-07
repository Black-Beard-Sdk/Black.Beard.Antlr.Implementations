using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptTSqlVisitor1 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRoot";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return context.Strategy == "_";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
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
;

        }

    }


}
