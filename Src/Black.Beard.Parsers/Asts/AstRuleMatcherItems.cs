using System.Text;

namespace Bb.Asts
{

    public class AstRuleMatcherItems
    {

        public AstRuleMatcherItems(
              int index
            , params AstRuleMatcherItem[] items)
        {
            this.Index = index;
            this.Items = items;
        }

        public int Index { get; }

        public int Count => Items.Length;

        public AstRuleMatcherItem[] Items { get;}

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            foreach (AstRuleMatcherItem item in Items )
            {
                sb.Append(item.ToString());
                sb.Append(" ");
            }

            return sb.ToString().Trim();

        }

    }

}
