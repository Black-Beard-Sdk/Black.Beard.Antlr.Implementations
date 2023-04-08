#nullable disable
// Generate by Models.Bases : samedi 8 avril 2023
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bb.Asts.TSql
{
    using System;
    using Bb.Parsers;
    using Antlr4.Runtime;
    using Antlr4.Runtime.Tree;
    
    
    /// <summary>
    /// batch_level_statement
    /// 	 : create_or_alter_function
    /// 	 | create_or_alter_procedure
    /// 	 | create_or_alter_trigger
    /// 	 | create_view
    /// </summary>
    public abstract partial class AstBatchLevelStatement : AstRule
    {
        
        internal AstBatchLevelStatement(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstBatchLevelStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstBatchLevelStatement(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// create_or_alter_function : 
        ///    CREATE OR ALTER FUNCTION funcName=schema_func_proc_ref ( procedure_params ) func_body_returns_select ; 
        /// </summary>
        public static AstCreateOrAlterFunction CreateOrAlterFunction()
        {
            return AstCreateOrAlterFunction.CreateOrAlterFunction();
        }
    }
    
    /// <summary>
    /// dml_clause
    /// 	 : merge_statement
    /// 	 | delete_statement
    /// 	 | insert_statement
    /// 	 | select_statement_standalone
    /// 	 | update_statement
    /// </summary>
    public abstract partial class AstDmlClause : AstRule
    {
        
        internal AstDmlClause(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstDmlClause(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstDmlClause(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// merge_statement : 
        ///    with_expression? MERGE TOP ( expression ) PERCENT? INTO ddl_object with_table_hints? as_table_alias? USING table_sources ON search_condition when_matches output_clause? update_option_clause? ; 
        /// </summary>
        public static AstMergeStatement MergeStatement()
        {
            return AstMergeStatement.MergeStatement();
        }
    }
    
    /// <summary>
    /// ddl_clause
    /// 	 : alter_application_role
    /// 	 | alter_assembly
    /// 	 | alter_asymmetric_key
    /// 	 | alter_authorization
    /// 	 | alter_authorization_for_azure_dw
    /// 	 | alter_authorization_for_parallel_dw
    /// 	 | alter_authorization_for_sql_database
    /// 	 | alter_availability_group
    /// 	 | alter_certificate
    /// 	 | alter_column_encryption_key
    /// 	 | alter_credential
    /// 	 | alter_cryptographic_provider
    /// 	 | alter_database
    /// 	 | alter_db_role
    /// 	 | alter_endpoint
    /// 	 | create_or_alter_event_session
    /// 	 | alter_external_data_source
    /// 	 | alter_external_library
    /// 	 | alter_external_resource_pool
    /// 	 | alter_fulltext_catalog
    /// 	 | alter_fulltext_stoplist
    /// 	 | alter_index
    /// 	 | alter_login_azure_sql
    /// 	 | alter_login_azure_sql_dw_and_pdw
    /// 	 | alter_login_sql_server
    /// 	 | alter_master_key_azure_sql
    /// 	 | alter_master_key_sql_server
    /// 	 | alter_message_type
    /// 	 | alter_partition_function
    /// 	 | alter_partition_scheme
    /// 	 | alter_remote_service_binding
    /// 	 | alter_resource_governor
    /// 	 | alter_schema_azure_sql_dw_and_pdw
    /// 	 | alter_schema_sql
    /// 	 | alter_sequence
    /// 	 | alter_server_audit
    /// 	 | alter_server_audit_specification
    /// 	 | alter_server_configuration
    /// 	 | alter_server_role
    /// 	 | alter_server_role_pdw
    /// 	 | alter_service
    /// 	 | alter_service_master_key
    /// 	 | alter_symmetric_key
    /// 	 | alter_table
    /// 	 | alter_user
    /// 	 | alter_user_azure_sql
    /// 	 | alter_workload_group
    /// 	 | create_application_role
    /// 	 | create_assembly
    /// 	 | create_asymmetric_key
    /// 	 | create_column_encryption_key
    /// 	 | create_column_master_key
    /// 	 | create_credential
    /// 	 | create_cryptographic_provider
    /// 	 | create_database
    /// 	 | create_db_role
    /// 	 | create_event_notification
    /// 	 | create_external_library
    /// 	 | create_external_resource_pool
    /// 	 | create_fulltext_catalog
    /// 	 | create_fulltext_stoplist
    /// 	 | create_index
    /// 	 | create_columnstore_index
    /// 	 | create_nonclustered_columnstore_index
    /// 	 | create_login_azure_sql
    /// 	 | create_login_pdw
    /// 	 | create_login_sql_server
    /// 	 | create_master_key_azure_sql
    /// 	 | create_master_key_sql_server
    /// 	 | create_or_alter_broker_priority
    /// 	 | create_remote_service_binding
    /// 	 | create_resource_pool
    /// 	 | create_route
    /// 	 | create_rule
    /// 	 | create_schema
    /// 	 | create_schema_azure_sql_dw_and_pdw
    /// 	 | create_search_property_list
    /// 	 | create_security_policy
    /// 	 | create_sequence
    /// 	 | create_server_audit
    /// 	 | create_server_audit_specification
    /// 	 | create_server_role
    /// 	 | create_service
    /// 	 | create_statistics
    /// 	 | create_synonym
    /// 	 | create_table
    /// 	 | create_type
    /// 	 | create_user
    /// 	 | create_user_azure_sql_dw
    /// 	 | create_workload_group
    /// 	 | create_xml_index
    /// 	 | create_xml_schema_collection
    /// 	 | create_partition_function
    /// 	 | create_partition_scheme
    /// 	 | drop_aggregate
    /// 	 | drop_application_role
    /// 	 | drop_assembly
    /// 	 | drop_asymmetric_key
    /// 	 | drop_availability_group
    /// 	 | drop_broker_priority
    /// 	 | drop_certificate
    /// 	 | drop_column_encryption_key
    /// 	 | drop_column_master_key
    /// 	 | drop_contract
    /// 	 | drop_credential
    /// 	 | drop_cryptograhic_provider
    /// 	 | drop_database
    /// 	 | drop_database_audit_specification
    /// 	 | drop_database_encryption_key
    /// 	 | drop_database_scoped_credential
    /// 	 | drop_db_role
    /// 	 | drop_default
    /// 	 | drop_endpoint
    /// 	 | drop_event_notifications
    /// 	 | drop_event_session
    /// 	 | drop_external_data_source
    /// 	 | drop_external_file_format
    /// 	 | drop_external_library
    /// 	 | drop_external_resource_pool
    /// 	 | drop_external_table
    /// 	 | drop_fulltext_catalog
    /// 	 | drop_fulltext_index
    /// 	 | drop_fulltext_stoplist
    /// 	 | drop_function
    /// 	 | drop_index
    /// 	 | drop_login
    /// 	 | drop_master_key
    /// 	 | drop_message_type
    /// 	 | drop_partition_function
    /// 	 | drop_partition_scheme
    /// 	 | drop_procedure
    /// 	 | drop_queue
    /// 	 | drop_remote_service_binding
    /// 	 | drop_resource_pool
    /// 	 | drop_route
    /// 	 | drop_rule
    /// 	 | drop_schema
    /// 	 | drop_search_property_list
    /// 	 | drop_security_policy
    /// 	 | drop_sequence
    /// 	 | drop_server_audit
    /// 	 | drop_server_audit_specification
    /// 	 | drop_server_role
    /// 	 | drop_service
    /// 	 | drop_signature
    /// 	 | drop_statistics
    /// 	 | drop_statistics_id_azure_dw_and_pdw
    /// 	 | drop_symmetric_key
    /// 	 | drop_synonym
    /// 	 | drop_table
    /// 	 | drop_trigger
    /// 	 | drop_type
    /// 	 | drop_user
    /// 	 | drop_view
    /// 	 | drop_workload_group
    /// 	 | drop_xml_schema_collection
    /// 	 | disable_trigger
    /// 	 | enable_trigger
    /// 	 | lock_table
    /// 	 | truncate_table
    /// 	 | update_statistics
    /// </summary>
    public abstract partial class AstDdlClause : AstRule
    {
        
        internal AstDdlClause(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstDdlClause(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstDdlClause(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// alter_application_role : 
        ///    ALTER APPLICATION ROLE role_id WITH COMMA? NAME EQUAL new_role=role_id COMMA? PASSWORD EQUAL application_role_password=stringtext COMMA? DEFAULT_SCHEMA EQUAL schema_id 
        /// </summary>
        public static AstAlterApplicationRole AlterApplicationRole()
        {
            return AstAlterApplicationRole.AlterApplicationRole();
        }
    }
    
    /// <summary>
    /// backup_statement
    /// 	 : backup_database
    /// 	 | backup_log
    /// 	 | backup_certificate
    /// 	 | backup_master_key
    /// 	 | backup_service_master_key
    /// </summary>
    public abstract partial class AstBackupStatement : AstRule
    {
        
        internal AstBackupStatement(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstBackupStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstBackupStatement(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// backup_database : 
        ///    BACKUP DATABASE database_id() READ_WRITE_FILEGROUPS group1=file_group_list group2=file_group_list backup_target? backup_settings? 
        /// </summary>
        public static AstBackupDatabase BackupDatabase()
        {
            return AstBackupDatabase.BackupDatabase();
        }
    }
    
    /// <summary>
    /// cfl_statement
    /// 	 : block_statement
    /// 	 | break_statement
    /// 	 | continue_statement
    /// 	 | goto_statement
    /// 	 | if_statement
    /// 	 | return_statement
    /// 	 | throw_statement
    /// 	 | try_catch_statement
    /// 	 | waitfor_statement
    /// 	 | while_statement
    /// 	 | print_statement
    /// 	 | raiseerror_statement
    /// </summary>
    public abstract partial class AstCflStatement : AstRule
    {
        
        internal AstCflStatement(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstCflStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstCflStatement(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// block_statement : 
        ///    BEGIN ; sql_clauses? END ; 
        /// </summary>
        public static AstBlockStatement BlockStatement()
        {
            return AstBlockStatement.BlockStatement();
        }
    }
    
    /// <summary>
    /// another_statement
    /// 	 : declare_statement
    /// 	 | execute_statement
    /// 	 | cursor_statement
    /// 	 | conversation_statement
    /// 	 | create_contract
    /// 	 | create_queue
    /// 	 | alter_queue
    /// 	 | kill_statement
    /// 	 | message_statement
    /// 	 | security_statement
    /// 	 | set_statement
    /// 	 | transaction_statement
    /// 	 | use_statement
    /// 	 | setuser_statement
    /// 	 | reconfigure_statement
    /// 	 | shutdown_statement
    /// 	 | checkpoint_statement
    /// </summary>
    public abstract partial class AstAnotherStatement : AstRule
    {
        
        internal AstAnotherStatement(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstAnotherStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstAnotherStatement(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// declare_statement : 
        ///    DECLARE local_id AS table_type_definition ; 
        /// </summary>
        public static AstDeclareStatement DeclareStatement()
        {
            return AstDeclareStatement.DeclareStatement();
        }
    }
    
    /// <summary>
    /// conversation_statement
    /// 	 : begin_conversation_timer
    /// 	 | begin_conversation_dialog
    /// 	 | end_conversation
    /// 	 | get_conversation
    /// 	 | send_conversation
    /// 	 | waitfor_conversation
    /// </summary>
    public abstract partial class AstConversationStatement : AstRule
    {
        
        internal AstConversationStatement(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstConversationStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstConversationStatement(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// begin_conversation_timer : 
        ///    BEGIN CONVERSATION TIMER ( conversation=local_id ) TIMEOUT EQUAL time ; 
        /// </summary>
        public static AstBeginConversationTimer BeginConversationTimer()
        {
            return AstBeginConversationTimer.BeginConversationTimer();
        }
    }
    
    /// <summary>
    /// create_or_alter_trigger
    /// 	 : create_or_alter_dml_trigger
    /// 	 | create_or_alter_ddl_trigger
    /// </summary>
    public abstract partial class AstCreateOrAlterTrigger : AstBatchLevelStatement
    {
        
        internal AstCreateOrAlterTrigger(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstCreateOrAlterTrigger(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstCreateOrAlterTrigger(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// create_or_alter_dml_trigger : 
        ///    CREATE OR ALTER TRIGGER schema_trigger_ref ON full_table_ref dml_trigger_options? for_after_instead dml_trigger_operations WITH APPEND NOT FOR REPLICATION AS sql_clauses 
        /// </summary>
        public static AstCreateOrAlterDmlTrigger CreateOrAlterDmlTrigger()
        {
            return AstCreateOrAlterDmlTrigger.CreateOrAlterDmlTrigger();
        }
    }
    
    /// <summary>
    /// constraint_delete_or_update
    /// 	 : on_delete
    /// 	 | on_update
    /// </summary>
    public abstract partial class AstConstraintDeleteOrUpdate : AstRule
    {
        
        internal AstConstraintDeleteOrUpdate(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstConstraintDeleteOrUpdate(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstConstraintDeleteOrUpdate(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// on_delete : 
        ///    ON DELETE NO ACTION 
        /// </summary>
        public static AstOnDelete OnDelete()
        {
            return AstOnDelete.OnDelete();
        }
    }
    
    /// <summary>
    /// database_mirroring_option
    /// 	 : mirroring_set_option
    /// </summary>
    public abstract partial class AstDatabaseMirroringOption : AstRule
    {
        
        internal AstDatabaseMirroringOption(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstDatabaseMirroringOption(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstDatabaseMirroringOption(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// mirroring_set_option : 
        ///    mirroring_partner partner_option 
        /// </summary>
        public static AstMirroringSetOption MirroringSetOption()
        {
            return AstMirroringSetOption.MirroringSetOption();
        }
    }
    
    /// <summary>
    /// witness_server
    /// 	 : partner_server
    /// </summary>
    public abstract partial class AstWitnessServer : AstRule
    {
        
        internal AstWitnessServer(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstWitnessServer(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstWitnessServer(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// partner_server : 
        ///    partner_server_tcp_prefix host : port_number 
        /// </summary>
        public static AstPartnerServer PartnerServer()
        {
            return AstPartnerServer.PartnerServer();
        }
    }
    
    /// <summary>
    /// drop_trigger
    /// 	 : drop_dml_trigger
    /// 	 | drop_ddl_trigger
    /// </summary>
    public abstract partial class AstDropTrigger : AstRule
    {
        
        internal AstDropTrigger(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstDropTrigger(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstDropTrigger(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// drop_dml_trigger : 
        ///    DROP TRIGGER if_exists? schema_trigger_refs ; 
        /// </summary>
        public static AstDropDmlTrigger DropDmlTrigger()
        {
            return AstDropDmlTrigger.DropDmlTrigger();
        }
    }
    
    /// <summary>
    /// rowset_function_limited
    /// 	 : openquery
    /// 	 | opendatasource
    /// </summary>
    public abstract partial class AstRowsetFunctionLimited : AstRule
    {
        
        internal AstRowsetFunctionLimited(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstRowsetFunctionLimited(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstRowsetFunctionLimited(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// openquery : 
        ///    OPENQUERY ( server_id , query=stringtext ) 
        /// </summary>
        public static AstOpenquery Openquery()
        {
            return AstOpenquery.Openquery();
        }
    }
    
    /// <summary>
    /// subquery
    /// 	 : select_statement
    /// </summary>
    public abstract partial class AstSubquery : AstRule
    {
        
        internal AstSubquery(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstSubquery(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstSubquery(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// select_statement : 
        ///    query_expression select_order_by_clause? for_clause? update_option_clause? ; 
        /// </summary>
        public static AstSelectStatement SelectStatement()
        {
            return AstSelectStatement.SelectStatement();
        }
    }
    
    /// <summary>
    /// asterisk
    /// 	 : star_asterisk
    /// 	 | table_asterisk
    /// 	 | updated_asterisk
    /// </summary>
    public abstract partial class AstAsterisk : AstRule
    {
        
        internal AstAsterisk(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstAsterisk(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstAsterisk(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// star_asterisk : 
        ///    STAR 
        /// </summary>
        public static AstStarAsterisk StarAsterisk()
        {
            return AstStarAsterisk.StarAsterisk();
        }
    }
    
    /// <summary>
    /// select_list_elem
    /// 	 : asterisk
    /// 	 | column_elem
    /// 	 | udt_elem
    /// 	 | expression_assign_elem
    /// 	 | expression_elem
    /// </summary>
    public abstract partial class AstSelectListElem : AstRule
    {
        
        internal AstSelectListElem(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstSelectListElem(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstSelectListElem(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// star_asterisk : 
        ///    STAR 
        /// </summary>
        public static AstStarAsterisk StarAsterisk()
        {
            return AstStarAsterisk.StarAsterisk();
        }
    }
    
    /// <summary>
    /// change_table
    /// 	 : change_table_changes
    /// 	 | change_table_version
    /// </summary>
    public abstract partial class AstChangeTable : AstRule
    {
        
        internal AstChangeTable(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstChangeTable(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstChangeTable(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// change_table_changes : 
        ///    CHANGETABLE ( CHANGES changetable=full_table_ref , NULL_ ) 
        /// </summary>
        public static AstChangeTableChanges ChangeTableChanges()
        {
            return AstChangeTableChanges.ChangeTableChanges();
        }
    }
    
    /// <summary>
    /// join_part
    /// 	 : join_on
    /// 	 | cross_join
    /// 	 | apply_enum
    /// 	 | pivot
    /// 	 | unpivot
    /// </summary>
    public abstract partial class AstJoinPart : AstRule
    {
        
        internal AstJoinPart(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstJoinPart(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstJoinPart(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// join_on : 
        ///    inner=INNER join_hint? JOIN source=table_source ON cond=search_condition 
        /// </summary>
        public static AstJoinOn JoinOn()
        {
            return AstJoinOn.JoinOn();
        }
    }
    
    /// <summary>
    /// xml_data_type_methods
    /// 	 : value_method
    /// 	 | query_method
    /// 	 | exist_method
    /// 	 | modify_method
    /// </summary>
    public abstract partial class AstXmlDataTypeMethods : AstRule
    {
        
        internal AstXmlDataTypeMethods(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstXmlDataTypeMethods(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstXmlDataTypeMethods(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// value_method : 
        ///    loc=local_id . call=value_call 
        /// </summary>
        public static AstValueMethod ValueMethod()
        {
            return AstValueMethod.ValueMethod();
        }
    }
    
    /// <summary>
    /// window_frame_bound
    /// 	 : window_frame_preceding
    /// 	 | window_frame_following
    /// </summary>
    public abstract partial class AstWindowFrameBound : AstRule
    {
        
        internal AstWindowFrameBound(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstWindowFrameBound(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstWindowFrameBound(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// window_frame_preceding : 
        ///    UNBOUNDED PRECEDING 
        /// </summary>
        public static AstWindowFramePreceding WindowFramePreceding()
        {
            return AstWindowFramePreceding.WindowFramePreceding();
        }
    }
    
    /// <summary>
    /// database_file_spec
    /// 	 : file_group
    /// 	 | file_spec
    /// </summary>
    public abstract partial class AstDatabaseFileSpec : AstRule
    {
        
        internal AstDatabaseFileSpec(ITerminalNode t) : 
                base(t)
        {
        }
        
        internal AstDatabaseFileSpec(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        internal AstDatabaseFileSpec(Position p) : 
                base(p)
        {
        }
        
        /// <summary>
        /// file_group : 
        ///    FILEGROUP file_group_id CONTAINS FILESTREAM DEFAULT() CONTAINS MEMORY_OPTIMIZED_DATA file_specs 
        /// </summary>
        public static AstFileGroup FileGroup()
        {
            return AstFileGroup.FileGroup();
        }
    }
}
