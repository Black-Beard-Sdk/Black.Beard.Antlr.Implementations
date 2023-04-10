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

            string _n = null;

            if (_nameOfMethod != null)
                _n = this._nameOfMethod(model);

            if (_n == null)
                return;

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

            
            if (!MemberExists(t.Members, method))
                t.Members.Add(method);

        }


        protected override bool MemberExists(CodeTypeMemberCollection members, CodeTypeMember member)
        {

            var m1 = member as CodeMemberMethod;
            var n = member.Name;

            foreach (CodeTypeMember item in members)
                if (item.Name == n)
                {

                    var m2 = item as CodeMemberMethod;
                    if (m2 == null)
                        return true;

                    if (CompareMethods(m1, m2))
                        return true;

                }

            return false;

        }

        private bool CompareMethods(CodeMemberMethod m1, CodeMemberMethod m2)
        {

            var countParameter = m1.Parameters.Count;

            if (m2.Parameters.Count == countParameter && m2.Parameters.Count == 0)
                return true;

            if (m2.Parameters.Count != countParameter)
                return false;

            for (int i = 0; i < countParameter; i++)
            {

                if (!CompareArguments(m1.Parameters[i], m2.Parameters[i]))
                    return false;

            }

            return true;

        }

        private bool CompareArguments(CodeParameterDeclarationExpression arg1, CodeParameterDeclarationExpression arg2)
        {
            return CompareTypes(arg1.Type, arg2.Type);
        }

        private bool CompareTypes(CodeTypeReference type1, CodeTypeReference type2)
        {

            if (type1.BaseType == type2.BaseType)
            {

                if (type1.ArrayRank == type2.ArrayRank)
                {

                    if (type1.TypeArguments.Count == type2.TypeArguments.Count)
                    {


                        if (type1.TypeArguments.Count == 0)
                            return true;

                        var countParameter = type1.TypeArguments.Count;

                        for (int i = 0; i < countParameter; i++)
                        {

                            if (!CompareTypes(type1.TypeArguments[i], type1.TypeArguments[i]))
                                return false;

                        }

                        return true;

                    }


                }

            }

            return false;
        }


        protected ModelTypeFrom modelTypeFrom;
        protected List<ModelArgument> _arguments;
        protected MemberAttributes _attributes;
        private Func<object> _actionType;
        private Func<object, string> _nameOfMethod;
        protected Action<CodeMemberMethod> _body;

        public override void Clean()
        {
            
            _arguments.Clear();

        }

        public Func<IEnumerable<object>> Items { get; internal set; }
        public Action<ModelMethod, object> Action { get; internal set; }
        public Action<ModelMethod> Action2 { get; internal set; }


    }



}
