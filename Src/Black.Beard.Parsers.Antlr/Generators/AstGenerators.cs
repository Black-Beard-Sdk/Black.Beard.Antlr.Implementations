using Bb.Asts;
using Bb.Configurations;
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


        public void Add(AstBase a)
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

            CSharpCodeProvider provider = new CSharpCodeProvider();

            foreach (AstGenerator g in this._generators)
            {

                foreach (var item in _asts)
                    g.Generate(ctx, item);
                
                WriteFile(ctx, provider, g);

            }

        }

        private static void WriteFile(Context ctx, CSharpCodeProvider provider, AstGenerator g)
        {
            var filename = Path.Combine(ctx.Path, g.Name + ".generated.cs");

            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                provider.GenerateCodeFromCompileUnit(g.CompileUnit, tw,
                    new CodeGeneratorOptions() { });
                tw.Close();
            }
        }

        private readonly List<AstGenerator> _generators;
        private readonly List<AstBase> _asts;

    }



}
