using Bb.SqlServer.Asts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.SqlServer
{

    public static partial class TSql
    {

        
     

             

        public static AstCreateDatabaseOptionList WithDatabaseOptions(params AstCreateDatabaseOption[] options)
        {
                       
            var opt = AstCreateDatabaseOptionList.New
                (
                    options
                );


            return opt;

        }
                
    }


}
