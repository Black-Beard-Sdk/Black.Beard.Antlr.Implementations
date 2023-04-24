using Bb.Asts;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bb.Generators
{


    public class ModelTypeFrom<T> : ModelTypeFrom
        where T : AstBase
    {

        public ModelTypeFrom(ModelTypeFrom parent, Func<T, bool> testGenerateIf, Action<T> prepareAction, Action<T, ModelTypeFrom<T>> action)
            : base(parent)
        {
            _typeBuilder = new List<ModelTypeFrom>();
            _parents = new List<Func<string>>();
            _methods = new List<ModelMethod>();
            _fields = new List<ModelField>();
            _properties = new List<ModelProperty>();
            this._attributes = TypeAttributes.Class | TypeAttributes.Public;
            this.testGenerateIf = testGenerateIf;
            this._prepareAction = prepareAction;
            this._action = action;
        }

        public ModelTypeFrom(ModelNamespace modelNamespace, Func<T, bool> testGenerateIf, Action<T> prepareAction, Action<T, ModelTypeFrom<T>> action)
            : base(modelNamespace)
        {
            _typeBuilder = new List<ModelTypeFrom>();
            _parents = new List<Func<string>>();
            _methods = new List<ModelMethod>();
            _fields = new List<ModelField>();
            _properties = new List<ModelProperty>();
            this._attributes = TypeAttributes.Class | TypeAttributes.Public;
            this.testGenerateIf = testGenerateIf;
            this._prepareAction = prepareAction;
            this._action = action;
        }

        public override Type Type => typeof(T);

        public new ModelTypeFrom<T> CreateTypeFrom(Func<T, bool> testGenerateIf, Action<T> prepare, Action<T, ModelTypeFrom<T>> action)
        {
            var t = new ModelTypeFrom<T>(this, testGenerateIf, prepare, action);
            this._typeBuilder.Add(t);
            return this;
        }

        public ModelTypeFrom<T> Attribute(TypeAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public ModelTypeFrom<T> IsInterface()
        {
            this._isInterface = true;
            this._isEnum = false;
            this._isStruct = false;
            _isStatic = false;
            return this;
        }

        public ModelTypeFrom<T> IsStatic()
        {
            this._isInterface = false;
            this._isEnum = false;
            this._isStruct = false;
            _isStatic = true;

            return this;
        }

        public ModelTypeFrom<T> IsEnum()
        {
            this._isInterface = false;
            this._isEnum = true;
            this._isStruct = false;
            _isStatic = false;
            return this;
        }


        public ModelTypeFrom<T> Documentation(Action<Documentation> action)
        {
            this._actionDocumentation = action;
            return this;
        }

        public ModelTypeFrom<T> IsStruct()
        {
            this._isInterface = false;
            this._isEnum = false;
            this._isStruct = true;
            return this;
        }

        /// <summary>
        /// Names the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ModelTypeFrom<T> Name(Func<string> name)
        {
            this._nameOfClass = name;
            return this;
        }

        /// <summary>
        /// Inherits the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ModelTypeFrom<T> Inherit(Func<string> name)
        {
            this._parents.Add(name);
            return this;
        }


        public ModelTypeFrom<T> Ctor(Action<ModelConstructor> action)
        {
            var m = new ModelConstructor(this);
            this._methods.Add(m);
            action(m);
            return this;
        }
        public ModelTypeFrom<T> CtorWhen(Func<bool> test, Action<ModelConstructor> action)
        {
            var m = new ModelConstructor(this) { Test = test };
            this._methods.Add(m);
            action(m);
            return this;
        }
        public ModelTypeFrom<T> Ctors(Func<IEnumerable<object>> items, Action<ModelConstructor, object> action)
        {
            Action<ModelMethod, object> a = (c, d) => action((ModelConstructor)c, d);
            var m = new ModelConstructor(this) { Items = items, Action = a };
            this._methods.Add(m);
            return this;
        }

        /// <summary>
        /// Add a field and use the action for generate.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public ModelTypeFrom<T> Field(Action<ModelField> action)
        {
            var m = new ModelField(this) { Items = null, Action = null, Action2 = action };
            this._fields.Add(m);
            return this;
        }
        public ModelTypeFrom<T> FieldWhen(Func<bool> test, Action<ModelField> action)
        {
            var m = new ModelField(this) { Test = test, Items = null, Action = null, Action2 = action };
            this._fields.Add(m);
            return this;
        }


        public ModelTypeFrom<T> Fields(Func<IEnumerable<object>> items, Action<ModelField, object> action)
        {
            var m = new ModelField(this) { Items = items, Action = action };
            this._fields.Add(m);
            return this;
        }

        public ModelTypeFrom<T> FieldsWhen(Func<bool> test, Func<IEnumerable<object>> items, Action<ModelField, object> action)
        {
            var m = new ModelField(this) { Test = test, Items = items, Action = action };
            this._fields.Add(m);
            return this;
        }


        public ModelTypeFrom<T> Property(Action<ModelProperty> action)
        {
            var m = new ModelProperty(this) { Items = null, Action = null, Action2 = action };
            this._properties.Add(m);
            return this;
        }
        public ModelTypeFrom<T> PropertyWhen(Func<bool> test, Action<ModelProperty> action)
        {
            var m = new ModelProperty(this) { Test = test, Items = null, Action = null, Action2 = action };
            this._properties.Add(m);
            return this;
        }

        public new ModelTypeFrom<T> Make(Action<CodeTypeDeclaration> action)
        {
            this._actions.Add(action);
            return this;
        }

        public ModelTypeFrom<T> Properties(Func<IEnumerable<object>> items, Action<ModelProperty, object> action)
        {
            var m = new ModelProperty(this) { Items = items, Action = action };
            this._properties.Add(m);
            return this;
        }
        public ModelTypeFrom<T> PropertiesWhen(Func<bool> test, Func<IEnumerable<object>> items, Action<ModelProperty, object> action)
        {
            var m = new ModelProperty(this) { Test = test, Items = items, Action = action };
            this._properties.Add(m);
            return this;
        }


        public ModelTypeFrom<T> MethodWhen(Func<bool> test, Action<ModelMethod> action)
        {
            var m = new ModelMethod(this) { Test = test };
            this._methods.Add(m);
            action(m);
            return this;
        }
        public ModelTypeFrom<T> Method(Action<ModelMethod> action)
        {
            var m = new ModelMethod(this);
            this._methods.Add(m);
            action(m);
            return this;
        }

        public ModelTypeFrom<T> Methods(Func<IEnumerable<object>> items, Action<ModelMethod, object> action)
        {
            var m = new ModelMethod(this) { Items = items, Action = action };
            this._methods.Add(m);
            return this;
        }

        /// <summary>
        /// Generates the specified CTX.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="ast">The ast.</param>
        /// <param name="namespace">The namespace.</param>
        /// <exception cref="System.Exception"></exception>
        internal override void Generate(Context ctx, AstBase ast, CodeTypeDeclarationCollection types)
        {
            if (ast is T a)
            {

                var b = new ModelTypeFrom<T>(_modelNamespace, testGenerateIf, _prepareAction, _action);
                var t = b.RunGeneration(ctx, a, types, _type, out CodeTypeDeclaration typeResult);
                if (t)
                {
                    if (_justOne)
                        _type = typeResult;
                }

            }

        }

        private bool RunGeneration(Context ctx, T ast, CodeTypeDeclarationCollection types, CodeTypeDeclaration type, out CodeTypeDeclaration typeResult)
        {

            typeResult = null;

            var result = BuildCode(ctx, ast);

            if (result)
            {

                if (type == null)
                {

                    var _n = this._nameOfClass();

                    if (!Exists(types, _n, out CodeTypeDeclaration typeResultDeclaration))
                    {
                        type = GenerateType(ctx, _n);
                        types.Add(type);
                        typeResult = type;
                    }
                    else
                        type = typeResult = typeResultDeclaration;

                }

                if (type != null)
                    GenerateMembers(ctx, ast, type);

            }

            return result;

        }

        private bool BuildCode(Context ctx, T ast)
        {

            var result = testGenerateIf != null ? testGenerateIf(ast) : true;

            if (result)
            {

                if (_prepareAction != null)
                    _prepareAction(ast);

                _action(ast, this);

                if (this._nameOfClass == null)
                    throw new InvalidOperationException("missing class name");

                if (this._templateSelectoraction != null)
                    ctx.Strategy = this._templateSelectoraction();
            }

            return result;

        }

        private CodeTypeDeclaration GenerateType(Context ctx, string name)
        {

            CodeTypeDeclaration type = new CodeTypeDeclaration(name)
            {
                IsPartial = true,
                TypeAttributes = _attributes,
            };

            if (_isStatic)
                type.Attributes = MemberAttributes.Static;

            if (_isInterface)
                type.IsInterface = true;

            else if (_isEnum)
                type.IsEnum = true;

            else if (_isStruct)
                type.IsStruct = true

            ;

            GenerateDocumentation(type, ctx);

            foreach (var parent in _parents)
                type.BaseTypes.Add(new CodeTypeReference(parent()));

            return type;

        }

        private void GenerateMembers(Context ctx, T ast, CodeTypeDeclaration type)
        {

            foreach (var p in _properties)
            {
                if (p.Items != null)
                {
                    var items = p.Items();
                    foreach (var item in items)
                    {
                        p.Clean();
                        p.Action(p, item);
                        p.Generate(ctx, item, type);
                    }
                }
                else
                {
                    p.Action2(p);
                    p.Generate(ctx, ast, type);
                }
            }

            foreach (var m in _methods)
            {

                if (m.Items != null)
                {
                    var items = m.Items();
                    foreach (var item in items)
                    {
                        m.Clean();
                        m.Action(m, item);
                        m.Generate(ctx, item, type);
                    }
                }
                else
                {
                    if (m.Action2 != null)
                        m.Action2(m);
                    m.Generate(ctx, ast, type);
                }

            }

            foreach (var f in _fields)
            {
                if (f.Items != null)
                {
                    var items = f.Items();
                    foreach (var item in items)
                    {
                        f.Clean();
                        f.Action(f, item);
                        f.Generate(ctx, item, type);
                    }
                }
                else
                {
                    f.Action2(f);
                    f.Generate(ctx, ast, type);
                }
            }

            foreach (var a in _actions)
                a(type);

            if (_typeBuilder.Count > 0)
            {
                var Ns = new CodeTypeDeclarationCollection();
                foreach (var t in _typeBuilder)
                    t.Generate(ctx, ast, Ns);
                var member = new CodeClassNested(Ns);
                type.Members.Add(member);
            }
        }

        private bool Exists(CodeTypeDeclarationCollection types, string n, out CodeTypeDeclaration resultModel)
        {

            resultModel = null;
            bool resultBool = false;

            foreach (CodeTypeDeclaration item in types)
                if (item.Name == n)
                {
                    resultModel = item;
                    return true;
                }

            return resultBool;

        }

        public ModelTypeFrom<T> AddTemplateSelector(Func<string> action)
        {
            this._templateSelectoraction = action;
            return this;
        }


        protected internal List<Func<string>> _parents { get; }

        private readonly List<ModelMethod> _methods;
        private readonly List<ModelField> _fields;
        private readonly List<ModelProperty> _properties;
        private readonly Action<T, ModelTypeFrom<T>> _action;

        private bool _isInterface;
        private bool _isStatic;
        private bool _isEnum;
        private bool _isStruct;
        internal bool _justOne;
        private CodeTypeDeclaration _type;
        private Func<string> _templateSelectoraction;
        private Func<T, bool> testGenerateIf;
        private readonly Action<T> _prepareAction;
        private TypeAttributes _attributes;

        protected internal Func<string> _nameOfClass { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    /// <seealso cref="Bb.Generators.ModelTypeFrom" />
    public abstract class ModelTypeFrom : ModelMember
    {

        public ModelTypeFrom(ModelTypeFrom parent)
        {
            this._parent = parent;
            _actions = new List<Action<CodeTypeDeclaration>>();
        }

        public ModelTypeFrom(ModelNamespace modelNamespace)
        {
            this._modelNamespace = modelNamespace;
            _actions = new List<Action<CodeTypeDeclaration>>();
        }

        public abstract Type Type { get; }


        protected void GenerateDocumentation(CodeTypeMember member, Context context)
        {

            if (_actionDocumentation != null)
            {
                var doc = new Documentation();
                _actionDocumentation(doc);
                doc.GenerateDocumentation(member, context);
            }

        }

        public ModelTypeFrom CreateTypeFrom<T>(Func<T, bool> testGenerateIf, Action<T> prepare, Action<T, ModelTypeFrom<T>> action)
           where T : AstBase
        {
            var t = new ModelTypeFrom<T>(this, testGenerateIf, prepare, action);
            this._typeBuilder.Add(t);
            return this;
        }

        public ModelTypeFrom Make(Action<CodeTypeDeclaration> action)
        {
            this._actions.Add(action);
            return this;
        }

        internal abstract void Generate(Context ctx, AstBase ast, CodeTypeDeclarationCollection types);


        protected Action<Documentation> _actionDocumentation;
        protected readonly List<Action<CodeTypeDeclaration>> _actions;
        protected ModelNamespace _modelNamespace;
        private ModelTypeFrom _parent;
        protected List<ModelTypeFrom> _typeBuilder;

    }



}
