using Bb.Asts;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Bb.Parsers
{

    public class SpliterBuilderVisitor : TreeRuleItemVisitor<List<TreeRuleItem>>
    {


        public SpliterBuilderVisitor()
        {

        }


        public override List<TreeRuleItem> Visit(TreeRuleItem i)
        {

            var list = new List<TreeRuleItem>() { i.CloneWithOutChildren() };

            foreach (var item in i)
            {

                var p = item.Accept(this);

                var list3 = new List<List<TreeRuleItem>>(p.Count - 1) { list };

                for (int l = 0; l < p.Count - 1; l++)
                    list3.Add(Clone(list));

                for (int l = 0; l < p.Count; l++)
                {
                    var toAdd = p[l];
                    if (!toAdd.IsEmpty)
                    {
                        var target = list3[l];
                        foreach (var item2 in target)
                        {
                            if (toAdd.Count == 0)
                                item2.Add(toAdd);
                            else
                            {
                                foreach (var item3 in toAdd)
                                    item2.Add(item3);
                            }
                        }

                    }
                }

                if (p.Count > 1)
                {
                    var l2 = new List<TreeRuleItem>(p.Count);
                    foreach (var item3 in list3)
                        l2.AddRange(item3);
                    list = l2;
                }

            }

            return list;

        }

        private List<TreeRuleItem> Clone(List<TreeRuleItem> list)
        {

            var list2 = new List<TreeRuleItem>(list.Capacity);

            foreach (var item in list)
                list2.Add(item.Clone());

            return list2;

        }

        public override List<TreeRuleItem> VisitAlternative(TreeRuleItem i)
        {

            List<TreeRuleItem> result = new List<TreeRuleItem>(i.Count);

            foreach (var item in i)
            {
                result.Add(item.Clone());
            }

            List<TreeRuleItem> result2 = new List<TreeRuleItem>(i.Count);
            foreach (var item in result)
            {

                result2.AddRange(item.Accept(this));

            }

            return result2;

        }


        public override List<TreeRuleItem> VisitBlock(TreeRuleItem i)
        {

            i.IsBlock = false;

            return i.Accept(this);

        }

        public override List<TreeRuleItem> VisitTerminal(TreeRuleItem i)
        {

            var result = new List<TreeRuleItem>(2);
            var c = i.Clone();

            //if (i.Occurence.Optional)
            //{
            //    c.Occurence = new Occurence(c.Occurence.Value, false);
            //    result.Add(TreeRuleItem.Default());
            //}

            result.Add(c);

            return result;

        }


        public override List<TreeRuleItem> VisitRuleRef(TreeRuleItem i)
        {
            var result = i.Clone();
            return new List<TreeRuleItem>() { result };
        }

    }

}
