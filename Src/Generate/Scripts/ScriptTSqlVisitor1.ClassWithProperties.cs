using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassVisitorWithProperties : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstRoot\"");

            return config.Inherit.Text;
        }

        public override string StrategyTemplateKey => "ClassWithProperties";

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
                      .CreateOneType<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                      {
                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Name(() => "ScriptTSqlVisitor")

                              .Method(m =>
                              {
                                  m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text))
                                   .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name.Text) + "Context", "context")
                                   .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                   .Return(() => "AstRoot")
                                   .Documentation(c => c.Summary(() => ast.ToString()))
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
                                       b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType().Create("context".Var(), "list".Var()));

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
