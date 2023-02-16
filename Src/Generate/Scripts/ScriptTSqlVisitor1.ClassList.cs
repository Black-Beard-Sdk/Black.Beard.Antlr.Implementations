using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassVisitorList : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRoot";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return TemplateSelector(ast, context) == "ClassList";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {
                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using(
                        "Bb.Asts",
                        "Bb.Parsers.TSql.Antlr",
                        "Antlr4.Runtime.Misc",
                        "Antlr4.Runtime.Tree",
                        "System.Collections"
                      )
                      .CreateOneType<AstRule>((ast, type) =>
                      {
                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .GenerateIf(() => Generate(ast, ctx))
                              .Name(() => "ScriptTSqlVisitor")

                              .Method(m =>
                              {
                                  m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name))
                                   .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name) + "Context", "context")
                                   .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                   .Return(() => "AstRoot")
                                   .Documentation(c => c.Summary(() => ast.ToString()))
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
                              });

                      });
                });
            })
;

        }

    }


}
