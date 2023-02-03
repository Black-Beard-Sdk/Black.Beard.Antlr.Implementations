using System.Text;

namespace Bb.Asts
{
    public class Writer
    {

        public Writer(StringBuilder? sb = null)
        {
            _sb = sb ?? new StringBuilder();
            _index = 0;
        }

        public Writer Append(params object[] values)
        {
            foreach (var value in values)
                _sb.Append(value);
            return this;
        }

        public void TrimBegin()
        {
            while (char.IsWhiteSpace(_sb[0]))
                _sb.Remove(0, 1);
        }

        public void TrimEnd()
        {
            while (char.IsWhiteSpace(_sb[_sb.Length - 1]))
                _sb.Remove(_sb.Length - 1, 1);
        }

        public void TrimBegin(params char[] toFind)
        {
            while (toFind.Contains(_sb[0]))
                _sb.Remove(0, 1);
        }

        public void TrimEnd(params char[] toFind)
        {
            while (toFind.Contains(_sb[_sb.Length - 1]))
                _sb.Remove(_sb.Length - 1, 1);
        }

        public void CleanIndent()
        {
            while (_index > 0)
                DelIndent();
        }

        public void DelIndent()
        {
            _index--;
            if (_index < 0)
                _index = 0;
            else
            {
                var last = _sb[_sb.Length - 1];
                if (last == '\t')
                    _sb.Remove(_sb.Length - 1, 1);
            }
        }

        public void AddIndent()
        {

            if (_index < 0)
                _index = 0;

            _index++;

            _sb.Append('\t');

        }

        public void AppendEndLine(params object[] values)
        {
            foreach (var value in values)
                _sb.Append(value);
            AppendEndLine();
        }

        public void AppendEndLine()
        {

            _sb.AppendLine();
            for (int i = 0; i < _index; i++)
                _sb.Append('\t');

        }

        public override string ToString()
        {
            return _sb.ToString();
        }

        public IDisposable Indent(StrategySerializationItem strategy, bool crlf = false)
        {

            var result = new _disposable(strategy, this);
            if (crlf)
                result.After.Add(c => c.AppendEndLine());
            return result;
        }

        public IDisposable Indent(bool crlf = false)
        {

            var strategy = StrategySerializationItem.Default;

            var result = new _disposable(strategy, this);
            if (crlf)
                result.After.Add(c => c.AppendEndLine());
            return result;
        }

        public IDisposable IndentWithParentheses(StrategySerializationItem strategy, bool crlf = false)
        {

            var result = new _disposable(strategy, this, "(", ")");
            if (crlf)
                result.After.Add(c => c.AppendEndLine());
            return result;
        }

        public IDisposable IndentWithParentheses(bool crlf = false)
        {

            var strategy = StrategySerializationItem.Default;

            var result = new _disposable(strategy, this, "(", ")");
            if (crlf)
                result.After.Add(c => c.AppendEndLine());
            return result;
        }


        public StringBuilder Text { get => _sb; }

        public SerializationStrategy Strategy { get; set; }

        private readonly StringBuilder _sb;
        private int _index;

        private class _disposable : IDisposable
        {

            public _disposable(StrategySerializationItem strategy, Writer writer, string start = null, string end = null)
            {
                this._strategy = strategy;
                this.After = new List<Action<Writer>>();
                this._end = end;
                this._writer = writer;

                if (!string.IsNullOrEmpty(start))
                    _writer.Append(start);

                if (this._strategy.Indent)
                {
                    this._writer.AddIndent();
                }

            }

            protected virtual void Dispose(bool disposing)
            {

                if (!disposedValue)
                {
                    if (disposing)
                    {

                        if (this._strategy.Indent)
                        {
                            this._writer.DelIndent();
                        }

                        if (!string.IsNullOrEmpty(_end))
                            _writer.Append(_end);

                        foreach (var item in After)
                            item(this._writer);

                    }

                    disposedValue = true;
                }
            }

            private readonly StrategySerializationItem _strategy;

            public List<Action<Writer>> After { get; set; }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            private bool disposedValue;
            private Writer _writer;
            private string _end;
        }

    }


}
