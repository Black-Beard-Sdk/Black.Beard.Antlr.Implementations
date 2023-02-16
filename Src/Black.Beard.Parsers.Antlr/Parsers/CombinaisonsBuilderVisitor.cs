using Bb.Asts;
using System.Text.RegularExpressions;

namespace Bb.Parsers
{

    public class CombinaisonsBuilderVisitor : TreeRuleItemVisitor<TreeRuleItem>
    {


        public CombinaisonsBuilderVisitor()
        {
        }
                
        public override TreeRuleItem Visit(TreeRuleItem i)
        {

            var result = i.Clone();

            return result;

        }


        public override TreeRuleItem VisitAlternative(TreeRuleItem i)
        {

            var result = i.Clone();

            return result;

        }


        public override TreeRuleItem VisitBlock(TreeRuleItem i)
        {

            var result = i.Clone();

            return result;

        }

        public override TreeRuleItem VisitTerminal(TreeRuleItem i)
        {
            var result = i.Clone();

            return result;
        }

        public override TreeRuleItem VisitRuleRef(TreeRuleItem i)
        {

            var result = i.Clone();

            if (i.Occurence.Optional)
            {

                var p = new TreeRuleItem()
                {
                    IsAlternative = true,
                };

                p.Add(result);
                p.Add(TreeRuleItem.Default());

                result = p;

            }

            return result;

        }


    }

}
