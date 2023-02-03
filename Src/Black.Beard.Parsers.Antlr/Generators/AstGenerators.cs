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

            _provider = new CSharpCodeProvider();
            _options = new CodeGeneratorOptions() { BracingStyle = "C" };

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
         
            HashSet<string> names = new HashSet<string>();

            var dir = new DirectoryInfo(ctx.Path);
            if (!dir.Exists)
                dir.Create();

            foreach (var item in dir.GetFiles("*.generated.cs"))
                names.Add(item.FullName);

            foreach (AstGenerator g in this._generators)
            {
                
                foreach (var item in _asts)
                    g.Generate(ctx, item);
                
                var name = WriteFile(ctx, g);
    
                if (names.Contains(name))
                    names.Remove(name);

            }

            foreach (var item in names)
                File.Delete(item);

        }

        private string WriteFile(Context ctx, AstGenerator g)
        {

            var filename = Path.Combine(ctx.Path, g.Name + ".generated.cs");                      

            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                _provider.GenerateCodeFromCompileUnit(g.CompileUnit, tw, _options);
                tw.Close();
            }

            return filename;

        }

        private readonly List<AstGenerator> _generators;
        private readonly List<AstBase> _asts;
        private readonly CSharpCodeProvider _provider;
        private readonly CodeGeneratorOptions _options;
    }



}
