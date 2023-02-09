using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassIdentifiers : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstTerminalIdentifier";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return context.Strategy == "ClassIdentifiers";
        }

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
                              .Comment(() => ast.ToString())
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name))
                              .Inherit(() => GetInherit(ast, ctx))


                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
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
                                           "Visit" + CodeHelper.FormatCsharp(ast.Name),
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
