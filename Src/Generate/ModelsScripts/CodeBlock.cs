using System.CodeDom;

namespace Generate.ModelsScripts
{

    public class CodeBlock
    {


        public CodeBlock(CodeStatementCollection code = null)
        {
            _datas = new Dictionary<object, Data>();
            _code = code;
            _stack = new Stack<LevelBloc>();
        }


        internal Dictionary<object, Data> _datas;
        private readonly CodeStatementCollection _code;
        private readonly Stack<LevelBloc> _stack;


        public Data GetDataFor(object key) => _datas[key];


        public LevelBloc CurrentBlock
        {
            get
            {
                if (_stack.Count > 0)
                    return _stack.Peek();
                return null;
            }
        }

        public LevelBloc Stack(object sourceItem, CodeStatementCollection collection = null)
        {

            LevelBloc last = null;
            if (this._stack.Count > 0)
                last = this._stack.Peek();

            if (collection == null)
            {
                var current = CurrentBlock;
                collection = CurrentBlock != null
                    ? current.Code
                    : this._code;
            }

            var result = new LevelBloc(this, last,
                collection ?? throw new NullReferenceException(nameof(collection)),
                sourceItem
                );

            this._stack.Push(result);

            return result;

        }


        public void Remove(LevelBloc levelBloc)
        {

            var t = this._stack.Pop();

            if (t != levelBloc)
                throw new InvalidOperationException();

            if (this._datas.ContainsKey(t.Code))
                this._datas.Remove(t.Code);

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }


    }

    public class LevelBloc : IDisposable
    {

        internal LevelBloc(CodeBlock root, LevelBloc parent, CodeStatementCollection collection, object sourceItem)
        {

            this.Code = collection ?? throw new NullReferenceException(nameof(collection));
            this._root = root;
            this._parent = parent;
            this._source = sourceItem;

            _variables = new Dictionary<string, Variable>();
            if (parent != null)
                foreach (var variable in parent.GetAllVariables())
                    _variables.Add(variable.Name, variable);

        }

        public void Dispose()
        {
            this._root.Remove(this);
        }

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
                this._root._datas.Add(this.Code, data = new Data());

            data.SetData(key, value);

        }

        private IEnumerable<Variable> GetAllVariables()
        {
            if (_parent != null)
                foreach (var variable in _parent.GetAllVariables())
                    yield return variable;
        }

        public Variable GetVariable(string name)
        {

            if (this._variables.TryGetValue(name, out var variable))
                return variable;

            return null;

        }

        public Variable CreateVariable(string name, string type = null)
        {

            if (this._variables.TryGetValue(name, out var variable))
                return variable;

            variable = new Variable(name, type);

            return variable;

        }

        public CodeStatementCollection Code { get; }

        //public void ResetCurrent()
        //{
        //    Current = _current;
        //}

        //internal void SetCurrent(CodeStatementCollection i)
        //{
        //    Current = i;
        //}

        private Dictionary<string, Variable> _variables;
        private readonly CodeBlock _root;
        private readonly LevelBloc _parent;
        private readonly object _source;
    }

    public class Variable
    {

        public Variable(string name, string type = null)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name { get; }
        public string Type { get; }

    }

    public class Data
    {
        public Data()
        {
            _datas = new Dictionary<string, object>();
        }

        internal object GetData(string key)
        {

            if (_datas.TryGetValue(key, out var data))
                return data;

            return null;

        }

        internal void SetData(string key, object value)
        {
            if (_datas.ContainsKey(key))
                _datas[key] = value;
            else
                _datas.Add(key, value);
        }

        internal bool DataExist(string key)
        {
            return _datas.ContainsKey(key);
        }

        private Dictionary<string, object> _datas;

    }


}
