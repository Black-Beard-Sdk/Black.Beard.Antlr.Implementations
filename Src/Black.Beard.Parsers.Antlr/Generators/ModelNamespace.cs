using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{
    public class ModelNamespace
    {

        public ModelNamespace(string name)
        {
            this._name = name;
            _usings = new HashSet<string>();
            this._typeBuilder = new List<ModelTypeFrom>();
        }

        public ModelNamespace Using(IEnumerable<string> usings)
        {
            foreach (var us in usings)
                if (!string.IsNullOrEmpty(us))
                    this._usings.Add(us);
            return this;
        }

        public ModelNamespace Using(params string[] usings)
        {
            foreach (var us in usings)
                if (!string.IsNullOrEmpty(us))
                    this._usings.Add(us);
            return this;
        }

        public ModelNamespace CreateTypeFrom<T>(Func<T, bool> testGenerateIf, Action<T> prepare, Action<T, ModelTypeFrom<T>> action)
            where T : AstBase
        {
            var t = new ModelTypeFrom<T>(this, testGenerateIf, prepare, action);
            this._typeBuilder.Add(t);
            return this;
        }

        public ModelNamespace CreateOneType<T>(Func<T, bool> testGenerateIf, Action<T> prepare, Action<T, ModelTypeFrom<T>> action)
            where T : AstBase
        {
            var t = new ModelTypeFrom<T>(this, testGenerateIf, prepare, action)
            {
                _justOne = true,
            };
            this._typeBuilder.Add(t);
            return this;
        }

        internal void Generate(Context ctx, AstBase ast, CodeCompileUnit compileUnit)
        {                      

            foreach (var type in _typeBuilder)
            {

                if (ast.GetType() == type.Type)
                {

                    if (this._namespace == null)
                    {
                        this._namespace = new CodeNamespace(this._name);
                        foreach (var us in _usings)
                            this._namespace.Imports.Add(new CodeNamespaceImport(us));
                        compileUnit.Namespaces.Add(this._namespace);
                    }

                    type.Generate(ctx, ast, _namespace.Types);

                }

            }        

        }

        private CodeNamespace _namespace;
        private HashSet<string> _usings;
        private List<ModelTypeFrom> _typeBuilder;

        private readonly string _name;
    }



}
