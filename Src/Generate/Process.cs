using Bb;
using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generate
{

    public class Process
    {
        private string _folder;

        public Process(FileInfo antlrParser, Context ctx)
        {

            if (antlrParser.Exists)
            {

                this._folder = antlrParser.Directory.FullName;

                var sb = new StringBuilder(antlrParser.LoadFromFile());

                var parser = ScriptParser.ParseString(sb, antlrParser.FullName);


                var visitor = new ScriptAntlrVisitor(parser.Parser, new Diagnostics(), this._folder);
                var ast = parser.Visit(visitor);


                var visitor2 = new ParentVisitor();
                visitor2.Visit(ast);


                var visitor3 = new CodeVisitor();
                visitor3.Visit(ast);


                var visitor4 = new CodeGeneratorVisitor(ctx)
                    .Add("model", c =>
                    {
                        c.Script = new ScriptAstOnNamespace()
                        {
                            NameOfClass = u => (u as AstRule).RuleName.Text
                        }
                        .Using("System")
                        .Using("Bb.Asts")
                        ;
                    })
                    ;
                visitor4.Visit(ast);


            }

        }


    }

}
