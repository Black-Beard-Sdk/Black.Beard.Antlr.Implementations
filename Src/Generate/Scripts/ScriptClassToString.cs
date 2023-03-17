using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{



    public class ScriptClassToString : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig(string.Empty);

            return config.Inherit.Text;

        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return TemplateSelector(ast, context) == "_";
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
                               //.GenerateIf(() => Generate(ast, ctx))
                               .Documentation(c => c.Summary(() => ast.ToString()))
                               .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                               //.Inherit(() => GetInherit(ast, ctx))


                               .Method(method =>
                               {
                                   method
                                    .Name(g => "ToString")
                                    .Argument("Writer", "wrt")
                                    .Attribute(MemberAttributes.Override | MemberAttributes.Public)
                                    .Body(b =>
                                    {
                                        //b.Statements.Call
                                        //(
                                        //    CodeHelper.Var("visitor"),
                                        //    "Visit" + CodeHelper.FormatCsharp(ast.Name),
                                        //    CodeHelper.This()
                                        //);
                                    });
                               })
                               ;

                       })
                       .CreateTypeFrom<AstLabeledAlt>((ast, type) =>
                       {
                           type.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text))
                               .Inherit(() => "AstRule")
                               .Documentation(c => c.Summary(() => ast.ToString()))

                               .Ctor((f) =>
                               {
                                   f.Argument(() => "ParserRuleContext", "ctx")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("ctx", "list");
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
                                            "Visit" + CodeHelper.FormatCamelUpercase(ast.Identifier.Text),
                                            CodeHelper.This()
                                        );
                                    });
                               });
                       });
                 });

             });

        }

    }


}
