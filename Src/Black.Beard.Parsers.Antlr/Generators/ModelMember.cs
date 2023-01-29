using System.CodeDom;

namespace Bb.Generators
{


    public class ModelMember
    {


        public Func<bool> Test { get; internal set; }


        protected virtual bool MemberExists(CodeTypeMemberCollection members, string n)
        {
            foreach (CodeTypeMember item in members)
                if (item.Name == n)
                    return true;
            return false;
        }



    }



}
