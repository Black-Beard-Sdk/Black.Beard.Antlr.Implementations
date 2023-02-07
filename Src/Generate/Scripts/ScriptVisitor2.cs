﻿using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;

namespace Generate.Scripts
{
    public class ScriptVisitor2 : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRule";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return context.Strategy == "_";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(Name, template =>
            {
                template.Namespace("Bb.Asts", ns =>
                {
                    ns.Using("System")
                      .CreateOneType<AstLabeledAlt>((ast, type) =>
                      {
                          type.Name(() => "IAstTSqlVisitor")
                          .IsInterface()
                          .Method(m =>
                          {
                              m.Name(g => "Visit" + CodeHelper.FormatCamelUpercase(ast.Identifier.Text))
                               .Argument(() => "Ast" + CodeHelper.FormatCsharp(ast.Identifier.Text), "a")
                              ;
                          });
                      });
                });
            });

        }

    }


}