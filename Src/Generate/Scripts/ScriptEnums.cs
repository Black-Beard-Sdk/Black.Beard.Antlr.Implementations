using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generate.Scripts
{


    public class ScriptEnums : ScriptBase
    {


        public override string GetInherit(AstRule ast, Context context)
        {
            return null; // "AstTerminal<Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum>";
        }

        public override string StrategyTemplateKey => "ClassEnum";


        protected override void ConfigureTemplate(Context context, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {

                    ns.CreateTypeFrom<AstRule>((ast, type) =>
                    {

                        type.AddTemplateSelector(() => TemplateSelector(ast, context))
                            .GenerateIf(() => Generate(ast, context))                            
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                            .Attribute(TypeAttributes.Public)
                            .Field((field) =>
                            {
                                field.Name(n => "_undefined");
                            })

                            .CtorWhen(() => context.Strategy == "ClassEnum", (f) =>
                            {
                                f.Attribute(MemberAttributes.Family)
                                .Argument(() => "ITerminalNode", "t")
                                 .Argument(() => typeof(string), "value")
                                 .Attribute(MemberAttributes.Public)
                                 .CallBase("t".Var());
                            })

                            .Method((method) =>
                            {

                                method.Name(n => "_undefined")
                                .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                                .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                                .Body(m =>
                                {
                                    var typeName = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    m.Statements.Return(CodeHelper.Create(typeName.AsType(), CodeHelper.Null()));

                                })
                                ;

                            })

                            .Methods(() => ast.GetTerminals(), (method, ast2) =>
                            {

                                method.Name(n => CodeHelper.FormatCsharp((ast2 as AstTerminalText).Text.ToLower()))
                                .Return(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                                .Attribute(MemberAttributes.Public | MemberAttributes.Static)
                                .Body(m =>
                                {

                                    var a = ast2 as AstBase;
                                    var l = a.Link as AstLexerRule;

                                    var typeName = "Ast" + CodeHelper.FormatCsharp(ast.Name.Text);
                                    m.Statements.Return(CodeHelper.Create(typeName.AsType(), "Position.Default".Var(), l.Value.ToString().Trim('\'').AsConstant()));

                                })
                                ;

                            });

                    });


                });
            });


        }

    }
}
