using Bb;
using Bb.Asts;
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

        public Process(FileInfo antlrParser)
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


                var ctx = new Context() 
                {
                    Path = ""
                }
                ;
                var visitor4 = new CodeGeneratorVisitor(ctx);
                visitor4.Visit(ast);


            }

        }


    }

}
