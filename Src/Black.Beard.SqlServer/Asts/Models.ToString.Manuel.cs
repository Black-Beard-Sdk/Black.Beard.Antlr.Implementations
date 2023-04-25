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

    public partial class AstOnPartitions
    {

        /// <summary>
        /// on_partitions
        /// 	 : ON  PARTITIONS  LR_BRACKET  partition_nums  (COMMA  partition_nums)*  RR_BRACKET
        /// </summary>
        private void CustomToString(Writer writer)
        {



        }

    }

    public partial class AstFilespec
    {

        /// <summary>
        /// filespec
        /// 	 : LR_BRACKET  NAME  EQUAL  file_group_id  (COMMA  NEWNAME  EQUAL  file_group_id | stringtext)?  (COMMA  FILENAME  EQUAL  file_name = stringtext)?  (COMMA  SIZE  EQUAL  size = file_size)?  (COMMA  MAXSIZE  EQUAL  max = file_size | UNLIMITED)?  (COMMA  FILEGROWTH  EQUAL  growth_increment = file_size)?  (COMMA  OFFLINE)?  RR_BRACKET
        /// </summary>
        private void CustomToString(Writer writer)
        {



        }


    }



}
