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

            var removeOptionalsVisitor = new RemoveOptionalsBuilderVisitor();
            var spliterVisitor = new SpliterBuilderVisitor();


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

                    if (ast.Name == "create_or_alter_event_session")
                    {

                    }
                    var visitor1 = new RuleIdVisitor();
                    var p = ast.Alternatives.Accept(visitor1);
                    var possibilites = p.Accept(removeOptionalsVisitor);
                    var possibilites2 = possibilites.Accept(spliterVisitor);

                    foreach (var alt in possibilites2)
                        if (alt.ContainsRules)
                        {

                            item.Ctor((f) =>
                            {
                                var ctor = f.Attribute(MemberAttributes.Public);

                                List<string> _args = new List<string>(alt.Count)
                                {
                                "Position.Default"
                                };

                                foreach (var arg in alt.Where(c => c.IsRuleRef))
                                {

                                    var argName = "arg" + _args.Count;
                                    _args.Add(argName);
                                    var vv = CodeHelper.FormatCsharp(arg.Name);
                                    ctor.Argument(() => ("Ast" + vv).AsType(), argName);

                                }

                                ctor.CallBase(_args.ToArray());


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
