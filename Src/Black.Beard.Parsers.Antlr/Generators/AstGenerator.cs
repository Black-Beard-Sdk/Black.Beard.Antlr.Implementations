using Bb.Asts;
using Bb.Configurations;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{

    public class AstGenerator
    {

        public AstGenerator()
        {

            CompileUnit = new CodeCompileUnit();
            this.Namespaces = new List<ModelNamespace>();
        }


        public string Name { get; internal set; }


        public CodeCompileUnit CompileUnit { get; private set; }


        public void Generate(Context ctx, AstBase ast)
        {
            foreach (var item in this.Namespaces)
                item.Generate(ctx, ast, this.CompileUnit);
        }

        public AstGenerator Namespace(string name, Action<ModelNamespace> action)
        {
            var ns = new ModelNamespace(name);
            this.Namespaces.Add(ns);
            action(ns);
            return this;
        }


        private List<ModelNamespace> Namespaces;

    }



}
