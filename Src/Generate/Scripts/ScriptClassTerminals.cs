using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{


    public class ScriptClassTerminals : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstTerminal<string>";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return TemplateSelector(ast, context) == "ClassTerminalAlias";
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
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name))
                              .Inherit(() => GetInherit(ast, ctx))


                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "t".Var().Call("GetText"));
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), "ctx".Var().Call("GetText"));
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "t")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
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
