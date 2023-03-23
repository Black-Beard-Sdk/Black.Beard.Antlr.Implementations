using Bb.Parsers;

namespace Bb.Asts
{
    public class ReferenceFinderVisitor : WalkerVisitor
    {

        private ReferenceFinderVisitor(AstRule tofind)
        {
            this._tofindText = tofind.Name.Text;
            _list = new HashSet<AstRule>();
        }

        public static List<AstRule> GetRules(AstRule a, AstRules rules) 
        {
            var visitor = new ReferenceFinderVisitor(a);
            visitor.Visit(rules);
            return new List<AstRule>(visitor._list);
        }


        public override void VisitRuleRef(AstRuleRef a)
        {

            if (a.Identifier.Text == this._tofindText)
                _list.Add(this._ToAdd);

            else
                base.VisitRuleRef(a);

        }

        public override void VisitRule(AstRule a)
        {
            this._ToAdd = a;
            base.VisitRule(a);
        }


        private readonly string _tofindText;
        private HashSet<AstRule> _list;
        private AstRule _ToAdd;
    }


}
