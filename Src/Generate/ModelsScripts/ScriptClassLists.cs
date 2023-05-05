using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.ModelsScripts
{
    public class ScriptClassLists : ScriptBase
    {

        private HashSet<string> _keys = new HashSet<string> { "ClassList" };

        public override string GetInherit(AstRule ast, Context context)
        {
            var astChild = ast.GetRules().FirstOrDefault();
            var txt = "AstRuleList<Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + ">";
            return GetInherit_Impl(txt, ast, context);
        }

        public override HashSet<string> StrategyTemplateKeys => _keys;

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {

                    ns.Using(Usings)
                      .Using("System")
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")
                      .Using("System.Collections")

                      .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))

                              .Ctor((f) =>
                              {

                                  var astChild = ast.GetRules().FirstOrDefault();

                                  f.Attribute(MemberAttributes.Public)
                                   .CallBase("Position.Default", "items");

                                  if (astChild != null)
                                      f.Argument("params Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + "[]", "items");

                              })

                              .Ctor((f) =>
                              {

                                  var astChild = ast.GetRules().FirstOrDefault();

                                  f.Argument(() => "Position", "position")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("position", "items");

                                  if (astChild != null)
                                      f.Argument("params Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + "[]", "items");

                              })

                              .Ctor((f) =>
                              {

                                  var astChild = ast.GetRules().FirstOrDefault();

                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx", "items");

                                  if (astChild != null)
                                      f.Argument("params Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + "[]", "items");

                              })

                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => typeof(int), "capacity")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx", "capacity");
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

                              .Method(method =>
                              {

                                  var astChild = ast.GetRules().FirstOrDefault();
                                  var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));

                                  method
                                   .Name(g => "New")
                                   .Argument("ParserRuleContext", "ctx")
                                   .Return(() => type)
                                   .Documentation(doc =>
                                   {
                                       doc.Summary(() => ast.ToString());
                                   })
                                   .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                   .Body(b =>
                                   {
                                       b.Statements.Return
                                       (
                                           CodeHelper.Create(type.AsType(), "ctx".Var(), "items".Var())
                                       );
                                   });

                                  if (astChild != null)
                                      method.Argument("params Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + "[]", "items");

                              })

                              .Method(method =>
                              {

                                  var astChild = ast.GetRules().FirstOrDefault();
                                  var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));

                                  method
                                   .Name(g => "New")
                                   .Argument("Position", "position")
                                   .Return(() => type)
                                   .Documentation(doc =>
                                   {
                                       doc.Summary(() => ast.ToString());
                                   })
                                   .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                   .Body(b =>
                                   {
                                       b.Statements.Return
                                       (
                                           CodeHelper.Create(type.AsType(), "position".Var(), "items".Var())
                                       );
                                   });

                                  if (astChild != null)
                                      method.Argument("params Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + "[]", "items");

                              })

                              .Method(method =>
                              {

                                  var astChild = ast.GetRules().FirstOrDefault();
                                  var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));

                                  method
                                   .Name(g => "New")
                                   .Return(() => type)
                                   .Documentation(doc =>
                                   {
                                       doc.Summary(() => ast.ToString());
                                   })
                                   .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                   .Body(b =>
                                   {
                                       b.Statements.Return
                                       (
                                           CodeHelper.Create(type.AsType(), "Position.Default".Var(), "items".Var())
                                       );
                                   });

                                  if (astChild != null)
                                      method.Argument("params Ast" + CodeHelper.FormatCsharp(astChild.Name.Text) + "[]", "items");

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


                              .Method(method =>
                              {
                                  var c = ast.Select<AstTerminalText>(c => c.Type == "TOKEN_REF");
                                  if (c.Count <= 1)
                                  {
                                      var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                      method.Name(g => "ToString")
                                            .Argument("Writer", "writer")
                                            .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                            .Body(b =>
                                            {

                                                GenerateClassListToString.Generate(ast, b.Statements);

                                            })
                                       ;
                                  }
                                  else
                                  {
                                      method.Name(g => "ToString")
                                            .Argument("Writer", "writer")
                                            .Argument("StrategySerializationItem", "strategy")
                                            .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                            .Return(() => typeof(bool))
                                            .Body(b =>
                                            {

                                                GenerateClassListToString.Generate(ast, b.Statements);
                                              b.Statements.Return(CodeHelper.AsConstant(true));

                                            })
                                       ;
                                  }

                              })

                              ;
                      });

                });

            });

        }

    }


}
