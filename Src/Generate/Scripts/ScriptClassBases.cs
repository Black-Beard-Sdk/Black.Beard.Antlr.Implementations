using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Generate.Scripts
{

    public class ScriptClassBases : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstBnfRule", ast, context);
        }

        public override string StrategyTemplateKey => "ClassBase";

        protected override bool Generate(AstRule ast, Context context)
        {
            var result = !ast.Configuration.Config.Generate;
            return result;
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

                .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                {

                    var item =
                    type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                        .Attribute(TypeAttributes.Abstract | TypeAttributes.Public)
                        .Documentation(c => c.Summary(() => ast.ToString()))
                        .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                        .Inherit(() => GetInherit(ast, ctx))

                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ITerminalNode", "t")
                                    .CallBase("t");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ParserRuleContext", "ctx")
                                    .CallBase("ctx");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "Position", "p")
                                    .CallBase("p");
                               })

                        .Make(t =>
                        {

                            HashSet<string> _h = new HashSet<string>();
                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                            var alternatives = ast.GetAlternativesWithOnlyRules(ctx);

                            foreach (var alt in alternatives)
                            {

                                var n1 = CodeHelper.FormatCsharp(alt.Name);
                                var n2 = "Ast" + n1;
                                var t1 = n2.AsType();

                                StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                List<string> arguments = new List<string>();

                                var method = n1.AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                    .BuildDocumentation(ast.Name.Text, alt, ctx);

                                if (alt.Count > 0)
                                    foreach (var itemAlt in alt)
                                        itemAlt.BuildStaticMethod(ast, method, arguments, uniqeConstraintKeyMethod);

                                var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                if (_h.Add(noDuplicateKey))
                                {
                                    List<CodeExpression> args = new List<CodeExpression>(arguments.Count);
                                    foreach (var itemArg in arguments)
                                        args.Add(itemArg.Var());

                                    methods.Add(method);
                                    var ret = CodeHelper.Call(t1, n1, args.ToArray());
                                    method.Statements.Return(ret);

                                }

                            }

                            foreach (var item in methods)
                                t.Members.Add(item);

                        })

                        ;

                })

                .CreateTypeFrom<AstLabeledAlt>(ast => true, null, (ast, type) =>
                       {
                           type.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
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
                                            "Visit" + CodeHelper.FormatCamelUpercase(ast.Name.Text),
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
