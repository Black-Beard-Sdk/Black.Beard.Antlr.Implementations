using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.Asts
{
    public abstract class AstRuleList<T> : AstRule, IEnumerable<T>
        where T : AstRule
    {


        public AstRuleList(ITerminalNode n)
            : base(n)
        {
            this._list = new List<T>();
        }

        public AstRuleList(ITerminalNode n, int capacity)
            : base(n)
        {
            this._list = new List<T>(capacity);
        }

        public AstRuleList(ParserRuleContext ctx)
            : base(ctx, null)
        {
            this._list = new List<T>();
        }

        public AstRuleList(ParserRuleContext ctx, int capacity)
            : base(ctx, null)
        {
            this._list = new List<T>(capacity);
        }

        public AstRuleList(Position n)
            : base(n)
        {
            this._list = new List<T>();
        }

        public AstRuleList(Position n, int capacity)
            : base(n)
        {
            this._list = new List<T>(capacity);
        }


        public T this[int item] { get => _list[item]; }


        public void Add(T item)
        {
            item.Parent = this;
            this._list.Add(item);
        }

        public void Insert (T item, int index)
        {
            item.Parent = this;
            this._list.Insert(index, item);
        }

        public void AddRange(params T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Remove(T item)
        {
            _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public bool Contains(T item) => _list.Contains(item);
        public T? Find(Predicate<T> match) => _list.Find(match);
        public List<T> FindAll(Predicate<T> match) => _list.FindAll(match);
        public int FindIndex(Predicate<T> match) => _list.FindIndex(match);
        public int RemoveAll(Predicate<T> match) => _list.RemoveAll(match);
        public T? FindLast(Predicate<T> match) => _list.FindLast(match);
        public int FindLastIndex(Predicate<T> match) => _list.FindLastIndex(match);
        public void ForEach(Action<T> action) => _list.ForEach(action);
        public List<T> GetRange(int index, int count) => _list.GetRange(index, count);
        public int IndexOf(T item) => _list.IndexOf(item);



        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_list).GetEnumerator();
        }

        public int Count => _list.Count;

        private readonly List<T> _list;

        protected string _charSplit = " ";

    }


}
