/*
T-SQL (Transact-SQL, MSSQL) grammar.
The MIT License (MIT).
Copyright (c) 2022, Gaël Beard (gaelgael5@gmail.com)
Copyright (c) 2017, Mark Adams (madams51703@gmail.com)
Copyright (c) 2015-2017, Ivan Kochurkin (kvanttt@gmail.com), Positive Technologies.
Copyright (c) 2016, Scott Ure (scott@redstormsoftware.com).
Copyright (c) 2016, Rui Zhang (ruizhang.ccs@gmail.com).
Copyright (c) 2016, Marcus Henriksson (kuseman80@gmail.com).
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

parser grammar TSqlParser;

options { tokenVocab=TSqlLexer; }

t_root
    : batchs SEMI* EOF
    | execute_body_batch go_statements? EOF
    ;

batchs
    : batch (SEMI+ batch)*
    ;

batch
    : execute_body_batch
    | sql_clauses
    | create_or_alter_function
    | create_or_alter_procedure
    | create_or_alter_trigger
    | create_view
    | go_statements
    ;

go_statements : go_statement+;

sql_clauses
    : sql_clause (SEMI+ sql_clause)* SEMI*
    ;

sql_clause
    // Data Manipulation Language: https://msdn.microsoft.com/en-us/library/ff848766(v=sql.120).aspx
    : merge_statement
    | delete_statement
    | insert_statement
    | select_statement_standalone
    | update_statement
    
    | alter_queue
    // Data Definition Language: https://msdn.microsoft.com/en-us/library/ff848799.aspx)
    | alter_application_role
    | alter_assembly
    | alter_asymmetric_key
    | alter_authorization
    | alter_authorization_for_azure_dw
    | alter_authorization_for_parallel_dw
    | alter_authorization_for_sql_database
    | alter_availability_group
    | alter_certificate
    | alter_column_encryption_key
    | alter_credential
    | alter_cryptographic_provider
    | alter_database
    | alter_db_role
    | alter_endpoint
    | alter_external_data_source
    | alter_external_library
    | alter_external_resource_pool
    | alter_fulltext_catalog
    | alter_fulltext_stoplist
    | alter_index
    | alter_login_azure_sql
    | alter_login_azure_sql_dw_and_pdw
    | alter_login_sql_server
    | alter_master_key_azure_sql
    | alter_master_key_sql_server
    | alter_message_type
    | alter_partition_function
    | alter_partition_scheme
    | alter_remote_service_binding
    | alter_resource_governor
    | alter_schema_azure_sql_dw_and_pdw
    | alter_schema_sql
    | alter_sequence
    | alter_server_audit
    | alter_server_audit_specification
    | alter_server_configuration
    | alter_server_role
    | alter_server_role_pdw
    | alter_service
    | alter_service_master_key
    | alter_symmetric_key
    | alter_table
    | alter_user
    | alter_user_azure_sql
    | alter_workload_group


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
    | create_database
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

    | drop_aggregate
    | drop_application_role
    | drop_assembly
    | drop_asymmetric_key
    | drop_availability_group
    | drop_broker_priority
    | drop_certificate
    | drop_column_encryption_key
    | drop_column_master_key
    | drop_contract
    | drop_credential
    | drop_cryptograhic_provider
    | drop_database
    | drop_database_audit_specification
    | drop_database_encryption_key
    | drop_database_scoped_credential
    | drop_db_role
    | drop_default
    | drop_endpoint
    | drop_event_notifications
    | drop_event_session
    | drop_external_data_source
    | drop_external_file_format
    | drop_external_library
    | drop_external_resource_pool
    | drop_external_table
    | drop_fulltext_catalog
    | drop_fulltext_index
    | drop_fulltext_stoplist
    | drop_function
    | drop_index
    | drop_login
    | drop_master_key
    | drop_message_type
    | drop_partition_function
    | drop_partition_scheme
    | drop_procedure
    | drop_queue
    | drop_remote_service_binding
    | drop_resource_pool
    | drop_route
    | drop_rule
    | drop_schema
    | drop_search_property_list
    | drop_security_policy
    | drop_sequence
    | drop_server_audit
    | drop_server_audit_specification
    | drop_server_role
    | drop_service
    | drop_signature
    | drop_statistics
    | drop_statistics_id_azure_dw_and_pdw
    | drop_symmetric_key
    | drop_synonym
    | drop_table

    // https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-trigger-transact-sql
    | drop_dml_trigger
    | drop_ddl_trigger
    | drop_type
    | drop_user
    | drop_view
    | drop_workload_group
    | drop_xml_schema_collection
    | disable_trigger
    | enable_trigger
    | lock_table
    | truncate_table
    | update_statistics

    // Control-of-Flow Language: https://docs.microsoft.com/en-us/sql/t-sql/language-elements/control-of-flow
    | block_statement
    | break_statement
    | continue_statement
    | goto_statement
    | if_statement
    | return_statement
    | throw_statement
    | try_catch_statement
    | waitfor_statement
    | while_statement
    | print_statement
    | raiseerror_statement
    

    | declare_statement
    | execute_statement
    | cursor_statement

    | begin_conversation_timer
    | begin_conversation_dialog
    | end_conversation
    | get_conversation
    | send_conversation
    | waitfor_conversation

    | kill_statement
    | message_statement
    | security_statement
    | set_statement
    | transaction_statement
    | use_statement
    | setuser_statement
    | reconfigure_statement
    | shutdown_statement
    | checkpoint_statement 
    
    | backup_database
    | backup_log
    | backup_certificate
    | backup_master_key
    | backup_service_master_key

    | dbcc_special
    | dbcc_clause
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/begin-end-transact-sql
block_statement
    : BEGIN SEMI? sql_clauses? END
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/break-transact-sql
break_statement
    : BREAK
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/continue-transact-sql
continue_statement
    : CONTINUE
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/goto-transact-sql
goto_statement
    : GOTO code_location_id
    | code_location_id COLON
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/return-transact-sql
return_statement
    : RETURN expression?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/if-else-transact-sql
if_statement
    : IF search_condition sql_clause_true=sql_clause (ELSE sql_clause_false=sql_clause)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/throw-transact-sql
throw_statement
    : THROW (throw_error_number COMMA throw_message COMMA throw_state)?
    ;

throw_error_number
    : decimal_local_id
    ;

throw_message
    : string_local_id
    ;

throw_state
    : decimal_local_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/try-catch-transact-sql
try_catch_statement
    : BEGIN TRY SEMI? try_clauses=sql_clauses END TRY SEMI? BEGIN CATCH SEMI? catch_clauses=sql_clauses? END CATCH
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/waitfor-transact-sql
waitfor_statement
    : WAITFOR receive_statement? COMMA? (delay_time_timeout timespan)?  expression?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/while-transact-sql
while_statement
    : WHILE search_condition while_statement_content
    ;

while_statement_content
    : sql_clause 
    | BREAK 
    | CONTINUE
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/print-transact-sql
print_statement
    : PRINT (expression | empty_value) local_ids?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/raiserror-transact-sql
raiseerror_statement
    : RAISERROR 
        LR_BRACKET 
            msg=decimal_string_local_id COMMA severity=constant_local_id 
            COMMA state=constant_local_id constant_local_ids? 
        RR_BRACKET 
        (WITH log_seterror_nowait)?
    | RAISERROR decimal formatstring=string_local_id_double_quote_id decimal_string_locals?
    ;

constant_local_ids : constant_local_id_or_null (COMMA constant_local_id_or_null)+;
constant_local_id_or_null : constant_local_id | NULL_;

empty_statement
    : SEMI
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-application-role-transact-sql
alter_application_role
    : ALTER APPLICATION ROLE role_id WITH 
      (COMMA? NAME EQUAL new_role=role_id)? 
      (COMMA? password_setting)? 
      (COMMA? default_schema_set)?
    ;

create_application_role
    : CREATE APPLICATION ROLE role_id WITH (COMMA? password_setting)? (COMMA? default_schema_set)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-aggregate-transact-sql

drop_aggregate
    : DROP AGGREGATE ( IF EXISTS )? schema_aggregate_ref
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-application-role-transact-sql
drop_application_role
    : DROP APPLICATION ROLE role_id
    ;

alter_assembly
    : alter_assembly_start assembly_id alter_assembly_clause
    ;

alter_assembly_start
    :  ALTER ASSEMBLY
    ;

alter_assembly_clause
    : alter_assembly_from_clause? alter_assembly_with_clause? alter_assembly_drop_clause? alter_assembly_add_clause?
    ;

alter_assembly_from_clause
    : alter_assembly_from_clause_start (client_assembly_specifier | alter_assembly_file_bits )
    ;

alter_assembly_from_clause_start
    : FROM
    ;

alter_assembly_drop_clause
    : alter_assembly_drop alter_assembly_drop_multiple_files
    ;

alter_assembly_drop_multiple_files
    : ALL
    | multiple_local_files
    ;

alter_assembly_drop
    : DROP
    ;

alter_assembly_add_clause
    : ADD FILE FROM alter_assembly_client_file_clause
    ;

// need to implement
alter_assembly_client_file_clause
    : assembly_file_name (AS id_)?
    ;

assembly_file_name
    : stringtext
    ;

//need to implement
alter_assembly_file_bits
    : AS id_
    ;

alter_assembly_with_clause
    : WITH assembly_option
    ;

client_assembly_specifier
    : network_file_share
    | local_file
    | stringtext
    ;

assembly_option
    : PERMISSION_SET EQUAL assembly_permission
    | VISIBILITY EQUAL on_off
    | UNCHECKED DATA
    | assembly_option COMMA
    ;

network_file_share
    : network_file_start network_computer file_path
    ;

network_file_start
    : DOUBLE_BACK_SLASH
    ;

file_path
    : file_directory_path_separator file_path
    | id_
    ;

file_directory_path_separator
    : BACKSLASH
    ;

local_file
    : local_drive file_path
    ;

local_drive
    : DISK_DRIVE
    ;
multiple_local_files
    : SINGLE_QUOTE local_file SINGLE_QUOTE COMMA
    | local_file
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-assembly-transact-sql
create_assembly
    : CREATE ASSEMBLY assembly_id (AUTHORIZATION owner_id)?
      FROM binary_content_nexts
      (WITH PERMISSION_SET EQUAL assembly_permission )?
    ;

binary_content_nexts
    : binary_content_next+
    ;

binary_content_next
    : COMMA? binary_content
    ;

binary_content
    : stringtext
    | binary_
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-assembly-transact-sql
drop_assembly
    : DROP ASSEMBLY ( IF EXISTS )? assemblies ( WITH NO DEPENDENTS )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-asymmetric-key-transact-sql

alter_asymmetric_key
    : ALTER ASYMMETRIC KEY asym_key_id (asymmetric_key_option | REMOVE PRIVATE KEY )
    ;

by_password_crypt
    : decryption_by_pwd
    | encryption_by_pwd
    ;

encryption_by_pwd : ENCRYPTION BY password_setting;
decryption_by_pwd : DECRYPTION BY password_setting;


//https://docs.microsoft.com/en-us/sql/t-sql/statements/create-asymmetric-key-transact-sql

create_asymmetric_key
    : CREATE ASYMMETRIC KEY asym_key_id
       (AUTHORIZATION database_id)?
       (FROM asymetric_key_from)?
       (WITH asymetric_key_with_info)?
       encryption_by_pwd?
    ;

asymetric_key_with_info
    : ALGORITHM EQUAL asymetric_algorithm  
    | PROVIDER_KEY_NAME EQUAL provider_key_name=stringtext 
    | CREATION_DISPOSITION EQUAL creation_disposition  
    ;

asymetric_key_from 
    : FILE EQUAL stringtext 
    | EXECUTABLE_FILE EQUAL stringtext 
    | ASSEMBLY assembly_id 
    | PROVIDER provider_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-asymmetric-key-transact-sql
drop_asymmetric_key
    : DROP ASYMMETRIC KEY key_name=id_ ( REMOVE PROVIDER KEY )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-authorization-transact-sql

alter_authorization
    : ALTER AUTHORIZATION ON (class_type DOUBLE_COLON)? entity=entity_name TO authorization_grantee
    ;

authorization_grantee
    : principal_name=id_
    | SCHEMA OWNER
    ;

alter_authorization_for_sql_database
    : ALTER AUTHORIZATION ON (class_type_for_sql_database DOUBLE_COLON)? entity=entity_name TO authorization_grantee
    ;

alter_authorization_for_azure_dw
    : ALTER AUTHORIZATION ON (class_type_for_azure_dw DOUBLE_COLON)? entity=entity_name_for_azure_dw_ref TO authorization_grantee
    ;

alter_authorization_for_parallel_dw
    : ALTER AUTHORIZATION ON (class_type_for_parallel_dw DOUBLE_COLON)? entity=entity_name_for_parallel_dw_ref TO authorization_grantee
    ;


class_type
    : OBJECT
    | ASSEMBLY
    | ASYMMETRIC KEY
    | AVAILABILITY GROUP
    | CERTIFICATE
    | CONTRACT
    | TYPE
    | DATABASE
    | ENDPOINT
    | FULLTEXT CATALOG
    | FULLTEXT STOPLIST
    | MESSAGE TYPE
    | REMOTE SERVICE BINDING
    | ROLE
    | ROUTE
    | SCHEMA
    | SEARCH PROPERTY LIST
    | SERVER ROLE
    | SERVICE
    | SYMMETRIC KEY
    | XML SCHEMA COLLECTION
    ;

class_type_for_sql_database
    : OBJECT
    | ASSEMBLY
    | ASYMMETRIC KEY
    | CERTIFICATE
    | TYPE
    | DATABASE
    | FULLTEXT CATALOG
    | FULLTEXT STOPLIST
    | ROLE
    | SCHEMA
    | SEARCH PROPERTY LIST
    | SYMMETRIC KEY
    | XML SCHEMA COLLECTION
    ;

class_type_for_azure_dw
    : SCHEMA
    | OBJECT
    ;

class_type_for_parallel_dw
    : DATABASE
    | SCHEMA
    | OBJECT
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/grant-transact-sql?view=sql-server-ver15
// SELECT DISTINCT '| ' + CLASS_DESC
// FROM sys.dm_audit_actions
// ORDER BY 1

class_type_for_grant
    : COLUMN encryption_master KEY
    | NOTIFICATION database_object_server
    | object_type_for_grant    
    ; 

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-availability-group-transact-sql
drop_availability_group
    : DROP AVAILABILITY GROUP group_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-availability-group-transact-sql
alter_availability_group
    : alter_availability_group_start add_remove_database
    ;

alter_availability_group_start
    : ALTER AVAILABILITY GROUP group_id
    ;

add_remove_database
    : add_remove DATABASE database_id
    ;

alter_options_listener
    : add_listener
    | MODIFY LISTENER alter_listener
    | restart_listener
    ;

restart_listener
    : restart_remove LISTENER listener_name
    ;

alter_listener
    : ADD IP LR_BRACKET ip_listener RR_BRACKET 
    | PORT EQUAL decimal 
    ;

range_ip_v4 : left=ipv4 right=ipv4;
range_ip_comma_v4 : left=ipv4 COMMA right=ipv4;
ip_listener : range_ip_v4 | ipv6;
ip_listener_comma : range_ip_comma_v4 | ipv6;

add_listener
    : ADD LISTENER listener_name LR_BRACKET address_listener RR_BRACKET
    ;

address_listener 
    : listener_dhcp
    | WITH IP LR_BRACKET listener_ip_address 
    ;

listener_name : stringtext;

listener_dhcp 
    : WITH DHCP ON LR_BRACKET range_ip_v4 RR_BRACKET
    ;

listener_ip_address 
    : ips (COMMA port=PORT EQUAL port_number)?
    ;

ips : LR_BRACKET ip_listener_comma RR_BRACKET (COMMA LR_BRACKET ip_listener_comma RR_BRACKET)*;

alter_availability_replicat_modify
    : MODIFY REPLICA ON server_instance_txt 
    (
          WITH LR_BRACKET alter_availability_replicat_primary RR_BRACKET
        | SECONDARY_ROLE LR_BRACKET alter_availability_secondary_role RR_BRACKET
        | PRIMARY_ROLE LR_BRACKET alter_availability_primary_role RR_BRACKET
    )
    ;

alter_availability_replicat_primary
    : ENDPOINT_URL EQUAL url_value
    | availability_mode_set
    | FAILOVER_MODE EQUAL failover=auto_manual 
    | seeding_mode_set
    | backup_priority_set
    ;

url_value: stringtext;

alter_availability_primary_role
    : allow_connections_set
    | READ_ONLY_ROUTING_LIST EQUAL LR_BRACKET routing_list RR_BRACKET
    | SESSION_TIMEOUT EQUAL session_timeout=decimal
    ;
routing_list : string_list | NONE;

alter_availability_secondary_role
    : allow_connections_set
    | READ_ONLY_ROUTING_LIST EQUAL LR_BRACKET routingList=stringtext RR_BRACKET
    ;


backup_priority_set : BACKUP_PRIORITY EQUAL decimal ;

alter_availability_replicat_add
    : ADD REPLICA ON server_instance_txt WITH 
    LR_BRACKET 
        (ENDPOINT_URL EQUAL stringtext)?   
        (COMMA? availability_mode_set)?    
        (COMMA? FAILOVER_MODE EQUAL auto_manual )?  
        (COMMA? seeding_mode_set)?  
        (COMMA? backup_priority_set)?  
        (COMMA? PRIMARY_ROLE LR_BRACKET ALLOW_CONNECTIONS EQUAL real_write_all RR_BRACKET)?   
        (COMMA? SECONDARY_ROLE LR_BRACKET ALLOW_CONNECTIONS EQUAL READ_ONLY RR_BRACKET)? 
    RR_BRACKET
    ;

alter_availability_replicat
    : alter_availability_replicat_add
    | REMOVE REPLICA ON server_instance_txt
    | alter_availability_replicat_modify
    ;

availability_group_options
    : JOIN
    | JOIN AVAILABILITY GROUP ON 
        (
            COMMA? ag_name=stringtext WITH LR_BRACKET 
            ( 
                listener_url_set
                COMMA availability_mode_set
                COMMA failover_mode_manuel
                COMMA seeding_mode_set
            ) RR_BRACKET
        )+
    | MODIFY AVAILABILITY GROUP ON 
        (
            COMMA? ag_name_modified=stringtext WITH LR_BRACKET 
            (
                listener_url_set
                (COMMA? availability_mode_set)? 
                (COMMA? failover_mode_manuel)? 
                (COMMA? seeding_mode_set)?
            ) RR_BRACKET
        )+
    ;

listener_url_set : LISTENER_URL EQUAL url_value;
availability_mode_set : AVAILABILITY_MODE EQUAL synch_asynch;
failover_mode_manuel :  FAILOVER_MODE EQUAL MANUAL;
seeding_mode_set : SEEDING_MODE EQUAL auto_manual;

alter_role
    : secondary_role_args
    | primary_role_args 
    ;

primary_role_args
    : PRIMARY_ROLE LR_BRACKET primary_role_config RR_BRACKET
    ;

primary_role_config 
    : allow_connections_set
    | READ_ONLY_ROUTING_LIST EQUAL LR_BRACKET string_list_not RR_BRACKET
    | SESSION_TIMEOUT EQUAL session_timeout=decimal
    ;

string_list_not : string_list | NONE;

secondary_role_args
    : SECONDARY_ROLE LR_BRACKET secondary_role_config RR_BRACKET
    ;

secondary_role_config 
    : allow_connections_set
    | READ_ONLY_ROUTING_LIST EQUAL LR_BRACKET stringtext RR_BRACKET
    ;

allow_connections_set : ALLOW_CONNECTIONS EQUAL no_real_write_all;

alter_availability_group_options
    : SET LR_BRACKET alter_availability_group_option_set RR_BRACKET  
    | add_remove_database
    | alter_availability_replicat
    | alter_options_listener
    | alter_role
    | availability_group_options
    | grant_deny CREATE ANY DATABASE    
    | FAILOVER
    | FORCE_FAILOVER_ALLOW_DATA_LOSS
    | OFFLINE
    | WITH LR_BRACKET DTC_SUPPORT EQUAL PER_DB RR_BRACKET
    ;

alter_availability_group_option_set
    : AUTOMATED_BACKUP_PREFERENCE EQUAL primary_secondary_none  
    | FAILURE_CONDITION_LEVEL EQUAL decimal   
    | HEALTH_CHECK_TIMEOUT EQUAL milliseconds=decimal  
    | DB_FAILOVER EQUAL on_off 
    | REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT EQUAL decimal 
    ;

server_instance_txt : stringtext;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-broker-priority-transact-sql
// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-broker-priority-transact-sql
create_or_alter_broker_priority
    : create_alter BROKER PRIORITY ConversationPriorityName=id_ FOR CONVERSATION
      SET LR_BRACKET
        broker_contract_name?
        broker_local_service_name?
        broker_remote_service_name?
        broker_priority_level?
      RR_BRACKET
    ;

broker_contract_name : CONTRACT_NAME EQUAL id_any COMMA?;
broker_local_service_name : LOCAL_SERVICE_NAME EQUAL (DOUBLE_FORWARD_SLASH? id_ | ANY ) COMMA?;
broker_remote_service_name : REMOTE_SERVICE_NAME EQUAL stringtext_any COMMA?;
broker_priority_level : PRIORITY_LEVEL EQUAL decimal_default;

id_any : id_ | ANY;
stringtext_any : id_ | ANY;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-broker-priority-transact-sql
drop_broker_priority
    : DROP BROKER PRIORITY ConversationPriorityName=id_
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-certificate-transact-sql
alter_certificate
    : ALTER CERTIFICATE certificate_id 
    (
          REMOVE PRIVATE_KEY 
        | WITH PRIVATE KEY LR_BRACKET private_keys RR_BRACKET 
        | WITH ACTIVE FOR BEGIN_DIALOG EQUAL on_off 
    )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-column-encryption-key-transact-sql
alter_column_encryption_key
    : ALTER COLUMN ENCRYPTION KEY column_encryption_key_id add_drop VALUE LR_BRACKET COLUMN_MASTER_KEY EQUAL column_master_key_name=id_ 
      ( COMMA ALGORITHM EQUAL algorithm_name=stringtext  COMMA ENCRYPTED_VALUE EQUAL binary_)? RR_BRACKET
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-column-encryption-key-transact-sql
create_column_encryption_key
    :   CREATE COLUMN ENCRYPTION KEY column_encryption_key_id
         WITH VALUES
           (LR_BRACKET COMMA? COLUMN_MASTER_KEY EQUAL column_master_key_name=id_ COMMA
           ALGORITHM EQUAL algorithm_name=stringtext  COMMA
           ENCRYPTED_VALUE EQUAL encrypted_value=binary_ RR_BRACKET COMMA?)+
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-certificate-transact-sql
drop_certificate
    : DROP CERTIFICATE certificate_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-column-encryption-key-transact-sql
drop_column_encryption_key
    : DROP COLUMN ENCRYPTION KEY encryptor_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-column-master-key-transact-sql
drop_column_master_key
    : DROP COLUMN MASTER KEY master_key
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-contract-transact-sql
drop_contract
    : DROP CONTRACT dropped_contract_name=id_
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-credential-transact-sql
drop_credential
    : DROP CREDENTIAL credential_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-cryptographic-provider-transact-sql
drop_cryptograhic_provider
    : DROP CRYPTOGRAPHIC PROVIDER provider_id
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-database-transact-sql
drop_database
    : DROP DATABASE ( IF EXISTS )? (COMMA? database_id)+
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-database-audit-specification-transact-sql
drop_database_audit_specification
    : DROP DATABASE AUDIT SPECIFICATION audit_id
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-database-encryption-key-transact-sql?view=sql-server-ver15
drop_database_encryption_key
    : DROP DATABASE ENCRYPTION KEY
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-database-scoped-credential-transact-sql
drop_database_scoped_credential
   : DROP DATABASE SCOPED CREDENTIAL credential_id
   ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-default-transact-sql
drop_default
    : DROP DEFAULT ( IF EXISTS )? (COMMA? default_ref)
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-endpoint-transact-sql
drop_endpoint
    : DROP ENDPOINT endpoint_id
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-external-data-source-transact-sql
drop_external_data_source
    : DROP EXTERNAL DATA SOURCE external_data_source_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-external-file-format-transact-sql
drop_external_file_format
    : DROP EXTERNAL FILE FORMAT external_file_format_id
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-external-library-transact-sql
drop_external_library
    : DROP EXTERNAL LIBRARY library_id
( AUTHORIZATION owner_id )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-external-resource-pool-transact-sql
drop_external_resource_pool
    : DROP EXTERNAL RESOURCE POOL pool_id
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-external-table-transact-sql
drop_external_table
    : DROP EXTERNAL TABLE database_schema_table_ref 
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-event-notification-transact-sql
drop_event_notifications
    : DROP EVENT NOTIFICATION notification_ids
        ON event_notification_on
    ;

event_notification_on
    : server_database 
    | QUEUE queue_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-event-session-transact-sql
drop_event_session
    : DROP EVENT SESSION event_session_id
        ON SERVER
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-fulltext-catalog-transact-sql
drop_fulltext_catalog
    : DROP FULLTEXT CATALOG catalog_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-fulltext-index-transact-sql
drop_fulltext_index
    : DROP FULLTEXT INDEX ON database_schema_table_ref
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-fulltext-stoplist-transact-sql
drop_fulltext_stoplist
    : DROP FULLTEXT STOPLIST stoplist_id
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-login-transact-sql
drop_login
    : DROP LOGIN login_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-master-key-transact-sql
drop_master_key
    : DROP MASTER KEY
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-message-type-transact-sql
drop_message_type
    : DROP MESSAGE TYPE message_type_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-partition-function-transact-sql
drop_partition_function
    : DROP PARTITION FUNCTION partition_function_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-partition-scheme-transact-sql
drop_partition_scheme
    : DROP PARTITION SCHEME partition_scheme_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-queue-transact-sql
drop_queue
    : DROP QUEUE database_schema_queue_ref
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-remote-service-binding-transact-sql
drop_remote_service_binding
    : DROP REMOTE SERVICE BINDING binding_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-resource-pool-transact-sql
drop_resource_pool
    : DROP RESOURCE POOL pool_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-role-transact-sql
drop_db_role
    : DROP ROLE ( IF EXISTS )? role_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-route-transact-sql
drop_route
    : DROP ROUTE route_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-rule-transact-sql
drop_rule
    : DROP RULE ( IF EXISTS )? (COMMA? schema_rule_ref)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-schema-transact-sql
drop_schema
    :  DROP SCHEMA ( IF EXISTS )? schema_identifier
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-search-property-list-transact-sql
drop_search_property_list
    : DROP SEARCH PROPERTY LIST property_list_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-security-policy-transact-sql
drop_security_policy
    : DROP SECURITY POLICY ( IF EXISTS )? schema_security_policy_ref
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-sequence-transact-sql
drop_sequence
    : DROP SEQUENCE ( IF EXISTS )? ( COMMA? database_schema_sequence_ref)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-server-audit-transact-sql
drop_server_audit
    : DROP SERVER AUDIT audit_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-server-audit-specification-transact-sql
drop_server_audit_specification
    : DROP SERVER AUDIT SPECIFICATION audit_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-server-role-transact-sql
drop_server_role
    : DROP SERVER ROLE role_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-service-transact-sql
drop_service
    : DROP SERVICE service_id
    ;
// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-signature-transact-sql
drop_signature
    : DROP COUNTER? SIGNATURE FROM schema_module_ref
        BY drop_signature_bys
    ;

drop_signature_bys
    : drop_signature_by (COMMA drop_signature_by)*
    ;

drop_signature_by
    : CERTIFICATE certificate_id
    | ASYMMETRIC KEY asym_key_id
    ;

drop_statistics_id_azure_dw_and_pdw
    :  DROP STATISTICS schema_object_statistics_ref
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-symmetric-key-transact-sql
drop_symmetric_key
    : DROP SYMMETRIC KEY symmetric_key_id (REMOVE PROVIDER KEY)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-synonym-transact-sql
drop_synonym
    : DROP SYNONYM ( IF EXISTS )? schema_synonym_ref
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-user-transact-sql
drop_user
    : DROP USER ( IF EXISTS )? user_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-workload-group-transact-sql
drop_workload_group
    : DROP WORKLOAD GROUP group_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-xml-schema-collection-transact-sql
drop_xml_schema_collection
    : DROP XML SCHEMA COLLECTION schema_sql_identifier_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/disable-trigger-transact-sql
disable_trigger
    : DISABLE trigger_setting
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/enable-trigger-transact-sql
enable_trigger
    : ENABLE trigger_setting
    ;

trigger_setting : TRIGGER trigger_name ON trigger_target;

trigger_name : schema_trigger_refs | ALL;
trigger_target : schema_object_ref | all_server_database;

lock_table
    : LOCK TABLE full_table_ref IN share_exclusive MODE lock_table_delay?
    ;

lock_table_delay 
    : WAIT seconds=decimal 
    | NOWAIT
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/truncate-table-transact-sql
truncate_table
    : TRUNCATE TABLE full_table_ref
      ( WITH LR_BRACKET PARTITIONS LR_BRACKET decimal_range RR_BRACKET decimals?)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-column-master-key-transact-sql
create_column_master_key
    : CREATE COLUMN MASTER KEY key_name=id_
         WITH LR_BRACKET
            KEY_STORE_PROVIDER_NAME EQUAL  key_store_provider_name=stringtext COMMA
            KEY_PATH EQUAL key_path=stringtext
           RR_BRACKET
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-credential-transact-sql
alter_credential
    : ALTER CREDENTIAL credential_id
        WITH IDENTITY EQUAL identity_name=stringtext
         ( COMMA SECRET EQUAL secret=stringtext )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-credential-transact-sql
create_credential
    : CREATE CREDENTIAL credential_id
        WITH IDENTITY EQUAL identity_name=stringtext
         ( COMMA SECRET EQUAL secret=stringtext )?
         (  FOR CRYPTOGRAPHIC PROVIDER cryptographic_provider_id)?
    ;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-cryptographic-provider-transact-sql
alter_cryptographic_provider
    : ALTER CRYPTOGRAPHIC PROVIDER provider_id (FROM FILE EQUAL crypto_provider_ddl_file=stringtext)? enable_disable?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-cryptographic-provider-transact-sql
create_cryptographic_provider
    : CREATE CRYPTOGRAPHIC PROVIDER provider_id
      FROM FILE EQUAL path_of_DLL=stringtext
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-event-notification-transact-sql
create_event_notification
    : CREATE EVENT NOTIFICATION event_notification_id
      ON  event_notification_on
        (WITH FAN_IN)?
        FOR (COMMA? event_type_or_group_id)+
          TO SERVICE  broker_service=stringtext  COMMA
             broker_service_specifier_or_current_database=stringtext
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-event-session-transact-sql
// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-event-session-transact-sql
// todo: not implemented
create_or_alter_event_session
    : create_alter EVENT SESSION event_session_id ON SERVER 
      create_or_alter_event_session_add_events?
      create_or_alter_event_session_del_events?
      create_or_alter_event_session_add_targets?
      create_or_alter_event_session_del_targets?
      create_or_alter_event_session_with?
      (STATE EQUAL start_stop)?
    ;

create_or_alter_event_session_with
    : WITH
           LR_BRACKET
           (COMMA? session_arg_max_memory)?
           (COMMA? session_arg_event_retention_mode)?
           (COMMA? session_arg_max_dispatch)?
           (COMMA? session_arg_max_event_size)?
           (COMMA? session_arg_memory_partition)?
           (COMMA? session_arg_track_causality)?
           (COMMA? session_arg_startup_state)?
           RR_BRACKET     
    ;

session_arg_max_memory : MAX_MEMORY EQUAL decimal memory_size_unity;
session_arg_event_retention_mode : EVENT_RETENTION_MODE EQUAL session_mode;
session_arg_max_dispatch : MAX_DISPATCH_LATENCY EQUAL (decimal SECONDS | INFINITE);
session_arg_max_event_size : MAX_EVENT_SIZE EQUAL decimal memory_size_unity;
session_arg_memory_partition : MEMORY_PARTITION_MODE EQUAL partition_mode;
session_arg_track_causality : TRACK_CAUSALITY EQUAL on_off;
session_arg_startup_state : STARTUP_STATE EQUAL on_off;


create_or_alter_event_session_add_event
    : ADD EVENT module_package_event_ref
      (
        LR_BRACKET
        (SET set_attributes)?
        event_session_actions
        where_session_condition?
        RR_BRACKET 
      )      
      ;



set_attribute : event_customizable_attribute_id EQUAL decimal_string;
event_session_action : ACTION LR_BRACKET event_module_package_action_refs RR_BRACKET;

where_session_condition : WHERE event_session_predicate_expression;

create_or_alter_event_session_add_target
    : ADD TARGET module_package_event_ref target_parameter_blocks
    ;

target_parameter_blocks : target_parameter_block*;
target_parameter_block : LR_BRACKET SET target_parameter_sets RR_BRACKET;

target_parameter_sets : target_parameter_value target_parameter_ids?;

target_parameter_ids : target_parameter_id (COMMA target_parameter_id)*;


target_parameter_set : target_parameter_id EQUAL target_parameter_value;
target_parameter_value : (LR_BRACKET? decimal RR_BRACKET? | stringtext);

create_or_alter_event_session_del_target
    : DROP TARGET module_package_event_ref
    ;


create_or_alter_event_session_del_event
    : DROP EVENT module_package_event_ref
    ;

start_stop 
    : START
    | STOP
    ;

event_session_predicate_expression
    : ( COMMA? and_or? NOT? ( event_session_predicate_factor | LR_BRACKET event_session_predicate_expression RR_BRACKET) )+
    ;

event_session_predicate_factor
    : event_session_predicate_leaf
    | LR_BRACKET event_session_predicate_expression RR_BRACKET
    ;

event_session_predicate_leaf
    : event_field_id 
    | event_session_id_source1 event_session_predicate_leaf_ope decimal_string
    | source1=full_predicate_source_ref LR_BRACKET event_session_id_source2 RR_BRACKET
    ;

event_session_id_source1 : event_field_id | full_predicate_source_ref;
event_session_id_source2 : event_field_id | full_predicate_source_ref COMMA decimal_string;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-external-data-source-transact-sql
alter_external_data_source
    : ALTER EXTERNAL DATA SOURCE data_source_id SET external_sources
    | ALTER EXTERNAL DATA SOURCE data_source_id 
        WITH LR_BRACKET 
            TYPE EQUAL BLOB_STORAGE COMMA 
            LOCATION EQUAL location=stringtext 
            external_credential? 
            RR_BRACKET
    ;

external_credential : CREDENTIAL EQUAL credential_id;

external_sources : external_source (COMMA external_source)*;

external_source
    : LOCATION EQUAL location=stringtext
    | RESOURCE_MANAGER_LOCATION EQUAL resource_manager_location=stringtext
    | CREDENTIAL EQUAL credential_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-external-library-transact-sql
alter_external_library
    : ALTER EXTERNAL LIBRARY library_id (AUTHORIZATION owner_id)?
        SET file_spec2 external_lib_infos
    ;

external_lib_infos
    : WITH LR_BRACKET LANGUAGE EQUAL code_language RR_BRACKET
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-external-library-transact-sql
create_external_library
    : CREATE EXTERNAL LIBRARY library_id (AUTHORIZATION owner_id)?
        FROM 
        file_spec2
        external_lib_infos
    ;

file_spec2
    : LR_BRACKET 
      CONTENT EQUAL code_content 
      (COMMA PLATFORM EQUAL platform)? 
      RR_BRACKET
    ;

code_content : stringtext | binary_ | NONE;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-external-resource-pool-transact-sql
alter_external_resource_pool
    : ALTER EXTERNAL RESOURCE POOL (pool_id | DEFAULT_DOUBLE_QUOTE) 
      WITH external_resource_with
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-external-resource-pool-transact-sql
create_external_resource_pool
    : CREATE EXTERNAL RESOURCE POOL pool_id 
      WITH external_resource_with
    ;

external_resource_with
    : LR_BRACKET 
        max_cpu
        ( 
              COMMA? AFFINITY CPU EQUAL (AUTO | decimal_ranges) 
            | NUMANODE EQUAL decimal_ranges
        )
        (COMMA? max_memory_set)?
        (COMMA? max_process_set)?

        RR_BRACKET
    ;
max_process_set : MAX_PROCESSES EQUAL decimal;
max_memory_set : MAX_MEMORY_PERCENT EQUAL decimal;
max_cpu : MAX_CPU_PERCENT EQUAL decimal;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-fulltext-catalog-transact-sql
alter_fulltext_catalog
    : ALTER FULLTEXT CATALOG catalog_id (REBUILD (WITH ACCENT_SENSITIVITY EQUAL on_off )? | REORGANIZE | AS DEFAULT )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-fulltext-catalog-transact-sql
create_fulltext_catalog
    : CREATE FULLTEXT CATALOG catalog_id
        (ON FILEGROUP file_group_id)?
        (IN PATH rootpath=stringtext)?
        (WITH ACCENT_SENSITIVITY EQUAL on_off )?
        (AS DEFAULT)?
        (AUTHORIZATION owner_id)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-fulltext-stoplist-transact-sql
alter_fulltext_stoplist
    : ALTER FULLTEXT STOPLIST stoplist_id 
    (
          ADD    stopword=stringtext LANGUAGE fulltext_languageList 
        | DROP ( stopword=stringtext LANGUAGE fulltext_languageList 
                    | ALL fulltext_languageList 
                    | ALL 
               ) 
    );

fulltext_languageList : stringtext | decimal | binary_;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-fulltext-stoplist-transact-sql
create_fulltext_stoplist
    :   CREATE FULLTEXT STOPLIST stoplist_id
          (FROM ( database_stoplist_ref | SYSTEM STOPLIST ) )?
          (AUTHORIZATION owner_id)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-login-transact-sql
alter_login_sql_server
    : ALTER LOGIN login_id alter_login_sql_server_infos
    ;
alter_login_sql_server_infos
    : enable_disable?  
    | WITH alter_login_sql_server_settings
    | add_drop CREDENTIAL credential_id 
    ;

alter_login_sql_server_settings
    :  pwd_settings? 
       old_pwd_strategies? 
      (DEFAULT_DATABASE EQUAL database_id)? 
      default_language_set?  
      (NAME EQUAL login_id)? 
      (CHECK_POLICY EQUAL check_policy=on_off )? 
      (CHECK_EXPIRATION EQUAL check_expiration=on_off )? 
      (CREDENTIAL EQUAL credential_id)? 
      (NO CREDENTIAL)? 
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-login-transact-sql
create_login_sql_server
    : CREATE LOGIN login_id (WITH create_login_sql_server_settings | FROM create_login_sql_server_from)
    ;

create_login_sql_server_settings
    :  
    (PASSWORD EQUAL pwd_value pwd_strategies? )?
    (COMMA? SID EQUAL sid=binary_)?
    (COMMA? DEFAULT_DATABASE EQUAL database_id)?
    (COMMA? default_language_set)?
    (COMMA? CHECK_EXPIRATION EQUAL check_expiration=on_off )?
    (COMMA? CHECK_POLICY EQUAL check_policy=on_off )?
    (COMMA? CREDENTIAL EQUAL credential_id)?
    ;

create_login_sql_server_from
    :
      WINDOWS (WITH (COMMA? DEFAULT_DATABASE EQUAL database_id)? (COMMA? default_language_set)? )
    | CERTIFICATE certificate_id
    | ASYMMETRIC KEY asym_key_id
        
    ;

alter_login_azure_sql
    : ALTER LOGIN login_id alter_login_azure_sql_infos
    ;

alter_login_azure_sql_infos
    : enable_disable? 
    | WITH alter_login_azure_sql_with    
    ;

alter_login_azure_sql_with
    : password_setting old_pwd? 
    | NAME EQUAL login_id 
    ;

create_login_azure_sql
    : CREATE LOGIN login_id
       WITH password_setting (SID EQUAL sid=binary_)?
    ;

alter_login_azure_sql_dw_and_pdw
    : ALTER LOGIN login_id login_pwd_strategy
    ;

login_pwd_strategy 
    : enable_disable? 
    | WITH change_password
    ;

change_password 
    : password_setting old_pwd_strategies?  
    | NAME EQUAL login_id
    ;

create_login_pdw
    : CREATE LOGIN login_id login_pdw_pwd
    ;

login_pdw_pwd 
    : WITH
    ( password_setting MUST_CHANGE?
        (CHECK_POLICY EQUAL on_off? )?
    )
    | FROM WINDOWS
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-master-key-transact-sql
alter_master_key_sql_server
    : ALTER MASTER KEY 
    ( 
          regenerate_mater_key
        | add_drop add_master_key
    )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-master-key-transact-sql
create_master_key_sql_server
    : CREATE MASTER KEY encryption_by_pwd
    ;

alter_master_key_azure_sql
    : ALTER MASTER KEY 
    (
          regenerate_mater_key 
        | ADD add_master_key
        | DROP encryption_by_pwd
    )
    ;

regenerate_mater_key : (FORCE)? REGENERATE WITH encryption_by_pwd;

add_master_key 
    : ENCRYPTION BY 
    (
          SERVICE MASTER KEY 
        | password_setting
    ) 
    ; 
create_master_key_azure_sql
    : CREATE MASTER KEY encryption_by_pwd?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-message-type-transact-sql
alter_message_type
    : ALTER MESSAGE TYPE message_type_id VALIDATION EQUAL message_validation_value
    ;

message_validation_value
    : message_validation_value_enum
    | VALID_XML WITH SCHEMA COLLECTION  schema_collection_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-partition-function-transact-sql
alter_partition_function
    : ALTER PARTITION FUNCTION partition_function_id LR_BRACKET RR_BRACKET split_or_merge RANGE LR_BRACKET decimal RR_BRACKET
    ;

split_or_merge
    : SPLIT
    | MERGE
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-partition-scheme-transact-sql
alter_partition_scheme
    : ALTER PARTITION SCHEME partition_scheme_id NEXT USED (file_group_id)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-remote-service-binding-transact-sql
alter_remote_service_binding
    : ALTER REMOTE SERVICE BINDING binding_id
        WITH (USER EQUAL user_id)?
             (COMMA ANONYMOUS EQUAL on_off )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-remote-service-binding-transact-sql
create_remote_service_binding
    : CREATE REMOTE SERVICE BINDING binding_id
         (AUTHORIZATION owner_id)?
         TO SERVICE remote_service_name=stringtext
         WITH (USER EQUAL user_id)?
              (COMMA ANONYMOUS EQUAL on_off )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-resource-pool-transact-sql
create_resource_pool
    : CREATE RESOURCE POOL pool_id create_resource_pool_infos?
    ;

create_resource_pool_infos 
    : WITH
      LR_BRACKET
        (COMMA? MIN_CPU_PERCENT EQUAL min_cpu_percent=decimal)?
        (COMMA? MAX_CPU_PERCENT EQUAL max_cpu_percent=decimal)?
        (COMMA? CAP_CPU_PERCENT EQUAL cap_cpu_percent=decimal)?
        (COMMA? AFFINITY SCHEDULER EQUAL resource_affinity_scheduler_value)?
        (COMMA? MIN_MEMORY_PERCENT EQUAL  min_memory_percent=decimal)?
        (COMMA? MAX_MEMORY_PERCENT EQUAL  max_memory_percent=decimal)?
        (COMMA? MIN_IOPS_PER_VOLUME EQUAL min_tops_percent=decimal)?
        (COMMA? MAX_IOPS_PER_VOLUME EQUAL max_tops_percent=decimal)?
      RR_BRACKET
    ;

resource_affinity_scheduler_value 
    : AUTO
    | LR_BRACKET decimal_ranges RR_BRACKET
    | NUMANODE EQUAL LR_BRACKET decimal_ranges RR_BRACKET
    ; 
decimal_range
    : dec_start=decimal 
    | dec_start=decimal TO dec_end=decimal
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-resource-governor-transact-sql
alter_resource_governor
    : ALTER RESOURCE GOVERNOR 
    (     disable_reconfigure 
        | WITH LR_BRACKET CLASSIFIER_FUNCTION EQUAL ( schema_func_proc_ref | NULL_ ) RR_BRACKET 
        | RESET STATISTICS 
        | WITH LR_BRACKET MAX_OUTSTANDING_IO_PER_VOLUME EQUAL max_outstanding_io_per_volume=decimal RR_BRACKET 
    )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-role-transact-sql
alter_db_role
    : ALTER ROLE old_role_name=role_id
        ( add_drop MEMBER database_id
        | WITH NAME EQUAL new_role_name=role_id )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-role-transact-sql
create_db_role
    : CREATE ROLE role_id (AUTHORIZATION owner_id)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-route-transact-sql
create_route
    : CREATE ROUTE route_id
        (AUTHORIZATION owner_id)?
        WITH
          (COMMA? SERVICE_NAME EQUAL route_service_name=stringtext)?
          (COMMA? BROKER_INSTANCE EQUAL broker_instance_identifier=stringtext)?
          (COMMA? LIFETIME EQUAL lifetime=decimal)?
           COMMA? ADDRESS EQUAL address=stringtext
          (COMMA MIRROR_ADDRESS EQUAL mirror_address=stringtext )?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-rule-transact-sql
create_rule
    : CREATE RULE schema_rule_ref
        AS search_condition
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-schema-transact-sql
alter_schema_sql
    : ALTER SCHEMA schema_identifier TRANSFER transfert_target? id_dot_id
    ;

id_dot_id : id_ (DOT id_)?;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-schema-transact-sql
create_schema
    : CREATE SCHEMA create_schema_name create_schema_targets?
    ;

create_schema_name
    : schema_identifier schema_authorization?
    | schema_identifier? schema_authorization
    ;

create_schema_targets : create_schema_target+;

create_schema_target
    : create_table
    | create_view
    | grant_deny enum_dml ON (SCHEMA DOUBLE_COLON)? object_identifier TO owner_id
    | REVOKE enum_dml ON (SCHEMA DOUBLE_COLON)? object_identifier FROM owner_id
    ;

schema_authorization : AUTHORIZATION owner_id;

enum_dml 
    : SELECT
    | INSERT
    | DELETE
    | UPDATE;

create_schema_azure_sql_dw_and_pdw
    : CREATE SCHEMA schema_identifier (AUTHORIZATION owner_id )?
    ;

alter_schema_azure_sql_dw_and_pdw
    : ALTER SCHEMA schema_identifier TRANSFER (OBJECT DOUBLE_COLON )? id_dot_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-search-property-list-transact-sql
create_search_property_list
    : CREATE SEARCH PROPERTY LIST source_list_id
        (FROM database_source_list_ref)?
        (AUTHORIZATION owner_id)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-security-policy-transact-sql
create_security_policy
    : CREATE SECURITY POLICY schema_security_policy_ref
        create_security_policy_adds
        (WITH LR_BRACKET STATE EQUAL on_off schema_binding? RR_BRACKET)?
        (NOT FOR REPLICATION)?
    ;

schema_binding : SCHEMABINDING on_off;

create_security_policy_add 
    : ADD filter_block? PREDICATE schema_security_predicate_function_id
      LR_BRACKET column_or_argument_ids RR_BRACKET
      ON database_schema_table_ref schema_table_ref_impacts?
    ;

schema_table_ref_impact
    : AFTER  insert_update
    | BEFORE update_delate
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-sequence-transact-sql
alter_sequence
    : ALTER SEQUENCE schema_sequence_ref 
      alter_sequence_restart? 
      alter_sequence_increment? 
      alter_sequence_min_value? 
      alter_sequence_max_value sequence_cycle? 
      sequence_cache?
    ;

alter_sequence_restart
    : RESTART (WITH decimal)? 
    ;

alter_sequence_increment
    : INCREMENT BY sequnce_increment=decimal 
    ;
sequence_cache
    : (CACHE decimal | NO CACHE)
    ;

alter_sequence_max_value
    : (MAXVALUE decimal | NO MAXVALUE)?
    ;

alter_sequence_min_value
    : MINVALUE decimal 
    | NO MINVALUE
    ;

max_value_decimal
    : MAXVALUE decimal
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-sequence-transact-sql
create_sequence
    : CREATE SEQUENCE schema_sequence_ref (AS data_type)?
        create_sequence_start?
        create_sequence_increment?
        create_sequence_min_value?
        create_sequence_max_value?
        sequence_cycle?
        sequence_cache?
    ;

create_sequence_increment
    : INCREMENT BY real
    ;

create_sequence_min_value
    : MINVALUE real? 
    | NO MINVALUE
    ;

create_sequence_max_value
    : MAXVALUE real? 
    | NO MAXVALUE
    ;

real : sign? decimal;

create_sequence_start : START WITH decimal;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-server-audit-transact-sql
alter_server_audit
    : ALTER SERVER AUDIT audit_id
        alter_server_audit_infos
    ;

alter_server_audit_infos
    : ( TO server_audit_file )?
      with_server_audit_file?
      where_server_audit_condition?
    | REMOVE WHERE
    | MODIFY NAME EQUAL audit_id
    ;

with_server_audit_file : WITH LR_BRACKET server_audit_file_infos? RR_BRACKET;

server_audit_file_info
    : QUEUE_DELAY EQUAL queue_delay=decimal
    | ON_FAILURE EQUAL continue_shutdown
    | STATE EQUAL on_off 
    ;

server_audit_file
    :   FILE LR_BRACKET server_audit_file_specs? RR_BRACKET 
      | APPLICATION_LOG
      | SECURITY_LOG
    ;

server_audit_file_spec
    : filepath_set
    | audit_maxsize
    | max_rollover_files_set
    | max_files_set
    | disk_space_set
    ;

alter_server_audit_condition
    : 
      COMMA? (NOT?) event_field_id audit_operator decimal_string
    | COMMA? and_or NOT? audit_operator decimal_string
                  
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-server-audit-transact-sql
create_server_audit
    : CREATE SERVER AUDIT audit_id create_server_audit_to_infos
    ;

create_server_audit_to_infos
    : (TO server_audit_file )? (WITH LR_BRACKET create_server_audit_withs? RR_BRACKET)? where_server_audit_condition?
    | REMOVE WHERE
    | MODIFY NAME EQUAL audit_id
    ;

where_server_audit_condition : WHERE alter_server_audit_condition?;

create_server_audit_with
    : QUEUE_DELAY EQUAL queue_delay=decimal
    | ON_FAILURE EQUAL continue_shutdown
    | STATE EQUAL state=on_off
    | audit1=audit_guid_id EQUAL audit2=audit_guid_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-server-audit-specification-transact-sql

alter_server_audit_specification
    : ALTER SERVER AUDIT SPECIFICATION audit_id
     alter_server_audit_specification_server?
     add_drop_audit_action_groups?
     server_audit_state?
    ;

server_audit_state : WITH LR_BRACKET STATE EQUAL on_off RR_BRACKET;
add_drop_audit_action_groups : add_drop_audit_action_group add_drop_audit_action_group+;
add_drop_audit_action_group : add_drop LR_BRACKET  audit_action_group_id RR_BRACKET;

alter_server_audit_specification_server : FOR SERVER AUDIT audit_id;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-server-audit-specification-transact-sql
create_server_audit_specification
    : CREATE SERVER AUDIT SPECIFICATION audit_id
      alter_server_audit_specification_server?
      server_audit_specification_add_groups?
      server_audit_state?
    ;

server_audit_specification_add_group : ADD LR_BRACKET  audit_action_group_id RR_BRACKET;
server_audit_specification_add_groups : server_audit_specification_add_group server_audit_specification_add_group+;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-server-configuration-transact-sql

alter_server_configuration
    : ALTER SERVER CONFIGURATION
      SET  
      ( 
          server_config_process_affinity 
        | server_config_diagnostic_log
        | server_config_failover
        | server_config_hadr_set
        | server_config_buffer_pool_ext
        | SET SOFTNUMA on_off 
      )
    ;

server_config_process_affinity
    : PROCESS AFFINITY 
    (
          process_cpu_set
        | process_numanode_set
    )
    ;

server_config_diagnostic_log
    : DIAGNOSTICS LOG 
    (
          on_off
        | diagnos_path_set
        | diagnos_max_size_set
        | diagnos_max_files_set
    )
    ;

server_config_hadr_set : HADR CLUSTER CONTEXT EQUAL (stringtext | LOCAL) ;
process_numanode_set : NUMANODE EQUAL decimal_range decimal_ranges;
process_cpu_set : CPU EQUAL (AUTO | decimal_range decimal_ranges );
diagnos_path_set : PATH EQUAL string_or_default;
diagnos_max_size_set :  MAX_SIZE EQUAL size_value;
diagnos_max_files_set :  MAX_FILES EQUAL decimal_default;
audit_maxsize : MAXSIZE EQUAL decimal_size_unlimited;
filepath_set : FILEPATH EQUAL stringtext;
max_rollover_files_set : MAX_ROLLOVER_FILES EQUAL decimal_unlimited;
max_files_set : MAX_FILES EQUAL decimal;
disk_space_set : RESERVE_DISK_SPACE EQUAL on_off ;
newname_set : NEWNAME EQUAL file_group_id | stringtext;
name_set : NAME EQUAL id_or_string;
filename_set : FILENAME EQUAL file = stringtext;
size_set : SIZE EQUAL size=file_size;
maxsize_set : MAXSIZE EQUAL max_file_size_value;
max_file_size_value : file_size | UNLIMITED;
filegrowth_set : FILEGROWTH EQUAL file_size;
decimal_unlimited : decimal | UNLIMITED;
decimal_size_unlimited : decimal size_unity | UNLIMITED;


server_config_failover
    : FAILOVER CLUSTER PROPERTY 
    (
          VERBOSELOGGING EQUAL verboselogging=string_or_default
        | SQLDUMPERFLAGS EQUAL sqldumperflags=string_or_default
        | SQLDUMPERPATH EQUAL sqldumperpath=string_or_default
        | SQLDUMPERTIMEOUT sqldumpertimeout=string_or_default
        | FAILURECONDITIONLEVEL EQUAL failure=string_or_default 
        | HEALTHCHECKTIMEOUT EQUAL health=decimal_default
    )
    ;

server_config_buffer_pool_ext
    : BUFFER POOL EXTENSION 
    (
          ON LR_BRACKET filename_set COMMA SIZE EQUAL size=decimal size_unity RR_BRACKET 
        | OFF 
    ) 
    ;

string_or_default 
    : stringtext
    | DEFAULT;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-server-role-transact-sql
alter_server_role
    : ALTER SERVER ROLE server_role_id
      ( add_drop MEMBER server_id
      | alter_server_role_new_name
      )
    ;

alter_server_role_new_name : WITH NAME EQUAL server_role_id;


// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-server-role-transact-sql
create_server_role
    : CREATE SERVER ROLE server_role_id (AUTHORIZATION server_id)?
    ;

alter_server_role_pdw
    : ALTER SERVER ROLE server_role_id add_drop MEMBER login_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-service-transact-sql
alter_service
    : ALTER SERVICE service_id (ON QUEUE schema_queue_ref)? alter_service_contracts?
    ;

alter_service_contract : add_drop contract_id;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-service-transact-sql
create_service
    : CREATE SERVICE service_id
        (AUTHORIZATION owner_id)?
        ON QUEUE schema_queue_ref
        ( LR_BRACKET contract_refs RR_BRACKET )?
    ;

contract_ref : contract_id | DEFAULT;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-service-master-key-transact-sql
alter_service_master_key
    : ALTER SERVICE MASTER KEY service_master_key_items
    ;

service_master_key_items
    : FORCE? REGENERATE 
    | WITH regenerate_account
    ;

regenerate_account 
    : OLD_ACCOUNT EQUAL acold_account_name=stringtext COMMA old_pwd 
    | NEW_ACCOUNT EQUAL new_account_name=stringtext COMMA new_password_set
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-symmetric-key-transact-sql

alter_symmetric_key
    : ALTER SYMMETRIC KEY symmetric_key_id 
    (
        add_drop ENCRYPTION BY 
        (
              CERTIFICATE certificate_id 
            | password_setting
            | SYMMETRIC KEY newkey=symmetric_key_id 
            | ASYMMETRIC KEY asym_key_id  
        ) 
    )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-synonym-transact-sql
create_synonym
    : CREATE SYNONYM schema_synonym_ref
        FOR //( 
              server_database_schema_object_ref
            // | (database_id DOT)? (schema_id_2_or_object_name=id_ DOT)?
            //)
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-user-transact-sql
alter_user
    : ALTER USER user_id WITH alter_user_items
    ;

alter_user_item
    : NAME EQUAL user_id 
    | DEFAULT_SCHEMA EQUAL schema_id_null
    | LOGIN EQUAL login_id 
    | password_setting old_pwd+ 
    | default_language_set
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS EQUAL on_off
    ;

schema_id_null : schema_identifier | NULL_ ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-user-transact-sql
create_user
    : CREATE USER user_id create_user_with_login
    | CREATE USER create_user_windows_principal_id
    | CREATE USER user_id create_user_without_login?
    ;

create_user_with_login
    : ( for_from LOGIN login_id )? ( WITH user_settings_shorts)?
    ;

create_user_without_login
    : WITHOUT LOGIN user_settings_shorts?
    | for_from CERTIFICATE certificate_id
    | for_from ASYMMETRIC KEY asym_key_id     
    ;

create_user_windows_principal_id
    : windows_principal_id (WITH user_settings)?
    | user_id WITH password_setting user_settings?
    | user_id FROM EXTERNAL PROVIDER
    ;

old_pwd_strategies : old_pwd pwd_strategies?;
pwd_settings : PASSWORD EQUAL pwd_value pwd_strategies?;

password_setting : PASSWORD EQUAL pwd;
new_password_set : NEW_PASSWORD EQUAL pwd;
old_pwd : OLD_PASSWORD EQUAL pwd;

pwd_value : pwd | binary_ HASHED;
pwd : stringtext;

user_settings_shorts : user_settings_short (COMMA user_settings_short)+;

user_settings_short
    : default_schema_set
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS EQUAL on_off    
    ;

user_settings : user_setting (COMMA user_setting)+;


default_schema_set : DEFAULT_SCHEMA EQUAL schema_identifier;

user_setting
    : default_schema_set
    | default_language_set
    | SID EQUAL binary_
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS EQUAL on_off                    
    ;

create_user_azure_sql_dw
    : CREATE USER user_id
        user_strategy?
        ( WITH default_schema_set)?

    | CREATE USER user_id
        FROM EXTERNAL PROVIDER
        ( WITH default_schema_set)?
    ;

user_strategy
    : for_from LOGIN login_id
    | WITHOUT LOGIN
    ;

alter_user_azure_sql
    : ALTER USER user_id WITH alter_user_azure_sql_infos
    ;

alter_user_azure_sql_infos : alter_user_azure_sql_info (COMMA alter_user_azure_sql_info)*;

alter_user_azure_sql_info
    : NAME EQUAL user_id 
    | default_schema_set
    | LOGIN EQUAL login_id  
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS EQUAL on_off 
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-workload-group-transact-sql

alter_workload_group
    : ALTER WORKLOAD GROUP workload_group_name
        workload_option?
        alter_workload_group_using?
    ;

workload_option
    : WITH LR_BRACKET workload_option_item RR_BRACKET 
    ;

workload_group_name : workload_group_group_id | DEFAULT_DOUBLE_QUOTE;

alter_workload_group_using : USING (workload_group_pool_id | DEFAULT_DOUBLE_QUOTE);

workload_option_item
    : (IMPORTANCE EQUAL importance_level)?
      (COMMA? REQUEST_MAX_MEMORY_GRANT_PERCENT EQUAL request_max_memory_grant=decimal)?
      (COMMA? REQUEST_MAX_CPU_TIME_SEC EQUAL request_max_cpu_time_sec=decimal)?
      (COMMA? REQUEST_MEMORY_GRANT_TIMEOUT_SEC EQUAL request_memory_grant_timeout_sec=decimal)?
      (COMMA? MAX_DOP EQUAL max_dop=decimal)?
      (COMMA? GROUP_MAX_REQUESTS EQUAL group_max_requests=decimal)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-workload-group-transact-sql
create_workload_group
    : CREATE WORKLOAD GROUP workload_group_group_id workload_option?
     (USING workload_group_id_or_defaults)?
    ;

workload_group_id_or_defaults : workload_group_id_or_default (COMMA workload_group_id_or_default)*;

workload_group_id_or_default : workload_group_pool_id | DEFAULT_DOUBLE_QUOTE;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-xml-schema-collection-transact-sql
create_xml_schema_collection
    : CREATE XML SCHEMA COLLECTION schema_sql_identifier_id AS string_id2
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-partition-function-transact-sql?view=sql-server-ver15
create_partition_function
    : CREATE PARTITION FUNCTION partition_function_id LR_BRACKET input_parameter_type=data_type RR_BRACKET
      AS RANGE left_right?
      FOR VALUES LR_BRACKET boundary_values=expression_list RR_BRACKET
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-partition-scheme-transact-sql?view=sql-server-ver15
create_partition_scheme
    : CREATE PARTITION SCHEME partition_scheme_id
      AS PARTITION partition_function_id
      ALL? TO LR_BRACKET file_group_ids RR_BRACKET
    ;

create_queue
    : CREATE QUEUE table_or_queue queue_settings?
      (ON file_group_id | DEFAULT)?
    ;


queue_settings
    : WITH
       (STATUS EQUAL status=on_off COMMA?)?
       (RETENTION EQUAL retention=on_off COMMA?)?
       (ACTIVATION LR_BRACKET
           (
             (
              (STATUS EQUAL activation_status=on_off COMMA? )?
              (PROCEDURE_NAME EQUAL func_proc_name_database_schema_ref COMMA?)?
              (MAX_QUEUE_READERS EQUAL max_readers=decimal COMMA?)?
              (EXECUTE AS (SELF | username=stringtext | OWNER) COMMA?)?
             )
             | DROP
           )
         RR_BRACKET COMMA?
       )?
       (POISON_MESSAGE_HANDLING LR_BRACKET (STATUS EQUAL on_off) RR_BRACKET)?
    ;

alter_queue
    : ALTER QUEUE table_or_queue (queue_settings | queue_action)
    ;

table_or_queue : complete_table_ref | queue_id;

queue_action
    : REBUILD ( WITH LR_BRACKET queue_rebuild_options RR_BRACKET)?
    | REORGANIZE (WITH LOB_COMPACTION EQUAL on_off)?
    | MOVE TO id1=id_default id2=id_default
    ;

id_default : id_ | DEFAULT;

queue_rebuild_options
    : MAXDOP EQUAL decimal
    ;

create_contract
    : CREATE CONTRACT contract_name_expression
      (AUTHORIZATION owner_id)?
      LR_BRACKET contract_items RR_BRACKET
    ;

contract_item 
    : contract_item_target SENT BY init_target_any
    ;

contract_item_target : message_type_id | DEFAULT;

message_statement
    : CREATE MESSAGE TYPE message_type_id
      (AUTHORIZATION owner_id)?
      (VALIDATION EQUAL message_validation_value )
    ;

// DML

// https://docs.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql
// note that there's a limit on number of when_matches but it has to be done runtime due to different ordering of statements allowed
merge_statement
    : with_expression?
      MERGE (TOP LR_BRACKET expression RR_BRACKET PERCENT?)?
      INTO? ddl_object with_table_hints? as_table_alias?
      USING table_sources
      ON search_condition
      when_matches
      output_clause?
      update_option_clause? SEMI
    ;

when_matches : when_matche+;

when_matche
    : (WHEN MATCHED (AND search_condition)? THEN merge_matched)+
    | (WHEN NOT MATCHED (BY TARGET)? (AND search_condition)? THEN merge_not_matched)
    | (WHEN NOT MATCHED BY SOURCE (AND search_condition)? THEN merge_matched)+
    ;

merge_matched
    : UPDATE SET update_elem_merges
    | DELETE
    ;

merge_not_matched
    : INSERT (LR_BRACKET column_name_list RR_BRACKET)?
      (table_value_constructor | DEFAULT VALUES)
    ;

// https://msdn.microsoft.com/en-us/library/ms189835.aspx
delete_statement
    : with_expression?
      DELETE (TOP LR_BRACKET expression RR_BRACKET PERCENT? | TOP decimal)?
      FROM? delete_statement_from
      with_table_hints?
      output_clause?
      (FROM table_sources)?
      (WHERE (search_condition | CURRENT OF (GLOBAL? cursor_name | cursor_var=local_id)))?
      for_clause? update_option_clause?
    ;

delete_statement_from
    : ddl_object
    | rowset_function_limited
    | table_var=local_id
    ;

// https://msdn.microsoft.com/en-us/library/ms174335.aspx
insert_statement
    : with_expression?
      INSERT (TOP LR_BRACKET expression RR_BRACKET PERCENT?)?
      INTO? (ddl_object | rowset_function_limited)
      with_table_hints?
      (LR_BRACKET insert_column_name_list RR_BRACKET)?
      output_clause?
      insert_statement_value
      for_clause? update_option_clause?
    ;

insert_statement_value
    : table_value_constructor
    | derived_table
    | execute_statement
    | DEFAULT VALUES
    ;

receive_statement
    : LR_BRACKET? RECEIVE receive_mode
      receive_ids? FROM complete_table_ref
      receive_into? 
      RR_BRACKET?
    ;

receive_into : INTO table_id where_condition;

where_condition : WHERE search_condition;

receive_id : local_id EQUAL expression;
receive_mode : receive_mode_enum | top_clause;

// https://msdn.microsoft.com/en-us/library/ms189499.aspx
select_statement_standalone
    : with_expression? select_statement
    ;

select_statement
    : query_expression select_order_by_clause? for_clause? update_option_clause? SEMI?
    ;

timespan
    : (local_id | constant)
    ;

// https://msdn.microsoft.com/en-us/library/ms177523.aspx
update_statement
    : with_expression?
      UPDATE (TOP LR_BRACKET expression RR_BRACKET PERCENT?)?
      (ddl_object | rowset_function_limited)
      with_table_hints?
      SET update_elems
      output_clause?
      (FROM table_sources)?
      (WHERE (search_condition | CURRENT OF (GLOBAL? cursor_name | cursor_var=local_id)))?
      for_clause? update_option_clause?
    ;


// https://msdn.microsoft.com/en-us/library/ms177564.aspx
output_clause
    : OUTPUT output_dml_list_elems
      (INTO (local_id | full_table_ref) (LR_BRACKET column_name_list RR_BRACKET)? )?
    ;

output_dml_list_elem
    : (expression | asterisk) as_column_alias?
    ;

// DDL

// https://msdn.microsoft.com/en-ie/library/ms176061.aspx
create_database
    : CREATE DATABASE database_id
        containment_set?
        database_on_primary?
        database_on_log?
        collate_set?
        database_with_option?
    ;

database_with_option : WITH create_database_option_list;
database_on_primary : ON PRIMARY? database_files;
database_on_log : LOG ON database_files;
collate_set : COLLATE collation_id;

// https://msdn.microsoft.com/en-us/library/ms188783.aspx
create_index
    : CREATE UNIQUE? clustered? INDEX index_id ON full_table_ref LR_BRACKET column_name_list_with_order RR_BRACKET
    (INCLUDE LR_BRACKET column_name_list RR_BRACKET )?
    where_condition?
    (create_index_options)?
    (ON file_group_id)?
    ;

relational_index_option
    : rebuild_index_option
    | DROP_EXISTING EQUAL on_off
    | OPTIMIZE_FOR_SEQUENTIAL_KEY EQUAL on_off
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-index-transact-sql
alter_index
    : ALTER INDEX index_name ON full_table_ref index_status
    ;

index_status 
    : index_status_enum 
    | RESUME resumable_index_options? 
    | reorganize_partition 
    | set_index_options 
    | rebuild_partition
    ;

index_status_enum 
    : DISABLE 
    | PAUSE 
    | ABORT 
    ;

index_name : (id_ | ALL);

resumable_index_option
    : MAXDOP EQUAL max_degree_of_parallelism=decimal
    | MAX_DURATION EQUAL max_duration=decimal MINUTES?
    | low_priority_lock_wait
    ;

reorganize_partition
    : REORGANIZE (PARTITION EQUAL decimal)? reorganize_options?
    ;

reorganize_option
    : LOB_COMPACTION EQUAL on_off
    | COMPRESS_ALL_ROW_GROUPS EQUAL on_off
    ;

set_index_option
    : ALLOW_ROW_LOCKS EQUAL on_off
    | ALLOW_PAGE_LOCKS EQUAL on_off
    | OPTIMIZE_FOR_SEQUENTIAL_KEY EQUAL on_off
    | IGNORE_DUP_KEY EQUAL on_off
    | STATISTICS_NORECOMPUTE EQUAL on_off
    | COMPRESSION_DELAY EQUAL delay=decimal MINUTES?
    ;

rebuild_partition
    : REBUILD (PARTITION EQUAL ALL)? rebuild_index_options?
    | REBUILD PARTITION EQUAL decimal single_partition_rebuild_index_options?
    ;

rebuild_index_option
    : PAD_INDEX EQUAL on_off
    | FILLFACTOR EQUAL decimal
    | SORT_IN_TEMPDB EQUAL on_off
    | IGNORE_DUP_KEY EQUAL on_off
    | STATISTICS_NORECOMPUTE EQUAL on_off
    | STATISTICS_INCREMENTAL EQUAL on_off
    | ONLINE EQUAL online_value
    | RESUMABLE EQUAL on_off
    | MAX_DURATION EQUAL times=decimal MINUTES?
    | ALLOW_ROW_LOCKS EQUAL on_off
    | ALLOW_PAGE_LOCKS EQUAL on_off
    | MAXDOP EQUAL max_degree_of_parallelism=decimal
    | DATA_COMPRESSION EQUAL datacompression_mode on_partitions?
    | XML_COMPRESSION EQUAL on_off on_partitions?
    ;

online_value 
    : ON (LR_BRACKET low_priority_lock_wait RR_BRACKET)? 
    | OFF
    ;


single_partition_rebuild_index_option
    : SORT_IN_TEMPDB EQUAL on_off
    | MAXDOP EQUAL max_degree_of_parallelism=decimal
    | RESUMABLE EQUAL on_off
    | DATA_COMPRESSION EQUAL datacompression_mode on_partitions?
    | XML_COMPRESSION EQUAL on_off on_partitions?
    | ONLINE EQUAL online_value
    ;

on_partitions
    : ON PARTITIONS LR_BRACKET
        partition_nums ( COMMA partition_nums )*
    RR_BRACKET
    ;

partition_nums : partition_number=decimal ( TO to_partition_number=decimal )?;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-columnstore-index-transact-sql?view=sql-server-ver15
create_columnstore_index
    : CREATE CLUSTERED COLUMNSTORE INDEX id_ ON full_table_ref
    create_columnstore_index_options?
    (ON table_id)?
    ;

columnstore_index_option
    :
      DROP_EXISTING EQUAL drop_existing=on_off
    | MAXDOP EQUAL max_degree_of_parallelism=decimal
    | ONLINE EQUAL online=on_off
    | COMPRESSION_DELAY EQUAL delay=decimal MINUTES?
    | DATA_COMPRESSION EQUAL datacompression_column_mode
        on_partitions?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-columnstore-index-transact-sql?view=sql-server-ver15
create_nonclustered_columnstore_index
    : CREATE NONCLUSTERED? COLUMNSTORE INDEX id_ ON full_table_ref LR_BRACKET column_name_list_with_order RR_BRACKET
    where_condition?
    create_columnstore_index_options?
    (ON group_id)?
    ;

create_xml_index
    : CREATE PRIMARY? XML INDEX index_id ON full_table_ref LR_BRACKET column_id RR_BRACKET
    using_xml_index? xml_index_options?
    ;

using_xml_index : USING XML INDEX index_id index_using_xml_mode?;

xml_index_option
    : PAD_INDEX EQUAL pad_index=on_off
    | FILLFACTOR EQUAL fillfactor=decimal
    | SORT_IN_TEMPDB EQUAL sort_in_tempdb=on_off
    | IGNORE_DUP_KEY EQUAL ignore_dup_key=on_off
    | DROP_EXISTING EQUAL drop_existing=on_off
    | ONLINE EQUAL online_value
    | ALLOW_ROW_LOCKS EQUAL allow_row_loks=on_off
    | ALLOW_PAGE_LOCKS EQUAL allow_page_locks=on_off
    | MAXDOP EQUAL max_degree_of_parallelism=decimal
    | XML_COMPRESSION EQUAL xml_compression=on_off
    ;


// https://msdn.microsoft.com/en-us/library/ms187926(v=sql.120).aspx
create_or_alter_procedure
    : procedure_declaration proc_keyword schema_func_proc_ref (SEMI decimal)?
      procedure_declaration_arguments?
      procedure_options?
      (FOR REPLICATION)? AS replication_alias
    ;

procedure_declaration_arguments 
    : procedure_params
    | LR_BRACKET procedure_params RR_BRACKET
    ;

procedure_declaration
    : procedure_declaration_create 
    | ALTER
    ;

procedure_declaration_create 
    : CREATE (OR alter_replace)?
    ;

replication_alias : as_external_name | sql_clause;
as_external_name
    : EXTERNAL NAME assembly_id DOT class_id DOT method_id
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-trigger-transact-sql
create_or_alter_trigger
    : create_or_alter_dml_trigger
    | create_or_alter_ddl_trigger
    ;

create_or_alter_dml_trigger
    : create_or_alter TRIGGER schema_trigger_ref
      ON full_table_ref
      dml_trigger_options?
      for_after_instead dml_trigger_operations
      (WITH APPEND)?
      (NOT FOR REPLICATION)?
      AS sql_clauses
    ;

create_or_alter 
    : CREATE (OR ALTER)?
    | ALTER
    ;

for_after_instead
    : FOR 
    | AFTER 
    | INSTEAD OF
    ;

dml_trigger_option
    : ENCRYPTION
    | execute_clause
    ;

dml_trigger_operation
    : INSERT 
    | UPDATE 
    | DELETE
    ;

create_or_alter_ddl_trigger
    : create_or_alter TRIGGER schema_trigger_ref
      ON all_server_database
      dml_trigger_options?
      for_after dml_trigger_operations
      AS sql_clauses
    ;

// https://msdn.microsoft.com/en-us/library/ms186755.aspx
create_or_alter_function
    : create_or_alter FUNCTION funcName=schema_func_proc_ref
        ((LR_BRACKET procedure_params RR_BRACKET) | LR_BRACKET RR_BRACKET) //must have (), but can be empty
        (func_body_returns_select | func_body_returns_table | func_body_returns_scalar)
    ;

func_body_returns_select
    : RETURNS TABLE
        function_options?
        AS? (as_external_name | RETURN (LR_BRACKET select_statement_standalone RR_BRACKET | select_statement_standalone))
    ;

func_body_returns_table
    : RETURNS local_id table_type_definition
        function_options?
        AS? (as_external_name |
        BEGIN
           sql_clauses?
           RETURN SEMI?
        END SEMI?)
    ;

func_body_returns_scalar
    : RETURNS data_type
        function_options?
        AS? (as_external_name 
    | BEGIN
        sql_clause?
        RETURN ret=expression SEMI?
     END)
    ;

procedure_param
    : arg_name=local_id AS? schema_type_ref VARYING? (EQUAL default_val=default_value)? param_way?
    ;

procedure_option
    : procedure_option_enum
    | execute_clause
    ;

function_option
    : function_option_enum
    | execute_clause
    ;

// https://msdn.microsoft.com/en-us/library/ms188038.aspx
create_statistics
    : CREATE STATISTICS id_ ON full_table_ref LR_BRACKET column_name_list RR_BRACKET
      (WITH statistics_with (COMMA NORECOMPUTE)? (COMMA INCREMENTAL EQUAL on_off)? )? 
    ;

statistics_with 
    : FULLSCAN 
    | SAMPLE decimal percent_row 
    | STATS_STREAM
    ; 

update_statistics
    : UPDATE STATISTICS complete_table_ref
        ( id_ | LR_BRACKET ids RR_BRACKET )?
        update_statistics_options?
    ;

update_statistics_option
    : ( FULLSCAN (COMMA? PERSIST_SAMPLE_PERCENT EQUAL on_off )? )
    | ( SAMPLE number=decimal percent_row
        (COMMA? PERSIST_SAMPLE_PERCENT EQUAL on_off )? )
    | RESAMPLE on_partitions?
    | STATS_STREAM EQUAL stats_stream_=expression
    | ROWCOUNT EQUAL decimal
    | PAGECOUNT EQUAL decimal
    | ALL
    | COLUMNS
    | INDEX
    | NORECOMPUTE
    | INCREMENTAL EQUAL on_off
    | MAXDOP EQUAL max_dregree_of_parallelism=decimal
    | AUTO_DROP EQUAL on_off
    ;

// https://msdn.microsoft.com/en-us/library/ms174979.aspx
create_table
    : CREATE TABLE full_table_ref 
    LR_BRACKET column_def_table_constraints table_indices_list?  RR_BRACKET 
    (LOCK simple_id)? 
    table_options?
    (ON on=group_id | DEFAULT)? 
    (TEXTIMAGE_ON text_image=group_id | DEFAULT)?
    ;

table_indices_list : table_indices (COMMA table_indices)*;

table_indices
    : INDEX id_  UNIQUE? clustered? LR_BRACKET column_name_list_with_order RR_BRACKET
    | INDEX id_ CLUSTERED COLUMNSTORE
    | INDEX id_ NONCLUSTERED? COLUMNSTORE LR_BRACKET column_name_list RR_BRACKET
    create_table_index_options?
    (ON group_id)?
    ;

table_options : tbl_option+;

tbl_option
    : WITH LR_BRACKET tableoptions RR_BRACKET 
    | tableoptions
    ;

tableoption 
    : table_opt_varname EQUAL table_opt_var_value
    | tableoption_cluster_mode
    | FILLFACTOR EQUAL decimal
    | table_distribution
    | DATA_COMPRESSION EQUAL compression_mode on_partitions?
    | XML_COMPRESSION EQUAL on_off on_partitions?
    ;

table_opt_varname 
    : simple_id 
    | keyword
    ;

table_opt_var_value 
    : simple_id 
    | keyword 
    | on_off 
    | decimal
    ;

table_distribution
    : DISTRIBUTION EQUAL HASH LR_BRACKET id_ RR_BRACKET 
    | CLUSTERED INDEX LR_BRACKET column_name_list_with_order RR_BRACKET
    ;

create_table_index_option
    : PAD_INDEX EQUAL on_off
    | FILLFACTOR EQUAL decimal
    | IGNORE_DUP_KEY EQUAL on_off
    | STATISTICS_NORECOMPUTE EQUAL on_off
    | STATISTICS_INCREMENTAL EQUAL on_off
    | ALLOW_ROW_LOCKS EQUAL on_off
    | ALLOW_PAGE_LOCKS EQUAL on_off
    | OPTIMIZE_FOR_SEQUENTIAL_KEY EQUAL on_off
    | DATA_COMPRESSION EQUAL index_strategy on_partitions?
    | XML_COMPRESSION EQUAL on_off on_partitions?
    ;


// https://msdn.microsoft.com/en-us/library/ms187956.aspx
create_view
    : CREATE VIEW schema_view_ref (LR_BRACKET column_name_list RR_BRACKET)?
      view_attributes?
      AS select_statement_standalone (WITH CHECK OPTION)? SEMI?
    ;

// https://msdn.microsoft.com/en-us/library/ms190273.aspx
alter_table
    : ALTER TABLE full_table_ref 
    (
          SET LR_BRACKET LOCK_ESCALATION EQUAL lock_mode RR_BRACKET
        | ADD column_def_table_constraints
        | ALTER COLUMN (column_definition | column_modifier)
        | DROP COLUMN ids
        | DROP CONSTRAINT constraint_id
        | WITH check_nocheck ADD alter_table_constraint
        | check_nocheck CONSTRAINT constraint_id
        | enable_disable TRIGGER id_?
        | REBUILD table_options
        | SWITCH switch_partition
    )
    SEMI?
    ;

alter_table_constraint
    : (CONSTRAINT constraint_id)? alter_table_constraint_foreign
    | CHECK LR_BRACKET search_condition RR_BRACKET 
            
    ;

alter_table_constraint_foreign
    : FOREIGN KEY LR_BRACKET fk=column_name_list RR_BRACKET REFERENCES full_table_ref 
      (
        LR_BRACKET pk=column_name_list RR_BRACKET
      )? 
      on1=constraint_delete_or_update? on2=constraint_delete_or_update?
    ;


 constraint_delete_or_update : on_delete | on_update;
 

switch_partition
    : (PARTITION? source_partition_number_expression=expression)?
      TO target_table=full_table_ref
      (PARTITION target_partition_number_expression=expression)?
      (WITH low_priority_lock_wait)?
    ;

low_priority_lock_wait
    : WAIT_AT_LOW_PRIORITY LR_BRACKET
      MAX_DURATION EQUAL max_duration=timespan MINUTES? COMMA
      ABORT_AFTER_WAIT EQUAL abort_after_wait=abord_after_mode RR_BRACKET
    ;

// https://msdn.microsoft.com/en-us/library/ms174269.aspx
alter_database
    : ALTER DATABASE (database_id | CURRENT) alter_database_new_infos
    ;

alter_database_new_infos
    : MODIFY NAME EQUAL database_id
    | collate_set
    | SET database_optionspec (WITH termination)?
    | add_or_modify_files
    | add_or_modify_filegroups
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-database-transact-sql-file-and-filegroup-options?view=sql-server-ver15
add_or_modify_files
    : ADD FILE filespecs (TO FILEGROUP file_group_id)?
    | ADD LOG FILE filespecs
    | REMOVE FILE file_group_id
    | MODIFY FILE filespec
    ;

filespec
    : LR_BRACKET name_set
        (COMMA newname_set)?
        (COMMA filename_set)?
        (COMMA size_set)?
        (COMMA maxsize_set)?
        (COMMA filegrowth_set)?
        (COMMA OFFLINE )?
      RR_BRACKET
    ;

add_or_modify_filegroups
    : ADD FILEGROUP file_group_id filegroup_predicate?
    | REMOVE FILEGROUP file_group_id
    | MODIFY FILEGROUP file_group_id modify_filegroups_options
    ;

modify_filegroups_options
    : filegroup_updatability_option
    | DEFAULT
    | NAME EQUAL file_group_id
    | AUTOGROW_SINGLE_FILE
    | AUTOGROW_ALL_FILES
    ;

filegroup_updatability_option
    : READONLY
    | READWRITE
    | READ_ONLY
    | READ_WRITE
    ;

// https://msdn.microsoft.com/en-us/library/bb522682.aspx
// Runtime check.
database_optionspec
    : auto_option
    | change_tracking_set
    | containment_set
    | cursor_option
    | database_mirroring_option
    | date_correlation_optimization_option
    | db_encryption_option
    | db_state_option
    | db_update_option
    | db_user_access_option
    | delayed_durability_option
    | db_option
    | database_filestream
    | hadr_options
    | mixed_page_allocation_option
    | parameterization_option
//  | query_store_options
    | recovery_option
//  | remote_data_archive_option
    | service_broker_option
    | snapshot_option
    | sql_option
    | target_recovery_time_option
    | termination
    ;

database_filestream : FILESTREAM LR_BRACKET database_filestream_option RR_BRACKET;

auto_option
    : AUTO_CLOSE on_off
    | AUTO_CREATE_STATISTICS  statistic_value
    | AUTO_SHRINK  on_off
    | AUTO_UPDATE_STATISTICS on_off
    | AUTO_UPDATE_STATISTICS_ASYNC  on_off
    ;

change_tracking_set
    : CHANGE_TRACKING EQUAL ( OFF | ON change_tracking_option_list)
    ;

change_tracking_option_item : change_tracking_option_list change_tracking_option_lists;

change_tracking_option_list
    : AUTO_CLEANUP EQUAL on_off
    | CHANGE_RETENTION EQUAL period
    ;

containment_set
    : CONTAINMENT EQUAL none_partial
    ;

cursor_option
    : CURSOR_CLOSE_ON_COMMIT on_off
    | CURSOR_DEFAULT local_global
    ;

listener_ip_addr : LISTENER_IP EQUAL (ALL | ipv4 | ipv6 | stringtext);

// https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-endpoint-transact-sql
alter_endpoint
    : ALTER ENDPOINT endpoint_id (AUTHORIZATION login_id)? ( STATE EQUAL state_enum )? 
        AS TCP LR_BRACKET LISTENER_PORT EQUAL decimal ( COMMA listener_ip_addr )? RR_BRACKET
        (     TSQL
            | alter_endpoint_service_broker
            | alter_endpoint_database_mirroring
        )
    ;

authentication_configuration
    : AUTHENTICATION EQUAL
    ( 
          WINDOWS authentication_mode?  (CERTIFICATE certificate_id)?
        | CERTIFICATE certificate_id  WINDOWS? authentication_mode?
    )
    ;

alter_endpoint_database_mirroring
    : FOR DATABASE_MIRRORING LR_BRACKET
                    authentication_configuration
                    ( COMMA? encryption_state encryption_algorithm? )?
                    COMMA? ROLE EQUAL role_mirroring
                    RR_BRACKET
    ;

alter_endpoint_service_broker
    : FOR SERVICE_BROKER LR_BRACKET
                    authentication_configuration
                    ( COMMA? encryption_state encryption_algorithm? )?
                    ( COMMA? MESSAGE_FORWARDING EQUAL enable_disable )?
                    ( COMMA? MESSAGE_FORWARD_SIZE EQUAL decimal)?
                    RR_BRACKET
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/create-endpoint-transact-sql
// todo: not implemented

/* Will visit later
*/
database_mirroring_option
    : mirroring_set_option
    ;

mirroring_set_option
    : mirroring_partner  partner_option
    | mirroring_witness  witness_option
    ;
mirroring_partner
    : PARTNER
    ;

mirroring_witness
    : WITNESS
    ;

witness_partner_equal
    : EQUAL
    ;


partner_option
    : witness_partner_equal partner_server
    | TIMEOUT decimal
    | partner_option_enum
    ;

witness_option
    : witness_partner_equal witness_server
    | OFF
    ;

witness_server
    : partner_server
    ;

partner_server
    : partner_server_tcp_prefix host COLON port_number
    ;

host
    : HOST
    ;

partner_server_tcp_prefix
    : TCP COLON DOUBLE_FORWARD_SLASH
    ;

port_number
    : decimal
    ;

date_correlation_optimization_option
    : DATE_CORRELATION_OPTIMIZATION on_off
    ;

db_encryption_option
    : ENCRYPTION on_off
    ;

delayed_durability_option : DELAYED_DURABILITY EQUAL delayed_durability;

language_setting : id_or_string;

language_setting_value
    : NONE 
    | lcid=decimal 
    | language_id
    ;

id_or_string
    : id_ 
    | stringtext
    ;

hadr_options
    : HADR 
    ( 
          ( AVAILABILITY GROUP EQUAL group_id | OFF ) 
        | suspend_resume 
    )
    ;

mixed_page_allocation_option
    : MIXED_PAGE_ALLOCATION on_off
    ;

recovery_option
    : recovery_option_enum
    | TORN_PAGE_DETECTION on_off
    | ACCELERATED_DATABASE_RECOVERY EQUAL on_off
    ;

service_broker_option:
    ENABLE_BROKER
    | DISABLE_BROKER
    | NEW_BROKER
    | ERROR_BROKER_CONVERSATIONS
    | HONOR_BROKER_PRIORITY on_off
    ;
snapshot_option
    : ALLOW_SNAPSHOT_ISOLATION on_off
    | READ_COMMITTED_SNAPSHOT on_off
    | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = on_off
    ;

sql_option
    : ANSI_NULL_DEFAULT on_off
    | ANSI_NULLS on_off
    | ANSI_PADDING on_off
    | ANSI_WARNINGS on_off
    | ARITHABORT on_off
    | COMPATIBILITY_LEVEL EQUAL decimal
    | CONCAT_NULL_YIELDS_NULL on_off
    | NUMERIC_ROUNDABORT on_off
    | QUOTED_IDENTIFIER on_off
    | RECURSIVE_TRIGGERS on_off
    ;

target_recovery_time_option
    : TARGET_RECOVERY_TIME EQUAL decimal seconds_minutes
    ;

termination
    : ROLLBACK AFTER seconds = decimal
    | ROLLBACK IMMEDIATE
    | NO_WAIT
    ;

// https://msdn.microsoft.com/en-us/library/ms176118.aspx
drop_index
    : DROP INDEX if_exists?
    ( drop_relational_or_xml_or_spatial_indexs
    | drop_backward_compatible_indexs
    )
    ;

drop_relational_or_xml_or_spatial_index
    : index_id ON complete_table_ref
    ;

// https://msdn.microsoft.com/en-us/library/ms174969.aspx
drop_procedure
    : DROP proc_keyword if_exists? func_proc_name_schemas
    ;

drop_dml_trigger
    : DROP TRIGGER if_exists? schema_trigger_refs
    ;

drop_ddl_trigger
    : DROP TRIGGER if_exists? schema_view_refs
    ON all_server_database
    ;

// https://msdn.microsoft.com/en-us/library/ms190290.aspx
drop_function
    : DROP FUNCTION if_exists? func_proc_name_schemas
    ;

// https://msdn.microsoft.com/en-us/library/ms175075.aspx
drop_statistics
    : DROP STATISTICS full_table_ref_columns
    ;

full_table_ref_column : (full_table_ref DOT)? name=id_;


// https://msdn.microsoft.com/en-us/library/ms173790.aspx
drop_table
    : DROP TABLE if_exists? table_names
    ;

// https://msdn.microsoft.com/en-us/library/ms173492.aspx
drop_view
    : DROP VIEW if_exists? schema_view_refs
    ;

if_exists : IF EXISTS;

create_type
    : CREATE TYPE name = schema_type_ref
      (FROM data_type default_value)?
      (AS TABLE LR_BRACKET column_def_table_constraints RR_BRACKET)?
    ;

drop_type:
    DROP TYPE ( IF EXISTS )? name = schema_type_ref
    ;

rowset_function_limited
    : openquery_args
    | open_data_source
    ;

// https://msdn.microsoft.com/en-us/library/ms188427(v=sql.120).aspx
openquery_args
    : OPENQUERY LR_BRACKET server_id COMMA query=stringtext RR_BRACKET
    ;

// https://msdn.microsoft.com/en-us/library/ms179856.aspx
open_data_source
    : OPENDATASOURCE LR_BRACKET provider=stringtext COMMA init=stringtext RR_BRACKET DOT database_schema_table_ref
    ;

// Other statements.

// https://msdn.microsoft.com/en-us/library/ms188927.aspx
declare_statement
    : DECLARE local_id AS? declare_object_table
    | DECLARE declare_locals
    | DECLARE local_id AS? xml_type_definition
    | WITH XMLNAMESPACES LR_BRACKET xml_declarations RR_BRACKET
    ;

declare_object_table : table_type_definition | full_table_ref;

xml_declaration
    : xml_namespace_uri=stringtext AS id_
    | DEFAULT stringtext
    ;

// https://msdn.microsoft.com/en-us/library/ms181441(v=sql.120).aspx
cursor_statement
    // https://msdn.microsoft.com/en-us/library/ms175035(v=sql.120).aspx
    : CLOSE GLOBAL? cursor_name
    // https://msdn.microsoft.com/en-us/library/ms188782(v=sql.120).aspx
    | DEALLOCATE GLOBAL? CURSOR? cursor_name
    // https://msdn.microsoft.com/en-us/library/ms180169(v=sql.120).aspx
    | declare_cursor
    // https://msdn.microsoft.com/en-us/library/ms180152(v=sql.120).aspx
    | fetch_cursor
    // https://msdn.microsoft.com/en-us/library/ms190500(v=sql.120).aspx
    | OPEN GLOBAL? cursor_name
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/backup-transact-sql
backup_database
    : BACKUP DATABASE ( database_id )
          (READ_WRITE_FILEGROUPS group1=file_group_list? )? group2=file_group_list?
    backup_target? backup_settings?
    ;

file_group_assign : file_file_group EQUAL file_or_filegroup=stringtext;

backup_log
    : BACKUP LOG database_id backup_target? backup_settings?
    ;
    
backup_target
    : backup_to backup_to_mirror
    ;

backup_to
    : TO ( logical_device_ids TO disk_tape_url_values)
    ;

backup_to_mirror
    : MIRROR TO (logical_device_ids | disk_tape_url_values)
    ;
disk_tape_url_value :disk_tape_url EQUAL string_id;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/backup-certificate-transact-sql
backup_certificate
    : BACKUP CERTIFICATE certificate_id TO FILE EQUAL cert_file=stringtext
    ( WITH PRIVATE KEY LR_BRACKET backup_certificate_private_keys RR_BRACKET)?
    ;

backup_certificate_private_keys : backup_certificate_private_key  (COMMA backup_certificate_private_key )*;

backup_certificate_private_key 
    : FILE EQUAL private_key_file=stringtext
    | by_password_crypt
    ;

backup_settings : WITH backup_setting+;

backup_setting
    : DIFFERENTIAL
    | COPY_ONLY
    | CREDENTIAL
    | FILE_SNAPSHOT
    | NO_CHECKSUM
    | CHECKSUM
    | STOP_ON_ERROR
    | CONTINUE_AFTER_ERROR
    | RESTART
    | DESCRIPTION EQUAL string_id
    | NAME EQUAL backup_id
    | EXPIREDATE EQUAL string_id 
    | RETAINDAYS EQUAL decimal_id
    | MEDIADESCRIPTION EQUAL string_id
    | MEDIANAME EQUAL stringtext
    | BLOCKSIZE EQUAL decimal_id
    | BUFFERCOUNT EQUAL decimal_id
    | MAXTRANSFER EQUAL decimal_id
    | STATS (EQUAL decimal)?
    | ENCRYPTION LR_BRACKET
        ALGORITHM EQUAL algorithm_short
        COMMA SERVER CERTIFICATE EQUAL server_certificate_value
        RR_BRACKET
    | compression
    | rewind
    | load_moun_load
    | init_no_init
    | no_skip
    | format_noformat
    ;

server_certificate_value 
    :   encryptor_id
    | SERVER ASYMMETRIC KEY EQUAL encryptor_id
    ;
// https://docs.microsoft.com/en-us/sql/t-sql/statements/backup-master-key-transact-sql
backup_master_key
    : BACKUP MASTER KEY TO FILE EQUAL master_key_backup_file=stringtext
      encryption_by_pwd
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/statements/backup-service-master-key-transact-sql
backup_service_master_key
    : BACKUP SERVICE MASTER KEY TO FILE EQUAL service_master_key_backup_file=stringtext
         encryption_by_pwd
    ;

kill_statement
    : KILL (kill_process | kill_query_notification | kill_stats_job)
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/kill-transact-sql
kill_process
    : (session=decimal_string | UOW) (WITH STATUSONLY)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/kill-query-notification-subscription-transact-sql
kill_query_notification
    : QUERY NOTIFICATION SUBSCRIPTION (ALL | subscription=decimal)
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/kill-stats-job-transact-sql
kill_stats_job
    : STATS JOB job=decimal
    ;

// https://msdn.microsoft.com/en-us/library/ms188332.aspx
execute_statement
    : EXECUTE execute_body
    ;

execute_body_batch
    : func_proc_name_server_database_schema execute_statement_args?
    ;

//https://docs.microsoft.com/it-it/sql/t-sql/language-elements/execute-transact-sql?view=sql-server-ver15
execute_body
    : (return_status=local_id EQUAL)? body_kind execute_statement_arg?
    | LR_BRACKET execute_var_strings RR_BRACKET (AS? login_user EQUAL stringtext)? (AT_KEYWORD server_id)?
    ;

body_kind : func_proc_name_server_database_schema | execute_var_string;

execute_statement_arg
    : execute_statement_arg_unnamed execute_statement_args?    //Unnamed params can continue unnamed
    | execute_statement_arg_nameds //Named can only be continued by unnamed
    ;

execute_statement_arg_named
    : name=local_id EQUAL value=execute_parameter
    ;

execute_statement_arg_unnamed
    : value=execute_parameter
    ;

execute_parameter
    : constant 
    | local_id output_out? 
    | id_ 
    | DEFAULT 
    | NULL_
    ;

execute_var_string
    : source=local_id output_out? (PLUS more=local_id (PLUS execute_var_string)?)?
    | stringtext (PLUS local_id (PLUS execute_var_string)?)?
    ;

// https://msdn.microsoft.com/en-us/library/ff848791.aspx
security_statement
    // https://msdn.microsoft.com/en-us/library/ms188354.aspx
    : execute_clause
    // https://msdn.microsoft.com/en-us/library/ms187965.aspx
    | GRANT grant_mode
            (ON (class_type_for_grant DOUBLE_COLON)? table=full_table_ref)? 
            TO to_principal_rincipal_ids 
            (WITH GRANT OPTION)? 
            (AS as_principal=principal_id)?
    // https://msdn.microsoft.com/en-us/library/ms178632.aspx
    | REVERT (LR_BRACKET WITH COOKIE EQUAL local_id RR_BRACKET)?
    | open_key
    | close_key
    | create_key
    | create_certificate
    ;

grant_mode 
    : ALL PRIVILEGES? 
    | grant_permission (LR_BRACKET column_name_list RR_BRACKET)?
    ; 
to_principal_rincipal_ids : principal_id (COMMA principal_id)*;

principal_id
    : id_
    | PUBLIC
    ;

create_certificate
    : CREATE CERTIFICATE certificate_id (AUTHORIZATION user_id)?
      (FROM existing_keys | generate_new_keys)
      (ACTIVE FOR BEGIN DIALOG EQUAL on_off)?
    ;

existing_keys
    : ASSEMBLY assembly_id
    | EXECUTABLE? FILE EQUAL path_to_file=stringtext (WITH PRIVATE KEY LR_BRACKET private_key_options RR_BRACKET)?
    ;

private_key_options
    : (FILE | binary_) EQUAL path=stringtext (COMMA encryption_decryption BY password_setting)?
    ;

generate_new_keys
    : encryption_by_pwd?
      WITH SUBJECT EQUAL certificate_subject_name=stringtext (COMMA date_options)?
    ;

date_option
    : start_date_expiry_date EQUAL stringtext
    ;

open_key
    : OPEN SYMMETRIC KEY symmetric_key_id DECRYPTION BY decryption_mechanism
    | OPEN MASTER KEY decryption_by_pwd
    ;

close_key
    : CLOSE SYMMETRIC KEY symmetric_key_id
    | CLOSE ALL SYMMETRIC KEYS
    | CLOSE MASTER KEY
    ;

create_key
    : CREATE MASTER KEY encryption_by_pwd
    | CREATE SYMMETRIC KEY symmetric_key_id
        (AUTHORIZATION user_id)?
        (FROM PROVIDER provider_id)?
        WITH create_key_options
    ;

create_key_option
    : key_options 
    | ENCRYPTION BY encryption_mechanism
    ;

create_key_options
    : create_key_option (COMMA create_key_option)*
    ;

key_options
    : KEY_SOURCE EQUAL pass_phrase=stringtext
    | ALGORITHM EQUAL algorithm
    | IDENTITY_VALUE EQUAL identity_phrase=stringtext
    | PROVIDER_KEY_NAME EQUAL key_name_in_provider=stringtext
    | CREATION_DISPOSITION EQUAL creation_disposition
    ;

encryption_mechanism
    : CERTIFICATE certificate_id
    | ASYMMETRIC KEY asym_key_id
    | SYMMETRIC KEY symmetric_key_id
    | password_setting
    ;

decryption_mechanism
    : CERTIFICATE certificate_id (WITH password_setting)?
    | ASYMMETRIC KEY asym_key_id (WITH password_setting)?
    | SYMMETRIC KEY symmetric_key_id
    | password_setting
    ;


grant_permission_alter
    : ALTER ( ANY ( APPLICATION ROLE
                  | ASSEMBLY
                  | ASYMMETRIC KEY
                  | AVAILABILITY GROUP
                  | CERTIFICATE
                  | COLUMN ( ENCRYPTION KEY | MASTER KEY )
                  | CONNECTION
                  | CONTRACT
                  | CREDENTIAL
                  | DATABASE ( AUDIT
                             | DDL TRIGGER
                             | EVENT ( NOTIFICATION | SESSION )
                             | SCOPED CONFIGURATION
                             )?
                  | DATASPACE
                  | ENDPOINT
                  | EVENT ( NOTIFICATION | SESSION )
                  | EXTERNAL ( DATA SOURCE | FILE FORMAT | LIBRARY)
                  | FULLTEXT CATALOG
                  | LINKED SERVER
                  | LOGIN
                  | MASK
                  | MESSAGE TYPE
                  | REMOTE SERVICE BINDING
                  | ROLE
                  | ROUTE
                  | SCHEMA
                  | SECURITY POLICY
                  | SERVER ( AUDIT | ROLE )
                  | SERVICE
                  | SYMMETRIC KEY
                  | USER
                  )
            | RESOURCES
            | SERVER STATE
            | SETTINGS
            | TRACE
            )?
    ;

grant_permission_create
    : CREATE ( AGGREGATE
             | ANY DATABASE
             | ASSEMBLY
             | ASYMMETRIC KEY
             | AVAILABILITY GROUP
             | CERTIFICATE
             | CONTRACT
             | DATABASE (DDL EVENT NOTIFICATION)?
             | DDL EVENT NOTIFICATION
             | DEFAULT
             | ENDPOINT
             | EXTERNAL LIBRARY
             | FULLTEXT CATALOG
             | FUNCTION
             | MESSAGE TYPE
             | PROCEDURE
             | QUEUE
             | REMOTE SERVICE BINDING
             | ROLE
             | ROUTE
             | RULE
             | SCHEMA
             | SEQUENCE
             | SERVER ROLE
             | SERVICE
             | SYMMETRIC KEY
             | SYNONYM
             | TABLE
             | TRACE EVENT NOTIFICATION
             | TYPE
             | VIEW
             | XML SCHEMA COLLECTION
             )
    ;

// https://docs.microsoft.com/en-us/sql/relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql?view=sql-server-ver15
// SELECT DISTINCT '| ' + permission_name
// FROM sys.fn_builtin_permissions (DEFAULT)
// ORDER BY 1
grant_permission
    : grant_permission_enum
    | grant_permission_alter
    | grant_permission_create
    ;

// https://msdn.microsoft.com/en-us/library/ms190356.aspx
// https://msdn.microsoft.com/en-us/library/ms189484.aspx
set_statement
    : SET local_id (DOT member_name=id_)? EQUAL expression
    | SET local_id assignment_operator expression
    | SET local_id EQUAL CURSOR declare_set_cursor_common (FOR cursor_mode)?
    // https://msdn.microsoft.com/en-us/library/ms189837.aspx
    | set_special
    ;

cursor_mode 
    : READ ONLY 
    | UPDATE (OF column_name_list)?
    ;

// https://msdn.microsoft.com/en-us/library/ms174377.aspx
transaction_statement
    // https://msdn.microsoft.com/en-us/library/ms188386.aspx
    : BEGIN DISTRIBUTED transaction transaction_ref?
    // https://msdn.microsoft.com/en-us/library/ms188929.aspx
    | BEGIN transaction (transaction_ref (WITH MARK mark=stringtext)?)?
    // https://msdn.microsoft.com/en-us/library/ms190295.aspx
    | COMMIT transaction (transaction_ref (WITH LR_BRACKET DELAYED_DURABILITY EQUAL on_off RR_BRACKET)?)?
    // https://msdn.microsoft.com/en-us/library/ms178628.aspx
    | COMMIT WORK?
    | COMMIT transaction_identifier
    | ROLLBACK transaction_identifier
    // https://msdn.microsoft.com/en-us/library/ms181299.aspx
    | ROLLBACK transaction transaction_ref?
    // https://msdn.microsoft.com/en-us/library/ms174973.aspx
    | ROLLBACK WORK?
    // https://msdn.microsoft.com/en-us/library/ms188378.aspx
    | SAVE transaction transaction_ref?
    ;

transaction_ref
    : id_
    | local_id
    ;

// https://msdn.microsoft.com/en-us/library/ms188037.aspx
go_statement
    : GO (count=decimal)?
    ;

// https://msdn.microsoft.com/en-us/library/ms188366.aspx
use_statement
    : USE database_id
    ;

setuser_statement
    : SETUSER user=stringtext?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/reconfigure-transact-sql
reconfigure_statement
    : RECONFIGURE (WITH OVERRIDE)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/shutdown-transact-sql
shutdown_statement
    : SHUTDOWN (WITH NOWAIT)?
    ;

checkpoint_statement
    : CHECKPOINT (checkPointDuration=decimal)?
    ;

//These are dbcc commands with strange syntax that doesn't fit the regular dbcc syntax
dbcc_special
    : DBCC SHRINKLOG (LR_BRACKET SIZE EQUAL dbcc_special_size RR_BRACKET)?
    ;

dbcc_special_size 
    : constant_expression 
    id_ 
    | DEFAULT
    ;

dbcc_clause
    : DBCC name=dbcc_command (LR_BRACKET expression_list RR_BRACKET)? (WITH dbcc_options)?
    ;

dbcc_command
    : simple_id | keyword
    ;

execute_clause
    : EXECUTE AS execute_clause_mode
    ;

execute_clause_mode 
    : execute_clause_mode_enum 
    | stringtext
    ;

declare_local
    : local_id AS? data_type (EQUAL expression)?
    ;

table_type_definition
    : TABLE LR_BRACKET column_def_table_constraints table_type_indices?  RR_BRACKET
    ;

table_type_indice
    : type_indice LR_BRACKET column_name_list_with_order RR_BRACKET
    | CHECK LR_BRACKET search_condition RR_BRACKET
    ;

type_indice
    : indice_name clustered?
    | UNIQUE
    ;

indice_name
    : PRIMARY KEY 
    | INDEX index_id
    ;

xml_type_definition
    : XML LR_BRACKET content_document? xml_schema_collection RR_BRACKET
    ;

xml_schema_collection
    : left=simple_id DOT right=simple_id
    ;

column_def_table_constraints
    : column_def_table_constraint (COMMA? column_def_table_constraint)*
    ;

column_def_table_constraint
    : column_definition
    | materialized_column_definition
    | table_constraint
    ;

// https://msdn.microsoft.com/en-us/library/ms187742.aspx
// There is a documentation error: column definition elements can be given in
// any order
column_definition
    : column_id (data_type | AS expression PERSISTED? )
      column_definition_elements?
      column_index?
    ;

column_definition_element
    : FILESTREAM
    | collate_set
    | SPARSE
    | MASKED WITH LR_BRACKET FUNCTION EQUAL mask_function=stringtext RR_BRACKET
    | (CONSTRAINT constraint_id)? DEFAULT  constant_expr=expression
    | IDENTITY (LR_BRACKET seed=decimal COMMA increment=decimal RR_BRACKET)?
    | NOT FOR REPLICATION
    | GENERATED ALWAYS AS generation_mode start_end  HIDDEN_KEYWORD?
    // NULL / NOT NULL is a constraint
    | ROWGUIDCOL
    | ENCRYPTED WITH
        LR_BRACKET column_encryption_key_id EQUAL key_name=stringtext COMMA
            ENCRYPTION_TYPE EQUAL encryption_mode COMMA
            ALGORITHM EQUAL algo=stringtext
        RR_BRACKET
    | column_constraint
    ;

column_modifier
    : id_ add_drop 
    (
        column_modifier_enum
      | MASKED (WITH (FUNCTION EQUAL stringtext | LR_BRACKET FUNCTION EQUAL stringtext RR_BRACKET))?
    )
    ;

materialized_column_definition
    : id_ compute_as expression materialized_mode?
    ;

// https://msdn.microsoft.com/en-us/library/ms186712.aspx
// There is a documentation error: NOT NULL is a constraint
// and therefore can be given a name.
column_constraint
    : (CONSTRAINT constraint_id)?
      (
        null_notnull
      | (
            primary_key_unique
            clustered?
            primary_key_options
        )
      | (
            (FOREIGN KEY)?
            foreign_key_options
        )
      | check_constraint
      )
    ;

column_index
    :
        INDEX index_id?
        create_table_index_options?
        on_partition_or_filegroup?
        ( FILESTREAM_ON ( filestream_filegroup_or_partition_schema_id | NULL_DOUBLE_QUOTE ) )?
    ;

on_partition_or_filegroup
    :
        ON (
            (partition_scheme_id LR_BRACKET partition_column_id RR_BRACKET)
            | file_group_id 
            | DEFAULT_DOUBLE_QUOTE
        )
    ;

// https://msdn.microsoft.com/en-us/library/ms188066.aspx
table_constraint
    : (CONSTRAINT constraint_id)?
        (
            (
                primary_key_unique
                clustered?
                LR_BRACKET column_name_list_with_order RR_BRACKET
                primary_key_options
            )
            |
            (
                FOREIGN KEY
                LR_BRACKET fk = column_name_list RR_BRACKET
                foreign_key_options
            )
            |
            (
                CONNECTION
                LR_BRACKET connection_nodes RR_BRACKET
            )
            |
            (
                DEFAULT LR_BRACKET?  ((stringtext | PLUS | function_call | decimal)+ | NEXT VALUE FOR full_table_ref) RR_BRACKET? FOR id_
            )
            | check_constraint
        )
    ;


connection_node
    : from_node_table=id_ TO to_node_table=id_
    ;

primary_key_options
    : (WITH FILLFACTOR EQUAL decimal)?
      alter_table_index_options?
      on_partition_or_filegroup?
    ;

foreign_key_options
    : REFERENCES full_table_ref LR_BRACKET pk = column_name_list RR_BRACKET
      on_delete?
      on_update?
      (NOT FOR REPLICATION)?
    ;

check_constraint
    : CHECK (NOT FOR REPLICATION)? LR_BRACKET search_condition RR_BRACKET
    ;


// https://msdn.microsoft.com/en-us/library/ms186869.aspx
alter_table_index_option
    : PAD_INDEX EQUAL on_off
    | FILLFACTOR EQUAL decimal
    | IGNORE_DUP_KEY EQUAL on_off
    | STATISTICS_NORECOMPUTE EQUAL on_off
    | ALLOW_ROW_LOCKS EQUAL on_off
    | ALLOW_PAGE_LOCKS EQUAL on_off
    | OPTIMIZE_FOR_SEQUENTIAL_KEY EQUAL on_off
    | SORT_IN_TEMPDB EQUAL on_off
    | MAXDOP EQUAL max_degree_of_parallelism=decimal
    | DATA_COMPRESSION EQUAL index_strategy on_partitions?
    | XML_COMPRESSION EQUAL on_off on_partitions?
    | table_distribution
    | ONLINE EQUAL online_value
    | RESUMABLE EQUAL on_off
    | MAX_DURATION EQUAL times=decimal MINUTES?
    ;

// https://msdn.microsoft.com/en-us/library/ms180169.aspx
declare_cursor
    : DECLARE cursor_name
      (CURSOR (declare_set_cursor_common (FOR UPDATE (OF column_name_list)?)?)?
      | sensitive? SCROLL? CURSOR FOR select_statement_standalone (FOR (READ ONLY | UPDATE | (OF column_name_list)))?
      ) SEMI?
    ;

declare_set_cursor_common
    : declare_set_cursor_common_partials?
      FOR select_statement_standalone
    ;

declare_set_cursor_common_partial
    : local_global
    | declare_set_cursor_common_partial_enum
    ;

fetch_cursor
    : FETCH ((fetch_cursor_strategy | absolute_relative expression)? FROM)?
      GLOBAL? cursor_name (INTO local_ids)? SEMI?
    ;

// https://msdn.microsoft.com/en-us/library/ms190356.aspx
// Runtime check.
set_special
    : SET left=id_ set_special_set_value
    | SET STATISTICS statistic_kind statistics=on_off
    | SET ROWCOUNT local_id_decimal
    | SET TEXTSIZE decimal
    // https://msdn.microsoft.com/en-us/library/ms173763.aspx
    | SET TRANSACTION ISOLATION LEVEL transaction_level
    // https://msdn.microsoft.com/en-us/library/ms188059.aspx
    | SET IDENTITY_INSERT full_table_ref identity_insert=on_off
    | SET special_lists list=on_off
    | SET modify_method
    ;


transaction_level : transaction_isolation | decimal;

local_id_decimal : local_id | decimal;

set_special_set_value : id_ | constant_local_id | on_off;

constant_local_id
    : constant
    | local_id
    ;

empty_value : DOUBLE_QUOTE_ID;

// Expression.

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/expressions-transact-sql
// Operator precendence: https://docs.microsoft.com/en-us/sql/t-sql/language-elements/operator-precedence-transact-sql
expression
    : primitive_expression
    | function_call
    | expression DOT expression_chained
    | expression DOT hierarchyid_call
    | expression COLLATE id_
    | case_expression
    | full_column_name
    | bracket_expression
    | unary_operator_expression
    | left=expression op=expression_operator right=expression
    | expression time_zone
    | over_clause
    | DOLLAR_ACTION
    ;

expression_chained 
    : value_call 
    | query_call 
    | exist_call 
    | modify_call
    ;

time_zone
    : AT_KEYWORD TIME ZONE expression
    ;

primitive_expression
    : DEFAULT | NULL_ | local_id | constant
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/case-transact-sql
case_expression
    : CASE caseExpr=expression switch_section+ (ELSE elseExpr=expression)? END
    | CASE switch_search_condition_section+ (ELSE elseExpr=expression)? END
    ;

unary_operator_expression
    : BIT_NOT expression 
    | plus_minus expression
    ;

bracket_expression
    : LR_BRACKET expression RR_BRACKET | LR_BRACKET subquery RR_BRACKET
    ;

constant_expression
    : NULL_
    | constant
    // system functions: https://msdn.microsoft.com/en-us/library/ms187786.aspx
    | function_call
    | local_id         // TODO: remove.
    | LR_BRACKET constant_expression RR_BRACKET
    ;

subquery
    : select_statement
    ;

common_table_expression
    : expression_name=id_ (LR_BRACKET columns=column_name_list RR_BRACKET)? AS LR_BRACKET cte_query=select_statement RR_BRACKET
    ;

update_elem
    : local_id EQUAL full_column_name update_operator expression //Combined variable and column update
    | column_ref update_operator expression
    | udt_column_id DOT method_id LR_BRACKET expression_list RR_BRACKET
    //| full_column_name DOT WRITE (expression, )
    ;

column_ref : full_column_name | local_id;

update_operator : EQUAL | assignment_operator;

update_elem_merge
    : column_ref update_operator expression
    | udt_column_id DOT method_id LR_BRACKET expression_list RR_BRACKET
    //| full_column_name DOT WRITE (expression, )
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/queries/search-condition-transact-sql
search_condition
    : NOT* sub_search_condition 
    | left=search_condition and_or right=search_condition // AND takes precedence over OR
    ;

sub_search_condition 
    : predicate_expr
    | LR_BRACKET search_condition RR_BRACKET
    ;

predicate_expr
    : EXISTS LR_BRACKET subquery RR_BRACKET
    | freetext_predicate
    | predicate_binary
    | predicate_multi_assign
    | expression comparison_operator all_some_any LR_BRACKET subquery RR_BRACKET
    | predicate_tier
    | predicate_not_in
    | predicate_not_like
    | expression IS null_notnull
    ;

predicate_multi_assign : left=expression MULT_ASSIGN right=expression;  ////SQL-82 syntax for left outer joins; '*='. See https://stackoverflow.com/questions/40665/in-sybase-sql
predicate_binary : left=expression comparison_operator right=expression;
predicate_tier : init=expression NOT* BETWEEN left=expression AND right=expression;
predicate_not_like : left=expression NOT* LIKE right=expression (ESCAPE escape=expression)?;
predicate_not_in : left=expression NOT* IN LR_BRACKET (subquery | expression_list) RR_BRACKET;

// Changed union rule to sql_union to avoid union construct with C++ target.  Issue reported by person who generates into C++.  This individual reports change causes generated code to work

query_expression
    : query_specification select_order_by_clause? unions=sql_unions? //if using top, order by can be on the "top" side of union :/
    | LR_BRACKET left=query_expression RR_BRACKET (UNION ALL? right=query_expression)?   
    ;

sql_unions : sql_union+;

sql_union
    : join_mode sql_union_def
    ;
sql_union_def 
    : query_specification 
    | LR_BRACKET query_expression RR_BRACKET
    ;

// https://msdn.microsoft.com/en-us/library/ms176104.aspx
query_specification
    : SELECT allOrDistinct=all_distinct? top=top_clause?
      columns=select_list
      // https://msdn.microsoft.com/en-us/library/ms188029.aspx
      (INTO into=full_table_ref)?
      (FROM from=table_sources)?
      where_condition?
      // https://msdn.microsoft.com/en-us/library/ms177673.aspx
      (GROUP BY ((groupByAll=ALL? groupBys_list) | GROUPING SETS LR_BRACKET groupSet_list RR_BRACKET))?
      (HAVING having=search_condition)?
    ;

// https://msdn.microsoft.com/en-us/library/ms189463.aspx
top_clause
    : TOP (top_percent | top_count) (WITH TIES)?
    ;



top_percent
    : percent_constant PERCENT
    | LR_BRACKET topper_expression=expression RR_BRACKET PERCENT
    ;

percent_constant: real | float | decimal;

top_count
    : count_constant=decimal
    | LR_BRACKET topcount_expression=expression RR_BRACKET
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/queries/select-over-clause-transact-sql?view=sql-server-ver16
order_by_clause
    : ORDER BY order_by_expression (COMMA order_by_expression)*
    ;

// https://msdn.microsoft.com/en-us/library/ms188385.aspx
select_order_by_clause
    : order_by_clause
      (OFFSET offset_exp=expression offset_rows=row_rows (FETCH fetch_offset=first_next fetch_exp=expression fetch_rows=row_rows ONLY)?)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/queries/select-for-clause-transact-sql
for_clause
    : FOR BROWSE
    | for_clause_xml_raw
    | FOR XML EXPLICIT xml_common_directives? (COMMA XMLDATA)?
    | FOR XML PATH (LR_BRACKET stringtext RR_BRACKET)? xml_common_directives? (COMMA ELEMENTS absent_xsinil?)?
    | for_clause_json
    ;

xml_common_directive
    : BINARY_KEYWORD BASE64 
    | TYPE 
    | ROOTWORD (LR_BRACKET stringtext RR_BRACKET)?
    ;

for_clause_xml_raw
    : FOR XML (RAW (LR_BRACKET xmlraw=stringtext RR_BRACKET)? | AUTO) xml_common_directives?
      (COMMA (XMLDATA | XMLSCHEMA (LR_BRACKET xml_schema=stringtext RR_BRACKET)?))?
      (COMMA ELEMENTS absent_xsinil?)?
      ;

for_clause_json
    : FOR JSON auto_path clause_json_infos?
    ;

clause_json_info
    : ROOTWORD (LR_BRACKET stringtext RR_BRACKET)
    | INCLUDE_NULL_VALUES
    | WITHOUT_ARRAY_WRAPPER
    ;

order_by_expression
    : order_by=expression (ascending=ASC | descending=DESC)?
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/queries/select-group-by-transact-sql?view=sql-server-ver15
grouping_sets_item
    : grouping_sets_list
    | LR_BRACKET grouping_sets_list? RR_BRACKET
    ;

group_by_item
    : expression
    /*| rollup_spec
    | cube_spec
    | grouping_sets_spec
    | grand_total*/
    ;

update_option
    : FAST number_rows=decimal
    | MAXDOP number_of_processors=decimal
    | MAXRECURSION number_recursion=decimal
    | USE PLAN stringtext
    | OPTIMIZE FOR LR_BRACKET optimize_for_args RR_BRACKET
    | update_option_enum
    ;
 
optimize_for_arg
    : local_id (UNKNOWN | EQUAL (constant | NULL_))
    ;

// https://docs.microsoft.com/ru-ru/sql/t-sql/queries/select-clause-transact-sql
asterisk
    : star_asterisk
    | table_asterisk
    | updated_asterisk
    ;

table_asterisk
    : full_table_ref DOT STAR
    ;

column_elem
    : column_elem_target as_column_alias?
    ;

column_elem_target
    : full_column_name 
    | DOLLAR IDENTITY 
    | DOLLAR ROWGUID 
    | NULL_
    ;

udt_elem
    : udt_column_id DOT non_static_attr_id udt_method_arguments as_column_alias?
    | udt_column_id DOUBLE_COLON static_attr_id udt_method_arguments? as_column_alias?
    ;

expression_elem
    : leftAlias=column_alias eq=EQUAL leftAssignment=expression
    | expressionAs=expression as_column_alias?
    ;

select_list_elem
    : asterisk
    | column_elem
    | udt_elem
    | expression_assign_elem
    | expression_elem
    ;

expression_assign_elem
    : local_id update_operator expression
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/queries/from-transact-sql
table_source
    : table_source_item_joined
    //| LR_BRACKET database_schema_table_ref RR_BRACKET
    ;

table_source_item_joined
    : table_source_item joins=join_part*
    | LR_BRACKET table_source_item_joined RR_BRACKET joins=join_part*
    ;

table_source_item
    : complete_table_ref deprecated_table_hint          as_table_alias // this is currently allowed
    | complete_table_ref                                as_table_alias? table_hints?
    | rowset_function                                   as_table_alias?
    | LR_BRACKET derived_table RR_BRACKET               as_table_alias_column?
    | change_table                                      as_table_alias?
    | nodes_method                                      as_table_alias_column?
    | function_call                                     as_table_alias_column?
    | loc_id=local_id                                   as_table_alias?
    | loc_id_call=local_id DOT loc_fcall=function_call  as_table_alias_column?
    | open_xml
    | open_json
    | DOUBLE_COLON oldstyle_fcall=function_call         as_table_alias? // Build-in function (old syntax)
    ;

as_table_alias_column : as_table_alias column_alias_list?;

table_hints
    : with_table_hints 
    | deprecated_table_hint 
    | sybase_legacy_hints
    ;

// https://docs.microsoft.com/en-us/sql/t-sql/functions/openxml-transact-sql
open_xml
    : OPENXML LR_BRACKET expression COMMA expression2 RR_BRACKET
    (WITH LR_BRACKET schema_declaration RR_BRACKET )? as_table_alias?
    ;

open_json
    : OPENJSON LR_BRACKET expression2 RR_BRACKET
    (WITH LR_BRACKET json_declaration RR_BRACKET )? as_table_alias?
    ;

json_column_declaration
    : column_declaration (AS JSON)?
    ;

column_declaration
    : id_ data_type stringtext?
    ;

change_table
    : change_table_changes
    | change_table_version
    ;

change_table_changes
    : CHANGETABLE LR_BRACKET CHANGES changetable=full_table_ref COMMA (NULL_ | decimal_local_id) RR_BRACKET
    ;

change_table_version
    : CHANGETABLE LR_BRACKET VERSION versiontable=full_table_ref COMMA pk_columns=full_column_name_list COMMA pk_values=select_list RR_BRACKET
    ;

// https://msdn.microsoft.com/en-us/library/ms191472.aspx
join_part
    // https://msdn.microsoft.com/en-us/library/ms173815(v=sql.120).aspx
    : join_on
    | cross_join
    | apply_enum
    | pivot_join
    | unpivot_join
    ;
join_on
    : (inner=INNER? | join_type outer=OUTER?) join_hint?
       JOIN source=table_source ON cond=search_condition
    ;

cross_join
    : CROSS JOIN table_source
    ;

apply_enum
    : apply_style APPLY source=table_source
    ;

pivot_join
    : PIVOT pivot_clause as_table_alias
    ;

unpivot_join
    : UNPIVOT unpivot_clause as_table_alias
    ;

pivot_clause
    : LR_BRACKET aggregate_windowed_function FOR full_column_name IN column_alias_list RR_BRACKET
    ;

unpivot_clause
    : LR_BRACKET unpivot_exp=expression FOR full_column_name IN LR_BRACKET full_column_name_list RR_BRACKET RR_BRACKET
    ;



// https://msdn.microsoft.com/en-us/library/ms190312.aspx
rowset_function
    : OPENROWSET LR_BRACKET providerName=stringtext COMMA connectionString=stringtext COMMA sql=stringtext RR_BRACKET
    | OPENROWSET LR_BRACKET BULK data_file=stringtext COMMA buk_extended_options RR_BRACKET
    ;

buk_extended_options 
    : bulk_options 
    | id_
    ;

// runtime check.
bulk_option
    : id_ EQUAL bulk_option_value= decimal_string
    ;

derived_table
    : subquery
    | LR_BRACKET subqueries RR_BRACKET
    | table_value_constructor
    | LR_BRACKET table_value_constructor RR_BRACKET
    ;

subqueries : subquery (UNION ALL subquery)*;

function_call
    : ranking_windowed_function                         //#RANKING_WINDOWED_FUNC
    | aggregate_windowed_function                       //#AGGREGATE_WINDOWED_FUNC
    | analytic_windowed_function                        //#ANALYTIC_WINDOWED_FUNC
    | built_in_functions                                // #BUILT_IN_FUNC
    | scalar_function_name LR_BRACKET expression_list? RR_BRACKET     //#SCALAR_FUNCTION
    | freetext_function                                 //#FREE_TEXT
    | partition_function                                //#PARTITION_FUNC
    | hierarchyid_static_method                         //#HIERARCHYID_METHOD
    ;

partition_function
    : (database_id DOT)? DOLLAR_PARTITION DOT function_id LR_BRACKET expression RR_BRACKET
    ;

freetext_function
    : containstable_freetexttable LR_BRACKET freetext_table_andcolumn_names COMMA expression_language (COMMA expression)? RR_BRACKET
    | semantic_table LR_BRACKET freetext_table_andcolumn_names COMMA expression RR_BRACKET
    | SEMANTICSIMILARITYDETAILSTABLE LR_BRACKET full_table_ref COMMA name1=full_column_name COMMA expr1=expression COMMA name2=full_column_name COMMA expr2=expression RR_BRACKET
    ;

freetext_predicate
    : CONTAINS LR_BRACKET predicate_contains COMMA rule=expression RR_BRACKET
    | FREETEXT LR_BRACKET freetext_table_andcolumn_names COMMA expression_language RR_BRACKET
    ;

predicate_contains
    : full_column_name
    | full_column_names
    | STAR
    | PROPERTY LR_BRACKET full_column_name COMMA property=expression RR_BRACKET
    ;

freetext_table_andcolumn_names
    : full_table_ref COMMA (full_column_name | full_column_names | STAR )
    ;

built_in_functions
    // Metadata functions
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/app-name-transact-sql?view=sql-server-ver16
    : APP_NAME LR_BRACKET RR_BRACKET                                                                        //#APP_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/applock-mode-transact-sql?view=sql-server-ver16
    | APPLOCK_MODE LR_BRACKET database_principal=expression COMMA resource_name=expression COMMA lock_owner=expression RR_BRACKET //#APPLOCK_MODE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/applock-test-transact-sql?view=sql-server-ver16
    | APPLOCK_TEST LR_BRACKET database_principal=expression COMMA resource_name=expression COMMA lockmode=expression COMMA lock_owner=expression RR_BRACKET //#APPLOCK_TEST
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/assemblyproperty-transact-sql?view=sql-server-ver16
    | ASSEMBLYPROPERTY LR_BRACKET assemblyName=expression COMMA propertyName=expression RR_BRACKET          //#ASSEMBLYPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/col-length-transact-sql?view=sql-server-ver16
    | COL_LENGTH LR_BRACKET table=expression COMMA column=expression RR_BRACKET                             //#COL_LENGTH
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/col-name-transact-sql?view=sql-server-ver16
    | COL_NAME LR_BRACKET table=expression COMMA column=expression RR_BRACKET                               //#COL_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/columnproperty-transact-sql?view=sql-server-ver16
    | COLUMNPROPERTY LR_BRACKET idExpression=expression COMMA column=expression COMMA property=expression RR_BRACKET  //#COLUMNPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/databasepropertyex-transact-sql?view=sql-server-ver16
    | DATABASEPROPERTYEX LR_BRACKET database=expression COMMA property=expression RR_BRACKET                //#DATABASEPROPERTYEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/db-id-transact-sql?view=sql-server-ver16
    | DB_ID LR_BRACKET databaseName=expression? RR_BRACKET                                                  //#DB_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/db-name-transact-sql?view=sql-server-ver16
    | DB_NAME LR_BRACKET database=expression? RR_BRACKET                                                    //#DB_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/file-id-transact-sql?view=sql-server-ver16
    | FILE_ID LR_BRACKET file_name=expression RR_BRACKET                                                    //#FILE_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/file-idex-transact-sql?view=sql-server-ver16
    | FILE_IDEX LR_BRACKET file_name=expression RR_BRACKET                                                  //#FILE_IDEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/file-name-transact-sql?view=sql-server-ver16
    | FILE_NAME LR_BRACKET file=expression RR_BRACKET                                                       //#FILE_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/filegroup-id-transact-sql?view=sql-server-ver16
    | FILEGROUP_ID LR_BRACKET filegroup_name=expression RR_BRACKET                                          //#FILEGROUP_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/filegroup-name-transact-sql?view=sql-server-ver16
    | FILEGROUP_NAME LR_BRACKET filegroup_id=expression RR_BRACKET                                          //#FILEGROUP_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/filegroupproperty-transact-sql?view=sql-server-ver16
    | FILEGROUPPROPERTY LR_BRACKET filegroup=expression COMMA property=expression RR_BRACKET                //#FILEGROUPPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/fileproperty-transact-sql?view=sql-server-ver16
    | FILEPROPERTY LR_BRACKET file=expression COMMA property=expression RR_BRACKET                          //#FILEPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/filepropertyex-transact-sql?view=sql-server-ver16
    | FILEPROPERTYEX LR_BRACKET name=expression COMMA property=expression RR_BRACKET                        //#FILEPROPERTYEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/fulltextcatalogproperty-transact-sql?view=sql-server-ver16
    | FULLTEXTCATALOGPROPERTY LR_BRACKET catalog=expression COMMA property=expression RR_BRACKET            //#FULLTEXTCATALOGPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/fulltextserviceproperty-transact-sql?view=sql-server-ver16
    | FULLTEXTSERVICEPROPERTY LR_BRACKET property=expression RR_BRACKET                                     //#FULLTEXTSERVICEPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/index-col-transact-sql?view=sql-server-ver16
    | INDEX_COL LR_BRACKET tableOrView=expression COMMA index=expression COMMA key=expression RR_BRACKET                                        //#INDEX_COL
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/indexkey-property-transact-sql?view=sql-server-ver16
    | INDEXKEY_PROPERTY LR_BRACKET object=expression COMMA index=expression COMMA key=expression COMMA property=expression RR_BRACKET           //#INDEXKEY_PROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/indexproperty-transact-sql?view=sql-server-ver16
    | INDEXPROPERTY LR_BRACKET object=expression COMMA index_or_statistics=expression COMMA property=expression RR_BRACKET                      //#INDEXPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/next-value-for-transact-sql?view=sql-server-ver16
    | NEXT VALUE FOR sequenceName=full_table_ref ( OVER LR_BRACKET order_by_clause RR_BRACKET )?                                                //#NEXT_VALUE_FOR
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/object-definition-transact-sql?view=sql-server-ver16
    | OBJECT_DEFINITION LR_BRACKET object=expression RR_BRACKET                                                                                 //#OBJECT_DEFINITION
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/object-id-transact-sql?view=sql-server-ver16
    | OBJECT_ID LR_BRACKET objectName=expression ( COMMA object_type=expression )? RR_BRACKET                                                   //#OBJECT_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/object-name-transact-sql?view=sql-server-ver16
    | OBJECT_NAME LR_BRACKET object=expression ( COMMA database=expression )? RR_BRACKET                                                        //#OBJECT_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/object-schema-name-transact-sql?view=sql-server-ver16
    | OBJECT_SCHEMA_NAME LR_BRACKET target_object=expression ( COMMA database=expression )? RR_BRACKET                                          //#OBJECT_SCHEMA_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/objectproperty-transact-sql?view=sql-server-ver16
    | OBJECTPROPERTY LR_BRACKET idExpression=expression COMMA property=expression RR_BRACKET                                                              //#OBJECTPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/objectpropertyex-transact-sql?view=sql-server-ver16
    | OBJECTPROPERTYEX LR_BRACKET idExpression=expression COMMA property=expression RR_BRACKET                                                            //#OBJECTPROPERTYEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/original-db-name-transact-sql?view=sql-server-ver16
    | ORIGINAL_DB_NAME LR_BRACKET RR_BRACKET                                                                                                    //#ORIGINAL_DB_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/parsename-transact-sql?view=sql-server-ver16
    | PARSENAME LR_BRACKET objectName=expression COMMA object_piece=expression RR_BRACKET                                                       //#PARSENAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/schema-id-transact-sql?view=sql-server-ver16
    | SCHEMA_ID LR_BRACKET schemaName=expression? RR_BRACKET                                                                                    //#SCHEMA_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/schema-name-transact-sql?view=sql-server-ver16
    | SCHEMA_NAME LR_BRACKET schemaId=expression? RR_BRACKET                                                                                    //#SCHEMA_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/scope-identity-transact-sql?view=sql-server-ver16
    | SCOPE_IDENTITY LR_BRACKET RR_BRACKET                                                                                                      //#SCOPE_IDENTITY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/serverproperty-transact-sql?view=sql-server-ver16
    | SERVERPROPERTY LR_BRACKET property=expression RR_BRACKET                                                                                  //#SERVERPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/stats-date-transact-sql?view=sql-server-ver16
    | STATS_DATE LR_BRACKET target_object=expression COMMA stats=expression RR_BRACKET                                                          //#STATS_DATE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/type-id-transact-sql?view=sql-server-ver16
    | TYPE_ID LR_BRACKET type_name=expression RR_BRACKET                                                                                        //#TYPE_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/type-name-transact-sql?view=sql-server-ver16
    | TYPE_NAME LR_BRACKET type=expression RR_BRACKET                                                                                           //#TYPE_NAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/typeproperty-transact-sql?view=sql-server-ver16
    | TYPEPROPERTY LR_BRACKET type=expression COMMA property=expression RR_BRACKET                                                              //#TYPEPROPERTY
    // String functions
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/ascii-transact-sql?view=sql-server-ver16
    | ASCII LR_BRACKET character_expression=expression RR_BRACKET                                                                               //#ASCII
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/char-transact-sql?view=sql-server-ver16
    | CHAR LR_BRACKET integer_expression=expression RR_BRACKET                                                                                  //#CHAR
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/charindex-transact-sql?view=sql-server-ver16
    | CHARINDEX LR_BRACKET expressionToFind=expression COMMA expressionToSearch=expression ( COMMA start_location=expression )? RR_BRACKET      //#CHARINDEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/concat-transact-sql?view=sql-server-ver16
    | CONCAT LR_BRACKET expressions RR_BRACKET                                                                                                  //#CONCAT
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/concat-ws-transact-sql?view=sql-server-ver16
    | CONCAT_WS LR_BRACKET expressions RR_BRACKET                                                                                               //#CONCAT_WS
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/difference-transact-sql?view=sql-server-ver16
    | DIFFERENCE LR_BRACKET character_expression_1=expression COMMA character_expression_2=expression RR_BRACKET                                //#DIFFERENCE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/format-transact-sql?view=sql-server-ver16
    | FORMAT LR_BRACKET expressions RR_BRACKET                                                                                                  //#FORMAT
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/left-transact-sql?view=sql-server-ver16
    | LEFT LR_BRACKET character_expression=expression COMMA integer_expression=expression RR_BRACKET                                            //#LEFT
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/len-transact-sql?view=sql-server-ver16
    | LEN LR_BRACKET string_expression=expression RR_BRACKET                                                                                    //#LEN
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/lower-transact-sql?view=sql-server-ver16
    | LOWER LR_BRACKET character_expression=expression RR_BRACKET                                                                               //#LOWER
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/ltrim-transact-sql?view=sql-server-ver16
    | LTRIM LR_BRACKET character_expression=expression RR_BRACKET                                                                               //#LTRIM
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/nchar-transact-sql?view=sql-server-ver16
    | NCHAR LR_BRACKET integer_expression=expression RR_BRACKET                                                                                 //#NCHAR
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/patindex-transact-sql?view=sql-server-ver16
    | PATINDEX LR_BRACKET pattern=expression COMMA string_expression=expression RR_BRACKET                                                      //#PATINDEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/quotename-transact-sql?view=sql-server-ver16
    | QUOTENAME LR_BRACKET character_string=expression ( COMMA quote_character=expression )? RR_BRACKET                                         //#QUOTENAME
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/replace-transact-sql?view=sql-server-ver16
    | REPLACE LR_BRACKET input=expression COMMA replacing=expression COMMA with=expression RR_BRACKET                                           //#REPLACE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/replicate-transact-sql?view=sql-server-ver16
    | REPLICATE LR_BRACKET string_expression=expression COMMA integer_expression=expression RR_BRACKET                                          //#REPLICATE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/reverse-transact-sql?view=sql-server-ver16
    | REVERSE LR_BRACKET string_expression=expression RR_BRACKET                                                                                //#REVERSE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/right-transact-sql?view=sql-server-ver16
    | RIGHT LR_BRACKET character_expression=expression COMMA integer_expression=expression RR_BRACKET                                           //#RIGHT
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/rtrim-transact-sql?view=sql-server-ver16
    | RTRIM LR_BRACKET character_expression=expression RR_BRACKET                                                                               //#RTRIM
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/soundex-transact-sql?view=sql-server-ver16
    | SOUNDEX LR_BRACKET character_expression=expression RR_BRACKET                                                                             //#SOUNDEX
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/space-transact-sql?view=sql-server-ver16
    | SPACE_KEYWORD LR_BRACKET integer_expression=expression RR_BRACKET                                                                         //#SPACE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/str-transact-sql?view=sql-server-ver16
    | STR LR_BRACKET float_expression=expression ( COMMA length_expression=expression ( COMMA decimal_expr=expression )? )? RR_BRACKET          //#STR
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/string-agg-transact-sql?view=sql-server-ver16
    | STRING_AGG LR_BRACKET expr=expression COMMA separator=expression RR_BRACKET (WITHIN GROUP LR_BRACKET order_by_clause RR_BRACKET)?         //#STRINGAGG
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/string-escape-transact-sql?view=sql-server-ver16
    | STRING_ESCAPE LR_BRACKET text_=expression COMMA type_=expression RR_BRACKET                                                               //#STRING_ESCAPE
    // https://msdn.microsoft.com/fr-fr/library/ms188043.aspx
    | STUFF LR_BRACKET str=expression COMMA from=decimal COMMA to=decimal COMMA str_with=expression RR_BRACKET                                  //#STUFF
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/substring-transact-sql?view=sql-server-ver16
    | SUBSTRING LR_BRACKET string_expression=expression COMMA start_=expression COMMA length=expression RR_BRACKET                              //#SUBSTRING
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/translate-transact-sql?view=sql-server-ver16
    | TRANSLATE LR_BRACKET inputString=expression COMMA characters=expression COMMA translations=expression RR_BRACKET                          //#TRANSLATE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/trim-transact-sql?view=sql-server-ver16
    | TRIM LR_BRACKET ( characters=expression FROM )? string_=expression RR_BRACKET                                                             ///#TRIM
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/unicode-transact-sql?view=sql-server-ver16
    | UNICODE LR_BRACKET ncharacter_expression=expression RR_BRACKET                                                                            //#UNICODE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/upper-transact-sql?view=sql-server-ver16
    | UPPER LR_BRACKET character_expression=expression RR_BRACKET                                                                               //#UPPER
    // System functions
    // https://msdn.microsoft.com/en-us/library/ms173784.aspx
    | BINARY_CHECKSUM LR_BRACKET expression_or_star RR_BRACKET                                                                         //#BINARY_CHECKSUM
    // https://msdn.microsoft.com/en-us/library/ms189788.aspx
    | CHECKSUM LR_BRACKET expression_or_star RR_BRACKET                                                                                //#CHECKSUM
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/compress-transact-sql?view=sql-server-ver16
    | COMPRESS LR_BRACKET expr=expression RR_BRACKET                                                                                            //#COMPRESS
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/connectionproperty-transact-sql?view=sql-server-ver16
    | CONNECTIONPROPERTY LR_BRACKET cnx_property=stringtext RR_BRACKET                                                                              //#CONNECTIONPROPERTY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/context-info-transact-sql?view=sql-server-ver16
    | CONTEXT_INFO LR_BRACKET RR_BRACKET                                                                                                        //#CONTEXT_INFO
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/current-request-id-transact-sql?view=sql-server-ver16
    | CURRENT_REQUEST_ID LR_BRACKET RR_BRACKET                                                                                                  //#CURRENT_REQUEST_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/current-transaction-id-transact-sql?view=sql-server-ver16
    | CURRENT_TRANSACTION_ID LR_BRACKET RR_BRACKET                                                                                              //#CURRENT_TRANSACTION_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/decompress-transact-sql?view=sql-server-ver16
    | DECOMPRESS LR_BRACKET expr=expression RR_BRACKET                                                                                          //#DECOMPRESS
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/error-line-transact-sql?view=sql-server-ver16
    | ERROR_LINE LR_BRACKET RR_BRACKET                                                                                                          //#ERROR_LINE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/error-message-transact-sql?view=sql-server-ver16
    | ERROR_MESSAGE LR_BRACKET RR_BRACKET                                                                                                       //#ERROR_MESSAGE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/error-number-transact-sql?view=sql-server-ver16
    | ERROR_NUMBER LR_BRACKET RR_BRACKET                                                                                                        //#ERROR_NUMBER
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/error-procedure-transact-sql?view=sql-server-ver16
    | ERROR_PROCEDURE LR_BRACKET RR_BRACKET                                                                                                     //#ERROR_PROCEDURE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/error-severity-transact-sql?view=sql-server-ver16
    | ERROR_SEVERITY LR_BRACKET RR_BRACKET                                                                                                      //#ERROR_SEVERITY
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/error-state-transact-sql?view=sql-server-ver16
    | ERROR_STATE LR_BRACKET RR_BRACKET                                                                                                         //#ERROR_STATE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/formatmessage-transact-sql?view=sql-server-ver16
    | FORMATMESSAGE LR_BRACKET format_argument COMMA expressions RR_BRACKET                                                                     //#FORMATMESSAGE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/get-filestream-transaction-context-transact-sql?view=sql-server-ver16
    | GET_FILESTREAM_TRANSACTION_CONTEXT LR_BRACKET RR_BRACKET                                                                                  //#GET_FILESTREAM_TRANSACTION_CONTEXT
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/getansinull-transact-sql?view=sql-server-ver16
    | GETANSINULL LR_BRACKET (database_name=stringtext)? RR_BRACKET                                                                             //#GETANSINULL
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/host-id-transact-sql?view=sql-server-ver16
    | HOST_ID LR_BRACKET RR_BRACKET                                                                                                             //#HOST_ID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/host-name-transact-sql?view=sql-server-ver16
    | HOST_NAME LR_BRACKET RR_BRACKET                                                                                                           //#HOST_NAME
    // https://msdn.microsoft.com/en-us/library/ms184325.aspx
    | ISNULL LR_BRACKET left=expression COMMA right=expression RR_BRACKET                                                                       //#ISNULL
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/isnumeric-transact-sql?view=sql-server-ver16
    | ISNUMERIC LR_BRACKET expression RR_BRACKET                                                                                                //#ISNUMERIC
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/min-active-rowversion-transact-sql?view=sql-server-ver16
    | MIN_ACTIVE_ROWVERSION LR_BRACKET RR_BRACKET                                                                                               //#MIN_ACTIVE_ROWVERSION
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/newid-transact-sql?view=sql-server-ver16
    | NEWID LR_BRACKET RR_BRACKET                                                                                                               //#NEWID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/newsequentialid-transact-sql?view=sql-server-ver16
    | NEWSEQUENTIALID LR_BRACKET RR_BRACKET                                                                                                     //#NEWSEQUENTIALID
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/rowcount-big-transact-sql?view=sql-server-ver16
    | ROWCOUNT_BIG LR_BRACKET RR_BRACKET                                                                                                        //#ROWCOUNT_BIG
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/session-context-transact-sql?view=sql-server-ver16
    | SESSION_CONTEXT LR_BRACKET session_key=stringtext RR_BRACKET                                                                                      //#SESSION_CONTEXT
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/xact-state-transact-sql?view=sql-server-ver16
    | XACT_STATE LR_BRACKET RR_BRACKET                                                                                                          //#XACT_STATE
    // https://msdn.microsoft.com/en-us/library/hh231076.aspx
    // https://msdn.microsoft.com/en-us/library/ms187928.aspx
    | CAST LR_BRACKET expression AS data_type RR_BRACKET                                                                                        //#CAST
    | TRY_CAST LR_BRACKET expression AS data_type RR_BRACKET                                                                                    //#TRY_CAST
    | CONVERT LR_BRACKET convert_data_type=data_type COMMA expression2 RR_BRACKET                                                               //#CONVERT
    // https://msdn.microsoft.com/en-us/library/ms190349.aspx
    | COALESCE LR_BRACKET expression_list RR_BRACKET                                                                                            //#COALESCE
    //https://infocenter.sybase.com/help/index.jsp?topic=/com.sybase.infocenter.dc36271.1572/html/blocks/CJADIDHD.htm
    | CURRENT_DATE LR_BRACKET RR_BRACKET                                                                                                        //#CURRENT_DATE
    // https://msdn.microsoft.com/en-us/library/ms188751.aspx
    | CURRENT_TIMESTAMP                                                                                                                         //#CURRENT_TIMESTAMP
    // https://msdn.microsoft.com/en-us/library/ms176050.aspx
    | CURRENT_USER                                                                                                                              //#CURRENT_USER
    // https://msdn.microsoft.com/en-us/library/ms186819.aspx
    | DATEADD LR_BRACKET datepart=id_simple COMMA number=expression COMMA date=expression RR_BRACKET                                                   //#DATEADD
    // https://msdn.microsoft.com/en-us/library/ms189794.aspx
    | DATEDIFF LR_BRACKET datepart=id_simple COMMA date_first=expression COMMA date_second=expression RR_BRACKET                                       //#DATEDIFF
    // https://msdn.microsoft.com/en-us/library/ms174395.aspx
    | DATENAME LR_BRACKET datepart=id_simple COMMA date=expression RR_BRACKET                                                                          //#DATENAME
    // https://msdn.microsoft.com/en-us/library/ms174420.aspx
    | DATEPART LR_BRACKET datepart=id_simple COMMA date=expression RR_BRACKET                                                                          //#DATEPART
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/getdate-transact-sql
    | GETDATE LR_BRACKET RR_BRACKET                                                                                                             //#GETDATE
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/getdate-transact-sql
    | GETUTCDATE LR_BRACKET RR_BRACKET                                                                                                          //#GETUTCDATE
    // https://msdn.microsoft.com/en-us/library/ms189838.aspx
    | IDENTITY LR_BRACKET data_type (COMMA seed=decimal)? (COMMA increment=decimal)? RR_BRACKET                                                 //#IDENTITY
    // https://msdn.microsoft.com/en-us/library/bb839514.aspx
    | MIN_ACTIVE_ROWVERSION LR_BRACKET RR_BRACKET                                                                                               //#MIN_ACTIVE_ROWVERSION
    // https://msdn.microsoft.com/en-us/library/ms177562.aspx
    | NULLIF LR_BRACKET left=expression COMMA right=expression RR_BRACKET                                                                       //#NULLIF
    // https://msdn.microsoft.com/en-us/library/ms177587.aspx
    | SESSION_USER                                                                                                                              //#SESSION_USER
    // https://msdn.microsoft.com/en-us/library/ms179930.aspx
    | SYSTEM_USER                                                                                                                               //#SYSTEM_USER
    | USER                                                                                                                                      //#USER
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/parse-transact-sql
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/try-parse-transact-sql
    | PARSE LR_BRACKET str=expression AS data_type ( USING culture=expression )? RR_BRACKET                                                     //#PARSE
    // https://docs.microsoft.com/en-us/sql/t-sql/xml/xml-data-type-methods
    | xml_data_type_methods                                                                                                                     //#XML_DATA_TYPE_FUNC
    // https://docs.microsoft.com/en-us/sql/t-sql/functions/logical-functions-iif-transact-sql
    | IIF LR_BRACKET cond=search_condition COMMA left=expression COMMA right=expression RR_BRACKET                                              //#IIF
    ;

expression_or_star :  STAR | expressions;
format_argument 
    : decimal 
    | stringtext 
    | local_id;


xml_data_type_methods
    : value_method
    | query_method
    | exist_method
    | modify_method
    ;

value_method
    : (loc=local_id | value=full_column_name | eventdata=EVENTDATA LR_BRACKET RR_BRACKET  | query=query_method | LR_BRACKET subquery RR_BRACKET) DOT call=value_call
    ;

value_call
    :  VALUE LR_BRACKET xquery=stringtext COMMA sqltype=stringtext RR_BRACKET
    ;

query_method
    : (loc=local_id | value=full_column_name | LR_BRACKET subquery RR_BRACKET ) DOT call=query_call
    ;

query_call
    : QUERY LR_BRACKET xquery=stringtext RR_BRACKET
    ;

exist_method
    : (loc=local_id | value=full_column_name | LR_BRACKET subquery RR_BRACKET) DOT call=exist_call
    ;

exist_call
    : EXIST LR_BRACKET xquery=stringtext RR_BRACKET
    ;

modify_method
    : (loc=local_id | value=full_column_name | LR_BRACKET subquery RR_BRACKET) DOT call=modify_call
    ;

modify_call
    : MODIFY LR_BRACKET xml_dml=stringtext RR_BRACKET
    ;

hierarchyid_call
    : GETANCESTOR LR_BRACKET n=expression RR_BRACKET
    | GETDESCENDANT LR_BRACKET child1=expression COMMA child2=expression RR_BRACKET
    | GETLEVEL LR_BRACKET RR_BRACKET
    | ISDESCENDANTOF LR_BRACKET parent_=expression RR_BRACKET
    | GETREPARENTEDVALUE LR_BRACKET oldroot=expression COMMA newroot=expression RR_BRACKET
    | TOSTRING LR_BRACKET RR_BRACKET
    ;

hierarchyid_static_method
    : HIERARCHYID DOUBLE_COLON (GETROOT LR_BRACKET RR_BRACKET | PARSE LR_BRACKET input=expression RR_BRACKET)
    ;

nodes_method
    : (loc=local_id | value=full_column_name | LR_BRACKET subquery RR_BRACKET) DOT NODES LR_BRACKET xquery=stringtext RR_BRACKET
    ;


switch_section
    : WHEN when_expr=expression THEN then_expr=expression
    ;

switch_search_condition_section
    : WHEN search_condition THEN expression
    ;

as_column_alias
    : AS? column_alias
    ;

as_table_alias
    : AS? table_alias
    ;

table_alias
    : id_
    ;

// https://msdn.microsoft.com/en-us/library/ms187373.aspx
with_table_hints
    : WITH LR_BRACKET table_hint (COMMA? table_hint)* RR_BRACKET
    ;

deprecated_table_hint
    : LR_BRACKET table_hint RR_BRACKET
    ;

// https://infocenter-archive.sybase.com/help/index.jsp?topic=/com.sybase.infocenter.dc00938.1502/html/locking/locking103.htm
// https://infocenter-archive.sybase.com/help/index.jsp?topic=/com.sybase.dc32300_1250/html/sqlug/sqlug792.htm
// https://infocenter-archive.sybase.com/help/index.jsp?topic=/com.sybase.dc36271_36272_36273_36274_1250/html/refman/X35229.htm
// Legacy hint with no parenthesis and no WITH keyword. Actually conflicts with table alias name except for holdlock which is
// a reserved keyword in this grammar. We might want a separate sybase grammar variant.
sybase_legacy_hints
    : sybase_legacy_hint+
    ;

sybase_legacy_hint
    : HOLDLOCK
    | NOHOLDLOCK
    | READPAST
    | SHARED
    ;

// For simplicity, we don't build subsets for INSERT/UPDATE/DELETE/SELECT/MERGE
// which means the grammar accept slightly more than the what the specification (documentation) says.
table_hint
    : NOEXPAND
    | INDEX hint_index
    | FORCESEEK ( LR_BRACKET index_value LR_BRACKET column_name_list RR_BRACKET RR_BRACKET )?
    | FORCESCAN
    | HOLDLOCK
    | NOLOCK
    | NOWAIT
    | PAGLOCK
    | READCOMMITTED
    | READCOMMITTEDLOCK
    | READPAST
    | READUNCOMMITTED
    | REPEATABLEREAD
    | ROWLOCK
    | SERIALIZABLE
    | SNAPSHOT
    | SPATIAL_WINDOW_MAX_CELLS EQUAL decimal
    | TABLOCK
    | TABLOCKX
    | UPDLOCK
    | XLOCK
    | KEEPIDENTITY
    | KEEPDEFAULTS
    | IGNORE_CONSTRAINTS
    | IGNORE_TRIGGERS
    ;

hint_index 
    : LR_BRACKET index_values RR_BRACKET
    | EQUAL LR_BRACKET index_value RR_BRACKET
    | EQUAL index_value // examples in the doc include this syntax
    ;

index_value
    : id_ 
    | decimal
    ;

column_alias
    : id_
    | stringtext
    ;

// https://msdn.microsoft.com/en-us/library/ms189798.aspx
ranking_windowed_function
    : ranking_windowed LR_BRACKET RR_BRACKET over_clause
    | NTILE LR_BRACKET expression RR_BRACKET over_clause
    ;

// https://msdn.microsoft.com/en-us/library/ms173454.aspx
aggregate_windowed_function
    : agg_function LR_BRACKET all_distinct_expression RR_BRACKET over_clause?
    | count_count_big LR_BRACKET all_distinct_expression_or_star RR_BRACKET over_clause?
    | CHECKSUM_AGG LR_BRACKET all_distinct_expression RR_BRACKET
    | GROUPING LR_BRACKET expression RR_BRACKET
    | GROUPING_ID LR_BRACKET expression_list RR_BRACKET
    ;

all_distinct_expression_or_star : STAR | all_distinct_expression;

// https://docs.microsoft.com/en-us/sql/t-sql/functions/analytic-functions-transact-sql
analytic_windowed_function
    : first_last_value LR_BRACKET expression RR_BRACKET over_clause
    | lag_lead LR_BRACKET expression  (COMMA expression2 )? RR_BRACKET over_clause
    | cume_percent LR_BRACKET RR_BRACKET OVER LR_BRACKET (PARTITION BY expression_list)? order_by_clause RR_BRACKET
    | percentil LR_BRACKET expression RR_BRACKET WITHIN GROUP LR_BRACKET order_by_clause RR_BRACKET OVER LR_BRACKET (PARTITION BY expression_list)? RR_BRACKET
    ;

all_distinct_expression
    : all_distinct? expression
    ;

// https://msdn.microsoft.com/en-us/library/ms189461.aspx
over_clause
    : OVER LR_BRACKET (PARTITION BY expression_list)? order_by_clause? row_or_range_clause? RR_BRACKET
    ;

row_or_range_clause
    : row_range window_frame_extent
    ;

window_frame_extent
    : window_frame_preceding
    | BETWEEN left=window_frame_bound AND right=window_frame_bound
    ;

window_frame_bound
    : window_frame_preceding
    | window_frame_following
    ;

window_frame_preceding
    : UNBOUNDED PRECEDING
    | decimal PRECEDING
    | CURRENT ROW
    ;

window_frame_following
    : UNBOUNDED FOLLOWING
    | decimal FOLLOWING
    ;

create_database_option_list : create_database_option ( COMMA create_database_option )*;
create_database_option
    : FILESTREAM database_filestream_options
    | db_chaining_set
    | trustworthy_set
    | default_language_set
    | default_fulltext_language_set
    | nested_triggers_set
    | transform_noise_words_set
    | two_digit_year_cutoff_set
    ;

database_filestream_options : database_filestream_option (COMMA database_filestream_option)*;

database_filestream_option
    : non_transacted_access_set
    | directory_name_set         
    ;

db_chaining_set : DB_CHAINING on_off;
trustworthy_set : TRUSTWORTHY on_off;
default_language_set : DEFAULT_LANGUAGE EQUAL language_setting_value;
default_fulltext_language_set : DEFAULT_FULLTEXT_LANGUAGE EQUAL language_setting;
nested_triggers_set : NESTED_TRIGGERS EQUAL on_off;
transform_noise_words_set : TRANSFORM_NOISE_WORDS EQUAL on_off;
two_digit_year_cutoff_set : TWO_DIGIT_YEAR_CUTOFF EQUAL decimal;

db_option
    : db_chaining_set
    | trustworthy_set
    | default_language_set
    | default_fulltext_language_set
    | nested_triggers_set
    | transform_noise_words_set
    | two_digit_year_cutoff_set
    ;

directory_name_set : DIRECTORY_NAME EQUAL stringtext;
non_transacted_access_set : NON_TRANSACTED_ACCESS EQUAL off_read_only_full;

database_file
    : file_group | file_spec
    ;

file_group
    : FILEGROUP file_group_id
     ( CONTAINS FILESTREAM )?
     ( DEFAULT )?
     ( CONTAINS MEMORY_OPTIMIZED_DATA )?
     file_specs
    ;

file_spec
    : LR_BRACKET
      name_set COMMA?
      filename_set COMMA?
      ( size_set COMMA? )?
      ( maxsize_set COMMA? )?
      ( filegrowth_set COMMA? )?
      RR_BRACKET
    ;

cursor_name
    : id_
    | local_id
    ;

null_or_default
    :(null_notnull | DEFAULT constant_expression (COLLATE id_)? (WITH VALUES)?)
    ;

scalar_function_name
    : func_proc_name_server_database_schema
    | scalar_function_name_enum
    ;

begin_conversation_timer
    : BEGIN CONVERSATION TIMER LR_BRACKET conversation=local_id RR_BRACKET TIMEOUT EQUAL timespan
    ;

begin_conversation_dialog
    : BEGIN DIALOG (CONVERSATION)? dialog_handle=local_id
      FROM SERVICE initiator_service_name=service_name_expr
      TO SERVICE target_service_name=service_name_expr (COMMA service_broker_guid=stringtext)?
      ON CONTRACT contract_name_expression
      (WITH
        (relayed_conversation EQUAL group=local_id COMMA?)?
        (LIFETIME EQUAL decimal_local_id COMMA?)?
        (ENCRYPTION EQUAL on_off)? 
      )?      
    ;

contract_name_expression
    : id_ 
    | expression
    ;

service_name_expr
    : id_ 
    | expression
    ;

end_conversation
    : END CONVERSATION conversation_handle=local_id SEMI?
      (
        WITH 
            (
                ERROR EQUAL faliure_code=string_local_id 
                DESCRIPTION EQUAL failure_text=string_local_id
            )? CLEANUP? 
      )?
    ;

waitfor_conversation
    : WAITFOR? LR_BRACKET get_conversation RR_BRACKET (COMMA? TIMEOUT timeout=timespan)?
    ;

get_conversation
    :GET CONVERSATION GROUP conversation_group=string_local_id FROM queue=database_schema_queue_ref
    ;

send_conversation
    : SEND ON CONVERSATION conversation_handle=string_local_id
      MESSAGE TYPE messageTypeName=expression
      (LR_BRACKET messageBodyEexpression=string_local_id RR_BRACKET )?
    ;

// https://msdn.microsoft.com/en-us/library/ms187752.aspx
// TODO: implement runtime check or add new tokens.

data_type
    : scaled=data_type_scaled LR_BRACKET MAX RR_BRACKET
    | ext_type_id LR_BRACKET decimal_scale_prec RR_BRACKET
    | ext_type_id LR_BRACKET scale=decimal RR_BRACKET
    | ext_type_id IDENTITY (LR_BRACKET identity_seed RR_BRACKET)?
    | double_prec=DOUBLE PRECISION?
    | unscaled_type_id
    ;

decimal_scale_prec : scale=decimal COMMA prec=decimal;
identity_seed : seed=decimal COMMA inc=decimal;

default_value
    : NULL_
    | DEFAULT
    | constant
    ;

// https://msdn.microsoft.com/en-us/library/ms179899.aspx
constant
    : stringtext // string, datetime or uniqueidentifier
    | binary_
    | real
    | sign? decimal_float
    | sign? dollar= DOLLAR decimal_float       // money
    | parameter
    ;

decimal_float : decimal | float;

sign
    : PLUS
    | MINUS
    ;

keyword
    : ABORT
    | ABSOLUTE
    | ACCENT_SENSITIVITY
    | ACCESS
    | ACTION
    | ACTIVATION
    | ACTIVE
    | ADD   // ?
    | ADDRESS
    | AES_128
    | AES_192
    | AES_256
    | AFFINITY
    | AFTER
    | AGGREGATE
    | ALGORITHM
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS
    | ALLOW_PAGE_LOCKS
    | ALLOW_ROW_LOCKS
    | ALLOW_SNAPSHOT_ISOLATION
    | ALLOWED
    | ALWAYS
    | ANSI_DEFAULTS
    | ANSI_NULL_DEFAULT
    | ANSI_NULL_DFLT_OFF
    | ANSI_NULL_DFLT_ON
    | ANSI_NULLS
    | ANSI_PADDING
    | ANSI_WARNINGS
    | APP_NAME
    | APPLICATION_LOG
    | APPLOCK_MODE
    | APPLOCK_TEST
    | APPLY
    | ARITHABORT
    | ARITHIGNORE
    | ASCII
    | ASSEMBLY
    | ASSEMBLYPROPERTY
    | AT_KEYWORD
    | AUDIT
    | AUDIT_GUID
    | AUTO
    | AUTO_CLEANUP
    | AUTO_CLOSE
    | AUTO_CREATE_STATISTICS
    | AUTO_DROP
    | AUTO_SHRINK
    | AUTO_UPDATE_STATISTICS
    | AUTO_UPDATE_STATISTICS_ASYNC
    | AUTOGROW_ALL_FILES
    | AUTOGROW_SINGLE_FILE
    | AVAILABILITY
    | AVG
    | BACKUP_PRIORITY
    | BASE64
    | BEGIN_DIALOG
    | BIGINT
    | BINARY_KEYWORD
    | BINARY_CHECKSUM
    | BINDING
    | BLOB_STORAGE
    | BROKER
    | BROKER_INSTANCE
    | BULK_LOGGED
    | CALLER
    | CAP_CPU_PERCENT
    | CAST
    | TRY_CAST
    | CATALOG
    | CATCH
    | CHANGE
    | CHANGE_RETENTION
    | CHANGE_TRACKING
    | CHAR
    | CHARINDEX
    | CHECKSUM
    | CHECKSUM_AGG
    | CLEANUP
    | COL_LENGTH
    | COL_NAME
    | COLLECTION
    | COLUMN_ENCRYPTION_KEY
    | COLUMN_MASTER_KEY
    | COLUMNPROPERTY
    | COLUMNS
    | COLUMNSTORE
    | COLUMNSTORE_ARCHIVE
    | COMMITTED
    | COMPATIBILITY_LEVEL
    | COMPRESS_ALL_ROW_GROUPS
    | COMPRESSION_DELAY
    | CONCAT
    | CONCAT_WS
    | CONCAT_NULL_YIELDS_NULL
    | CONTENT
    | CONTROL
    | COOKIE
    | COUNT
    | COUNT_BIG
    | COUNTER
    | CPU
    | CREATE_NEW
    | CREATION_DISPOSITION
    | CREDENTIAL
    | CRYPTOGRAPHIC
    | CUME_DIST
    | CURSOR_CLOSE_ON_COMMIT
    | CURSOR_DEFAULT
    | DATA
    | DATABASE_PRINCIPAL_ID
    | DATABASEPROPERTYEX
    | DATE_CORRELATION_OPTIMIZATION
    | DATEADD
    | DATEDIFF
    | DATENAME
    | DATEPART
    | DAYS
    | DB_CHAINING
    | DB_FAILOVER
    | DB_ID
    | DB_NAME
    | DECRYPTION
    | DEFAULT_DOUBLE_QUOTE
    | DEFAULT_FULLTEXT_LANGUAGE
    | DEFAULT_LANGUAGE
    | DEFINITION
    | DELAY
    | DELAYED_DURABILITY
    | DELETED
    | DENSE_RANK
    | DEPENDENTS
    | DES
    | DESCRIPTION
    | DESX
    | DETERMINISTIC
    | DHCP
    | DIALOG
    | DIFFERENCE
    | DIRECTORY_NAME
    | DISABLE
    | DISABLE_BROKER
    | DISABLED
    | DOCUMENT
    | DROP_EXISTING
    | DYNAMIC
    | ELEMENTS
    | EMERGENCY
    | EMPTY
    | ENABLE
    | ENABLE_BROKER
    | ENCRYPTED
    | ENCRYPTED_VALUE
    | ENCRYPTION
    | ENCRYPTION_TYPE
    | ENDPOINT_URL
    | ERROR_BROKER_CONVERSATIONS
    | EXCLUSIVE
    | EXECUTABLE
    | EXIST
    | EXPAND
    | EXPIRY_DATE
    | EXPLICIT
    | FAIL_OPERATION
    | FAILOVER_MODE
    | FAILURE
    | FAILURE_CONDITION_LEVEL
    | FAST
    | FAST_FORWARD
    | FILE_ID
    | FILE_IDEX
    | FILE_NAME
    | FILEGROUP
    | FILEGROUP_ID
    | FILEGROUP_NAME
    | FILEGROUPPROPERTY
    | FILEGROWTH
    | FILENAME
    | FILEPATH
    | FILEPROPERTY
    | FILEPROPERTYEX
    | FILESTREAM
    | FILTER
    | FIRST
    | FIRST_VALUE
    | FMTONLY
    | FOLLOWING
    | FORCE
    | FORCE_FAILOVER_ALLOW_DATA_LOSS
    | FORCED
    | FORCEPLAN
    | FORCESCAN
    | FORMAT
    | FORWARD_ONLY
    | FULLSCAN
    | FULLTEXT
    | FULLTEXTCATALOGPROPERTY
    | FULLTEXTSERVICEPROPERTY
    | GB
    | GENERATED
    | GETDATE
    | GETUTCDATE
    | GLOBAL
    | GO
    | GROUP_MAX_REQUESTS
    | GROUPING
    | GROUPING_ID
    | HADR
    | HASH
    | HEALTH_CHECK_TIMEOUT
    | HIDDEN_KEYWORD
    | HIGH
    | HONOR_BROKER_PRIORITY
    | HOURS
    | IDENTITY_VALUE
    | IGNORE_CONSTRAINTS
    | IGNORE_DUP_KEY
    | IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX
    | IGNORE_TRIGGERS
    | IMMEDIATE
    | IMPERSONATE
    | IMPLICIT_TRANSACTIONS
    | IMPORTANCE
    | INCLUDE_NULL_VALUES
    | INCREMENTAL
    | INDEX_COL
    | INDEXKEY_PROPERTY
    | INDEXPROPERTY
    | INITIATOR
    | INPUT
    | INSENSITIVE
    | INSERTED
    | INT
    | IP
    | ISOLATION
    | JOB
    | JSON
    | KB
    | KEEP
    | KEEPDEFAULTS
    | KEEPFIXED
    | KEEPIDENTITY
    | KEY_SOURCE
    | KEYS
    | KEYSET
    | LAG
    | LAST
    | LAST_VALUE
    | LEAD
    | LEN
    | LEVEL
    | LIST
    | LISTENER
    | LISTENER_URL
    | LOB_COMPACTION
    | LOCAL
    | LOCATION
    | LOCK
    | LOCK_ESCALATION
    | LOGIN
    | LOOP
    | LOW
    | LOWER
    | LTRIM
    | MANUAL
    | MARK
    | MASKED
    | MATERIALIZED
    | MAX
    | MAX_CPU_PERCENT
    | MAX_DOP
    | MAX_FILES
    | MAX_IOPS_PER_VOLUME
    | MAX_MEMORY_PERCENT
    | MAX_PROCESSES
    | MAX_QUEUE_READERS
    | MAX_ROLLOVER_FILES
    | MAXDOP
    | MAXRECURSION
    | MAXSIZE
    | MB
    | MEDIUM
    | MEMORY_OPTIMIZED_DATA
    | MESSAGE
    | MIN
    | MIN_ACTIVE_ROWVERSION
    | MIN_CPU_PERCENT
    | MIN_IOPS_PER_VOLUME
    | MIN_MEMORY_PERCENT
    | MINUTES
    | MIRROR_ADDRESS
    | MIXED_PAGE_ALLOCATION
    | MODE
    | MODIFY
    | MOVE
    | MULTI_USER
    | NAME
    | NCHAR
    | NESTED_TRIGGERS
    | NEW_ACCOUNT
    | NEW_BROKER
    | NEW_PASSWORD
    | NEWNAME
    | NEXT
    | NO
    | NO_TRUNCATE
    | NO_WAIT
    | NOCOUNT
    | NODES
    | NOEXEC
    | NOEXPAND
    | NOLOCK
    | NON_TRANSACTED_ACCESS
    | NORECOMPUTE
    | NORECOVERY
    | NOTIFICATIONS
    | NOWAIT
    | NTILE
    | NULL_DOUBLE_QUOTE
    | NUMANODE
    | NUMBER
    | NUMERIC_ROUNDABORT
    | OBJECT
    | OBJECT_DEFINITION
    | OBJECT_ID
    | OBJECT_NAME
    | OBJECT_SCHEMA_NAME
    | OBJECTPROPERTY
    | OBJECTPROPERTYEX
    | OFFLINE
    | OFFSET
    | OLD_ACCOUNT
    | ONLINE
    | ONLY
    | OPEN_EXISTING
    | OPENJSON
    | OPTIMISTIC
    | OPTIMIZE
    | OPTIMIZE_FOR_SEQUENTIAL_KEY
    | ORIGINAL_DB_NAME
    | OUT
    | OUTPUT
    | OVERRIDE
    | OWNER
    | OWNERSHIP
    | PAD_INDEX
    | PAGE_VERIFY
    | PAGECOUNT
    | PAGLOCK
    | PARAMETERIZATION
    | PARSENAME
    | PARSEONLY
    | PARTITION
    | PARTITIONS
    | PARTNER
    | PATH
    | PATINDEX
    | PAUSE
    | PERCENT_RANK
    | PERCENTILE_CONT
    | PERCENTILE_DISC
    | PERSIST_SAMPLE_PERCENT
    | POISON_MESSAGE_HANDLING
    | POOL
    | PORT
    | PRECEDING
    | PRIMARY_ROLE
    | PRIOR
    | PRIORITY
    | PRIORITY_LEVEL
    | PRIVATE
    | PRIVATE_KEY
    | PRIVILEGES
    | PROCEDURE_NAME
    | PROPERTY
    | PROVIDER
    | PROVIDER_KEY_NAME
    | QUERY
    | QUEUE
    | QUEUE_DELAY
    | QUOTED_IDENTIFIER
    | QUOTENAME
    | RANDOMIZED
    | RANGE
    | RANK
    | RC2
    | RC4
    | RC4_128
    | READ_COMMITTED_SNAPSHOT
    | READ_ONLY
    | READ_ONLY_ROUTING_LIST
    | READ_WRITE
    | READCOMMITTED
    | READCOMMITTEDLOCK
    | READONLY
    | READPAST
    | READUNCOMMITTED
    | READWRITE
    | REBUILD
    | RECEIVE
    | RECOMPILE
    | RECOVERY
    | RECURSIVE_TRIGGERS
    | RELATIVE
    | REMOTE
    | REMOTE_PROC_TRANSACTIONS
    | REMOTE_SERVICE_NAME
    | REMOVE
    | REORGANIZE
    | REPEATABLE
    | REPEATABLEREAD
    | REPLACE
    | REPLICA
    | REPLICATE
    | REQUEST_MAX_CPU_TIME_SEC
    | REQUEST_MAX_MEMORY_GRANT_PERCENT
    | REQUEST_MEMORY_GRANT_TIMEOUT_SEC
    | REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT
    | RESAMPLE
    | RESERVE_DISK_SPACE
    | RESOURCE
    | RESOURCE_MANAGER_LOCATION
    | RESTRICTED_USER
    | RESUMABLE
    | RETENTION
    | REVERSE
    | ROBUST
    | ROOTWORD
    | ROUTE
    | ROW
    | ROW_NUMBER
    | ROWGUID
    | ROWLOCK
    | ROWS
    | RTRIM
    | SAMPLE
    | SCHEMA_ID
    | SCHEMA_NAME
    | SCHEMABINDING
    | SCOPE_IDENTITY
    | SCOPED
    | SCROLL
    | SCROLL_LOCKS
    | SEARCH
    | SECONDARY
    | SECONDARY_ONLY
    | SECONDARY_ROLE
    | SECONDS
    | SECRET
    | SECURABLES
    | SECURITY
    | SECURITY_LOG
    | SEEDING_MODE
    | SELF
    | SEMI_SENSITIVE
    | SEND
    | SENT
    | SEQUENCE
    | SEQUENCE_NUMBER
    | SERIALIZABLE
    | SERVERPROPERTY
    | SESSION_TIMEOUT
    | SETERROR
    | SHARE
    | SHARED
    | SHOWPLAN
    | SHOWPLAN_ALL
    | SHOWPLAN_TEXT
    | SHOWPLAN_XML
    | SIGNATURE
    | SIMPLE
    | SINGLE_USER
    | SIZE
    | SMALLINT
    | SNAPSHOT
    | SORT_IN_TEMPDB
    | SOUNDEX
    | SPACE_KEYWORD
    | SPARSE
    | SPATIAL_WINDOW_MAX_CELLS
    | STANDBY
    | START_DATE
    | STATIC
    | STATISTICS_INCREMENTAL
    | STATISTICS_NORECOMPUTE
    | STATS_DATE
    | STATS_STREAM
    | STATUS
    | STATUSONLY
    | STDEV
    | STDEVP
    | STOPLIST
    | STR
    | STRING_AGG
    | STRING_ESCAPE
    | STUFF
    | SUBJECT
    | SUBSCRIBE
    | SUBSCRIPTION
    | SUBSTRING
    | SUM
    | SUSPEND
    | SYMMETRIC
    | SYNCHRONOUS_COMMIT
    | SYNONYM
    | SYSTEM
    | TABLOCK
    | TABLOCKX
    | TAKE
    | TARGET_RECOVERY_TIME
    | TB
    | TEXTIMAGE_ON
    | THROW
    | TIES
    | TIME
    | TIMEOUT
    | TIMER
    | TINYINT
    | TORN_PAGE_DETECTION
    | TRACKING
    | TRANSACTION_ID
    | TRANSFORM_NOISE_WORDS
    | TRANSLATE
    | TRIM
    | TRIPLE_DES
    | TRIPLE_DES_3KEY
    | TRUSTWORTHY
    | TRY
    | TSQL
    | TWO_DIGIT_YEAR_CUTOFF
    | TYPE
    | TYPE_ID
    | TYPE_NAME
    | TYPE_WARNING
    | TYPEPROPERTY
    | UNBOUNDED
    | UNCOMMITTED
    | UNICODE
    | UNKNOWN
    | UNLIMITED
    | UNMASK
    | UOW
    | UPDLOCK
    | UPPER
    | USING
    | VALID_XML
    | VALIDATION
    | VALUE
    | VAR
    | VARBINARY_KEYWORD
    | VARP
    | VERSION
    | VIEW_METADATA
    | VIEWS
    | WAIT
    | WELL_FORMED_XML
    | WITHOUT_ARRAY_WRAPPER
    | WORK
    | WORKLOAD
    | XLOCK
    | XML
    | XML_COMPRESSION
    | XMLDATA
    | XMLNAMESPACES
    | XMLSCHEMA
    | XSINIL
    | ZONE
//More keywords that can also be used as IDs
    | ABORT_AFTER_WAIT
    | ABSENT
    | ADMINISTER
    | AES
    | ALLOW_CONNECTIONS
    | ALLOW_MULTIPLE_EVENT_LOSS
    | ALLOW_SINGLE_EVENT_LOSS
    | ANONYMOUS
    | APPEND
    | APPLICATION
    | ASYMMETRIC
    | ASYNCHRONOUS_COMMIT
    | AUTHENTICATE
    | AUTHENTICATION
    | AUTOMATED_BACKUP_PREFERENCE
    | AUTOMATIC
    | AVAILABILITY_MODE
    | BEFORE
    | BLOCK
    | BLOCKERS
    | BLOCKSIZE
    | BLOCKING_HIERARCHY
    | BUFFER
    | BUFFERCOUNT
    | CACHE
    | CALLED
    | CERTIFICATE
    | CHANGETABLE
    | CHANGES
    | CHECK_POLICY
    | CHECK_EXPIRATION
    | CLASSIFIER_FUNCTION
    | CLUSTER
    | COMPRESS
    | COMPRESSION
    | CONNECT
    | CONNECTION
    | CONFIGURATION
    | CONNECTIONPROPERTY
    | CONTAINMENT
    | CONTEXT
    | CONTEXT_INFO
    | CONTINUE_AFTER_ERROR
    | CONTRACT
    | CONTRACT_NAME
    | CONVERSATION
    | COPY_ONLY
    | CURRENT_REQUEST_ID
    | CURRENT_TRANSACTION_ID
    | CYCLE
    | DATA_COMPRESSION
    | DATA_SOURCE
    | DATABASE_MIRRORING
    | DATASPACE
    | DDL
    | DECOMPRESS
    | DEFAULT_DATABASE
    | DEFAULT_SCHEMA
    | DIAGNOSTICS
    | DIFFERENTIAL
    | DISTRIBUTION
    | DTC_SUPPORT
    | ENABLED
    | ENDPOINT
    | ERROR
    | ERROR_LINE
    | ERROR_MESSAGE
    | ERROR_NUMBER
    | ERROR_PROCEDURE
    | ERROR_SEVERITY
    | ERROR_STATE
    | EVENT
    | EVENTDATA
    | EVENT_RETENTION_MODE
    | EXECUTABLE_FILE
    | EXPIREDATE
    | EXTENSION
    | EXTERNAL_ACCESS
    | FAILOVER
    | FAILURECONDITIONLEVEL
    | FAN_IN
    | FILE_SNAPSHOT
    | FORCESEEK
    | FORCE_SERVICE_ALLOW_DATA_LOSS
    | FORMATMESSAGE
    | GET
    | GET_FILESTREAM_TRANSACTION_CONTEXT
    | GETANCESTOR
    | GETANSINULL
    | GETDESCENDANT
    | GETLEVEL
    | GETREPARENTEDVALUE
    | GETROOT
    | GOVERNOR
    | HASHED
    | HEALTHCHECKTIMEOUT
    | HEAP
    | HIERARCHYID
    | HOST_ID
    | HOST_NAME
    | IIF
    | IO
    | INCLUDE
    | INCREMENT
    | INFINITE
    | INIT
    | INSTEAD
    | ISDESCENDANTOF
    | ISNULL
    | ISNUMERIC
    | KERBEROS
    | KEY_PATH
    | KEY_STORE_PROVIDER_NAME
    | LANGUAGE
    | LIBRARY
    | LIFETIME
    | LINKED
    | LINUX
    | LISTENER_IP
    | LISTENER_PORT
    | LOCAL_SERVICE_NAME
    | LOG
    | MASK
    | MATCHED
    | MASTER
    | MAX_MEMORY
    | MAXTRANSFER
    | MAXVALUE
    | MAX_DISPATCH_LATENCY
    | MAX_DURATION
    | MAX_EVENT_SIZE
    | MAX_SIZE
    | MAX_OUTSTANDING_IO_PER_VOLUME
    | MEDIADESCRIPTION
    | MEDIANAME
    | MEMBER
    | MEMORY_PARTITION_MODE
    | MESSAGE_FORWARDING
    | MESSAGE_FORWARD_SIZE
    | MINVALUE
    | MIRROR
    | MUST_CHANGE
    | NEWID
    | NEWSEQUENTIALID
    | NOFORMAT
    | NOINIT
    | NONE
    | NOREWIND
    | NOSKIP
    | NOUNLOAD
    | NO_CHECKSUM
    | NO_COMPRESSION
    | NO_EVENT_LOSS
    | NOTIFICATION
    | NTLM
    | OLD_PASSWORD
    | ON_FAILURE
    | OPERATIONS
    | PAGE
    | PARAM_NODE
    | PARTIAL
    | PASSWORD
    | PERMISSION_SET
    | PER_CPU
    | PER_DB
    | PER_NODE
    | PERSISTED
    | PLATFORM
    | POLICY
    | PREDICATE
    | PROCESS
    | PROFILE
    | PYTHON
    | R
    | READ_WRITE_FILEGROUPS
    | REGENERATE
    | RELATED_CONVERSATION
    | RELATED_CONVERSATION_GROUP
    | REQUIRED
    | RESET
    | RESOURCES
    | RESTART
    | RESUME
    | RETAINDAYS
    | RETURNS
    | REWIND
    | ROLE
    | ROUND_ROBIN
    | ROWCOUNT_BIG
    | RSA_512
    | RSA_1024
    | RSA_2048
    | RSA_3072
    | RSA_4096
    | SAFETY
    | SAFE
    | SCHEDULER
    | SCHEME
    | SCRIPT
    | SERVER
    | SERVICE
    | SERVICE_BROKER
    | SERVICE_NAME
    | SESSION
    | SESSION_CONTEXT
    | SETTINGS
    | SHRINKLOG
    | SID
    | SKIP_KEYWORD
    | SOFTNUMA
    | SOURCE
    | SPECIFICATION
    | SPLIT
    | SQL
    | SQLDUMPERFLAGS
    | SQLDUMPERPATH
    | SQLDUMPERTIMEOUT
    | STATE
    | STATS
    | START
    | STARTED
    | STARTUP_STATE
    | STOP
    | STOPPED
    | STOP_ON_ERROR
    | SUPPORTED
    | SWITCH
    | TAPE
    | TARGET
    | TCP
    | TOSTRING
    | TRACE
    | TRACK_CAUSALITY
    | TRANSFER
    | UNCHECKED
    | UNLOCK
    | UNSAFE
    | URL
    | USED
    | VERBOSELOGGING
    | VISIBILITY
    | WAIT_AT_LOW_PRIORITY
    | WINDOWS
    | WITHOUT
    | WITNESS
    | XACT_ABORT
    | XACT_STATE
    //Build-ins:
    | VARCHAR
    | NVARCHAR
    | PRECISION //For some reason this is possible to use as ID
    ;

// https://msdn.microsoft.com/en-us/library/ms175874.aspx
id_
    : ID
    | empty_value
    | DOUBLE_QUOTE_BLANK
    | SQUARE_BRACKET_ID
    | keyword
    ;

simple_id
    : ID
    ;

// https://msdn.microsoft.com/en-us/library/ms188074.aspx
// Spaces are allowed for comparison operators.
file_size
    : decimal file_size_unity?
    ;

ipv4 : IPV4_ADDR;
ipv6 : IPV6_ADDR;
float : FLOAT;
decimal : DECIMAL;
id_simple : ID;

filestream_filegroup_or_partition_schema_id : id_;
action_id : id_;
aggregate_id : id_;
schema_identifier : id_;
assembly_id : id_;
asym_key_id : id_;
audit_action_group_id : id_;
audit_guid_id : id_;
audit_id : id_;
user_id : id_;
backup_id : id_;
binding_id : id_;
catalog_id : id_;
certificate_id : id_;
class_id : id_;
collation_id : id_;
column_encryption_key_id : id_;
column_or_argument_id : id_;
constraint_id : id_;
credential_id : id_;
cryptographic_provider_id : id_;
data_source_id : id_;
service_id : id_;
encryptor_id : id_;
endpoint_id : id_;
event_customizable_attribute_id : id_;
event_field_id : id_;
event_module_guid_id : id_;
event_notification_id : id_;
event_package_id : id_;
event_session_id : id_;
event_type_or_group_id : id_;
ext_type_id : id_;
external_data_source_id : id_;
external_file_format_id : id_;
external_pool_id : id_;
function_id : id_;
group_id : id_;
index_id : id_;
language_id : id_;
library_id : id_;
server_id : id_;
logical_device_id : id_;
login_id : id_;
master_key : id_;
method_id : id_;
contract_id : id_;
module_id : id_;
network_computer : id_;
role_id : id_;
file_group_id : id_;
non_static_attr_id : id_;
notification_id : id_;
object_identifier : id_;
owner_id : id_;
partition_column_id : id_;
pool_id : id_;
predicate_source_id : id_;
property_list_id : id_;
provider_id : id_;
database_id : id_;
route_id : id_;
rule_id : id_;
column_id : id_;
schema_collection_id : id_;
security_policy_id : id_;
security_predicate_function_id : id_;
sequence_id : id_;
server_role_id : id_;
source_list_id : id_;
sql_identifier_id : id_;
static_attr_id : id_;
statistics_id : id_;
stoplist_id : id_;
symmetric_key_id : id_; 
synonym_id : id_;
table_or_view_id : id_ ;
view_id : id_ ;
table_id : id_;
target_id : id_;
target_parameter_id : id_;
trigger_id : id_;
tvf_schema_id : id_;
udt_column_id : id_;
unscaled_type_id : id_;
windows_principal_id : id_;
workload_group_group_id : id_;
workload_group_pool_id : id_;
partition_scheme_id : id_;
queue_id : id_;
partition_function_id: id_;
message_type_id:id_;
code_location_id : id_;
transaction_identifier : id_;

schema_security_predicate_function_id : tvf_schema_id DOT security_predicate_function_id;



binary_: BINARY;
local_id : LOCAL_ID;
decimal_id : decimal | id_;
string_id : stringtext | id_;
stringtext : STRING;
string_id2 : stringtext | id_ | local_id;
string_local_id : stringtext | local_id;
decimal_local_id : decimal | local_id;
decimal_string : decimal | stringtext;
decimal_string_local_id : decimal | stringtext | local_id;
string_local_id_double_quote_id : stringtext | local_id | empty_value;

// ---------------  Composites Ids ---------------

server_database_schema_object_ref : (server_id DOT)? (database_id DOT)? (schema_identifier DOT)? object_identifier;

database_stoplist_ref : (database_id DOT)? stoplist_id;

event_module_package_action_ref : (event_module_guid_id DOT)? event_package_id DOT action_id;

schema_sequence_ref : (schema_identifier DOT)? sequence_id;

schema_queue_ref : (schema_identifier DOT) queue_id;

module_package_event_ref : (event_module_guid_id DOT)? event_package_id DOT target_id;

schema_rule_ref : (schema_identifier DOT)? rule_id;

schema_module_ref : (schema_identifier DOT)? module_id;

database_schema_sequence_ref : (database_id DOT)? schema_sequence_ref;

schema_object_statistics_ref : schema_object_ref DOT statistics_id;
schema_synonym_ref : ( schema_identifier DOT )? synonym_id;

default_ref : (schema_identifier DOT)? id_;

schema_sql_identifier_id : ( schema_identifier DOT )?  sql_identifier_id;

schema_trigger_ref : (schema_identifier DOT)? trigger_id;

schema_object_ref : (schema_identifier DOT)? object_identifier;

schema_security_policy_ref : (schema_identifier DOT)? security_policy_id;

schema_aggregate_ref : ( schema_identifier DOT )? aggregate_id;

database_schema_queue_ref  : (database_schema_ref DOT)? queue_id
    ;

database_schema_ref : (database_id DOT)? schema_identifier;

drop_backward_compatible_index : (schema_identifier DOT )? table_or_view_id DOT index_id
    ;

complete_table_ref
    : ( server_id DOT DOT schema_identifier DOT
    |   server_id DOT database_id DOT schema_identifier  DOT
    |                 database_id DOT schema_identifier? DOT
    |                                 schema_identifier  DOT 
      )? table_id
    ;

full_table_ref
    : database_schema_table_ref
    | database_schema_ref? blocking_hierarchy=BLOCKING_HIERARCHY
    ;

database_schema_table_ref : database_id? DOT schema_identifier? DOT table_id;

entity_name_for_azure_dw_ref
    : schema_identifier
    | schema_identifier DOT object_identifier
    ;

entity_name_for_parallel_dw_ref
    : schema_identifier
    | schema_identifier DOT object_identifier
    ;


func_proc_name_server_database_schema
    : server_database_schema_function_ref
    | func_proc_name_database_schema_ref
    ;

func_proc_name_database_schema_ref
    : database_schema_function_ref
    | schema_func_proc_ref
    ;

server_database_schema_function_ref : server_id? DOT database_id? DOT schema_identifier? DOT function_id;
database_schema_function_ref : database_id? DOT schema_identifier? DOT function_id;
schema_func_proc_ref : (schema_identifier DOT )? function_id;

ddl_object
    : complete_table_ref
    | local_id
    ;

full_column_name
    : deleteed_inserted_column_id
    | full_column_ref
    ;

deleteed_inserted_column_id : deleteed_inserted DOT column_id;

full_column_ref
    : server_id? DOT schema_identifier? DOT table_id? DOT column_id
    |                schema_identifier? DOT table_id? DOT column_id
    |                                       table_id? DOT column_id
    |                                                     column_id
    ;

entity_name
    : (server_id DOT database_id DOT schema_identifier  DOT
    |                database_id DOT schema_identifier? DOT
    |                                schema_identifier  DOT )? object_identifier
    ;

column_name_list_with_order
    : column_ordered (COMMA column_ordered)*
    ;

column_ordered : column_id asc_desc?;

column_or_argument_ids : column_or_argument_id (COMMA? column_or_argument_id)*;


// ---------------  Listes ---------------
schema_view_ref : (schema_identifier DOT )? view_id;

decimals : decimal (COMMA decimal)+;

schema_type_ref : (schema_identifier DOT )? id_;

database_source_list_ref : (database_id DOT)? source_list_id;

full_predicate_source_ref : (event_module_guid_id DOT)? event_package_id DOT predicate_source_id;

assembly_class_method_ref : assembly_id DOT class_id DOT method_id;

schema_trigger_refs : schema_trigger_ref (COMMA schema_trigger_ref)*;

ids : id_ (COMMA id_ )*;

update_statistics_options : WITH update_statistics_option (COMMA update_statistics_option)*;

function_options : WITH function_option (COMMA function_option)*;

procedure_params : procedure_param (COMMA procedure_param)*;

dml_trigger_options : WITH dml_trigger_option (COMMA dml_trigger_option)*;

table_type_indices : table_type_indice (COMMA table_type_indice)*;

dml_trigger_operations : dml_trigger_operation (COMMA dml_trigger_operation)*;

procedure_options : WITH procedure_option (COMMA procedure_option)*;

logical_device_ids : logical_device_id (COMMA logical_device_id)*;

disk_tape_url_values : disk_tape_url_value (COMMA disk_tape_url_value)*;

xml_index_options : WITH LR_BRACKET xml_index_option (COMMA xml_index_option)* RR_BRACKET ;

create_columnstore_index_options : WITH LR_BRACKET columnstore_index_option (COMMA columnstore_index_option)* RR_BRACKET;

single_partition_rebuild_index_options : WITH LR_BRACKET single_partition_rebuild_index_option (COMMA single_partition_rebuild_index_option)* RR_BRACKET;

rebuild_index_options : WITH LR_BRACKET rebuild_index_option (COMMA rebuild_index_option)* RR_BRACKET;

full_table_ref_columns : full_table_ref_column (COMMA full_table_ref_column)*;

set_index_options : SET LR_BRACKET set_index_option (COMMA set_index_option)* RR_BRACKET;

reorganize_options : WITH LR_BRACKET (reorganize_option (COMMA reorganize_option)*) RR_BRACKET;

private_keys : privatekey (COMMA privatekey)*;


privatekey 
    : FILE EQUAL stringtext 
    | by_password_crypt;

server_audit_file_specs : server_audit_file_spec (COMMA server_audit_file_spec)*;
create_or_alter_event_session_del_events : create_or_alter_event_session_del_event (COMMA create_or_alter_event_session_del_event)*;
create_or_alter_event_session_add_events : create_or_alter_event_session_add_event (COMMA create_or_alter_event_session_add_event)*;
create_or_alter_event_session_add_targets : create_or_alter_event_session_add_target+;
create_or_alter_event_session_del_targets : create_or_alter_event_session_del_target+;
event_session_actions : event_session_action (COMMA event_session_action)*;

set_attributes : set_attribute (COMMA set_attribute)*;

pwd_strategies : pwd_strategy+;

event_module_package_action_refs : event_module_package_action_ref (COMMA event_module_package_action_ref);


resumable_index_options : WITH LR_BRACKET (resumable_index_option (COMMA resumable_index_option)*) RR_BRACKET;
create_index_options : WITH LR_BRACKET relational_index_options RR_BRACKET;

relational_index_options : relational_index_option (COMMA relational_index_option)*;

database_files : database_file ( COMMA database_file )*;

output_dml_list_elems : output_dml_list_elem (COMMA output_dml_list_elem)*;

update_elems : update_elem (COMMA update_elem)*;

create_security_policy_adds : create_security_policy_add (COMMA? create_security_policy_add)*;

update_elem_merges : update_elem_merge (COMMA update_elem_merge)*;

file_group_ids : file_group_id (COMMA file_group_id)*;

string_list : stringtext (COMMA stringtext)*;

asymmetric_key_option : WITH PRIVATE KEY LR_BRACKET by_password_crypt ( COMMA by_password_crypt)? RR_BRACKET;

assemblies : assembly_id (COMMA? assembly_id)+;

decimal_string_locals : decimal_string_local_id (COMMA decimal_string_local_id)*;

column_name_list : id_ (COMMA id_)*;

//For some reason, sql server allows any number of prefixes:  Here, h is the column: a.b.c.d.e.f.g.h
insert_column_name_list : insert_column_id (COMMA insert_column_id)*;

insert_column_id : (source=id_? DOT )* column_id;

file_specs : file_spec ( COMMA file_spec )*;

expression_list : expression (COMMA expression)*;

change_tracking_option_items : change_tracking_option_item (change_tracking_option_item)*;

create_server_audit_withs : create_server_audit_with (COMMA create_server_audit_with)*;

table_value_constructor : VALUES LR_BRACKET expression_list RR_BRACKET (COMMA LR_BRACKET expression_list RR_BRACKET)*;

column_alias_list : LR_BRACKET alias+=column_alias (COMMA alias+=column_alias)* RR_BRACKET;

index_values : index_value (COMMA index_value)*;

expressions : expression (COMMA expression)*;

full_column_names : LR_BRACKET full_column_name (COMMA full_column_name)* RR_BRACKET;

expression_language : expression  (COMMA LANGUAGE expression)?;

bulk_options : bulk_option (COMMA bulk_option)*;

full_column_name_list : full_column_name (COMMA full_column_name)*;

schema_declaration : column_declaration (COMMA column_declaration)*;

json_declaration : json_column_declaration (COMMA json_column_declaration)*;

expression2 : expression (COMMA expression)?;

table_sources : table_source (COMMA table_source)*;

// https://msdn.microsoft.com/en-us/library/ms176104.aspx
select_list : select_list_elem (COMMA select_list_elem)*;

udt_method_arguments : LR_BRACKET execute_var_string (COMMA execute_var_string)* RR_BRACKET;

optimize_for_args : optimize_for_arg (COMMA optimize_for_arg)*;

// https://msdn.microsoft.com/en-us/library/ms181714.aspx
update_option_clause : OPTION LR_BRACKET update_option (COMMA update_option)* RR_BRACKET;

grouping_sets_list : group_by_item (COMMA group_by_item)*;

groupSet_list : grouping_sets_item (COMMA grouping_sets_item)*;

groupBys_list : group_by_item (COMMA group_by_item)*;

// https://msdn.microsoft.com/en-us/library/ms175972.aspx
with_expression : WITH common_table_expression (COMMA common_table_expression)*;

special_lists : special_list (COMMA special_list)*;

local_ids : local_id (COMMA local_id)*;

alter_table_index_options : WITH LR_BRACKET alter_table_index_option (COMMA alter_table_index_option)* RR_BRACKET;

connection_nodes : connection_node ( COMMA connection_node )*;

dbcc_options : simple_id (COMMA simple_id)?;

execute_var_strings : execute_var_string (COMMA execute_var_string)*;

execute_statement_arg_nameds : execute_statement_arg_named (COMMA execute_statement_arg_named)*;

execute_statement_args : execute_statement_arg (COMMA execute_statement_arg)*;

declare_locals : declare_local (COMMA loc+=declare_local)*;

xml_declarations : xml_declaration (COMMA xml_declaration)*;

schema_view_refs : schema_view_ref (COMMA schema_view_ref)*;

table_names : full_table_ref (COMMA full_table_ref)*;

func_proc_name_schemas : schema_func_proc_ref (COMMA schema_func_proc_ref)*;

drop_backward_compatible_indexs : drop_backward_compatible_index (COMMA drop_backward_compatible_index)*;

drop_relational_or_xml_or_spatial_indexs : drop_relational_or_xml_or_spatial_index (COMMA drop_relational_or_xml_or_spatial_index)*;

change_tracking_option_lists : change_tracking_option_list (COMMA change_tracking_option_list)*;

filespecs : filespec (COMMA filespec)*;

view_attributes : WITH view_attribute (COMMA view_attribute)*;

create_table_index_options : WITH LR_BRACKET create_table_index_option ( COMMA create_table_index_option)* RR_BRACKET;

tableoptions : tableoption (COMMA tableoption)*;

alter_user_items : alter_user_item (COMMA alter_user_item)*;

contract_refs : contract_ref (COMMA contract_ref)*;

alter_service_contracts : contract_id (COMMA contract_id);

decimal_ranges : decimal_range (COMMA? decimal_range )*;

server_audit_file_infos : server_audit_file_info (COMMA server_audit_file_info)*;

clause_json_infos : (COMMA clause_json_info)+;

contract_items : contract_item (COMMA contract_item+)*;

date_options : date_option (COMMA date_option)+;

xml_common_directives : xml_common_directive (COMMA xml_common_directive)*;

receive_ids : receive_id (COMMA receive_id)+;

declare_set_cursor_common_partials : declare_set_cursor_common_partial+;

column_definition_elements : column_definition_element+;

file_group_list :  file_group_assign (COMMA file_group_assign)*;

schema_table_ref_impacts : schema_table_ref_impact (COMMA schema_table_ref_impact)*;

notification_ids : notification_id (COMMA notification_id)*;

assembly_permission : SAFE | EXTERNAL_ACCESS | UNSAFE;

object_type_for_grant
    : APPLICATION ROLE
    | ASSEMBLY
    | ASYMMETRIC KEY
    | AUDIT
    | AVAILABILITY GROUP
    | BROKER PRIORITY
    | CERTIFICATE
    | CONTRACT
    | CREDENTIAL
    | CRYPTOGRAPHIC PROVIDER
    | DATABASE ( AUDIT SPECIFICATION
               | ENCRYPTION KEY
               | EVENT SESSION
               | SCOPED ( CONFIGURATION
                        | CREDENTIAL
                        | RESOURCE GOVERNOR )
               )?
    | ENDPOINT
    | EVENT SESSION
    | EXTERNAL ( DATA SOURCE
               | FILE FORMAT
               | LIBRARY
               | RESOURCE POOL
               | TABLE
               | CATALOG
               | STOPLIST
               )
    | LOGIN
    | MASTER KEY
    | MESSAGE TYPE
    | OBJECT
    | PARTITION ( FUNCTION | SCHEME)
    | REMOTE SERVICE BINDING
    | RESOURCE GOVERNOR
    | ROLE
    | ROUTE
    | SCHEMA
    | SEARCH PROPERTY LIST
    | SERVER ( ( AUDIT SPECIFICATION? ) | ROLE )?
    | SERVICE
    | SQL LOGIN
    | SYMMETRIC KEY
    | TRIGGER ( DATABASE | SERVER)
    | TYPE
    | USER
    | XML SCHEMA COLLECTION
    ;
encryption_master : ENCRYPTION | MASTER ;
database_object_server : DATABASE | OBJECT | SERVER;

all_server_database
    : ALL SERVER 
    | DATABASE
    ;

server_database
    : SERVER 
    | DATABASE
    ;

for_after
    : FOR 
    | AFTER
    ;
share_exclusive : SHARE | EXCLUSIVE;

create_alter : CREATE | ALTER;


file_size_unity
    : KB | MB | GB | TB | MODULE
    ;

memory_size_unity
    : KB | MB
    ;

partition_mode : NONE | PER_NODE | PER_CPU;

session_mode 
    : ALLOW_SINGLE_EVENT_LOSS 
    | ALLOW_MULTIPLE_EVENT_LOSS | NO_EVENT_LOSS
    ;

disable_reconfigure : DISABLE | RECONFIGURE;

transfert_target : (OBJECT | TYPE | XML SCHEMA COLLECTION) DOUBLE_COLON;

insert_update : INSERT | UPDATE;
update_delate : UPDATE | DELETE;

filter_block : FILTER | BLOCK;

init_target_any : INITIATOR | TARGET | ANY;

receive_mode_enum : ALL | DISTINCT | STAR;

datacompression_mode : NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE;
datacompression_column_mode : COLUMNSTORE | COLUMNSTORE_ARCHIVE;

index_using_xml_mode : FOR (VALUE | PATH | PROPERTY)?;

proc_keyword : PROC | PROCEDURE;
alter_replace : ALTER | REPLACE;

param_way : OUT | OUTPUT | READONLY;

percent_row : PERCENT | ROWS;

function_option_enum
    : ENCRYPTION
    | SCHEMABINDING
    | RETURNS NULL_ ON NULL_ INPUT
    | CALLED ON NULL_ INPUT
    ;

procedure_option_enum
    : ENCRYPTION
    | RECOMPILE
    ;

row_rows : ROW | ROWS;

compression_mode : NONE | ROW | PAGE;
start_end : START | END;
generation_mode : ROW | TRANSACTION_ID | SEQUENCE_NUMBER;

encryption_mode : DETERMINISTIC | RANDOMIZED;

tableoption_cluster_mode : CLUSTERED COLUMNSTORE INDEX | HEAP;

lock_mode : AUTO | TABLE | DISABLE;

 check_nocheck : CHECK | NOCHECK;

data_type_scaled : VARCHAR | NVARCHAR | BINARY_KEYWORD | VARBINARY_KEYWORD | SQUARE_BRACKET_ID;

abord_after_mode : NONE | SELF | BLOCKERS;

local_global : LOCAL | GLOBAL;

state_enum : STARTED | STOPPED | DISABLED;

authentication_mode : NTLM |KERBEROS | NEGOTIATE;

encryption_state : ENCRYPTION EQUAL ( DISABLED | SUPPORTED | REQUIRED );
encryption_algorithm :  ALGORITHM ( AES | RC4 | AES RC4 | RC4 AES );
role_mirroring : WITNESS | PARTNER | ALL;

partner_option_enum
    : FAILOVER
    | FORCE_SERVICE_ALLOW_DATA_LOSS
    | OFF
    | RESUME
    | SAFETY (FULL | OFF )
    | SUSPEND
    ;

delayed_durability : DISABLED | ALLOWED | FORCED;

suspend_resume : SUSPEND | RESUME;


parameterization_option
    : PARAMETERIZATION ( SIMPLE | FORCED )
    ;

recovery_option_enum
    : RECOVERY ( FULL | BULK_LOGGED | SIMPLE )
    | PAGE_VERIFY ( CHECKSUM | TORN_PAGE_DETECTION | NONE )
    ;

seconds_minutes : SECONDS | MINUTES;

compression : COMPRESSION | NO_COMPRESSION;
init_no_init : NOINIT | INIT;
no_skip : NOSKIP | SKIP_KEYWORD;
format_noformat : NOFORMAT | FORMAT;

login_user : LOGIN | USER;

output_out : OUTPUT | OUT;

start_date_expiry_date : START_DATE | EXPIRY_DATE;

execute_clause_mode_enum : CALLER | SELF | OWNER;

content_document : CONTENT | DOCUMENT;

materialized_mode : MATERIALIZED | NOT MATERIALIZED;

column_modifier_enum
    :   ROWGUIDCOL
      | PERSISTED
      | NOT FOR REPLICATION
      | SPARSE
      | HIDDEN_KEYWORD
    ;

compute_as : COMPUTE | AS;

primary_key_unique : PRIMARY KEY | UNIQUE;

declare_set_cursor_common_partial_enum
    : (FORWARD_ONLY | SCROLL)
    | (STATIC | KEYSET | DYNAMIC | FAST_FORWARD)
    | (READ_ONLY | SCROLL_LOCKS | OPTIMISTIC)
    | TYPE_WARNING
    ;

absolute_relative
    : ABSOLUTE 
    | RELATIVE
    ;

fetch_cursor_strategy
    : NEXT 
    | PRIOR 
    | FIRST 
    | LAST
    ;

statistic_kind : IO | TIME | XML | PROFILE;

transaction_isolation 
    : READ UNCOMMITTED 
    | READ COMMITTED 
    | REPEATABLE READ 
    | SNAPSHOT 
    | SERIALIZABLE 
    ;

special_list
    : ANSI_NULLS
    | QUOTED_IDENTIFIER
    | ANSI_PADDING
    | ANSI_WARNINGS
    | ANSI_DEFAULTS
    | ANSI_NULL_DFLT_OFF
    | ANSI_NULL_DFLT_ON
    | ARITHABORT
    | ARITHIGNORE
    | CONCAT_NULL_YIELDS_NULL
    | CURSOR_CLOSE_ON_COMMIT
    | FMTONLY
    | FORCEPLAN
    | IMPLICIT_TRANSACTIONS
    | NOCOUNT
    | NOEXEC
    | NUMERIC_ROUNDABORT
    | PARSEONLY
    | REMOTE_PROC_TRANSACTIONS
    | SHOWPLAN_ALL
    | SHOWPLAN_TEXT
    | SHOWPLAN_XML
    | XACT_ABORT
    ;

expression_operator 
    : STAR | DIVIDE | MODULE | PLUS | MINUS
    | BIT_AND | BIT_XOR | BIT_OR | DOUBLE_BAR
    ;

parameter
    : PLACEHOLDER;

all_some_any : ALL | SOME | ANY;

join_mode : UNION ALL? | EXCEPT | INTERSECT;

all_distinct : ALL | DISTINCT;

delay_time_timeout : DELAY | TIME | TIMEOUT;

creation_disposition : CREATE_NEW | OPEN_EXISTING;

asymetric_algorithm : RSA_4096 | RSA_3072 | RSA_2048 | RSA_1024 | RSA_512;

add_remove : ADD | REMOVE;
restart_remove : RESTART | REMOVE;
synch_asynch :SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT;
auto_manual : AUTOMATIC | MANUAL;
real_write_all : READ_WRITE | ALL;
no_real_write_all : NO | READ_WRITE | ALL;
primary_secondary_none : PRIMARY | SECONDARY_ONLY| SECONDARY | NONE;
grant_deny : GRANT | DENY;

add_drop : ADD | DROP;

event_session_predicate_leaf_ope 
    : EQUAL 
    | (LESS GREATER) 
    | (EXCLAMATION EQUAL) 
    | GREATER  
    | (GREATER EQUAL) 
    | LESS 
    | LESS EQUAL;

set_add : SET|ADD;


platform : WINDOWS | LINUX;
code_language : R | PYTHON;


pwd_strategy : MUST_CHANGE | UNLOCK;

enable_disable 
    : ENABLE
    | DISABLE
    ;


message_validation_value_enum
    : NONE 
    | EMPTY 
    | WELL_FORMED_XML
    ;

sequence_cycle : CYCLE | NO CYCLE;

size_unity : MB | GB | TB;


continue_shutdown : CONTINUE | SHUTDOWN | FAIL_OPERATION;

audit_operator 
    : EQUAL
    | LESS GREATER
    | EXCLAMATION EQUAL
    | GREATER
    | GREATER EQUAL
    | LESS
    | LESS EQUAL
    ;

and_or
    : AND
    | OR
    ;

size_value : decimal MB | DEFAULT;
decimal_default : decimal | DEFAULT;

for_from : FOR | FROM;


importance_level : LOW | MEDIUM | HIGH;

left_right : LEFT | RIGHT;

none_partial : NONE | PARTIAL;

index_strategy : NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE;

view_attribute
    : ENCRYPTION | SCHEMABINDING | VIEW_METADATA
    ;

filegroup_predicate : CONTAINS FILESTREAM | CONTAINS MEMORY_OPTIMIZED_DATA;


statistic_value 
    : OFF 
    | ON ( INCREMENTAL EQUAL ON | OFF  )
    ;
period : DAYS | HOURS | MINUTES;

db_state_option
    : ONLINE 
    | OFFLINE 
    | EMERGENCY
    ;

db_update_option : READ_ONLY | READ_WRITE;

db_user_access_option : SINGLE_USER | RESTRICTED_USER | MULTI_USER;
disk_tape_url : DISK | TAPE | URL;



file_file_group : FILE|FILEGROUP;

load_moun_load : LOAD | NOUNLOAD;
rewind : REWIND | NOREWIND;

algorithm_short
    : AES_128
    | AES_192
    | AES_256
    | TRIPLE_DES_3KEY
    ;

encryption_decryption : DECRYPTION | ENCRYPTION;

algorithm
    : DES
    | TRIPLE_DES
    | TRIPLE_DES_3KEY
    | RC2
    | RC4
    | RC4_128
    | DESX
    | AES_128
    | AES_192
    | AES_256
    ;

    grant_permission_enum
    : ADMINISTER ( BULK OPERATIONS | DATABASE BULK OPERATIONS)
    | AUTHENTICATE SERVER?
    | BACKUP ( DATABASE | LOG )
    | CHECKPOINT
    | CONNECT ( ANY DATABASE | REPLICATION | SQL )?
    | CONTROL SERVER?
    | DELETE
    | EXECUTE ( ANY EXTERNAL SCRIPT )?
    | EXTERNAL ACCESS ASSEMBLY
    | IMPERSONATE ( ANY LOGIN )?
    | INSERT
    | KILL DATABASE CONNECTION
    | RECEIVE
    | REFERENCES
    | SELECT ( ALL USER SECURABLES )?
    | SEND
    | SHOWPLAN
    | SHUTDOWN
    | SUBSCRIBE QUERY NOTIFICATIONS
    | TAKE OWNERSHIP
    | UNMASK
    | UNSAFE ASSEMBLY
    | UPDATE
    | VIEW ( ANY ( DATABASE | DEFINITION | COLUMN ( ENCRYPTION | MASTER ) KEY DEFINITION )
           | CHANGE TRACKING
           | DATABASE STATE
           | DEFINITION
           | SERVER STATE
           )
    ;

transaction : TRAN | TRANSACTION;

on_delete
    : ON DELETE (NO ACTION | CASCADE | SET NULL_ | SET DEFAULT)
    ;

on_update
    : ON UPDATE (NO ACTION | CASCADE | SET NULL_ | SET DEFAULT)
    ;

sensitive
    : SEMI_SENSITIVE 
    | INSENSITIVE
    ;


plus_minus : PLUS | MINUS;

first_next : FIRST | NEXT;

absent_xsinil : ABSENT | XSINIL;

auto_path : AUTO | PATH;

update_option_enum
    : (HASH | ORDER) GROUP
    | (MERGE | HASH | CONCAT) UNION
    | (LOOP | MERGE | HASH) JOIN
    | EXPAND VIEWS
    | FORCE ORDER
    | IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX
    | KEEP PLAN
    | KEEPFIXED PLAN
    | OPTIMIZE FOR UNKNOWN
    | PARAMETERIZATION (SIMPLE | FORCED)
    | RECOMPILE
    | ROBUST PLAN
    ;

star_asterisk
    : STAR
    ;

    updated_asterisk
    : (INSERTED | DELETED) DOT STAR
    ;


join_type : LEFT | RIGHT | FULL;

join_hint : LOOP | HASH | MERGE | REMOTE;

apply_style : CROSS | OUTER;

containstable_freetexttable : CONTAINSTABLE | FREETEXTTABLE;
semantic_table : SEMANTICSIMILARITYTABLE | SEMANTICKEYPHRASETABLE;


ranking_windowed : RANK | DENSE_RANK | ROW_NUMBER;
agg_function : AVG | MAX | MIN | SUM | STDEV | STDEVP | VAR | VARP;

count_count_big : COUNT | COUNT_BIG;

percentil : PERCENTILE_CONT | PERCENTILE_DISC;
cume_percent : CUME_DIST | PERCENT_RANK;
first_last_value : FIRST_VALUE | LAST_VALUE;
lag_lead : LAG | LEAD;

row_range : ROWS | RANGE;

off_read_only_full : OFF | READ_ONLY | FULL;

asc_desc : ASC | DESC;

on_off
    : ON
    | OFF
    ;
clustered
    : CLUSTERED
    | NONCLUSTERED
    ;

null_notnull
    : NOT? NULL_
    ;

scalar_function_name_enum
    : RIGHT
    | LEFT
    | BINARY_CHECKSUM
    | CHECKSUM
    ;


relayed_conversation : RELATED_CONVERSATION | RELATED_CONVERSATION_GROUP;


comparison_operator
    : EQUAL | GREATER | LESS | LESS EQUAL | GREATER EQUAL | LESS GREATER | EXCLAMATION EQUAL | EXCLAMATION GREATER | EXCLAMATION LESS
    ;

assignment_operator
    : PLUS_ASSIGN | MINUS_ASSIGN | MULT_ASSIGN | DIV_ASSIGN | MOD_ASSIGN | AND_ASSIGN | XOR_ASSIGN | OR_ASSIGN
    ;

log_seterror_nowait : LOG | SETERROR | NOWAIT;

deleteed_inserted : DELETED | INSERTED;

