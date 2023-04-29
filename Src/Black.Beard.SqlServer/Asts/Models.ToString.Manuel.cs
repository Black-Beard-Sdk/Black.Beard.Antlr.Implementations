//#nullable disable

//namespace Bb.SqlServer.Asts
//{
//    using System;
//    using Bb.Parsers;
//    using Antlr4.Runtime;
//    using Antlr4.Runtime.Tree;
//    using Bb.Asts;
//    using Microsoft.VisualBasic;

//    public partial class AstBatchs
//    {

//        /// <summary>
//        /// batchs
//        /// 	 : batch  SEMI*  (go_statements  SEMI*  batch  SEMI*)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
            
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (AstBatch item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstSqlClauses
//    {

//        /// <summary>
//        /// sql_clauses
//        /// 	 : sql_clause  (SEMI+  sql_clause)*  SEMI*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (AstSqlClause item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);

//            }

//        }

//    }

//    public partial class AstIps
//    {

//        /// <summary>
//        /// ips
//        /// 	 : LR_BRACKET  ip_listener_comma  RR_BRACKET  
//        /// 	   (COMMA  LR_BRACKET  ip_listener_comma  RR_BRACKET)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            writer.EnsureEndBy(' ');

//            bool test = false;
//            foreach (AstIpListenerComma item in this)
//            {

//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
                
//                writer.Append(Constants.LR_BRACKET);

//                item.ToString(writer);

//                writer.Append(Constants.AstRRBracket);

//            }

//        }

//    }

//    public partial class AstOnPartitions
//    {

//        /// <summary>
//        /// on_partitions
//        /// 	 : ON  PARTITIONS  
//        /// 	   LR_BRACKET  
//        /// 	        partition_nums  (COMMA  partition_nums)*  
//        /// 	   RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            writer.EnsureEndBy(' ');

//            bool test = false;

//            AstOnPartitions.New().ToString(writer);

//            writer.Append(Constants.LR_BRACKET);

//            foreach (AstPartitionNums item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//            writer.Append(Constants.AstRRBracket);


//        }

//    }


//    public partial class AstOrderByClause
//    {


//        /// <summary>
//        /// order_by_clause
//        /// 	 : ORDER  BY  order_by_expression  (COMMA  order_by_expression)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }


//    }


//    public partial class AstSubqueries
//    {

//        /// <summary>
//        /// subqueries
//        /// 	 : subquery  (UNION  ALL  subquery)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstWithTableHints
//    {

//        /// <summary>
//        /// with_table_hints
//        /// 	 : WITH  LR_BRACKET  table_hint  (COMMA?  table_hint)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            writer.EnsureEndBy(' ');
//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstUpdateStatisticsOptions
//    {


//        /// <summary>
//        /// update_statistics_options
//        /// 	 : WITH  update_statistics_option  (COMMA  update_statistics_option)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstFunctionOptions
//    {

//        /// <summary>
//        /// function_options
//        /// 	 : WITH  function_option  (COMMA  function_option)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }


//    }

//    public partial class AstDmlTriggerOptions
//    {

//        /// <summary>
//        /// dml_trigger_options
//        /// 	 : WITH  dml_trigger_option  (COMMA  dml_trigger_option)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstProcedureOptions
//    {

//        /// <summary>
//        /// procedure_options
//        /// 	 : WITH  procedure_option  (COMMA  procedure_option)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstXmlIndexOptions
//    {

//        /// <summary>
//        /// xml_index_options
//        /// 	 : WITH  LR_BRACKET  xml_index_option  (COMMA  xml_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstCreateColumnstoreIndexOptions
//    {

//        /// <summary>
//        /// create_columnstore_index_options
//        /// 	 : WITH  LR_BRACKET  columnstore_index_option  (COMMA  columnstore_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }


//    public partial class AstSinglePartitionRebuildIndexOptions
//    {

//        /// <summary>
//        /// single_partition_rebuild_index_options
//        /// 	 : WITH  LR_BRACKET  single_partition_rebuild_index_option  (COMMA  single_partition_rebuild_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstRebuildIndexOptions
//    {

//        /// <summary>
//        /// rebuild_index_options
//        /// 	 : WITH  LR_BRACKET  rebuild_index_option  (COMMA  rebuild_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }


//    }

//    public partial class AstSetIndexOptions
//    {

//        /// <summary>
//        /// set_index_options
//        /// 	 : SET  LR_BRACKET  set_index_option  (COMMA  set_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstReorganizeOptions
//    {

//        /// <summary>
//        /// reorganize_options
//        /// 	 : WITH  LR_BRACKET  (reorganize_option  (COMMA  reorganize_option)*)  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }


//    }

//    public partial class AstResumableIndexOptions
//    {

//        /// <summary>
//        /// resumable_index_options
//        /// 	 : WITH  LR_BRACKET  (resumable_index_option  (COMMA  resumable_index_option)*)  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }


//    }

//    public partial class AstAsymmetricKeyOption
//    {

//        /// <summary>
//        /// asymmetric_key_option
//        /// 	 : WITH  PRIVATE  KEY  LR_BRACKET  by_password_crypt  (COMMA  by_password_crypt)?  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstTableValueConstructor
//    {

//        /// <summary>
//        /// table_value_constructor
//        /// 	 : VALUES  LR_BRACKET  expression_list  RR_BRACKET  (COMMA  LR_BRACKET  expression_list  RR_BRACKET)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstColumnAliasList
//    {

//        /// <summary>
//        /// column_alias_list
//        /// 	 : LR_BRACKET  alias += column_alias  (COMMA  alias += column_alias)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {
//            writer.EnsureEndBy(' ');

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstFullColumnNames
//    {

//        /// <summary>
//        /// full_column_names
//        /// 	 : LR_BRACKET  full_column_name  (COMMA  full_column_name)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }

//    }

//    public partial class AstExpressionLanguage
//    {

//        /// <summary>
//        /// expression_language
//        /// 	 : expression  (COMMA  LANGUAGE  expression)?
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//            bool test = false;

//            foreach (Ast item in this)
//            {
//                if (test)
//                    writer.Append(Constants.SEMI);
//                else
//                    test = true;
//                item.ToString(writer);
//            }

//        }


//    }

//    public partial class AstUdtMethodArguments
//    {

//        /// <summary>
//        /// udt_method_arguments
//        /// 	 : LR_BRACKET  execute_var_string  (COMMA  execute_var_string)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//        }

//    }

//    public partial class AstUpdateOptionClause
//    {

//        /// <summary>
//        /// update_option_clause
//        /// 	 : OPTION  LR_BRACKET  update_option  (COMMA  update_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//        }

//    }

//    public partial class AstWithExpression
//    {

//        /// <summary>
//        /// with_expression
//        /// 	 : WITH  common_table_expression  (COMMA  common_table_expression)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//        }

//    }

//    public partial class AstAlterTableIndexOptions
//    {

//        /// <summary>
//        /// alter_table_index_options
//        /// 	 : WITH  LR_BRACKET  alter_table_index_option  (COMMA  alter_table_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//        }

//    }

//    public partial class AstViewAttributes
//    {

//        /// <summary>
//        /// view_attributes
//        /// 	 : WITH  view_attribute  (COMMA  view_attribute)*
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//        }

//    }

//    public partial class AstCreateTableIndexOptions
//    {

//        /// <summary>
//        /// create_table_index_options
//        /// 	 : WITH  LR_BRACKET  create_table_index_option  (COMMA  create_table_index_option)*  RR_BRACKET
//        /// </summary>
//        private void CustomToString(Writer writer)
//        {

//        }

//    }



//}







