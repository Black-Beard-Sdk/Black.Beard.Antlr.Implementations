using Bb.Parsers;
using Newtonsoft.Json.Linq;
using System.CodeDom;

namespace Bb.Generators
{


    public class ModelMember
    {

        public ModelMember()
        {

        }


        public Func<bool> Test { get; internal set; }


        protected void GenerateDocumentation(CodeTypeMember member, Context context)
        {

            if (_actionDocumentation != null)
            {
                var doc = new Documentation();
                _actionDocumentation(doc);
                doc.GenerateDocumentation(member, context);
            }

        }

        /// <summary>
        /// Members the exists.
        /// </summary>
        /// <param name="members">The members.</param>
        /// <param name="n">The n.</param>        
        protected virtual bool MemberExists(CodeTypeMemberCollection members, CodeTypeMember member)
        {

            var n = member.Name;

            foreach (CodeTypeMember item in members)
                if (item.Name == n)
                    return true;
            return false;
        }

        public virtual void Clean()
        {

        }

        protected static IEnumerable<T> Select<T>(CodeTypeMemberCollection items)
            where T : CodeTypeMember
        {
            foreach (var item in items)
                if (item is T t)
                    yield return t;
        }


        protected Action<Documentation> _actionDocumentation;

    }

}
