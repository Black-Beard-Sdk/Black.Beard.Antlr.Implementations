using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Text;
using System.Xml.Linq;

namespace Generate.ModelsScripts
{


    public class ScriptClassTerminals : ScriptBase
    {

        
        private HashSet<string> _keys = new HashSet<string> { "ClassTerminalAlias" };

        public override string GetInherit(AstRule ast, Context context)
        {

            if (ast.IsConstant())
                return GetInherit_Impl("AstBnfRule", ast, context);

            var type2 = ast.Select(c => c.Type == "TOKEN_REF").FirstOrDefault()?.Type();
            if (type2 != null)
                return GetInherit_Impl("AstTerminal" + type2, ast, context);

            return GetInherit_Impl("AstTerminalString", ast, context);

        }

        public override HashSet<string> StrategyTemplateKeys => _keys;

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
                              .CtorWhen(() => ast.IsDynamic()
                              ,(f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "t".Var().Call("GetText"));
                              })
                              .CtorWhen(() => ast.IsDynamic()
                              , (f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })
                              .CtorWhen(() => ast.IsDynamic()
                              , (f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Argument(() => nameof(String), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })


                              // ParserRuleContext
                              .CtorWhen(() => ast.IsDynamic()
                              ,(f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), "ctx".Var().Call("GetText"));
                              })
                             .CtorWhen(() => ast.IsDynamic()
                             ,(f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), "value".Var());
                              })
                             .CtorWhen(() => ast.IsDynamic()
                             , (f) =>
                             {
                                 f.Argument(() => "ParserRuleContext", "ctx")
                                  .Argument(() => nameof(String), "value")
                                  .Attribute(MemberAttributes.Public)
                                  .CallBase("ctx".Var(), "value".Var());
                             })


                             .CtorWhen(() => ast.IsDynamic(), (f) =>
                              {
                                  f.Argument(() => "Position", "t")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })
                             .CtorWhen(() => ast.IsDynamic() && GetType(ast) != "String", (f) =>
                              {
                                  f.Argument(() => "Position", "t")
                                   .Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })


                             .CtorWhen(() => ast.IsDynamic(), (f) =>
                              {
                                  f.Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("Position.Default".Var(), "value".Var());
                              })
                             .CtorWhen(() => ast.IsDynamic() && GetType(ast) != "String", (f) =>
                              {
                                  f.Argument(() => GetType(ast), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("Position.Default".Var(), "value".Var());
                              })

                             .CtorWhen(() => ast.IsConstant()
                             , (f) =>
                             {
                                 f
                                  .Attribute(MemberAttributes.Public)
                                  .CallBase("Position.Default".Var());
                             })
                             .CtorWhen(() => ast.IsConstant()
                             , (f) =>
                             {
                                 f.Argument(() => "ParserRuleContext", "ctx")
                                  .Attribute(MemberAttributes.Public)
                                  .CallBase("ctx".Var());
                             })

                             .Method(method =>
                             {
                                 var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                 method.Name(g => "Null")
                                       .Return(() => type + "?")
                                       .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                       .Body(b => b.Statements.Return(CodeHelper.Null()))
                                  ;

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

                                 field.Name("_ruleValue")
                                 .Type(typeof(string))
                                 .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                 .Value((a) =>
                                 {
                                     return ast.Alternatives.ToString();
                                 })
                                 ;
                             })
                            .Field(field =>
                            {
                                field.Name("_ruleName")
                                     .Type(typeof(string))
                                     .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                     .Value((a) =>
                                     {
                                         return ast.Name.Text;
                                     });
                            })
                            .Property(property =>
                            {
                                property.Name((a) => "RuleName")
                                        .Type(() => typeof(string))
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Get((a) => a.Return("_ruleName".Var()))
                                        .HasSet(false)
                                        ;
                            })
                            .Property(property =>
                            {
                                property.Name((a) => "RuleValue")
                                        .Type(() => typeof(string))
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Get((a) => a.Return("_ruleValue".Var()))
                                        .HasSet(false)
                                        ;
                            })

                            .Field(field =>
                            {
                                field.Name("_isTerminal")
                                     .Type(typeof(bool))
                                     .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                     .Value((a) =>
                                     {

                                         var items = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");
                                         foreach (var item in items)
                                         {
                                             foreach (var item1 in item.Item)
                                             {
                                                 var oo = item1.Origin.Select(c => c.Type == nameof(AstRuleRef)).FirstOrDefault();
                                                 if (oo != null)
                                                     return CodeHelper.AsConstant(false);
                                             }
                                         }

                                         return CodeHelper.AsConstant(true);
                                     });
                            })

                            .Property(property =>
                            {
                                property.Name((a) => "IsTerminal")
                                        .Type(() => typeof(bool))
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Get((a) => a.Return("_isTerminal".Var()))
                                        .HasSet(false)
                                        ;
                            })

                            .Make(t =>
                            {

                                HashSet<string> _h = new HashSet<string>();
                                List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                                var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                                foreach (var alt in alternatives)
                                {
                                    var alternative = alt.Item;

                                    var txt = alternative.ToString().Trim();
                                    var n1 = CodeHelper.FormatCsharp(txt);
                                    var n2 = "Ast" + n1;
                                    var t1 = n2.AsType();

                                    StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                    List<string> arguments = new List<string>();

                                    var type = ast.Type();
                                    var typeAsType = type.AsType();
                                    var method1 = "New".AsMethod(typeAsType, MemberAttributes.Public | MemberAttributes.Static)
                                      .BuildDocumentation(ast.Name.Text, alternative, ctx);

                                    if (alternative.Count > 0)
                                    {

                                        foreach (var itemAlt in alternative)
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

                            .Make(t =>
                            {

                                HashSet<string> _h = new HashSet<string>();
                                List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                                var alternatives = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                                foreach (var alt in alternatives)
                                {
                                    var alternative = alt.Item;

                                    var txt = alternative.ToString().Trim();
                                    var n1 = CodeHelper.FormatCsharp(txt);
                                    var n2 = "Ast" + n1;
                                    var t1 = n2.AsType();

                                    StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                    List<string> arguments = new List<string>();

                                    var type = ast.Type();
                                    var typeAsType = type.AsType();
                                    var method1 = type.AsMethod("implicit operator".AsType(), MemberAttributes.Public | MemberAttributes.Static)
                                      .BuildDocumentation(ast.Name.Text, alternative, ctx);

                                    if (alternative.Count > 0)
                                    {

                                        foreach (var itemAlt in alternative)
                                            itemAlt.BuildStaticMethod(ast, method1, arguments, uniqeConstraintKeyMethod);

                                        var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                        if (arguments.Count == 1 && _h.Add(noDuplicateKey))
                                        {
                                            List<CodeExpression> args = new List<CodeExpression>(arguments.Count);
                                            var itemArg = arguments[0];
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

                            .Method(method =>
                            {
                                var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                method.Name(g => "ToString")
                                      .Argument("Writer", "writer")
                                      .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                      .Body(b =>
                                      {

                                          GenerateClassTerminalToString.Generate(ast, b.Statements);

                                      })
                                 ;

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

    }


}
