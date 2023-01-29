using Bb.Asts;
using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{

    public class ModelField : ModelMember
    {

        public ModelField(ModelTypeFrom modelTypeFrom)
        {
            this.modelTypeFrom = modelTypeFrom;
        }

        public ModelField Name(Func<object, string> name)
        {
            this._nameOfField = name;
            return this;
        }

        public ModelField Value(Func<object, string> name)
        {
            this._nameOfField = name;
            return this;
        }

        public ModelField Attribute(MemberAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public ModelField Type(Type typeReturn)
        {
            this._type = new CodeTypeReference(typeReturn);
            return this;
        }

        public ModelField Type(string typeReturn)
        {
            this._type = new CodeTypeReference(typeReturn);
            return this;
        }


        public virtual void Generate(Context ctx, object model, CodeTypeDeclaration t)
        {

            if (Test != null && !Test())
                return;

            var _n = this._nameOfField(model);
            if (!MemberExists(t.Members, _n))
            {

                var configurationType = ctx.CurrentConfigurationType;
                ctx.CurrentConfigurationMethod = configurationType.GetMethod(_n);

                CodeMemberField field = new CodeMemberField()
                {
                    Name = _n,
                    Attributes = _attributes,
                    Type = _type,
                };

                if (_valueOfField != null)
                    field.InitExpression = new CodePrimitiveExpression((string)_valueOfField(model));

                t.Members.Add(field);

            }

        }


        protected ModelTypeFrom modelTypeFrom;
        private Func<object, string> _nameOfField;
        private Func<object, string> _valueOfField;
        protected MemberAttributes _attributes;
        private CodeTypeReference _type;

        public Func<IEnumerable<AstTerminalText>> Items { get; internal set; }
        public Action<ModelField, object> Action { get; internal set; }
        public Action<ModelField> Action2 { get; internal set; }
    }


}
