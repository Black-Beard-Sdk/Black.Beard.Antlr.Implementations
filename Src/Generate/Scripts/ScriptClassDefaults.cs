using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Diagnostics;

namespace Generate.Scripts
{

    public class ScriptClassDefaults : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("'AstRule'");

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

                    var item =
                    type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                        .GenerateIf(() => Generate(ast, ctx))
                        .Documentation(c => c.Summary(() => ast.ToString()))
                        .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
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
                                     "Visit" + CodeHelper.FormatCsharp(ast.Name.Text),
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
                        })

                        .Make(t =>
                        {

                            var r = ast.Root();

                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                            foreach (AstLabeledAlt alternative in ast.Alternatives)
                            {

                                var allCombinations = alternative.ResolveAllCombinations();
                                foreach (var item_poss in allCombinations.OrderBy(c => c.ToString()))
                                    Debug.WriteLine(ast.Name + " : " + item_poss.ToString());

                                foreach (var alt in allCombinations)
                                {

                                    var method = new CodeMemberMethod() 
                                    {
                                        Attributes = MemberAttributes.Public | MemberAttributes.Static 
                                    };

                                    foreach (var item in alt)
                                    {

                                        var itemResult = ast.ResolveByName(ResolveKey(item));
                                        if (itemResult != null && itemResult is AstRule r1 && r1?.Configuration != null)
                                        {
                                            var config = r1.Configuration.Config;
                                            if (config.Generate)
                                            {

                                                var typeName = new CodeTypeReference("Ast" + CodeHelper.FormatCsharp(item.Name));

                                                if (item.Occurence.Value == OccurenceEnum.Any)
                                                    typeName = new CodeTypeReference(typeof(IEnumerable<>).Name, typeName);

                                                method.Parameters.Add(new CodeParameterDeclarationExpression(typeName, CodeHelper.FormatCsharpArgument(item.Name)));

                                            }
                                            else
                                            {

                                            }
                                        }
                                        else if (itemResult != null && itemResult is AstLexerRule r2)
                                        {
                                            //var config = r2.Configuration.Config;
                                            //if (config.Generate)
                                            //{

                                            //    var typeName = new CodeTypeReference("Ast" + CodeHelper.FormatCsharp(item.Name));

                                            //    if (item.Occurence.Value == OccurenceEnum.Any)
                                            //        typeName = new CodeTypeReference(typeof(IEnumerable<>).Name, typeName);

                                            //    method.Parameters.Add(new CodeParameterDeclarationExpression(typeName, CodeHelper.FormatCsharpArgument(item.Name)));

                                            //}
                                            //else
                                            //{

                                            //}
                                        }
                                        else
                                        {

                                        }
                                    }

                                    methods.Add(method);

                                }

                                foreach (var item in methods)
                                {
                                    t.Members.Add(item);
                                }


                                //if (alt.ContainsRules)
                                //        t.Ctor((f) =>
                                //        {
                                //            var ctor = f.Attribute(MemberAttributes.Public);
                                //            List<string> _args = new List<string>(alt.Count)
                                //        {
                                //            "Position.Default"
                                //        };
                                //            foreach (var arg in alt.Where(c => c.IsRuleRef))
                                //            {
                                //                var argName = "arg" + _args.Count;
                                //                _args.Add(argName);
                                //                var vv = CodeHelper.FormatCsharp(arg.Name);
                                //                ctor.Argument(() => ("Ast" + vv).AsType(), argName);
                                //            }
                                //            ctor.CallBase(_args.ToArray());
                                //        });

                            }


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

        private string ResolveKey(TreeRuleItem item)
        {

            if (!string.IsNullOrEmpty(item.Name))
                return item.Name;

            return null;

        }
    }


}
