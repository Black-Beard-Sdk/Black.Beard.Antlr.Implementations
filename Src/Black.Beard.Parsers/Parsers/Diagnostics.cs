using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Bb.Parsers
{

    public class Diagnostics : IList<ErrorModel>, INotifyCollectionChanged
    {


        #region Add

        public void AddInformation(string filename, int line, int startIndex, int column, string text, string message)
        {
            Add(new ErrorModel()
            {
                Filename = filename,
                Line = line,
                StartIndex = startIndex,
                Column = column,
                Text = text,
                Message = message,
                Severity = SeverityEnum.Information,
            });

        }

        public void AddInformation(string filename, TokenLocation location, string text, string message)
        {
            Add(new ErrorModel()
            {
                Filename = filename,
                Line = location.Line,
                StartIndex = location.StartIndex,
                Column = location.Column,
                Text = text,
                Message = message,
                Severity = SeverityEnum.Information,
            });

        }

        public void AddWarning(string filename, int line, int startIndex, int column, string text, string message)
        {
            Add(new ErrorModel()
            {
                Filename = filename,
                Line = line,
                StartIndex = startIndex,
                Column = column,
                Text = text,
                Message = message,
                Severity = SeverityEnum.Warning,
            });

        }

        public void AddWarning(string filename, TokenLocation location, string text, string message)
        {

            if (location == null)
                location = new TokenLocation();

            Add(new ErrorModel()
            {
                Filename = filename,
                Line = location.Line,
                StartIndex = location.StartIndex,
                Column = location.Column,
                Text = text,
                Message = message,
                Severity = SeverityEnum.Warning,
            });

        }

        public void AddError(string filename, int line, int startIndex, int column, string text, string message)
        {
            Add(new ErrorModel()
            {
                Filename = filename,
                Line = line,
                StartIndex = startIndex,
                Column = column,
                Text = text,
                Message = message,
                Severity = SeverityEnum.Error,
            });

        }

        public void AddError(string filename, TokenLocation location, string text, string message)
        {

            if (location == null)
                location = new TokenLocation();

            Add(new ErrorModel()
            {
                Filename = filename,
                Line = location?.Line ?? 0,
                StartIndex = location?.StartIndex ?? 0,
                Column = location?.Column ?? 0,
                Text = text,
                Message = message,
                Severity = SeverityEnum.Error,
            });
        }

        public void AddDiagnostic(SeverityEnum severityEnum, string filename, int line, int startIndex, int column, string text, string message)
        {
            Add(new ErrorModel()
            {
                Filename = filename,
                Line = line,
                StartIndex = startIndex,
                Column = column,
                Text = text,
                Message = message,
                Severity = severityEnum,
            });
        }

        public void AddDiagnostic(SeverityEnum severityEnum, string filename, TokenLocation location, string text, string message)
        {

            if (location == null)
                location = new TokenLocation();

            Add(new ErrorModel()
            {
                Filename = filename,
                Line = location?.Line ?? 0,
                StartIndex = location?.StartIndex ?? 0,
                Column = location?.Column ?? 0,
                Text = text,
                Message = message,
                Severity = severityEnum,
            });
        }

        #endregion Add

        #region Implemente IList

        public IEnumerator<ErrorModel> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(ErrorModel item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, ErrorModel item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public void Add(ErrorModel item)
        {
            if (item != null)
            {
                _list.Add(item);
                if (CollectionChanged != null)
                {
                    var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add);
                    args.NewItems.Add(item);
                    CollectionChanged(this, args);
                }
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(ErrorModel item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(ErrorModel[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(ErrorModel item)
        {
            return _list.Remove(item);
        }

        #endregion Implemente IList

        public IEnumerable<ErrorModel> Errors { get => _list.Where(c => c.Severity == SeverityEnum.Error); }

        public bool Success { get => !_list.Any(c => c.Severity == SeverityEnum.Error); }

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public ErrorModel this[int index] { get => _list[index]; set => _list[index] = value; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private List<ErrorModel> _list = new List<ErrorModel>();

    }


}
