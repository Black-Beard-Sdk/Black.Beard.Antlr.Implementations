using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassVisitorTerminalAlias : ScriptBase
    {            

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRoot";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return TemplateSelector(ast, context) == "ClassTerminalAlias";
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
                                       b.Statements.Return(("Ast" + CodeHelper.FormatCsharp(ast.Name)).AsType().Create("context".Var()));
                                   });
                              });

                      });
                });
            })
;

        }
              
    }


}
