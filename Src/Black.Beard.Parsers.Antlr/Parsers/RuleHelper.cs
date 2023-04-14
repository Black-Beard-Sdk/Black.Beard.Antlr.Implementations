using Bb.Asts;

namespace Bb.Parsers
{
    public static class RuleHelper
    {

        public static List<AstBase> ResolveAllCombinations(this TreeRuleItem self)
        {

            List<AstBase> result = new List<AstBase>(self.Count);

            foreach (var item in self)
                result.Add(item.Origin);

            return result;

        }

        public static List<TreeRuleItem> ResolveAllCombinations(this AstLabeledAlt self)
        {

            var spliterVisitor = new SpliterBuilderVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();
            var visitor1 = new RuleIdVisitor();
            var p = self.Rule.Accept(visitor1);
            var possibilites2 = p.Accept(spliterVisitor);
            possibilites2 = cleanDuplicated.Visit(possibilites2);
            var possibilites3 = new List<TreeRuleItem>(p.Count);
            HashSet<string> names = new HashSet<string>();
            foreach (var p1 in possibilites2)
            {
                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(p1);
            }

            return possibilites3;

        }

        public static List<TreeRuleItem> ResolveAllCombinations(this AstRule self)
        {
            var spliterVisitor = new SpliterBuilderVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();
            var visitor1 = new RuleIdVisitor();
            var p = self.Alternatives.Accept(visitor1);
            var possibilites2 = p.Accept(spliterVisitor);
            possibilites2 = cleanDuplicated.Visit(possibilites2);
            var possibilites3 = new List<TreeRuleItem>(p.Count);
            HashSet<string> names = new HashSet<string>();
            foreach (var p1 in possibilites2)
            {
                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(p1);
            }
            return possibilites3;
        }

        //public static List<TreeRuleItem> GetAllCombinations(this AstRule self)
        //{

        //    var possibilites = new List<TreeRuleItem>();

        //    //var removeOptionalsVisitor = new RemoveOptionalsBuilderVisitor();
        //    var visitor1 = new RuleIdVisitor();
        //    var spliterVisitor = new SpliterBuilderVisitor();

        //    foreach (var alternative in self.Alternatives)
        //    {
        //        var p1 = alternative.Accept(visitor1);
        //        var p2 = p1.Accept(spliterVisitor);
        //        possibilites.AddRange(p2);
        //    }

        //    var possibilites2 = new List<TreeRuleItem>();

        //    HashSet<string> names = new HashSet<string>();
        //    foreach (var p1 in possibilites)
        //    {
        //        var txt = p1.ToString();
        //        if (!string.IsNullOrEmpty(txt) && names.Add(txt))
        //        {
        //            p1.Name = self.Name.Text;
        //            possibilites2.Add(p1);
        //        }
        //    }

        //    return possibilites2;

        //}

    }

}
