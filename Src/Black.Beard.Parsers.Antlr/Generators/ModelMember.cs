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
        protected virtual bool MemberExists(CodeTypeMemberCollection members, string n)
        {
            foreach (CodeTypeMember item in members)
                if (item.Name == n)
                    return true;
            return false;
        }

        protected Action<Documentation> _actionDocumentation;

    }

}
