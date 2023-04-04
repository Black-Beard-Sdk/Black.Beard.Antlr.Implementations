using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Diagnostics;
using System.Text;

namespace Generate.Scripts
{

    public class ScriptClassDefaults : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstRule\"");

            return config.Inherit.Text;

        }

        public override string StrategyTemplateKey => "_";

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
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ITerminalNode", "t")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("t");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ParserRuleContext", "ctx")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("ctx");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "Position", "p")
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("p");
                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("Position.Default");
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

                            HashSet<string> _h = new HashSet<string>();
                            List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                            foreach (AstLabeledAlt alternative in ast.Alternatives)
                            {

                                var allCombinations = alternative.ResolveAllCombinations();

                                foreach (TreeRuleItem alt in allCombinations)
                                {

                                    StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                    var name = CodeHelper.FormatCsharp(ast.Name.Text);
                                    var t1 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType();
                                    List<string> arguments = new List<string>();

                                    var method = name.AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                        .BuildDocumentation(alt, ctx);

                                    var t2 = "List<AstRoot>".AsType();
                                    method.Statements.Add(CodeHelper.DeclareAndCreate("arguments", t2));

                                    Action<TreeRuleItem> act = itemAst =>
                                    {

                                        string name = null;
                                        CodeTypeReference argumentTypeName = null;
                                        string varName = null;

                                        var itemResult = ast.ResolveByName(itemAst.ResolveKey());
                                        if (itemResult != null && itemResult is AstRule r1 && r1?.Configuration != null)
                                        {                                            
                                            name = "Ast" + CodeHelper.FormatCsharp(itemAst.Name);
                                            argumentTypeName = new CodeTypeReference(name);
                                            
                                            if (string.IsNullOrEmpty(itemAst.Label))
                                                varName = CodeHelper.FormatCsharpArgument(itemAst.Name);
                                            else
                                                varName = CodeHelper.FormatCsharpArgument(itemAst.Label);


                                        }
                                        else if (itemResult != null && itemResult is AstLexerRule r2)
                                        {

                                            switch (r2.Configuration.Config.Kind)
                                            {
                                                case TokenTypeEnum.Pattern:
                                                case TokenTypeEnum.String:
                                                case TokenTypeEnum.Identifier:
                                                    name = nameof(String);
                                                    varName = "txt";
                                                    break;
                                                case TokenTypeEnum.Boolean:
                                                    name = nameof(Boolean);
                                                    varName = "boolean";
                                                    break;
                                                case TokenTypeEnum.Decimal:
                                                    name = nameof(Decimal);
                                                    varName = "_decimal";
                                                    break;
                                                case TokenTypeEnum.Int:
                                                    name = nameof(Int64);
                                                    varName = "integer";
                                                    break;
                                                case TokenTypeEnum.Real:
                                                    name = nameof(Double);
                                                    varName = "real";
                                                    break;
                                                case TokenTypeEnum.Hexa:
                                                    name = "";
                                                    varName = "";
                                                    break;
                                                case TokenTypeEnum.Binary:
                                                    name = "Object";
                                                    varName = "_binary";
                                                    break;

                                                case TokenTypeEnum.Operator:
                                                case TokenTypeEnum.Ponctuation:
                                                case TokenTypeEnum.Other:
                                                case TokenTypeEnum.Comment:
                                                case TokenTypeEnum.Constant:
                                                default:
                                                    break;
                                            }

                                            argumentTypeName = new CodeTypeReference(name);

                                            if (!string.IsNullOrEmpty(itemAst.Label))
                                                varName = CodeHelper.FormatCsharpArgument(itemAst.Label);

                                        }

                                        if (name != null)
                                        {

                                            if (itemAst.Occurence.Value == OccurenceEnum.Any)
                                                argumentTypeName = new CodeTypeReference(typeof(IEnumerable<>).Name, argumentTypeName);

                                            method.Parameters.Add(new CodeParameterDeclarationExpression(argumentTypeName, varName));
                                            uniqeConstraintKeyMethod.Append(name);
                                            arguments.Add(varName);
                                        }
                                        

                                    };

                                    if (alt.Count > 0)
                                        foreach (var itemAlt in alt)
                                            act(itemAlt);
                                    else
                                        act(alt);

                                    if (method.Parameters.Count > 0)
                                    {

                                        var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                        if (_h.Add(noDuplicateKey))
                                        {
                                            methods.Add(method);
                                            method.Statements.Add(CodeHelper.DeclareAndCreate("result", t1, "arguments".Var()));
                                            method.Statements.Return("result".Var());
                                        }

                                    }

                                }                       

                            }

                            foreach (var item in methods)
                            {
                                t.Members.Add(item);
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

       
    }


}
