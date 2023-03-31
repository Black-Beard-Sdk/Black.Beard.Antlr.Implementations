using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{
    public class ModelProperty : ModelMember
    {

        public ModelProperty(ModelTypeFrom modelTypeFrom)
        {
            this.modelTypeFrom = modelTypeFrom;
            this._hasGet = true;
            this._hasSet = true;
        }

        public ModelProperty Name(Func<object, string> name)
        {
            this._nameOfProperty = name;
            return this;
        }

        public ModelProperty Documentation(Action<Documentation> action)
        {
            this._actionDocumentation = action;
            return this;
        }

        /// <summary>
        /// Attributes the specified attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public ModelProperty Attribute(MemberAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public ModelProperty HasGet(bool value)
        {
            this._hasGet = value;
            return this;
        }

        public ModelProperty Get(Action<CodeStatementCollection> action)
        {
            this._getAction = action;
            return this;
        }

        public ModelProperty Set(Action<CodeStatementCollection> action)
        {
            this._setAction = action;
            return this;
        }

        public ModelProperty HasSet(bool value)
        {
            this._hasSet = value;
            return this;
        }

        public ModelProperty Type(Func<Type> typeReturn)
        {
            this._actionType = () => typeReturn();
            return this;
        }

        public ModelProperty Type(Func<string> typeReturn)
        {
            this._actionType = () => typeReturn();
            return this;
        }


        public virtual void Generate(Context ctx, object model, CodeTypeDeclaration t)
        {

            if (Test != null && !Test())
                return;

            var _n = this._nameOfProperty(model);

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
                type = new CodeTypeReference(typeof(object));

            CodeMemberProperty property = new CodeMemberProperty()
            {
                Name = _n,
                Attributes = _attributes,
                Type = type,
                HasGet = _hasGet,
                HasSet = _hasSet,
            };

            GenerateDocumentation(property, ctx);

            if (_getAction != null)
                _getAction(property.GetStatements);

            if (_setAction != null)
                _setAction(property.SetStatements);

            if (!MemberExists(t.Members, property))
                t.Members.Add(property);

        }


        protected ModelTypeFrom modelTypeFrom;
        private Func<object, string> _nameOfProperty;
        protected MemberAttributes _attributes;
        private Func<object> _actionType;
        private bool _hasGet;
        private bool _hasSet;
        private Action<CodeStatementCollection> _getAction;
        private Action<CodeStatementCollection> _setAction;

        public Func<IEnumerable<object>> Items { get; internal set; }
        public Action<ModelProperty, object> Action { get; internal set; }
        public Action<ModelProperty> Action2 { get; internal set; }

        protected Action<Documentation> _actionDocumentation;

    }



}
