using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{
    public class AstGenerator
    {


        public string Name { get; internal set; }


        public ScriptAstBase Script { get; set; }


        public void Generate(Context ctx, AstBase ast, CodeObject codeObject)
        {
            Script.GenerateTo(  codeObject , ctx, ast);
        }


    }



}
