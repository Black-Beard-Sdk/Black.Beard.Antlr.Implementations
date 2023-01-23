using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{

    public abstract class ScriptAstBase<T>
        where T : CodeObject
    {


        public ScriptAstBase()
        {
        }

        public CodeCompileUnit GetCompileUnit(Context ctx)
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            return compileUnit;
        }

        public abstract T Get(Context ctx, CodeCompileUnit compileUnit);

        public void GenerateTo(CodeObject codeObject, Context ctx, AstBase model)
        {
            GenerateTo((T)codeObject, ctx, model);
        }


        public abstract void GenerateTo(T o, Context ctx, AstBase model);

    
    }



}
