using System.Data;
using static Antlr4.Runtime.Atn.SemanticContext;

namespace Bb.Asts
{
    public class AstRuleEvaluator
    {

        public AstRuleEvaluator(AstRuleMatcherList rules)
        {
            _rules = rules;
        }

        public int Evaluate<T>(AstRootList<T> items)
        {

            Iterator<T> iterator = items.Get();

            for (int indexRule = 0; indexRule < _rules.Count; indexRule++)
            {
                iterator.Reset();
            
                var r = ParseItem(_rules[indexRule], iterator);
                
                if (r > 0)
                    return r;

            }

            return 0;

        }

        private int ParseItem<T>(AstRuleMatcherItems item, Iterator<T> iterator)
        {

            if (iterator.Count > item.Count)
                return 0;

            
            for (int indexItem = 0; indexItem < item.Count; indexItem++)
            {
                T current = iterator.Current;
                AstRuleMatcherItem item2 = item.Items[indexItem];

                if (current == null)
                {

                    if (item2.Optional)
                    {
                        if (indexItem + 1 == item.Count)
                            return item.Index;
                    }
                    else
                        return 0;

                }
                else if (item2.Type.IsAssignableFrom(current.GetType()))
                {
                    if (indexItem + 1 == item.Count)
                        return item.Index;

                    iterator.Next();

                }
                else
                    return 0;


            }

            return 0;

        }

        private AstRuleMatcherList _rules;
    }

}
