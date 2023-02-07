using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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

        protected override bool Generate(AstRule ast, Context context)
        {
            return context.Strategy == "ClassEnum";
        }

        protected override void ConfigureTemplate(Context context, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {

                template.Namespace("Bb.Asts", ns =>
                {

                    ns.CreateTypeFrom<AstRule>((ast, type) =>
                    {

                        type.AddTemplateSelector(() => TemplateSelector(ast, context))
                            .GenerateIf(() => Generate(ast, context))
                            .IsEnum()
                            .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name) + "Enum")
                            .Attribute(MemberAttributes.Public)
                            .Field((field) =>
                            {
                                field.Name(n => "_undefined");
                            })
                            .Fields(() => ast.GetTerminals(), (field, ast2) =>
                            {

                                field.Name(n => CodeHelper.FormatCsharp((ast2 as AstTerminalText).Text.ToLower()))
                                ;

                            });

                    });


                });
            });


        }

    }
}
