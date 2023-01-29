﻿using Bb.Asts;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bb.Generators
{


    public class ModelTypeFrom<T> : ModelTypeFrom
        where T : AstBase
    {

        public ModelTypeFrom(ModelNamespace modelNamespace, Action<T, ModelTypeFrom<T>> action)
            : base(modelNamespace)
        {
            _parents = new List<Func<string>>();
            _methods = new List<ModelMethod>();
            _fields = new List<ModelField>();
            _properties = new List<ModelProperty>();
            this._action = action;
        }

        public override Type Type => typeof(T);

        public ModelTypeFrom<T> Attribute(MemberAttributes attributes)
        {
            this._attributes = attributes;
            return this;
        }

        public ModelTypeFrom<T> IsInterface()
        {
            this._isInterface = true;
            this._isEnum = false;
            this._isStruct = false;
            return this;
        }

        public ModelTypeFrom<T> IsEnum()
        {
            this._isInterface = false;
            this._isEnum = true;
            this._isStruct = false;
            return this;
        }

        public ModelTypeFrom<T> IsStruct()
        {
            this._isInterface = false;
            this._isEnum = false;
            this._isStruct = true;
            return this;
        }

        public ModelTypeFrom<T> Name(Func<string> name)
        {
            this._nameOfClass = name;
            return this;
        }

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
        public ModelTypeFrom<T> Ctor(Func<bool> test, Action<ModelConstructor> action)
        {
            var m = new ModelConstructor(this) { Test = test };
            this._methods.Add(m);
            action(m);
            return this;
        }


        public ModelTypeFrom<T> Field(Action<ModelField> action)
        {
            var m = new ModelField(this) { Items = null, Action = null, Action2 = action };
            this._fields.Add(m);
            return this;
        }
        public ModelTypeFrom<T> Field(Func<bool> test, Action<ModelField> action)
        {
            var m = new ModelField(this) { Test = test, Items = null, Action = null, Action2 = action };
            this._fields.Add(m);
            return this;
        }


        public ModelTypeFrom<T> Fields(Func<IEnumerable<AstTerminalText>> items, Action<ModelField, object> action)
        {
            var m = new ModelField(this) { Items = items, Action = action };
            this._fields.Add(m);
            return this;
        }
        public ModelTypeFrom<T> Fields(Func<bool> test, Func<IEnumerable<AstTerminalText>> items, Action<ModelField, object> action)
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
        public ModelTypeFrom<T> Property(Func<bool> test, Action<ModelProperty> action)
        {
            var m = new ModelProperty(this) { Test = test, Items = null, Action = null, Action2 = action };
            this._properties.Add(m);
            return this;
        }


        public ModelTypeFrom<T> Properties(Func<IEnumerable<object>> items, Action<ModelProperty, object> action)
        {
            var m = new ModelProperty(this) { Items = items, Action = action };
            this._properties.Add(m);
            return this;
        }
        public ModelTypeFrom<T> Properties(Func<bool> test, Func<IEnumerable<object>> items, Action<ModelProperty, object> action)
        {
            var m = new ModelProperty(this) { Test = test, Items = items, Action = action };
            this._properties.Add(m);
            return this;
        }


        public ModelTypeFrom<T> Method(Func<bool> test, Action<ModelMethod> action)
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


        internal override void Generate(Context ctx, AstBase ast, CodeNamespace @namespace)
        {

            if (ast is T a)
            {

                ctx.CurrentConfiguration = ctx.Configuration.GetConfiguration(ast);
                if (ctx.CurrentConfiguration.Generate)
                {
                    var b = new ModelTypeFrom<T>(_modelNamespace, _action);
                    var t = b.RunGeneration(ctx, a, @namespace, _type);

                    if (_justOne)
                        _type = t;
                }
            }

        }

        private CodeTypeDeclaration RunGeneration(Context ctx, T ast, CodeNamespace @namespace, CodeTypeDeclaration type)
        {

            _action(ast, this);

            if (this._templateSelectoraction != null)
                ctx.Strategy = this._templateSelectoraction();

            var t = _generateIf != null ? _generateIf() : true;

            if (t && type == null)
            {

                var _n = this._nameOfClass();

                if (!Exists(@namespace.Types, _n))
                {

                    ctx.CurrentConfigurationType = ctx.CurrentConfiguration.GetType(_n);

                    if (ctx.CurrentConfigurationType.Generate)
                    {
                        type = new CodeTypeDeclaration(_n)
                        {
                            IsPartial = true,
                            IsInterface = _isInterface,
                            IsEnum = _isEnum,
                            IsStruct = _isStruct,
                            Attributes = _attributes,
                        };

                        foreach (var parent in _parents)
                            type.BaseTypes.Add(new CodeTypeReference(parent()));

                        @namespace.Types.Add(type);

                    }

                }

            }

            if (type != null)
            {

                foreach (var p in _properties)
                {
                    if (p.Items != null)
                    {
                        var items = p.Items();
                        foreach (var item in items)
                        {
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
                    m.Generate(ctx, ast, type);

                foreach (var f in _fields)
                {
                    if (f.Items != null)
                    {
                        var items = f.Items();
                        foreach (var item in items)
                        {
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

            }

            return type;

        }

        private bool Exists(CodeTypeDeclarationCollection types, string n)
        {

            foreach (CodeTypeDeclaration item in types)
                if (item.Name == n)
                    return true;

            return false;

        }

        public ModelTypeFrom<T> AddTemplateSelector(Func<string> action)
        {
            this._templateSelectoraction = action;
            return this;
        }

        public ModelTypeFrom<T> GenerateIf(Func<bool> action)
        {
            this._generateIf = action;
            return this;
        }

        protected internal List<Func<string>> _parents { get; }

        private readonly List<ModelMethod> _methods;
        private readonly List<ModelField> _fields;
        private readonly List<ModelProperty> _properties;

        private readonly Action<T, ModelTypeFrom<T>> _action;
        private MemberAttributes _attributes;
        private bool _isInterface;
        private bool _isEnum;
        private bool _isStruct;
        internal bool _justOne;
        private CodeTypeDeclaration _type;
        private Func<string> _templateSelectoraction;
        private Func<bool> _generateIf;

        protected internal Func<string> _nameOfClass { get; set; }

    }


    public abstract class ModelTypeFrom : ModelMember
    {

        public ModelTypeFrom(ModelNamespace modelNamespace)
        {
            this._modelNamespace = modelNamespace;
        }

        public abstract Type Type { get; }

        protected ModelNamespace _modelNamespace;

        internal abstract void Generate(Context ctx, AstBase ast, CodeNamespace @namespace);

    }



}
