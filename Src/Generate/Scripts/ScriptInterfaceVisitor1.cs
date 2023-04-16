using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;

namespace Generate.Scripts
{

    public class ScriptInterfaceVisitor1 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("", ast, context);
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
                    .CreateOneType<AstRule>(ast => Generate(ast, ctx), ast =>
                    {
                        ctx.Variables["combinaisons"] = ast.ResolveAllCombinations();

                    }, (ast, type) =>
                    {
                        type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                            .Name(() => "IAstTSqlVisitor")
                            .IsInterface()
                            
                            .Methods(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons"), (method, m) =>
                            {

                                var cc = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                                var mo = m as AlternativeTreeRuleItem;
                                var model = mo.Item;

                                var t = ast.Configuration.Config.TemplateSetting.TemplateName
                                     ?? ast.Configuration.Config.CalculatedTemplateSetting.Setting.TemplateName;

                                if (t == "_")
                                {
                                    var type = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    if (cc.Count == 1)
                                    {
                                        method.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                         .Argument(() => type, "a")
                                        ;
                                    }
                                    else
                                    {
                                        method.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                         .Argument(() => type + "." + type + mo.AlternativeIdentifier.ToString(), "a")
                                         .Documentation(c => c.Summary(() => ast.Name.Text + " : " + model.ToString()))
                                         ;
                                    }
                                }
                                else
                                {
                                    method.Name(g => "Visit" + CodeHelper.FormatCsharp(ast.Name.Text))
                                     .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text), "a")
                                     .Documentation(c => c.Summary(() => ast.Name.Text + " : " + method.ToString()))
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
