using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{

    public class ScriptClassIdentifiers : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstTerminalIdentifier", ast, context);
        }

        public override string StrategyTemplateKey => "ClassIdentifiers";

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")

                      .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), (ast) =>
                      {

                          ctx.Variables["combinaisons"] = ast.ResolveAllCombinations();

                      }, (ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))

                              .Properties(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons"), (property, model) =>
                              {

                                  property.Name(a => "Value")
                                    .Attribute(MemberAttributes.Public)
                                    .HasSet(false)
                                    .Type(GetType(ctx, model))
                                    .Get(stm =>
                                    {
                                        stm.Return("_value".Var());
                                    });

                                  property.Type(GetType(ctx, model));

                              })

                              .Fields(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons"), (field, model) =>
                              {

                                  field.Name(a => "_value")
                                      .Attribute(MemberAttributes.Private)
                                      .Type(GetType(ctx, model))
                                      ;

                              })

                              .Ctors(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons"), (ctor, model) =>
                              {

                                  ctor.Argument(() => "ITerminalNode", "t")
                                      .Argument(() =>
                                      {
                                          var p = (model as AlternativeTreeRuleItem).Item;

                                          if (p.Origin.IsTerminal)
                                              return nameof(String);

                                          var o = p.Origin.Select(c => c.Type == "TOKEN_REF").FirstOrDefault();
                                          if (o != null)
                                          {
                                              return o.Type();

                                          }

                                          return p.Origin.Type();

                                      }, (model) => "value")
                                      .Attribute(MemberAttributes.Public)
                                      .CallBase("t".Var())
                                      .Body(ctor =>
                                      {
                                          var p = (model as AlternativeTreeRuleItem).Item;
                                          string template = p?.Origin?.Link.GetTemplate() ?? string.Empty;
                                          CodeExpression result = null;
                                          if (template == "ClassIdentifiers")
                                          {
                                              result = "value.Value".Var();
                                          }
                                          else
                                          {
                                              string type = string.Empty;
                                              if (p.Origin.IsTerminal)
                                                  type = nameof(String);
                                              else
                                                  type = p.Origin.Type();
                                              result = "value".Var();
                                              if (type == nameof(String))
                                                  result = result.Cast("AstTerminalString".AsType());
                                          }
                                          ctor.Statements.Assign("_value".Var(), result);
                                      })
                                      ;
                              })

                              .Ctors(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons"), (ctor, model) =>
                              {

                                  ctor.Argument(() => "ParserRuleContext", "ctx")
                                      .Argument(() =>
                                      {

                                          var p = (model as AlternativeTreeRuleItem).Item;

                                          if (p.Origin.IsTerminal)
                                              return nameof(String);

                                          return p.Origin.Type();


                                      }, (model) => "value")
                                      .Attribute(MemberAttributes.Public)
                                      .CallBase("ctx".Var())
                                      .Body(ctor =>
                                      {

                                          var p = (model as AlternativeTreeRuleItem).Item;
                                          string template = p?.Origin?.Link.GetTemplate() ?? string.Empty;
                                          CodeExpression result = null;

                                          if (template == "ClassIdentifiers")
                                          {
                                              result = "value.Value".Var();
                                          }
                                          else
                                          {

                                              string type = string.Empty;
                                              if (p.Origin.IsTerminal)
                                                  type = nameof(String);
                                              else
                                                  type = p.Origin.Type();

                                              result = "value".Var();
                                              if (type == nameof(String))
                                                  result = result.Cast("AstTerminalString".AsType());

                                          }

                                          ctor.Statements.Assign("_value".Var(), result);

                                      })
                                      ;
                              })

                              .Ctors(() => ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons"), (ctor, model) =>
                              {

                                  ctor.Argument(() => "Position", "position")
                                      .Argument(() =>
                                      {

                                          var p = (model as AlternativeTreeRuleItem).Item;

                                          if (p.Origin.IsTerminal)
                                              return nameof(String);

                                          return p.Origin.Type();


                                      }, (model) => "value")
                                      .Attribute(MemberAttributes.Public)
                                      .CallBase("position".Var())
                                      .Body(ctor =>
                                      {

                                          var p = (model as AlternativeTreeRuleItem).Item;
                                          string template = p?.Origin?.Link.GetTemplate() ?? string.Empty;
                                          CodeExpression result = null;

                                          if (template == "ClassIdentifiers")
                                          {
                                              result = "value.Value".Var();
                                          }
                                          else
                                          {

                                              string type = string.Empty;
                                              if (p.Origin.IsTerminal)
                                                  type = nameof(String);
                                              else
                                                  type = p.Origin.Type();

                                              result = "value".Var();
                                              if (type == nameof(String))
                                                  result = result.Cast("AstTerminalString".AsType());

                                          }

                                          ctor.Statements.Assign("_value".Var(), result);

                                      })
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

                              .Field(field =>
                                {
                                    field.Name("_kind")
                                         .Type("AstKindEnum")
                                         .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                         .Value((a) =>
                                         {
                                             return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("AstKindEnum".AsType()), "Identifier");
                                         });
                                })
                              .Property(property =>
                                {
                                    property.Name((a) => "Kind")
                                            .Type(() => "AstKindEnum")
                                            .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                            .Get((a) => a.Return("_kind".Var()))
                                            .HasSet(false)
                                            ;
                                })

                              .MethodWhen(() =>
                              {
                                  var o = ast.Select(c => c.Type == nameof(AstRuleRef)).ToList();
                                  foreach (var item in o)
                                  {
                                      var r1 = item.Link as AstRule;
                                      if (r1.Name.Text == "id_")
                                          return true;
                                  }

                                  return false;

                              }, method =>
                                {

                                    var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));

                                    method
                                     .Name(g => "New")
                                     .Argument("Position", "position")
                                     .Argument(() => typeof(String), "id")
                                     .Return(() => type)
                                     .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                     .Body(b =>
                                     {
                                         b.Statements.Return
                                         (
                                             CodeHelper.Create(type.AsType(), "position".Var(), CodeHelper.Create("AstId".AsType(), "position".Var(), "id".Var()))
                                         );
                                     })
                                     ;

                                })
                                .MethodWhen(() =>
                                {
                                    var o = ast.Select(c => c.Type == nameof(AstRuleRef)).ToList();
                                    foreach (var item in o)
                                    {
                                        var r1 = item.Link as AstRule;
                                        if (r1.Name.Text == "id_")
                                            return true;
                                    }

                                    return false;

                                }, method =>
                                {

                                    var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));

                                    method
                                     .Name(g => "New")                                     
                                     .Argument(() => typeof(String), "id")
                                     .Return(() => type)
                                     .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                     .Body(b =>
                                     {
                                         b.Statements.Return
                                         (
                                             type.AsType().Call("New", "Position.Default".Var(), "id".Var())
                                         );
                                     })
                                     ;

                                })

                                .Method(method =>
                                {
                                    var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                    method
                                     .Name(g => "Null")
                                     .Return(() => type + "?")
                                     .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                     .Body(b =>b.Statements.Return(CodeHelper.Null()))
                                     ;

                                })

                              ;
                      });
                });

            });

        }

        private static Func<string> GetType(Context ctx, object model)
        {

            Func<string> result = () => "AstRoot";

            var p1 = (model as AlternativeTreeRuleItem).Item;
            string template = p1?.Origin?.Link.GetTemplate() ?? string.Empty;
            if (template == "ClassIdentifiers")
            {

            }
            else
            {
                var combinaisons = ctx.Variables.Get<AlternativeTreeRuleItemList>("combinaisons");

                if (combinaisons.Count() == 1)
                {

                    var p = (model as AlternativeTreeRuleItem).Item;
                    var b = p.Origin.Select(c => c.TerminalKind == TokenTypeEnum.Identifier).Any();

                    if (b)
                        result = () => "AstTerminalString";

                    else
                        result = () => p.Origin.Type();
                }

            }
            return result;

        }


    }


}
