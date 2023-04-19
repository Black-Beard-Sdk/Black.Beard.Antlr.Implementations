using System.Text;

namespace Bb.Asts
{

    public class AstRuleMatcherItem
    {

        public AstRuleMatcherItem(Type type, bool optional, bool isAny, string ruleName = null)
        {
            this.Type = type;
            this.Optional = optional;
            this.IsAny = isAny;
            this.RuleName = ruleName;
        }

        public Type Type { get; set; }

        public bool Optional { get; set; }

        public bool IsAny { get; set; }
        public string RuleName { get; }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(Type.Name.Substring(3));
            if (Optional)
            {
                if (IsAny)
                    sb.Append('*');
                else
                    sb.Append('?');
            }
            else if (IsAny)
                sb.Append('+');

            return sb.ToString();

        }

    }

}
