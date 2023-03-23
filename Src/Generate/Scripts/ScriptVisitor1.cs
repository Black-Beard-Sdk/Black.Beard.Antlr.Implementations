using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;

namespace Generate.Scripts
{

    public class ScriptVisitor1 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return null; // "AstRule";
        }

        public override string StrategyTemplateKey => "ScriptVisitor1";

        protected override bool Generate(AstRule ast, Context context)
        {
            return true; // TemplateSelector(ast, context) == "_";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {
                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                    .CreateOneType<AstRule>((ast, type) =>
                    {
                        type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                            .GenerateIf(() => Generate(ast, ctx))
                            .Name(() => "IAstTSqlVisitor")
                            .IsInterface()
                            .Method(m =>
                            {
                                m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                 .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text), "a")
                                ;
                            });
                    });
                });
            });

        }

    }


}
