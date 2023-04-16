using Bb.Asts;
using System.Collections;
using System.Diagnostics;

namespace Bb.Parsers
{


    public class AlternativeTreeRuleItemList : IEnumerable<AlternativeTreeRuleItem>
    {


        public AlternativeTreeRuleItemList()
        {
            _list = new List<AlternativeTreeRuleItem>();
        }


        public AlternativeTreeRuleItemList(int capacity)
        {
            _list = new List<AlternativeTreeRuleItem>(capacity);
        }

        public AlternativeTreeRuleItem this[int index] { get => _list[0]; set => _list[0] = value; }

        public void Add(TreeRuleItem item)
        {
            Add(new AlternativeTreeRuleItem(item));
        }


        public void Add(AlternativeTreeRuleItem item)
        {
            _list.Add(item);
            item.RebuildIndex(_list);
        }


        public void AddRange(IEnumerable<TreeRuleItem> items)
        {

            var l = new List<AlternativeTreeRuleItem>(items.Count());
            foreach (var item in items)
                l.Add(new AlternativeTreeRuleItem(item));

            AddRange(l);
        
        }


        public void AddRange(IEnumerable<AlternativeTreeRuleItem> items)
        {
            _list.AddRange(items);

            foreach (var item in items)
                item.RebuildIndex(_list);
        }


        public int Count => _list.Count;


        public IEnumerator<AlternativeTreeRuleItem> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        private List<AlternativeTreeRuleItem> _list;

    }

    public class AlternativeTreeRuleItem
    {

        public AlternativeTreeRuleItem(TreeRuleItem item)
        {
            this.Item = item;
        }

        public TreeRuleItem Item { get; }

        public int AlternativeIdentifier { get; private set; }

        internal void RebuildIndex(List<AlternativeTreeRuleItem> list)
        {
            this.AlternativeIdentifier = list.IndexOf(this) + 1;
        }


    }


    [DebuggerDisplay("{ToString()}")]
    public class TreeRuleItem : IEnumerable<TreeRuleItem>
    {

        public static TreeRuleItem Default(Action<TreeRuleItem> action = null)
        {

            var result = new TreeRuleItem() { IsRuleRef = true, IsEmpty = true };
            if (action != null)
                action(result);
            return result;

        }

        public TreeRuleItem this[int index] { get { return _list[index]; } set { _list[index] = value; } }

        public int Count => _list.Count;

        public void Add(TreeRuleItem item)
        {
            if (_list.Count > 0)
                this._list[_list.Count - 1].IsLast = false;
            item.IsLast = true;
            this._list.Add(item);
        }

        public void AddRange(IEnumerable<TreeRuleItem> collection)
        {
            this._list.AddRange(collection);
        }

        public TreeRuleItem(string name = null)
        {
            this._list = new List<TreeRuleItem>();
            this.Name = name;
        }

        private List<TreeRuleItem> _list;
        private string _txt;

        public string Name { get; internal set; }

        public int Id { get; set; }

        public bool IsTerminal { get; internal set; }

        public bool IsAlternative { get; internal set; }

        public Occurence Occurence { get; set; }

        public bool IsBlock { get; internal set; }

        public bool IsRange { get; internal set; }

        public bool IsRuleRef { get; internal set; }

        public bool IsEmpty { get; private set; }

        public bool ContainsRules
        {
            get
            {

                if (this._list.Any(c => c.IsRuleRef))
                    return true;

                foreach (var item in this._list)
                    if (item.ContainsRules)
                        return true;

                return false;

            }
        }

        public bool IsLast { get; private set; }
        public bool IsConstant { get; internal set; }
        public AstBase Origin { get; internal set; }
        public string Label { get; internal set; }
        public int AlternativeIdentifier { get; internal set; }

        public override string ToString()
        {

            if (string.IsNullOrEmpty(this._txt))
            {
                var wrt = new Writer();
                ToString(wrt);
                _txt = wrt.ToString();
            }

            return _txt;

        }

        private void ToString(Writer wrt)
        {

            if (!string.IsNullOrEmpty(Label))
            {
                wrt.Append(Label);
                wrt.Append('=');
            }

            if (!string.IsNullOrEmpty(Name))
                wrt.Append(Name);

            if (IsBlock)
            {
                wrt.Append("(");

                if (IsAlternative)
                    ToString(wrt, " | ");
                else
                    ToString(wrt, " ");
                wrt.TrimEnd();
                wrt.Append(")");
            }

            else if (IsRange)
                ToString(wrt, " .. ");

            else if (IsAlternative)
                ToString(wrt, " | ");

            else
                ToString(wrt, " ");

            WriteOccurence(wrt);
            wrt.EnsureEndBy(' ');

        }

        private void ToString(Writer wrt, string com)
        {

            string comma = string.Empty;

            foreach (var item in this)
            {
                wrt.TrimEnd();
                wrt.EnsureEndBy(' ');
                wrt.EnsureEndBy(comma);
                item.ToString(wrt);
                comma = com;
            }

            wrt.TrimEnd();

        }

        protected void WriteOccurence(Writer wrt)
        {

            switch (this.Occurence.Value)
            {

                case OccurenceEnum.Any:
                    if (this.Occurence.Optional)
                        wrt.Append("*");
                    else
                        wrt.Append("+");
                    break;

                case OccurenceEnum.One:
                    if (this.Occurence.Optional)
                        wrt.Append("?");
                    break;

                default:
                    break;

            }



        }

        public void Accept(TreeRuleItemVisitor visitor)
        {

            if (IsRuleRef)
                visitor.VisitRuleRef(this);

            else if (IsTerminal)
                visitor.VisitTerminal(this);

            else if (IsAlternative)
                visitor.VisitAlternative(this);

            else if (IsBlock)
                visitor.VisitBlock(this);

            else if (IsRange)
                visitor.VisitRange(this);

            else
                visitor.Visit(this);
        }

        public T Accept<T>(TreeRuleItemVisitor<T> visitor)
        {

            if (IsRuleRef)
                return visitor.VisitRuleRef(this);

            else if (IsTerminal)
                return visitor.VisitTerminal(this);

            else if (IsAlternative)
                return visitor.VisitAlternative(this);

            else if (IsBlock)
                return visitor.VisitBlock(this);

            else if (IsRange)
                return visitor.VisitRange(this);

            return visitor.Visit(this);
        }

        public IEnumerable<TreeRuleItem> Split()
        {

            foreach (var item in this)
                yield return item.Clone();

        }

        public TreeRuleItem Clone(Action<TreeRuleItem> action = null)
        {

            var result = CloneWithOutChildren();

            foreach (var item in this)
                result.Add(item.Clone());

            if (action != null)
                action(result);

            return result;

        }

        public TreeRuleItem CloneWithOutChildren(Action<TreeRuleItem> action = null)
        {

            var result = new TreeRuleItem(this.Name)
            {
                Id = this.Id,
                IsTerminal = this.IsTerminal,
                IsAlternative = this.IsAlternative,
                IsRuleRef = this.IsRuleRef,
                Occurence = this.Occurence.Clone(),
                IsBlock = this.IsBlock,
                IsRange = this.IsRange,
                IsEmpty = this.IsEmpty,
                IsConstant = this.IsConstant,
                Origin = this.Origin,
                Label= this.Label,
                AlternativeIdentifier = this.AlternativeIdentifier,
            };

            if (action!= null)
                action(result);

            return result;

        }

        public IEnumerator<TreeRuleItem> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        internal void Remove(TreeRuleItem item)
        {
            _list.Remove(item);
            CleanText();
        }

        internal void CleanText()
        {
            _txt = null;
        }

    }

}
