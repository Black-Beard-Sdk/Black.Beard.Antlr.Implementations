using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{
    public class ModelArgument
    {

        public ModelArgument(Func<object, string> name, Func<object> type)
        {
            this._name = name;
            this._type = type;
        }

        private Func<object, string> _name;
        private Func<object> _type;

        public void Generate(Context ctx, object model, CodeMemberMethod method)
        {

            CodeTypeReference type = null;
            var t = _type();
            
            if (t is string s)
                type = new CodeTypeReference(s);

            else if (t is Type t2)
                type = new CodeTypeReference(t2);

            else if (t is CodeTypeReference r)
                type = r;

            else
                throw new NotImplementedException(t.ToString());

            method.Parameters.Add(new CodeParameterDeclarationExpression(type, _name(model)));

        }
    }



}
