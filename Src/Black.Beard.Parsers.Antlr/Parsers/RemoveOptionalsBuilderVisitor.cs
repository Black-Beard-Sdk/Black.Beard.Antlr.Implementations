namespace Bb.Parsers
{
    public class RemoveOptionalsBuilderVisitor : TreeRuleItemVisitor<TreeRuleItem>
    {

        public RemoveOptionalsBuilderVisitor()
        {
        }

        public override TreeRuleItem Visit(TreeRuleItem i)
        {

            var result = i.CloneWithOutChildren();

            foreach (var item in i)
                result.Add(item.Accept(this));

            return result;

        }

        public override TreeRuleItem VisitAlternative(TreeRuleItem i)
        {
            this._alternatives++;
            TreeRuleItem result = i.CloneWithOutChildren();
            foreach (var item in i)
                result.Add(item.Accept(this));
            this._alternatives--;
            return result;
        }

        public override TreeRuleItem VisitBlock(TreeRuleItem i)
        {

            var result = i.Clone();

            if (i.Occurence.Optional)
            {

                var p = new TreeRuleItem()
                {
                    IsAlternative = true,
                    Origin = i.Origin,
                    Label = i.Label,
                };

                result.Occurence = result.Occurence.CloneNoOptional();
                p.Add(result);
                p.Add(TreeRuleItem.Default());

                result = new TreeRuleItem() 
                {
                    IsBlock = true 
                };
                result.Add(p);

            }

            return result;

        }

        public override TreeRuleItem VisitTerminal(TreeRuleItem i)
        {

            var result = i.Clone();

            if (i.Occurence.Optional)
            {

                var p = new TreeRuleItem()
                {
                    IsAlternative = true,
                    Origin = i.Origin,
                    Label = i.Label,
                };

                result.Occurence = result.Occurence.CloneNoOptional();
                p.Add(result);
                p.Add(TreeRuleItem.Default());

                result = new TreeRuleItem() 
                {
                    IsBlock = true 
                };
                result.Add(p);

            }

            return result;

        }

        public override TreeRuleItem VisitRuleRef(TreeRuleItem i)
        {

            var result = i.Clone();

            if (!i.IsLast && this._alternatives > 0)
            {
                if (i.Occurence.Optional)
                {

                    var p = new TreeRuleItem()
                    {
                        IsAlternative = true,
                        Origin = i.Origin,
                        Label = i.Label,
                    };

                    result.Occurence = result.Occurence.CloneNoOptional();
                    p.Add(result);
                    p.Add(TreeRuleItem.Default());

                    result = new TreeRuleItem() 
                    {
                        IsBlock = true 
                    };

                    result.Add(p);

                }
            }
            return result;

        }

        private int _alternatives;

    }

}
