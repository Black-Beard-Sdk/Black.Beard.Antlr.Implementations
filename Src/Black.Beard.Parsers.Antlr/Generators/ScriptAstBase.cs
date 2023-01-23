using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{


    public abstract class ScriptAstBase
    {


        public CodeCompileUnit GetCompileUnit(Context ctx)
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            return compileUnit;
        }

        public abstract CodeObject Get(Context ctx, CodeCompileUnit compileUnit);


        public abstract void GenerateTo(CodeObject codeObject, Context ctx, AstBase model);

    }


    public abstract class ScriptAstBase<T> : ScriptAstBase
        where T : CodeObject
    {

        public ScriptAstBase()
        {

        }

        public abstract void GenerateTo(T o, Context ctx, AstBase model);

        public override void GenerateTo(CodeObject codeObject, Context ctx, AstBase model)
        {
            GenerateTo((T)codeObject, ctx, model);
        }

    }



}
