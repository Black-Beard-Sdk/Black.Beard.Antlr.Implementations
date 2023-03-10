using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptTSqlVisitor2 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRoot";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return TemplateSelector(ast, context) == "_";
        }

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
            });

        }

    }


}
