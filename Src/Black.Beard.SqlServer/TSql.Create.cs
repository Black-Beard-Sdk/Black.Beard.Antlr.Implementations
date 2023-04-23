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

        public static AstRoot ParseSql(this string sql)
        {
            return ParseSql(new StringBuilder(sql));
        }

        public static AstRoot ParseSql(this StringBuilder sql)
        {
            var parser = Parser.SqlServerScriptParser.ParseString(sql);
            var result = parser.GetModel();
            return result;
        }

        public static class Create
        {

            /// <summary>
            /// create_database
            /// </summary>
            public static void Database(string database)
            {
                AstCreateDatabase.New
                (
                    database,
                    AstContainmentSet.New(AstNonePartial.None()),
                    AstDatabaseOnPrimary.New
                    (
                        AstDatabaseFiles.New
                        (
                            DatabaseFile("", "", 1, 1, 1, UnitySizeEnum.Gb)
                        )
                    ), 
                    AstDatabaseOnLog.New
                    (
                       AstDatabaseFiles.New
                        (
                            FileGroup("", 
                                FileSpec("", "", 1, 1, 1, UnitySizeEnum.Gb),
                                FileSpec("", "", 1, 1, 1, UnitySizeEnum.Gb)
                            )
                        )
                    ),
                    AstCollate.New(AstCollationId.New("")), 
                    AstCreateDatabaseOptionList.New
                    (
                        AstCreateDatabaseOption.New
                        (
                            AstDatabaseFilestreamOptions.New
                            (
                                AstDatabaseFilestreamOption.New(
                            )
                        )
                    )

                ); ;
            }

            /*
    | create_contract
    | create_queue
    | create_or_alter_event_session
    | create_application_role
    | create_assembly
    | create_asymmetric_key
    | create_column_encryption_key
    | create_column_master_key
    | create_credential
    | create_cryptographic_provider
    | create_db_role
    | create_event_notification
    | create_external_library
    | create_external_resource_pool
    | create_fulltext_catalog
    | create_fulltext_stoplist
    | create_index
    | create_columnstore_index
    | create_nonclustered_columnstore_index
    | create_login_azure_sql
    | create_login_pdw
    | create_login_sql_server
    | create_master_key_azure_sql
    | create_master_key_sql_server
    | create_or_alter_broker_priority
    | create_remote_service_binding
    | create_resource_pool
    | create_route
    | create_rule
    | create_schema
    | create_schema_azure_sql_dw_and_pdw
    | create_search_property_list
    | create_security_policy
    | create_sequence
    | create_server_audit
    | create_server_audit_specification
    | create_server_role
    | create_service
    | create_statistics
    | create_synonym
    | create_table
    | create_type
    | create_user
    | create_user_azure_sql_dw
    | create_workload_group
    | create_xml_index
    | create_xml_schema_collection
    | create_partition_function
    | create_partition_scheme
	*/


        }

    }





}
