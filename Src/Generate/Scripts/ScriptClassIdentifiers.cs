using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{

    public class ScriptClassIdentifiers : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstTerminalIdentifier\"");

            return config.Inherit.Text;

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

                      .CreateTypeFrom<AstRule>((ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .GenerateIf(() => Generate(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))


                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Arguments(
                                      () =>
                                       {

                                           if (ast.Name.Text == "host")
                                           {

                                               var combinaisons = ast.GetAllCombinations();

                                               // id_ DOT id_ 

                                               foreach (var item in combinaisons)
                                               {

                                                   var rules = item.Where(c => c.IsRuleRef).ToList();
                                                   if (rules.Count == 2)
                                                   {
                                                       var u = rules[0].Name == rules[1].Name;

                                                       var r = rules[0].Clone();
                                                       r.Occurence = new Occurence(OccurenceEnum.Any, rules[0].Occurence.Optional && rules[1].Occurence.Optional);



                                                   }
                                               }
                                           }

                                           return null;

                                       }, 
                                      (a) =>
                                       {

                                           return ("", "");

                                       })

                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var());
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var());
                              })
                              
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "position")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("position".Var());
                              })
                              .Method(method =>
                              {
                                  method
                                   .Name(g => "Accept")
                                   .Argument("IAstTSqlVisitor", "visitor")
                                   .Attribute(MemberAttributes.Override | MemberAttributes.Public)
                                   .Body(b =>
                                   {
                                       b.Statements.Call
                                       (
                                           CodeHelper.Var("visitor"),
                                           "Visit" + CodeHelper.FormatCsharp(ast.Name.Text),
                                           CodeHelper.This()
                                       );
                                   });
                              })
                              ;

                      });

                });

            });

        }

    }


}
