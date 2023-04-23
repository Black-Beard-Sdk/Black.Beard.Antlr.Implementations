using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.ModelsScripts
{
    public class ScriptClassVisitorDefaults : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstRoot", ast, context);
        }

        public override string StrategyTemplateKey => "_";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {
                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using(
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

                                  var alternatives = ast.ResolveAllCombinations();

                                  m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text))
                                   .Argument(() => ctx.AntlrParserRootName + "." + CodeHelper.FormatCamelUpercase(ast.Name.Text) 
                                                    + "Context", "context")
                                   .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                   .Return(() => "AstRoot")
                                   .Documentation(c => c.Summary(() => ast.ToString()))
                                   .Body(b =>
                                   {

                                       b.Statements.DeclareAndInitialize("list", "AstRootList<AstRoot>".AsType(), "GetList".Call("context".Var()));

                                       //if (alternatives.Count == 1)
                                       //{
                                       //    b.Statements.Return(
                                       //         ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType()
                                       //            .Create("context".Var(), "list".Var())
                                       //    );

                                       //}
                                       //else
                                       //{
                                           b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType()
                                                   .Call("Create", "context".Var(), "list".Var()));
                                       //}
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
