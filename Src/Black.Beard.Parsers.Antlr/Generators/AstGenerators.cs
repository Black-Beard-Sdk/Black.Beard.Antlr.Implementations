using Bb.Asts;
using Bb.Parsers;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Bb.Generators
{

    public class AstGenerators
    {

        public AstGenerators()
        {
            this._generators = new List<AstGenerator>();
            this._asts = new List<AstBase>();
        }


        public void Add(AstRule a)
        {
            this._asts.Add(a);
        }


        public void Add(AstGenerator g)
        {
            this._generators.Add(g);
        }


        internal void Clear()
        {
            this._asts.Clear();
        }


        public void Generate(Context ctx)
        {
            
            foreach (AstGenerator g in this._generators) 
            {

                var compileUnit = g.Script.GetCompileUnit(ctx);

                foreach (var item in _asts)
                    g.Generate(ctx, item);

                CSharpCodeProvider provider = new CSharpCodeProvider();

                var filename = Path.Combine(ctx.Path, g.Name + ".generated.cs");

                // Create a TextWriter to a StreamWriter to the output file.
                using (StreamWriter sw = new StreamWriter(filename, false))
                {
                    IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                    provider.GenerateCodeFromCompileUnit(compileUnit, tw,
                        new CodeGeneratorOptions() { });
                    tw.Close();
                }


            }

        }

        private readonly List<AstGenerator> _generators;
        private readonly List<AstBase> _asts;

    }



}
