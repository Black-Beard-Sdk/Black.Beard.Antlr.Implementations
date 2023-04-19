namespace Bb.Asts
{
    public class AstRuleMatcherList : List<AstRuleMatcherItems>
    {

        public AstRuleMatcherList(params AstRuleMatcherItems[] items)
        {
            this.AddRange(items);
        }

    }

}
