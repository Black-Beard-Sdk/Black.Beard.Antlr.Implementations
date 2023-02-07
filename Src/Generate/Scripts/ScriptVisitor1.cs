using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;

namespace Generate.Scripts
{

    public class ScriptVisitor1 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRule";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return context.Strategy == "_";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
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
            });

        }

    }


}
