using System.CodeDom;

namespace Generate.ModelsScripts
{
    public class LevelBloc : IDisposable
    {

        internal LevelBloc(CodeBlock root, LevelBloc parent, CodeStatementCollection collection, object sourceItem)
        {
            this.Code = collection ?? throw new NullReferenceException(nameof(collection));
            this._root = root;
            this._parent = parent;
            this._source = sourceItem;
        }

        public void Dispose()
        {
            foreach (var item in this._variables)
                this._root.RemoveVariable(item);
            this._root.Remove(this);
        }

        #region datas

        public T GetRecursiveData<T>(string key)
        {
            var result = GetRecursiveData(key);
            if (result == null)
                return default(T);

            return (T)result;
        }

        public T GetData<T>(string key)
        {
            var result = GetData(key);
            if (result == null)
                return default(T);

            return (T)result;
        }

        public object GetRecursiveData(string key)
        {

            if (this._root._datas.TryGetValue(this._source, out Data data))
            {

                var result = data.GetData(key);

                if (result != null)
                    return result;

            }

            if (this._parent != null)
                return _parent.GetRecursiveData(key);

            return null;

        }

        public object GetData(string key)
        {

            if (this._root._datas.TryGetValue(this._source, out Data data))
            {

                var result = data.GetData(key);

                if (result != null)
                    return result;

            }

            return null;

        }

        public LevelBloc GetLevelFromData(string key)
        {

            if (this._root._datas.TryGetValue(this._source, out var data))
                if (data.DataExist(key))
                    return this;

            if (this._parent != null)
                return _parent.GetLevelFromData(key);

            return null;

        }

        public bool DataExists(string key)
        {

            if (this._root._datas.TryGetValue(this._source, out var data))
                if (data.DataExist(key))
                    return true;

            if (this._parent != null)
                return _parent.DataExists(key);

            return false;

        }

        public void SetData(string key, object value)
        {

            if (!this._root._datas.TryGetValue(this._source, out Data data))
                this._root._datas.Add(this._source, data = new Data());

            data.SetData(key, value);

        }

        #endregion datas

        public Variable GetVariable(string name) => this._root.GetVariable(name);
        public bool VariableExists(string name) => this._root.VariableExists(name);

        public Variable CreateVariable(string name, string type)
        {
            var result = this._root.CreateVariable(name, type);
            _variables.Add(result);
            return result;
        }

        public Variable CreateVariable(string name, Type type)
        {
            var result = this._root.CreateVariable(name, type);
            _variables.Add(result);
            return result;
        }


        public Variable CreateOrGetVariable(string name, string type)
        {

            var variable = this._root.GetVariable(name);
            if (variable != null)
                return variable;

            variable = CreateOrGetVariable(name, type);
            _variables.Add(variable);

            return variable;
        
        }


        public Variable CreateOrGetVariable(string name, Type type)
        {

            var variable = this._root.GetVariable(name);
            if (variable != null)
                return variable;

            variable = CreateOrGetVariable(name, type);
            _variables.Add(variable);

            return variable;

        }



        public CodeMarker GetMarker()
        {
            return new CodeMarker(this);
        }

        public CodeStatementCollection Code { get; }

        private readonly CodeBlock _root;
        private readonly LevelBloc _parent;
        internal readonly object _source;
        private HashSet<Variable> _variables = new HashSet<Variable>();

    }


}
