using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Diagnostics;
using System.Text;
using System.Linq;

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

                .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                {

                    var item =
                    type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                        .Documentation(c => c.Summary(() => ast.ToString()))
                        .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                        .Attribute(ast.Alternatives.Count == 1 ? System.Reflection.TypeAttributes.Public : System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Abstract)
                        .Inherit(() => GetInherit(ast, ctx))


                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ITerminalNode", "t")
                                    .CallBase("t");

                                   if (ast.Alternatives.Count == 1)
                                   {
                                       f.Argument(() => "List<AstRoot>", "list");
                                   }

                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "ParserRuleContext", "ctx")
                                    .CallBase("ctx");

                                   if (ast.Alternatives.Count == 1)
                                   {
                                       f.Argument(() => "List<AstRoot>", "list");
                                   }

                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "Position", "p")
                                    .CallBase("p");

                                   if (ast.Alternatives.Count == 1)
                                   {
                                       f.Argument(() => "List<AstRoot>", "list");
                                   }


                               })
                        .Ctor((f) =>
                               {
                                   f.Attribute(MemberAttributes.FamilyAndAssembly)
                                    .Argument(() => "List<AstRoot>", "list")
                                    .CallBase("Position.Default");
                               })

                        .MethodWhen(() => ast.Alternatives.Count == 1, method =>
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

                        .MethodWhen(() => ast.Alternatives.Count > 1, method =>
                        {
                            method
                             .Name(g => "Create")
                             .Argument("ParserRuleContext", "ctx")
                             .Argument("List<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                             .Body(b =>
                             {

                                 b.Statements.DeclareAndInitialize("index", typeof(int).AsType(), CodeHelper.Call(("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType()
                                     , "Resolve", "list".Var()));

                                 for (int i = 0; i < ast.Alternatives.Count; i++)
                                 {
                                     b.Statements.If("index".Var().IsEqual(i.AsConstant()), _t =>
                                     {
                                         string typename = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                         var t = (typename + "." + typename + (i + 1));
                                         _t.Return(CodeHelper.Create(t.AsType(), "list".Var()));
                                     });
                                 }

                                 b.Statements.Return(CodeHelper.Null());

                             });
                        })

                        .MethodWhen(() => ast.Alternatives.Count > 1, method =>
                        {
                            method
                             .Name(g => "Resolve")
                             .Argument("List<AstRoot>", "list")
                             .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                             .Return(() => typeof(int))
                             .Body(b =>
                             {

                                 List<List<(string, bool, bool)>> alternatives = new List<List<(string, bool, bool)>>(ast.Alternatives.Count);
                                 foreach (var item in ast.Alternatives)
                                 {
                                     var r = item.GetRules().ToList();
                                     var l = new List<(string, bool, bool)>(r.Count);
                                     foreach (var item2 in r)
                                     {
                                         var type = "Ast" + CodeHelper.FormatCsharp(item2.ToString());
                                         var occurence = item2.ResolveOccurence();
                                         l.Add((type, occurence.Optional, occurence.Value == OccurenceEnum.Any));
                                     }
                                     alternatives.Add(l);
                                 }

                                 foreach (var alternative in alternatives.OrderByDescending(c => c.Count).GroupBy(c => c.Count))
                                 {

                                     var listCount = "list".Var().Property("Count");
                                     var constant = alternative.Key.AsConstant();

                                     b.Statements.If(CodeHelper.IsEqual(listCount, constant), _t =>
                                     {

                                         foreach (var rule in alternative)
                                         {

                                             var nt = _t;

                                             int i = 0;
                                             foreach (var item2 in rule)
                                             {

                                                 nt.If("AstRoot".AsType().Call("Eval", 
                                                     "list".Indexer(i.AsConstant()),
                                                     item2.Item1.AsType().Typeof(),
                                                     item2.Item2.AsConstant(),
                                                     item2.Item3.AsConstant()), _t2 =>
                                                 {
                                                     nt = _t2;
                                                 });

                                                 i++;

                                             }

                                             nt.Return((alternatives.IndexOf(rule) + 1).AsConstant());

                                         }

                                     });



                                 }

                                 b.Statements.Return(CodeHelper.AsConstant(0));


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

                            int i = 0;
                            foreach (AstLabeledAlt alternative in ast.Alternatives)
                            {

                                i++;
                                var allCombinations = alternative.ResolveAllCombinations();

                                foreach (TreeRuleItem alt in allCombinations)
                                {

                                    StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                    var name = CodeHelper.FormatCsharp(ast.Name.Text);
                                    var tname = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                    var tname2 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                    if (ast.Alternatives.Count > 1)
                                    {
                                        tname2 = tname + "." + tname + (i).ToString();
                                    }
                                    var t1 = tname.AsType();
                                    var t3 = tname2.AsType();
                                    List<string> arguments = new List<string>();

                                    var method = name.AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                        .BuildDocumentation(ast.Name.Text, alt, ctx);

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
                                            method.Statements.Add(CodeHelper.DeclareAndCreate("result", t3, "arguments".Var()));
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

                    if (ast.Alternatives.Count > 1)
                    {
                        int i = 0;
                        foreach (var ast2 in ast.Alternatives)
                        {

                            type.CreateTypeFrom<AstBase>(ast => true, null, (ast3, type2) =>
                            {

                                type2.Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + (++i).ToString())
                                .Documentation(c => c.Summary(() => ast.Name.Text + " : " + ast2.ToString()))
                                .Inherit(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
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
                                     .CallBase("ctx")
                                     .Body(b =>
                                     {

                                     })
                                     ;
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

                                //.Fields(() =>
                                //      {
                                //          List<object> _properties = new List<object>();
                                //          var p = ast2.Select(c => c.Type == nameof(AstAlternativeList));
                                //          if (p.Count == 0)
                                //          {
                                //              var p2 = ast2.Select(
                                //                  c => c.Type == nameof(AstRuleRef)
                                //                  , c => c.Type == nameof(AstTerminalText)
                                //                  );
                                //              foreach (AstBase item2 in p2)
                                //                  _properties.Add(item2);
                                //          }
                                //          return _properties;
                                //      },
                                //      (field, model) =>
                                //      {
                                //          field.Name(m => CodeHelper.FormatCsharpField((model as AstBase).ResolveName()))
                                //          .Type(() => "Ast" + CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                //          .Attribute(MemberAttributes.Private)
                                //          ;
                                //      }
                                //    )

                                ;

                            });
                        }
                    }

                })

                .CreateTypeFrom<AstLabeledAlt>(null, null, (ast, type) =>
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
