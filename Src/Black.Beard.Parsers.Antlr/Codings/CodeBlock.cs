using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Generate.ModelsScripts
{

    public class CodeBlock
    {

        public CodeBlock(CodeStatementCollection code = null)
        {
            _datas = new Dictionary<object, Data>();
            _code = code;
            _stack = new Stack<LevelBloc>();
            _variables = new Dictionary<string, Variable>();
        }


        internal Dictionary<object, Data> _datas;
        private readonly CodeStatementCollection _code;
        private readonly Stack<LevelBloc> _stack;


        public Data GetDataFor(object key)
        {

            if (!_datas.TryGetValue(key, out Data data))
                _datas.Add(key, data = new Data());

            return data;

        }


        public LevelBloc CurrentBlock
        {
            get
            {
                if (_stack.Count > 0)
                    return _stack.Peek();
                return null;
            }
        }

        public LevelBloc Stack(object sourceItem, CodeStatementCollection collection)
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

            if (this._datas.ContainsKey(t._source))
                this._datas.Remove(t._source);

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        #region variables

        public Variable CreateOrGetVariable(string name, string type)
        {

            if (this._variables.TryGetValue(name, out var variable))
                return variable;

            variable = new Variable(name, type, null);
            this._variables.Add(name, variable);

            return variable;
        }

        public Variable CreateVariable(string name, string type)
        {

            if (this._variables.TryGetValue(name, out var variable))
            {

                int index = 1;
                var name1 = name + (index);
                while (this._variables.ContainsKey(name1))
                {
                    index++;
                    name1 = name + index;
                }

                variable = new Variable(name1, type, null);
                this._variables.Add(variable.Name, variable);


            }
            else
                this._variables.Add(name, variable = new Variable(name, type, null));

            return variable;

        }

        public Variable CreateOrGetVariable(string name, Type type)
        {

            if (this._variables.TryGetValue(name, out var variable))
                return variable;

            variable = new Variable(name, null, type);
            this._variables.Add(name, variable);

            return variable;
        }

        public Variable CreateVariable(string name, Type type)
        {

            if (this._variables.TryGetValue(name, out var variable))
            {

                int index = 1;
                var name1 = name + (index);
                while (this._variables.ContainsKey(name1))
                {
                    index++;
                    name1 = name + index;
                }

                variable = new Variable(name1, null, type);
                this._variables.Add(variable.Name, variable);

            }
            else
                this._variables.Add(name, variable = new Variable(name, null, type));

            return variable;

        }

        public Variable GetVariable(string name)
        {

            if (this._variables.TryGetValue(name, out var variable))
                return variable;

            return null;

        }

        public void RemoveVariable(string name)
        {

            if (this._variables.ContainsKey(name))
                _variables.Remove(name);

        }

        public void RemoveVariable(Variable variable)
        {

            if (this._variables.ContainsValue(variable))
                _variables.Remove(variable.Name);

        }

        public bool VariableExists(string name)
        {
            return this._variables.ContainsKey(name);
        }

        #endregion

        private Dictionary<string, Variable> _variables;


    }


}
