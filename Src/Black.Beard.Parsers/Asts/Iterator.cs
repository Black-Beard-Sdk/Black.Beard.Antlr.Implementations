namespace Bb.Asts
{
    public class Iterator<T>
    {

        public Iterator(AstRootList<T> items)
        {
            _items = items;
            this._index = 0;
        }

        public void Reset()
        {
            _index = 0;
            Current = _items[_index];
        }

        public bool Next()
        {

            _index++;

            if (_index < _items.Count)
            {
                Current = _items[_index];
                return true;
            }

            Current = default(T);
            return false;

        }

        public int Index => _index;

        public int Count => _items.Count;

        public T Current { get; private set; }

        private readonly AstRootList<T> _items;
        private int _index;

    }

}
