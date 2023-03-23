using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassVisitorList : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
            {
                string cls = null;
                var astChilds = ast.GetRules();
                var astChild = astChilds.FirstOrDefault();
                if (astChild != null)
                {

                    //var pp = astChilds.Where(c => c.ResolveName() == ast.Name.Text).Count();

                    cls = astChild.Identifier.Text;
                }
                else
                {
                   


                }

                config.Inherit = new IdentifierConfig("\"AstRuleList<" + cls + "\"");
            }
            return config.Inherit.Text;
            
        }

        public override string StrategyTemplateKey => "ClassList";

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
                                  m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text))
                                   .Argument(() => "TSqlParser." + CodeHelper.FormatCamelUpercase(ast.Name.Text) + "Context", "context")
                                   .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                   .Return(() => "AstRoot")
                                   .Documentation(c => c.Summary(() => ast.ToString()))
                                   .Body(b =>
                                   {

                                       var astChild = ast.GetRules().FirstOrDefault();
                                       string cls;
                                       if (astChild?.Identifier != null)
                                       {
                                           cls = astChild.Identifier.Text;

                                           var t = "TSqlParser." + CodeHelper.FormatCamelUpercase(cls) + "Context";
                                           var t1 = t.AsType();
                                           t1.ArrayRank = 1;
                                           b.Statements.DeclareAndInitialize("source", t1, "context".Var().Call(astChild.ResolveName()));

                                           var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType();
                                           b.Statements.DeclareAndInitialize("list", type, type.Create("context".Var(), "source".Var().Field("Length")));
                                           b.Statements.ForEach(t.AsType(), "item", "source", stm =>
                                           {
                                               var v1 = ("Ast" + CodeHelper.FormatCsharp(astChild.Identifier.Text)).AsType();
                                               stm.DeclareAndInitialize("acceptResult", v1, "item".Var().Call("Accept", CodeHelper.This()).Cast(v1));
                                               stm.If("acceptResult".Var().IsNotEqual(CodeHelper.Null()), s =>
                                               {
                                                   s.Call("list".Var(), "Add", "acceptResult".Var());
                                               }
                                               );
                                           });
                                           b.Statements.Return("list".Var());

                                       }
                                       else
                                       {
                                           Console.WriteLine($"{ast.Name.Text} haven't rule. if the item is a terminal, create a new rule for.");
                                       }

                                       

                                   });
                              });

                      });
                });
            })
;

        }

    }


}
