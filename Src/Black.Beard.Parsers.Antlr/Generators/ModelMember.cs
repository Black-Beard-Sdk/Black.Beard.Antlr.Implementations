using Bb.Parsers;
using System.CodeDom;

namespace Bb.Generators
{


    public class ModelMember
    {


        public Func<bool> Test { get; internal set; }


        protected void GenerateDocumentation(CodeTypeMember member, Context context)
        {
            if (_actionComment != null)
            {
                var txt = _actionComment();
                var text = txt.Split('\r');
                member.Comments.Add(new CodeCommentStatement("<summary>", true));
                foreach (var item in text)
                    if (!string.IsNullOrEmpty(item.Trim()) && !string.IsNullOrEmpty(item.Trim()))
                        member.Comments.Add(new CodeCommentStatement(item.Trim('\n'), true));
                member.Comments.Add(new CodeCommentStatement("</summary>", true));


                member.Comments.Add(new CodeCommentStatement("<remarks>", true));
                if (!string.IsNullOrEmpty(context.Strategy))
                    member.Comments.Add(new CodeCommentStatement("Strategy : " + context.Strategy, true));
                else
                    member.Comments.Add(new CodeCommentStatement("Strategy : Default", true));

                member.Comments.Add(new CodeCommentStatement("</remarks>", true));

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

        protected Func<string> _actionComment;


    }



}
