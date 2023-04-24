using Bb.SqlServer.Asts;
using Bb.SqlServer.Parsers;
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
            public static AstCreateDatabase Database(string path, string database, bool containmentPartial)
            {
                return Database(path, database, containmentPartial, "DATABASE_DEFAULT");
            }

            /// <summary>
            /// create_database
            /// </summary>
            public static AstCreateDatabase Database(string path, string database, bool containmentPartial, AstCollationId collation)
            {

                var path1 = Path.Combine(path, database + ".mdf");
                var path2 = Path.Combine(path, database + "_log.ldf");

                var onPrimary = AstDatabaseFiles.New
                (
                    AstDatabaseFile.File(database, path1, 8192, UnitySizeEnum.Kb, 65536, UnitySizeEnum.Kb)
                );

                var onLog = AstDatabaseFiles.New
                (
                    AstDatabaseFile.File(database, path1, 8192, UnitySizeEnum.Kb, 2048, UnitySizeEnum.Gb, 65536, UnitySizeEnum.Kb)
                );

                return Database(database, containmentPartial, collation, onPrimary, onLog);

            }

            /// <summary>
            /// create_database
            /// </summary>
            public static AstCreateDatabase Database(string database, bool containmentPartial, AstCollationId collation, AstDatabaseFiles onPrimary, AstDatabaseFiles onLog, AstCreateDatabaseOptionList? options = null)
            {

                return AstCreateDatabase.New
                (
                    database,
                    AstContainmentSet.New(containmentPartial ? AstNonePartial.Partial() : AstNonePartial.None()),

                    AstDatabaseOnPrimary.New(onPrimary),
                    AstDatabaseOnLog.New(onLog),

                    AstCollateSet.New(collation),

                    options == null ? AstDatabaseWithOption.Null() : AstDatabaseWithOption.New(options)

                );

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
