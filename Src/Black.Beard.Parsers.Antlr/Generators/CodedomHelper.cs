using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Generators
{

    public static class CodedomHelper
    {

        public static bool MemberExists(CodeTypeMemberCollection members, CodeTypeMember member)
        {

            var m1 = member as CodeMemberMethod;
            var n = member.Name;

            foreach (CodeTypeMember item in members)
                if (item.Name == n)
                {

                    var m2 = item as CodeMemberMethod;
                    if (m2 == null)
                        return true;

                    if (CompareMethods(m1, m2))
                        return true;

                }

            return false;

        }

        private static bool CompareMethods(CodeMemberMethod m1, CodeMemberMethod m2)
        {

            var countParameter = m1.Parameters.Count;

            if (m2.Parameters.Count == countParameter && m2.Parameters.Count == 0)
                return true;

            if (m2.Parameters.Count != countParameter)
                return false;

            for (int i = 0; i < countParameter; i++)
            {

                if (!CompareArguments(m1.Parameters[i], m2.Parameters[i]))
                    return false;

            }

            return true;

        }

        private static bool CompareArguments(CodeParameterDeclarationExpression arg1, CodeParameterDeclarationExpression arg2)
        {
            return CompareTypes(arg1.Type, arg2.Type);
        }

        private static bool CompareTypes(CodeTypeReference type1, CodeTypeReference type2)
        {

            if (type1.BaseType == type2.BaseType)
            {

                if (type1.ArrayRank == type2.ArrayRank)
                {

                    if (type1.TypeArguments.Count == type2.TypeArguments.Count)
                    {


                        if (type1.TypeArguments.Count == 0)
                            return true;

                        var countParameter = type1.TypeArguments.Count;

                        for (int i = 0; i < countParameter; i++)
                        {

                            if (!CompareTypes(type1.TypeArguments[i], type1.TypeArguments[i]))
                                return false;

                        }

                        return true;

                    }


                }

            }

            return false;
        }


    }

}
