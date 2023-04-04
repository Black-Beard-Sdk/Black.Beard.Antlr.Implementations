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

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstTerminal<string>\"");

            return config.Inherit.Text;

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

                      .CreateTypeFrom<AstRule>((ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .GenerateIf(() => Generate(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))


                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "t".Var().Call("GetText"));
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), "ctx".Var().Call("GetText"));
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "t")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
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

                                  if (ast.Name.Text == "local_drive" /* "empty_statement"*/ /*"star_asterisk"*/)
                                  {

                                  }

                                  HashSet<string> _h = new HashSet<string>();
                                  List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                                  var alternatives = ast.GetAlternativesForTerminalsClass(ctx);

                                  foreach (var alt in alternatives)
                                  {


                                        var o = alt.Origin;
                                        if (o != null && o.Link is AstLexerRule astI)
                                        {

                                          if (astI.TerminalKind == TokenTypeEnum.Ponctuation)
                                              continue;

                                        }


                                      var n1 = CodeHelper.FormatCsharp(alt.Name);
                                      var n2 = "Ast" + n1;
                                      var t1 = n2.AsType();

                                      StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                      List<string> arguments = new List<string>();

                                      var method = n1.AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                        .BuildDocumentation(alt, ctx);

                                      if (alt.Count > 0)
                                          foreach (var itemAlt in alt)
                                              itemAlt.BuildStaticMethod(ast, method, arguments);

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

                      });

                });

            });

        }

    }


}
