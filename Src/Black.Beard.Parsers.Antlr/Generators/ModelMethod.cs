using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{


    public class ModelMethod : ModelMember
    {

        public ModelMethod(ModelTypeFrom modelTypeFrom)
        {
            this.modelTypeFrom = modelTypeFrom;
            this._arguments = new List<ModelArgument>();
            this._actionType = () => typeof(void);
        }

        public ModelMethod Name(Func<object, string> name)
        {
            this._nameOfMethod = name;
            return this;
        }              

        public ModelMethod Documentation(Action<Documentation> action)
        {
            this._actionDocumentation = action;
            return this;
        }

        public ModelMethod Argument(string type, string name)
        {
            var arg = new ModelArgument(c => name, () => type);
            this._arguments.Add(arg);
            return this;
        }

        public ModelMethod Argument(Func<object> type, string name)
        {
            var arg = new ModelArgument(c => name, type);
            this._arguments.Add(arg);
            return this;
        }

        public ModelMethod Argument(Func<object> type, Func<object, string> name)
        {
            var arg = new ModelArgument(name, type);
            this._arguments.Add(arg);
            return this;
        }



        public ModelMethod Attribute(MemberAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public ModelMethod Return(Func<Type> typeReturn)
        {
            this._actionType = () => typeReturn();
            return this;
        }

        public ModelMethod Return(Func<string> typeReturn)
        {
            this._actionType = () => typeReturn();
            return this;
        }


        public ModelMethod Body(Action<CodeMemberMethod> action)
        {
            this._body = action;
            return this;
        }


        public virtual void Generate(Context ctx, object model, CodeTypeDeclaration t)
        {

            if (Test != null && !Test())
                return;

            var _n = this._nameOfMethod(model);
            if (!MemberExists(t.Members, _n))
            {

                CodeTypeReference type = null;
                if (_actionType != null)
                {
                    var t1 = _actionType();

                    if (t1 is string s)
                        type = new CodeTypeReference(s);

                    else if (t1 is Type i)
                        type = new CodeTypeReference(i);
                }
                else
                    type = new CodeTypeReference(typeof(void));

                CodeMemberMethod method = new CodeMemberMethod()
                {
                    Name = _n,
                    Attributes = _attributes,
                    ReturnType = type,
                };

                GenerateDocumentation(method, ctx);

                foreach (var arg in _arguments)
                    arg.Generate(ctx, model, method);

                if (this._body != null)
                    this._body(method);

                t.Members.Add(method);

            }

        }


        protected ModelTypeFrom modelTypeFrom;
        protected List<ModelArgument> _arguments;
        protected MemberAttributes _attributes;
        private Func<object> _actionType;
        private Func<object, string> _nameOfMethod;
        protected Action<CodeMemberMethod> _body;

    }



}
