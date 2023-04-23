using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.HelperScripts
{

    public class ScriptHelper : ScriptBase
    {


        public string RuleName { get; set; }

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstTerminalIdentifier", ast, context);
        }

        public override string StrategyTemplateKey => "ClassIdentifiers";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")

                      .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "TSql")
                              .Attribute( System.Reflection.TypeAttributes.Public)
                              .IsStatic()
                                                         
                              .Method(method =>
                              {
                                  method
                                   .Name(g => "Create")
                                   .Attribute(MemberAttributes.Public)
                                   .Body(b =>
                                   {

                                    var o = GenerateCodeForIdentifierVisitor.GetExpression(ast);

                                   });
                              })
                              ;

                      });
                });

            });

        }

        private static Func<string> GetType(Context ctx, object model)
        {

            Func<string> result = () => "AstRoot";

            var p1 = (model as AlternativeTreeRuleItem).Item;
            string template = p1?.Origin?.Link.GetTemplate() ?? string.Empty;
            if (template == "ClassIdentifiers")
            {

            }
            else
            {
                var combinaisons = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                if (combinaisons.Count() == 1)
                {

                    var p = (model as AlternativeTreeRuleItem).Item;
                    var b = p.Origin.Select(c => c.TerminalKind == TokenTypeEnum.Identifier).Any();

                    if (b)
                        result = () => "AstTerminalString";

                    else
                        result = () => p.Origin.Type();
                }

            }
            return result;

        }


    }


}
