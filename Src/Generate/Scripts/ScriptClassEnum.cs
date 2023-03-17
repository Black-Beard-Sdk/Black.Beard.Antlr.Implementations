using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassEnum : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("'AstTerminal<Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum>'");

            return config.Inherit.Text;

        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return TemplateSelector(ast, context) == "ClassEnum";
        }

        protected override void ConfigureTemplate(Context context, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(this.Usings)
                      .Using("Antlr4.Runtime")
                      .Using("System.Collections")
                      .Using("Antlr4.Runtime.Tree")

                      .CreateTypeFrom<AstRule>((ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, context))
                              .GenerateIf(() => Generate(ast, context))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, context))

                              .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType().Call("GetValue", "value".Var()));
                              })
                              .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                              {
                                  f.Argument(() => "ITerminalNode", "t")
                                   .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum", "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("t".Var(), "value".Var());
                              })
                              .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx".Var(), ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType().Call("GetValue", "value".Var()));
                              })
                              .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                              {
                                  f.Argument(() => "Position", "p")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p".Var(), ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType().Call("GetValue", "value".Var()));
                              })
                              .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                              {
                                  f.Argument(() => "Position", "p")
                                   .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum", "value")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p".Var(), "value".Var());
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

                              .MethodWhen(() => context.Strategy == "ClassEnum", method =>
                              {
                                  method
                                   .Name(g => "GetValue")
                                   .Argument(() => typeof(string), "value")
                                   .Attribute(MemberAttributes.Family | MemberAttributes.Static)
                                   .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum")
                                   .Body(b =>
                                   {
                                       string typeEnum = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text) + "Enum";

                                       var items = ast.GetTerminals().ToList();
                                       foreach (AstTerminalText text in items)
                                       {
                                           var test = "value".Var().IsEqual(text.Text.AsConstant());
                                           b.Statements.If(test, t =>
                                           {
                                               t.Return(typeEnum.AsType().Field(CodeHelper.FormatCsharp(text.Text.ToLower())));
                                           });
                                       }

                                       b.Statements.Return(typeEnum.AsType().Field("_undefined"));

                                   });
                              });

                              
                      })
                      ;

                });

            });

        }

    }


}
