using Antlr4.Runtime;
using System.Collections;

namespace Bb.Asts
{


    public abstract class AstListBase<T, TVisitor> : AstBase<TVisitor>, IEnumerable<T>
        where T : AstBase<TVisitor>
    {

        public AstListBase(ParserRuleContext ctx, int capacity)
            : base(ctx)
        {
            this._list = new List<T>(capacity);
        }

        public AstListBase(ParserRuleContext ctx)
            : base(ctx)
        {
            this._list = new List<T>();
        }

        public void Add(T item)
        {
            item.Parent = this;
            this._list.Add(item);
        }

        public void AddRange(params T[] items)
        {
            foreach (var item in items) 
                Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        private readonly List<T> _list;

    }


}
