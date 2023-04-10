using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;

namespace Generate.Scripts
{

    public class ScriptInterfaceVisitor2 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("'AstRoot'");

            return config.Inherit.Text;

        }

        public override string StrategyTemplateKey => "_";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {
                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .CreateOneType<AstLabeledAlt>(null, null, (ast, type) =>
                      {
                          type.Name(() => "IAstTSqlVisitor")
                          .IsInterface()
                          .Method(m =>
                          {
                              m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text))
                               .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text), "a")
                              ;
                          });
                      });
                });
            });

        }

    }


}
