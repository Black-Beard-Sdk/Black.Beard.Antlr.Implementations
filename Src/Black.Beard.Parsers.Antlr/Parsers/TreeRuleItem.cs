using Bb.Asts;
using System.Diagnostics;

namespace Bb.Parsers
{

    [DebuggerDisplay("{ToString()}")]
    public class TreeRuleItem : List<TreeRuleItem>
    {

        public static TreeRuleItem Default(Action<TreeRuleItem> action = null)
        {

            var result = new TreeRuleItem();
            if (action != null)
                action(result);
            return result;

        }

        public TreeRuleItem(string name = null)
        {
            this.Name = name;
        }

        public string Name { get; }

        public int Id { get; set; }

        public bool IsTerminal { get; internal set; }

        public bool IsAlternative { get; internal set; }

        public Occurence Occurence { get; internal set; }

        public bool IsBlock { get; internal set; }

        public bool IsRange { get; internal set; }
        public bool IsRuleRef { get; internal set; }

        public override string ToString()
        {
            var wrt = new Writer();
            ToString(wrt);
            return wrt.ToString();
        }

        private void ToString(Writer wrt)
        {

            if (!string.IsNullOrEmpty(Name))
                wrt.Append(Name);

            if (IsBlock)
            {
                wrt.Append("(");
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

                case Occurence.Enum.Any:
                    if (this.Occurence.Optional)
                        wrt.Append("*");
                    else
                        wrt.Append("+");
                    break;

                case Occurence.Enum.One:
                default:
                    break;

            }

            if (this.Occurence.Optional)
                wrt.Append("?");

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

        public TreeRuleItem Clone()
        {
            var result = CloneWithOutChildren();

            foreach (var item in this)
                result.Add(item.Clone());

            return result;

        }

        public TreeRuleItem CloneWithOutChildren()
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
            };

            return result;

        }

    }

}
