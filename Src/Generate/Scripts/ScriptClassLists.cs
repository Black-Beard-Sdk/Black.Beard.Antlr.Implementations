using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassLists : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            var astChild = ast.GetRules().FirstOrDefault();
            var txt = "AstRuleList<Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + ">";
            return GetInherit_Impl(txt, ast, context);
        }

        public override string StrategyTemplateKey => "ClassList";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using("System")
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")
                      .Using("System.Collections")

                      .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))

                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx");
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => typeof(int), "capacity")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx", "capacity");
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
