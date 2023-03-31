using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{
    public class ModelConstructor : ModelMethod
    {

        public ModelConstructor(ModelTypeFrom modelTypeFrom)
            : base(modelTypeFrom)
        {
            this._callBase = new List<Func<object, CodeExpression>>();
        }

        public new ModelConstructor Attribute(MemberAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public new ModelConstructor Argument(string type, string name)
        {
            base.Argument(type, name);
            return this;
        }

        public new ModelConstructor Argument(Func<object> type, string name)
        {
            base.Argument(type, name);
            return this;
        }

        public new ModelConstructor Argument(Func<object> type, Func<object, string> name)
        {
            base.Argument(type, name);
            return this;
        }

        public ModelConstructor Arguments(Func<List<object>> list,  Func<object, (string, string)> func)
        {
            foreach (var item in list())
            {
                var i = func(item);
                base.Argument(i.Item1, i.Item2);
            }

            return this;
        }

        public ModelConstructor CallBase(params string[] values)
        {
            foreach (var value in values)
            {
                if (value == "null")
                    this._callBase.Add(c => new CodePrimitiveExpression(null));
                else
                    this._callBase.Add(c => new CodeVariableReferenceExpression(value));
            }

            return this;
        }

        public ModelConstructor CallBase(params CodeExpression[] values)
        {

            foreach (var value in values)
                    this._callBase.Add(c => value);

            return this;
        }

        public ModelConstructor CallBase(Func<object, CodeExpression> value)
        {
            this._callBase.Add(value);
            return this;
        }

        public new ModelConstructor Body(Action<CodeMemberMethod> action)
        {
            base.Body(action);
            return this;
        }

        public new ModelConstructor Documentation(Action<Documentation> action)
        {
            this._actionDocumentation = action;
            return this;
        }

        public override void Generate(Context ctx, object model, CodeTypeDeclaration t)
        {

            if (Test != null && !Test())
                return;

            CodeConstructor method = new CodeConstructor()
            {
                Attributes = _attributes,
            };

            foreach (var arg in _arguments)
                arg.Generate(ctx, model, method);


            foreach (var c in _callBase)
                method.BaseConstructorArgs.Add(c(model));

            if (this._body != null)
                this._body(method);

            t.Members.Add(method);

        }


        private readonly List<Func<object, CodeExpression>> _callBase;


    }



}
