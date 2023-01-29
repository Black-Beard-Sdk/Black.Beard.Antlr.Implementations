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
        }

        public ModelProperty Name(Func<object, string> name)
        {
            this._nameOfProperty = name;
            return this;
        }

        public ModelProperty Attribute(MemberAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public ModelProperty Type(Type typeReturn)
        {
            this._type = new CodeTypeReference(typeReturn);
            return this;
        }

        public ModelProperty Type(string typeReturn)
        {
            this._type = new CodeTypeReference(typeReturn);
            return this;
        }


        public virtual void Generate(Context ctx, object model, CodeTypeDeclaration t)
        {

            if (Test != null && !Test())
                return;

            var _n = this._nameOfProperty(model);
            if (!MemberExists(t.Members, _n))
            {

                var configurationType = ctx.CurrentConfigurationType;
                ctx.CurrentConfigurationMethod = configurationType.GetMethod(_n);

                CodeMemberProperty field = new CodeMemberProperty()
                {
                    Name = _n,
                    Attributes = _attributes,
                    Type = _type,
                };

                t.Members.Add(field);

            }

        }


        protected ModelTypeFrom modelTypeFrom;
        private Func<object, string> _nameOfProperty;
        protected MemberAttributes _attributes;
        private CodeTypeReference _type;

        public Func<IEnumerable<object>> Items { get; internal set; }
        public Action<ModelProperty, object> Action { get; internal set; }
        public Action<ModelProperty> Action2 { get; internal set; }
    }



}
