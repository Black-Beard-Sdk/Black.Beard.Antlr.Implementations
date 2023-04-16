using Bb.Asts;
using System.Collections.Generic;

namespace Bb.Parsers
{
    public class RemoveDuplicatedRulesBuilderVisitor : TreeRuleItemVisitor<List<TreeRuleItem>>
    {

        public RemoveDuplicatedRulesBuilderVisitor()
        {

        }


        public List<TreeRuleItem> Visit(List<TreeRuleItem> items)
        {

            while (RemoveRedondanteRulesInLines(items))
            {

                for (int i = 0; i < items.Count - 1; i++)
                {

                    if (items[i].Count == 1)
                    {
                        items[i] = items[i][0];
                    }

                }

            }

            while (CleanLines(items))
            {

            }

            return items;

        }

        private bool CleanLines(List<TreeRuleItem> items)
        {

            bool result = false;

            for (int i = 0; i < items.Count - 1; i++)
            {

                var item1 = items[i];
                var txt1 = item1.ToString()
                    .Replace("+", "").Replace("?", "").Replace("*", "").Replace("  ", " ").Trim()
                    ;

                for (int j = i + 1; j < items.Count - 1; j++)
                {

                    var item2 = items[j];
                    var txt2 = item2.ToString()
                        .Replace("+", "").Replace("?", "").Replace("*", "").Replace("  ", " ").Trim()
                        ;

                    if (txt1 == txt2)
                    {
                        Match(items, item1, item2);
                        result = true;
                    }

                }
            }

            return result;

        }

        private void Match(List<TreeRuleItem> items, TreeRuleItem item1, TreeRuleItem item2)
        {
            

            if (item1.Occurence.Optional)
            {
                if (item1.Occurence.Optional)
                {

                }
                else
                {

                }
            }
            else
            {
                if (item1.Occurence.Optional)
                {

                }
                else
                {
                    if (item1.Occurence == item2.Occurence)
                    {
                        items.Remove(item2);
                    }
                    if (item1.Occurence > item2.Occurence)
                    {

                    }
                    else
                    {
                        items.Remove(item1);
                    }
                }

            }

        }

        //private List<TreeRuleItem> Clone(List<TreeRuleItem> list)
        //{

        //    var list2 = new List<TreeRuleItem>(list.Capacity);

        //    foreach (var item in list)
        //        list2.Add(item.Clone());

        //    return list2;

        //}


        private static bool RemoveRedondanteRulesInLines(List<TreeRuleItem> items)
        {

            bool result = false;

            foreach (var item in items)
            {

                TreeRuleItem last = null;
                List<TreeRuleItem> toRemoves = new List<TreeRuleItem>(item.Count);

                foreach (var item2 in item)
                {
                    if (last != null)
                        if (last.Name == item2.Name)
                        {
                            if (last.Occurence == OccurenceEnum.Any && last.Occurence.Optional)
                            {

                            }
                            else if (item2.Occurence == OccurenceEnum.Any && item2.Occurence.Optional)
                            {
                                last.Occurence = new Occurence(OccurenceEnum.Any, false);
                                toRemoves.Add(item2);
                                last.CleanText();
                                item.CleanText();
                            }
                        }

                    last = item2;

                }

                foreach (var item3 in toRemoves)
                {
                    item.Remove(item3);
                    result = true;
                }
            }

            return result;

        }

        public override List<TreeRuleItem> VisitAlternative(TreeRuleItem i)
        {
            return base.VisitAlternative(i);
        }

        public override List<TreeRuleItem> Visit(TreeRuleItem i)
        {
            return base.Visit(i);
        }

        public override List<TreeRuleItem> VisitBlock(TreeRuleItem i)
        {
            return base.VisitBlock(i);
        }

        public override List<TreeRuleItem> VisitRange(TreeRuleItem i)
        {
            return base.VisitRange(i);
        }

        public override List<TreeRuleItem> VisitRuleRef(TreeRuleItem i)
        {
            return base.VisitRuleRef(i);
        }

        public override List<TreeRuleItem> VisitTerminal(TreeRuleItem i)
        {
            return base.VisitTerminal(i);
        }

    }

}
