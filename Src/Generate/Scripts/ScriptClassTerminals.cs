using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Text;
using System.Xml.Linq;

namespace Generate.Scripts
{


    public class ScriptClassTerminals : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            if (IsConstant(ast))
                return GetInherit_Impl("AstBnfRule", ast, context);

            var type2 = ast.Select(c => c.Type == "TOKEN_REF").FirstOrDefault()?.Type();
            if (type2 != null)
                return GetInherit_Impl("AstTerminal" + type2, ast, context);

            return GetInherit_Impl("AstTerminalString", ast, context);

        }

        public override string StrategyTemplateKey => "ClassTerminalAlias";


        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")

                      .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), ast =>
                      {
                          ctx.Variables["combinaisons"] = ast.ResolveAllCombinations();

                      }, (ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => ast.Type())
                              .Inherit(() => GetInherit(ast, ctx))

                              // ITerminalNode
                              .CtorWhen(() => IsDynamic(ast)                              
                              ,(f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "t".Var().Call("GetText"));
                              })
                              .CtorWhen(() => IsDynamic(ast)
                              , (f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })
                              .CtorWhen(() => IsDynamic(ast)
                              , (f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Argument(() => nameof(String), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })


                              // ParserRuleContext
                              .CtorWhen(() => IsDynamic(ast)
                              ,(f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), "ctx".Var().Call("GetText"));
                              })
                             .CtorWhen(() => IsDynamic(ast)
                             ,(f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), "value".Var());
                              })
                             .CtorWhen(() => IsDynamic(ast)
                             , (f) =>
                             {
                                 f.Argument(() => "ParserRuleContext", "ctx")
                                  .Argument(() => nameof(String), "value")
                                  .Attribute(MemberAttributes.Public)
                                  .CallBase("ctx".Var(), "value".Var());
                             })


                             .CtorWhen(() => IsDynamic(ast), (f) =>
                              {
                                  f.Argument(() => "Position", "t")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })
                             .CtorWhen(() => IsDynamic(ast) && GetType(ast) != "String", (f) =>
                              {
                                  f.Argument(() => "Position", "t")
                                   .Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })


                             .CtorWhen(() => IsDynamic(ast), (f) =>
                              {
                                  f.Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("Position.Default".Var(), "value".Var());
                              })
                             .CtorWhen(() => IsDynamic(ast) && GetType(ast) != "String", (f) =>
                              {
                                  f.Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("Position.Default".Var(), "value".Var());
                              })


                             .CtorWhen(() =>
                              {
                                  return IsConstant(ast);
                              }, (f) =>
                             {
                                 f
                                  .Attribute(MemberAttributes.Public)
                                  .CallBase("Position.Default".Var());
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

                             .Make(t =>
                              {

                                  HashSet<string> _h = new HashSet<string>();
                                  List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                                  var alternatives = ctx.Variables.Get<List<TreeRuleItem>>("combinaisons");

                                  foreach (var alt in alternatives)
                                  {

                                      var txt = alt.ToString().Trim();
                                      var n1 = CodeHelper.FormatCsharp(txt);
                                      var n2 = "Ast" + n1;
                                      var t1 = n2.AsType();

                                      StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                      List<string> arguments = new List<string>();

                                      var method1 = n1.AsMethod(ast.Type().AsType(), MemberAttributes.Public | MemberAttributes.Static)
                                        .BuildDocumentation(ast.Name.Text, alt, ctx);

                                      if (alt.Count > 0)
                                      {

                                          foreach (var itemAlt in alt)
                                              itemAlt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);

                                          var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                          if (_h.Add(noDuplicateKey))
                                          {
                                              List<CodeExpression> args = new List<CodeExpression>(arguments.Count);
                                              foreach (var itemArg in arguments)
                                                  args.Add(itemArg.Var());

                                              methods.Add(method1);
                                              //var ret = CodeHelper.Call(t1, n1);
                                              method1.Statements.Return(CodeHelper.Create(ast.Type().AsType(), args.ToArray()));
                                          }

                                      }
                                      
                                  }

                                  foreach (var item in methods)
                                      t.Members.Add(item);

                              })
                              ;

                      });

                });

            });

        }


        private static string GetType(AstBase ast)
        {
            string type = nameof(String);
            var type2 = ast.Select(c => c.Type == "TOKEN_REF").FirstOrDefault()?.Type();
            if (type2 != null)
                type = type2;
            return type;
        }

        private static bool IsConstant(AstRule ast)
        {
            var l = ast.Select(c => c.Type == nameof(AstTerminal)).FirstOrDefault();
            if (l != null)
            {
                var m = l.Select(c => c.Type == "TOKEN_REF").FirstOrDefault();
                if (m != null)
                {
                    var t = m.Link.TerminalKind;
                    if (t == TokenTypeEnum.Constant)
                        return true;

                    if (t == TokenTypeEnum.Ponctuation)
                        return true;

                    if (t == TokenTypeEnum.Operator)
                        return true;

                    else
                    {

                    }
                }
            }

            return false;

        }
        
        private static bool IsDynamic(AstRule ast)
        {
            var l = ast.Select(c => c.Type == nameof(AstTerminal)).FirstOrDefault();
            if (l != null)
            {
                var m = l.Select(c => c.Type == "TOKEN_REF").FirstOrDefault();
                if (m != null)
                {
                    var t = m.Link.TerminalKind;
                    switch (t)
                    {


                        case TokenTypeEnum.Identifier:
                        case TokenTypeEnum.Boolean:
                        case TokenTypeEnum.String:
                        case TokenTypeEnum.Decimal:
                        case TokenTypeEnum.Int:
                        case TokenTypeEnum.Real:
                        case TokenTypeEnum.Hexa:
                        case TokenTypeEnum.Binary:
                        case TokenTypeEnum.Pattern:
                            return true;

                        case TokenTypeEnum.Operator:
                        case TokenTypeEnum.Other:
                        case TokenTypeEnum.Constant:
                        case TokenTypeEnum.Comment:
                        case TokenTypeEnum.Ponctuation:
                        default:
                            break;
                    }
                }
            }

            return false;

        }


    }


}
