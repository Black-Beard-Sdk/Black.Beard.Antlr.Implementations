﻿using Bb.Asts;

namespace Bb.Parsers
{
    public static class RuleHelper
    {


        public static List<TreeRuleItem> ResolveAllCombinations(this AstLabeledAlt self)
        {
                        
            var removeOptionalsVisitor = new RemoveOptionalsBuilderVisitor();
            var spliterVisitor = new SpliterBuilderVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();

            var visitor1 = new RuleIdVisitor();
            var p = self.Rule.Accept(visitor1);
            //var possibilites = p.Accept(removeOptionalsVisitor);
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

            //if (self.Name.Text == "alter_credential")
            //{

            //}

            var removeOptionalsVisitor = new RemoveOptionalsBuilderVisitor();
            var spliterVisitor = new SpliterBuilderVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();

            var visitor1 = new RuleIdVisitor();
            var p = self.Alternatives.Accept(visitor1);
            var possibilites = p.Accept(removeOptionalsVisitor);
            var possibilites2 = possibilites.Accept(spliterVisitor);
            possibilites2 = cleanDuplicated.Visit(possibilites2);
            var possibilites3 = new List<TreeRuleItem>(possibilites.Count);
            HashSet<string> names = new HashSet<string>();
            foreach (var p1 in possibilites2)
            {
                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                {
                    p1.Name = self.Name.Text;
                    possibilites3.Add(p1);
                }
            }

            return possibilites3;

        }


    }

}