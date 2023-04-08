using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptTSqlVisitor2 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstRoot\"");

            return config.Inherit.Text;

        }

        public override string StrategyTemplateKey => "_";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {
                template.Namespace(Namespace, ns =>
                {
                    ns.Using(this.Usings)
                      .Using(
                        "Bb.Parsers.Tsql",
                        "Antlr4.Runtime.Misc",
                        "Antlr4.Runtime.Tree",
                        "Bb.Parsers.TSql.Antlr"
                      )

                      .CreateOneType<AstLabeledAlt>(null, null, (ast, type) =>
                      {
                          type.Name(() => "ScriptTSqlVisitor")
                          .Method(m =>
                          {

                              m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text))
                               .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name.Text) + "Context", "context")
                               .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                               .Return(() => "AstRoot")
                               .Body(b =>
                               {
                                   b.Statements.DeclareAndCreate("list", "List<AstRoot>".AsType());
                                   b.Statements.ForEach("IParseTree".AsType(), "item", "context.children", stm =>
                                   {
                                       stm.Call("list".Var(), "Add", CodeHelper.Var("enumerator.Current").Call("Accept", CodeHelper.This()));
                                   });

                                   b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType().Create("context".Var(), "list".Var()));

                               })

                               ;
                          });
                      });
                });
            });

        }

    }


}
