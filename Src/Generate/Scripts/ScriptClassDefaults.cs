using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{



    public class ScriptClassDefaults : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRule";
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

                           var item =
                           type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                               .GenerateIf(() => Generate(ast, ctx))
                               .Documentation(c => c.Summary(() => ast.ToString()))
                               .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name))
                               .Inherit(() => GetInherit(ast, ctx))


                               .Ctor((f) =>
                               {
                                   f.Argument(() => "ITerminalNode", "t")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("t", "list");

                               })
                               .Ctor((f) =>
                               {
                                   f.Argument(() => "ParserRuleContext", "ctx")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("ctx", "list");

                               })
                               .Ctor((f) =>
                               {
                                   f.Argument(() => "Position", "p")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .Attribute(MemberAttributes.Public)
                                    .CallBase("p", "list");

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

                               .Field(field =>
                               {
                                   field.Name("_rule")
                                   .Type(typeof(string))
                                   .Attribute(MemberAttributes.Family | MemberAttributes.Static)
                                   .Value((a) =>
                                   {
                                       return ast.ToString();
                                   })

                                   ;
                               });

                           foreach (var alt in ast.Alternatives)
                               if (alt.ContainsRules)
                               {

                                   var visitor1 = new RuleIdVisitor();
                                   var p = alt.Accept(visitor1);
                                   var visitor2 = new CombinaisonsBuilderVisitor();
                                   p.Accept(visitor2);

                                   item.Ctor((f) =>
                                   {
                                       var ctor = f.Argument(() => "Position", "p")
                                        .Attribute(MemberAttributes.Public);

                                       ctor.Argument(() => "List<AstRoot>", "list");
                                       ctor.CallBase("p", "list");


                                   });

                               }

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
