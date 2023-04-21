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

        public static AlternativeTreeRuleItemList ResolveAllCombinations(this AstLabeledAlt self)
        {

            var spliterVisitor = new SpliterBuilderVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();
            var visitor1 = new RuleIdVisitor();
            var p = self.Rule.Accept(visitor1);
            var possibilites2 = p.Accept(spliterVisitor);
            possibilites2 = cleanDuplicated.Visit(possibilites2);
            var possibilites3 = new AlternativeTreeRuleItemList(p.Count);
            HashSet<string> names = new HashSet<string>();
            foreach (var p1 in possibilites2)
            {

                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(p1);

            }

            return possibilites3;

        }

        public static AlternativeTreeRuleItemList ResolveAllCombinations(this AstRule self)
        {
            var spliterVisitor = new SpliterBuilderVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();
            var visitor1 = new RuleIdVisitor();
            TreeRuleItem p = self.Alternatives.Accept(visitor1);
            var possibilites2 = p.Accept(spliterVisitor);
            possibilites2 = cleanDuplicated.Visit(possibilites2);
            var possibilites3 = new AlternativeTreeRuleItemList(p.Count);
            HashSet<string> names = new HashSet<string>();

            foreach (var p1 in possibilites2)
            {

                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(p1);

            }

            return possibilites3;

        }

        public static AlternativeTreeRuleItemList ResolveAllCombinationsWithoutOptional(this AlternativeTreeRuleItemList self)
        {

            List<TreeRuleItem> possibilites2 = new List<TreeRuleItem>();

            var spliterVisitor = new SpliterOnOptionalVisitor();
            var cleanDuplicated = new RemoveDuplicatedRulesBuilderVisitor();
            var visitor3 = new RemoveOptionalsBuilderVisitor();

            foreach (AlternativeTreeRuleItem item in self)
            {
                var o = visitor3.Visit(item.Item);
                var possibilites = o.Accept(spliterVisitor);
                possibilites2.AddRange(cleanDuplicated.Visit(possibilites));
            }

            var possibilites3 = new AlternativeTreeRuleItemList(possibilites2.Count);
            HashSet<string> names = new HashSet<string>();

            foreach (var p1 in possibilites2)
            {

                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(p1);

            }

            return possibilites3;

        }

        public static AlternativeTreeRuleItemList ResolveAllCombinationsWithoutOptional(this AlternativeTreeRuleItem item)
        {

            var spliterVisitor = new SpliterOnOptionalVisitor();
            var visitor3 = new RemoveOptionalsBuilderVisitor();

            var o = visitor3.Visit(item.Item);
            var possibilites = o.Accept(spliterVisitor);

            var possibilites3 = new AlternativeTreeRuleItemList(possibilites.Count);
            HashSet<string> names = new HashSet<string>();

            foreach (var p1 in possibilites)
            {

                var txt = p1.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(p1);

            }

            return possibilites3;

        }

        public static AlternativeTreeRuleItemList RemoveNonDynamic(this AlternativeTreeRuleItemList self)
        {

            var possibilites3 = new AlternativeTreeRuleItemList(self.Count);
            HashSet<string> names = new HashSet<string>();

            foreach (var rule in self)
            {

                var cloned = rule.Item.CloneWithOutChildren();

                foreach (var item in rule.Item)
                if (item.WhereRuleOrIdentifiers())
                {
                        cloned.Add(item.Clone());
                }

                var txt = cloned.ToString();
                if (!string.IsNullOrEmpty(txt) && names.Add(txt))
                    possibilites3.Add(cloned);

            }

            return possibilites3;

        }



        public static bool WhereRuleOrIdentifiers(this TreeRuleItem item)
        {

            if (item.IsTerminal)
            {
                var link = item.Origin.Link;
                switch (link.TerminalKind)
                {
                    case TokenTypeEnum.Identifier:
                    case TokenTypeEnum.Boolean:
                    case TokenTypeEnum.String:
                    case TokenTypeEnum.Decimal:
                    case TokenTypeEnum.Int:
                    case TokenTypeEnum.Real:
                    case TokenTypeEnum.Hexa:
                    case TokenTypeEnum.Binary:
                    case TokenTypeEnum.Pattern:
                        return true;

                    case TokenTypeEnum.Constant:
                    case TokenTypeEnum.Ponctuation:
                    case TokenTypeEnum.Operator:
                    case TokenTypeEnum.Comment:
                    case TokenTypeEnum.Other:
                    default:
                        return false;
                }

            }
            else if (item.IsRuleRef)
                return true;

            return true;

        }


        public static bool IsConstant(this AstRule ast)
        {
            var l = ast.Select(c => c.Type == nameof(AstTerminal)).FirstOrDefault();
            if (l != null)
            {
                var m = l.Select(c => c.Type == "TOKEN_REF").FirstOrDefault();
                if (m != null)
                {
                    var t = m.Link.TerminalKind;
                    if (t == TokenTypeEnum.Constant)
                        return true;

                    if (t == TokenTypeEnum.Ponctuation)
                        return true;

                    if (t == TokenTypeEnum.Operator)
                        return true;

                    else
                    {

                    }
                }
            }

            return false;

        }

        public static bool IsDynamic(this AstRule ast)
        {
            var l = ast.Select(c => c.Type == nameof(AstTerminal)).FirstOrDefault();
            if (l != null)
            {
                var m = l.Select(c => c.Type == "TOKEN_REF").FirstOrDefault();
                if (m != null)
                {
                    var t = m.Link.TerminalKind;
                    switch (t)
                    {


                        case TokenTypeEnum.Identifier:
                        case TokenTypeEnum.Boolean:
                        case TokenTypeEnum.String:
                        case TokenTypeEnum.Decimal:
                        case TokenTypeEnum.Int:
                        case TokenTypeEnum.Real:
                        case TokenTypeEnum.Hexa:
                        case TokenTypeEnum.Binary:
                        case TokenTypeEnum.Pattern:
                            return true;

                        case TokenTypeEnum.Operator:
                        case TokenTypeEnum.Other:
                        case TokenTypeEnum.Constant:
                        case TokenTypeEnum.Comment:
                        case TokenTypeEnum.Ponctuation:
                        default:
                            break;
                    }
                }
            }

            return false;

        }


    }

}
