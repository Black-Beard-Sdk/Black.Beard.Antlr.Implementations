using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Parsers.TSql
{
    public partial class TSqlLexer
    {

        public static bool IsKeyWord(string text, int type)
        {

            var  o = DefaultVocabulary.GetDisplayName(type);

            var u = TSqlLexer.ruleNames[type];

            return true;

        }


    }
}
