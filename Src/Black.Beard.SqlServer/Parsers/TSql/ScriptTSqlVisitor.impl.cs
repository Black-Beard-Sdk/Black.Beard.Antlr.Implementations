using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers.TSql;

namespace Bb.Parsers.TSql
{

    public partial class ScriptTSqlVisitor
    {


        private static bool IsSymbol(string txt, string r)
        {
            bool test = false;

            for (int i = 0; i < txt.Length; i++)
            {
                char c = txt[i];
                if (c != ' ' && !char.IsLetter(c))
                {
                    test = true;
                    continue;
                }
            }

            if (test)
                test = (txt == r.Trim('\''));

            return test;

        }


    }

}


