using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;

namespace Generate.Scripts
{

    public class ScriptInterfaceVisitor1 : ScriptBase
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
                    .CreateOneType<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                    {
                        type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                            .Name(() => "IAstTSqlVisitor")
                            .IsInterface()
                            
                            .Methods(() => ast.Alternatives, (m, model) =>
                            {

                                if (ast.Name.Text == "t_root")
                                {

                                }

                                var t = ast.Configuration.Config.TemplateSetting.TemplateName
                                     ?? ast.Configuration.Config.CalculatedTemplateSetting.Setting.TemplateName;

                                if (t == "_")
                                {
                                    var type = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    if (ast.Alternatives.Count == 1)
                                    {
                                        m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                         .Argument(() => type, "a")
                                        ;
                                    }
                                    else
                                    {
                                        var index = ast.Alternatives.IndexOf((AstLabeledAlt)model) + 1;
                                        m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                         .Argument(() => type + "." + type + index.ToString(), "a")
                                         .Documentation(c => c.Summary(() => ast.Name.Text + " : " + model.ToString()))

                                         ;
                                    }
                                }
                                else
                                {
                                    m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                     .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text), "a")
                                     .Documentation(c => c.Summary(() => ast.Name.Text + " : " + m.ToString()))
                                     ;

                                }

                            })

                            //.MethodWhen(() => ast.Configuration.Config.TemplateSetting.TemplateName == "_", m =>
                            //{
                            //    m.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                            //     .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text), "a")
                            //     .Documentation(c => c.Summary(() => ast.Name.Text + " : " + m.ToString()))
                            //    ;
                            //})
                            
                            ;

                    });
                });
            });

        }

    }


}
