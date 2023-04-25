#nullable disable

namespace Bb.SqlServer.Asts
{
    using System;
    using Bb.Parsers;
    using Antlr4.Runtime;
    using Antlr4.Runtime.Tree;
    using Bb.Asts;

    public partial class AstBatchs
    {

        /// <summary>
        /// batchs
        /// 	 : batch  SEMI*  (go_statements  SEMI*  batch  SEMI*)*
        /// </summary>
        private void CustomToString(Writer writer)
        {

            foreach (var item in this)
            {

            }

        }

    }


    public partial class AstSqlClauses
    {

        /// <summary>
        /// sql_clauses
        /// 	 : sql_clause  (SEMI+  sql_clause)*  SEMI*
        /// </summary>
        private void CustomToString(Writer writer)
        {


        }

    }

    public partial class AstIps
    {

        /// <summary>
        /// ips
        /// 	 : LR_BRACKET  ip_listener_comma  RR_BRACKET  (COMMA  LR_BRACKET  ip_listener_comma  RR_BRACKET)*
        /// </summary>
        private void CustomToString(Writer writer)
        {


        }

    }





}
