using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{



    public class ScriptModelDefault : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRoot";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return context.Strategy == "_";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
             {

                 template.Namespace("Bb.Asts", ns =>
                 {
                     ns.Using("System")
                       .Using("Antlr4.Runtime")
                       .Using("Antlr4.Runtime.Tree")
                       .Using("Bb.Parsers")

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
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("t");

                               })
                               .Ctor((f) =>
                               {
                                   f.Argument(() => "ParserRuleContext", "ctx")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("ctx");

                               })
                               .Ctor((f) =>
                               {
                                   f.Argument(() => "Position", "p")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("p");

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

                       })
                       .CreateTypeFrom<AstLabeledAlt>((ast, type) =>
                       {
                           type.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text))
                               .Inherit(() => "AstRule")
                               .Comment(() => ast.ToString())

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
