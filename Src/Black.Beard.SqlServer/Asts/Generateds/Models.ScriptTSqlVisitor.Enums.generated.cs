//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bb.Parsers.TSql
{
    using System;
    using Bb.Parsers;
    using Bb.Asts.TSql;
    using Bb.Asts;
    using Bb.Parsers.TSql.Antlr;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using System.Collections;
    
    
    public partial class ScriptTSqlVisitor
    {
        
        /// <summary>
        /// binary_content
        /// 	 : STRING
        /// 	 | BINARY
        /// </summary>
        public override AstRoot VisitBinary_content(TSqlParser.Binary_contentContext context)
        {
            return new AstBinaryContent(context, context.GetText());
        }
        
        /// <summary>
        /// class_type_for_azure_dw
        /// 	 : SCHEMA
        /// 	 | OBJECT
        /// </summary>
        public override AstRoot VisitClass_type_for_azure_dw(TSqlParser.Class_type_for_azure_dwContext context)
        {
            return new AstClassTypeForAzureDw(context, context.GetText());
        }
        
        /// <summary>
        /// class_type_for_parallel_dw
        /// 	 : DATABASE
        /// 	 | SCHEMA
        /// 	 | OBJECT
        /// </summary>
        public override AstRoot VisitClass_type_for_parallel_dw(TSqlParser.Class_type_for_parallel_dwContext context)
        {
            return new AstClassTypeForParallelDw(context, context.GetText());
        }
        
        /// <summary>
        /// grant_deny
        /// 	 : GRANT
        /// 	 | DENY
        /// </summary>
        public override AstRoot VisitGrant_deny(TSqlParser.Grant_denyContext context)
        {
            return new AstGrantDeny(context, context.GetText());
        }
        
        /// <summary>
        /// add_drop
        /// 	 : ADD
        /// 	 | DROP
        /// </summary>
        public override AstRoot VisitAdd_drop(TSqlParser.Add_dropContext context)
        {
            return new AstAddDrop(context, context.GetText());
        }
        
        /// <summary>
        /// start_stop
        /// 	 : START
        /// 	 | STOP
        /// </summary>
        public override AstRoot VisitStart_stop(TSqlParser.Start_stopContext context)
        {
            return new AstStartStop(context, context.GetText());
        }
        
        /// <summary>
        /// code_content
        /// 	 : STRING
        /// 	 | BINARY
        /// 	 | NONE
        /// </summary>
        public override AstRoot VisitCode_content(TSqlParser.Code_contentContext context)
        {
            return new AstCodeContent(context, context.GetText());
        }
        
        /// <summary>
        /// code_language
        /// 	 : R
        /// 	 | PYTHON
        /// </summary>
        public override AstRoot VisitCode_language(TSqlParser.Code_languageContext context)
        {
            return new AstCodeLanguage(context, context.GetText());
        }
        
        /// <summary>
        /// enable_disable
        /// 	 : ENABLE
        /// 	 | DISABLE
        /// </summary>
        public override AstRoot VisitEnable_disable(TSqlParser.Enable_disableContext context)
        {
            return new AstEnableDisable(context, context.GetText());
        }
        
        /// <summary>
        /// split_or_merge
        /// 	 : SPLIT
        /// 	 | MERGE
        /// </summary>
        public override AstRoot VisitSplit_or_merge(TSqlParser.Split_or_mergeContext context)
        {
            return new AstSplitOrMerge(context, context.GetText());
        }
        
        /// <summary>
        /// enum_dml
        /// 	 : SELECT
        /// 	 | INSERT
        /// 	 | DELETE
        /// 	 | UPDATE
        /// </summary>
        public override AstRoot VisitEnum_dml(TSqlParser.Enum_dmlContext context)
        {
            return new AstEnumDml(context, context.GetText());
        }
        
        /// <summary>
        /// dml_trigger_operation
        /// 	 : (INSERT | UPDATE | DELETE)
        /// </summary>
        public override AstRoot VisitDml_trigger_operation(TSqlParser.Dml_trigger_operationContext context)
        {
            return new AstDmlTriggerOperation(context, context.GetText());
        }
        
        /// <summary>
        /// view_attribute
        /// 	 : ENCRYPTION
        /// 	 | SCHEMABINDING
        /// 	 | VIEW_METADATA
        /// </summary>
        public override AstRoot VisitView_attribute(TSqlParser.View_attributeContext context)
        {
            return new AstViewAttribute(context, context.GetText());
        }
        
        /// <summary>
        /// filegroup_updatability_option
        /// 	 : READONLY
        /// 	 | READWRITE
        /// 	 | READ_ONLY
        /// 	 | READ_WRITE
        /// </summary>
        public override AstRoot VisitFilegroup_updatability_option(TSqlParser.Filegroup_updatability_optionContext context)
        {
            return new AstFilegroupUpdatabilityOption(context, context.GetText());
        }
        
        /// <summary>
        /// local_global
        /// 	 : LOCAL
        /// 	 | GLOBAL
        /// </summary>
        public override AstRoot VisitLocal_global(TSqlParser.Local_globalContext context)
        {
            return new AstLocalGlobal(context, context.GetText());
        }
        
        /// <summary>
        /// db_state_option
        /// 	 : (ONLINE | OFFLINE | EMERGENCY)
        /// </summary>
        public override AstRoot VisitDb_state_option(TSqlParser.Db_state_optionContext context)
        {
            return new AstDbStateOption(context, context.GetText());
        }
        
        /// <summary>
        /// db_update_option
        /// 	 : READ_ONLY
        /// 	 | READ_WRITE
        /// </summary>
        public override AstRoot VisitDb_update_option(TSqlParser.Db_update_optionContext context)
        {
            return new AstDbUpdateOption(context, context.GetText());
        }
        
        /// <summary>
        /// db_user_access_option
        /// 	 : SINGLE_USER
        /// 	 | RESTRICTED_USER
        /// 	 | MULTI_USER
        /// </summary>
        public override AstRoot VisitDb_user_access_option(TSqlParser.Db_user_access_optionContext context)
        {
            return new AstDbUserAccessOption(context, context.GetText());
        }
        
        /// <summary>
        /// algorithm
        /// 	 : DES
        /// 	 | TRIPLE_DES
        /// 	 | TRIPLE_DES_3KEY
        /// 	 | RC2
        /// 	 | RC4
        /// 	 | RC4_128
        /// 	 | DESX
        /// 	 | AES_128
        /// 	 | AES_192
        /// 	 | AES_256
        /// </summary>
        public override AstRoot VisitAlgorithm(TSqlParser.AlgorithmContext context)
        {
            return new AstAlgorithm(context, context.GetText());
        }
        
        /// <summary>
        /// sensitive
        /// 	 : SEMI_SENSITIVE
        /// 	 | INSENSITIVE
        /// </summary>
        public override AstRoot VisitSensitive(TSqlParser.SensitiveContext context)
        {
            return new AstSensitive(context, context.GetText());
        }
        
        /// <summary>
        /// absolute_relative
        /// 	 : ABSOLUTE
        /// 	 | RELATIVE
        /// </summary>
        public override AstRoot VisitAbsolute_relative(TSqlParser.Absolute_relativeContext context)
        {
            return new AstAbsoluteRelative(context, context.GetText());
        }
        
        /// <summary>
        /// fetch_cursor_strategy
        /// 	 : NEXT
        /// 	 | PRIOR
        /// 	 | FIRST
        /// 	 | LAST
        /// </summary>
        public override AstRoot VisitFetch_cursor_strategy(TSqlParser.Fetch_cursor_strategyContext context)
        {
            return new AstFetchCursorStrategy(context, context.GetText());
        }
        
        /// <summary>
        /// special_list
        /// 	 : ANSI_NULLS
        /// 	 | QUOTED_IDENTIFIER
        /// 	 | ANSI_PADDING
        /// 	 | ANSI_WARNINGS
        /// 	 | ANSI_DEFAULTS
        /// 	 | ANSI_NULL_DFLT_OFF
        /// 	 | ANSI_NULL_DFLT_ON
        /// 	 | ARITHABORT
        /// 	 | ARITHIGNORE
        /// 	 | CONCAT_NULL_YIELDS_NULL
        /// 	 | CURSOR_CLOSE_ON_COMMIT
        /// 	 | FMTONLY
        /// 	 | FORCEPLAN
        /// 	 | IMPLICIT_TRANSACTIONS
        /// 	 | NOCOUNT
        /// 	 | NOEXEC
        /// 	 | NUMERIC_ROUNDABORT
        /// 	 | PARSEONLY
        /// 	 | REMOTE_PROC_TRANSACTIONS
        /// 	 | SHOWPLAN_ALL
        /// 	 | SHOWPLAN_TEXT
        /// 	 | SHOWPLAN_XML
        /// 	 | XACT_ABORT
        /// </summary>
        public override AstRoot VisitSpecial_list(TSqlParser.Special_listContext context)
        {
            return new AstSpecialList(context, context.GetText());
        }
        
        /// <summary>
        /// sybase_legacy_hint
        /// 	 : HOLDLOCK
        /// 	 | NOHOLDLOCK
        /// 	 | READPAST
        /// 	 | SHARED
        /// </summary>
        public override AstRoot VisitSybase_legacy_hint(TSqlParser.Sybase_legacy_hintContext context)
        {
            return new AstSybaseLegacyHint(context, context.GetText());
        }
        
        /// <summary>
        /// on_off
        /// 	 : ON
        /// 	 | OFF
        /// </summary>
        public override AstRoot VisitOn_off(TSqlParser.On_offContext context)
        {
            return new AstOnOff(context, context.GetText());
        }
        
        /// <summary>
        /// clustered
        /// 	 : CLUSTERED
        /// 	 | NONCLUSTERED
        /// </summary>
        public override AstRoot VisitClustered(TSqlParser.ClusteredContext context)
        {
            return new AstClustered(context, context.GetText());
        }
        
        /// <summary>
        /// sign
        /// 	 : PLUS
        /// 	 | MINUS
        /// </summary>
        public override AstRoot VisitSign(TSqlParser.SignContext context)
        {
            return new AstSign(context, context.GetText());
        }
        
        /// <summary>
        /// keyword
        /// 	 : ABORT
        /// 	 | ABSOLUTE
        /// 	 | ACCENT_SENSITIVITY
        /// 	 | ACCESS
        /// 	 | ACTION
        /// 	 | ACTIVATION
        /// 	 | ACTIVE
        /// 	 | ADD
        /// 	 | ADDRESS
        /// 	 | AES_128
        /// 	 | AES_192
        /// 	 | AES_256
        /// 	 | AFFINITY
        /// 	 | AFTER
        /// 	 | AGGREGATE
        /// 	 | ALGORITHM
        /// 	 | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS
        /// 	 | ALLOW_PAGE_LOCKS
        /// 	 | ALLOW_ROW_LOCKS
        /// 	 | ALLOW_SNAPSHOT_ISOLATION
        /// 	 | ALLOWED
        /// 	 | ALWAYS
        /// 	 | ANSI_DEFAULTS
        /// 	 | ANSI_NULL_DEFAULT
        /// 	 | ANSI_NULL_DFLT_OFF
        /// 	 | ANSI_NULL_DFLT_ON
        /// 	 | ANSI_NULLS
        /// 	 | ANSI_PADDING
        /// 	 | ANSI_WARNINGS
        /// 	 | APP_NAME
        /// 	 | APPLICATION_LOG
        /// 	 | APPLOCK_MODE
        /// 	 | APPLOCK_TEST
        /// 	 | APPLY
        /// 	 | ARITHABORT
        /// 	 | ARITHIGNORE
        /// 	 | ASCII
        /// 	 | ASSEMBLY
        /// 	 | ASSEMBLYPROPERTY
        /// 	 | AT_KEYWORD
        /// 	 | AUDIT
        /// 	 | AUDIT_GUID
        /// 	 | AUTO
        /// 	 | AUTO_CLEANUP
        /// 	 | AUTO_CLOSE
        /// 	 | AUTO_CREATE_STATISTICS
        /// 	 | AUTO_DROP
        /// 	 | AUTO_SHRINK
        /// 	 | AUTO_UPDATE_STATISTICS
        /// 	 | AUTO_UPDATE_STATISTICS_ASYNC
        /// 	 | AUTOGROW_ALL_FILES
        /// 	 | AUTOGROW_SINGLE_FILE
        /// 	 | AVAILABILITY
        /// 	 | AVG
        /// 	 | BACKUP_PRIORITY
        /// 	 | BASE64
        /// 	 | BEGIN_DIALOG
        /// 	 | BIGINT
        /// 	 | BINARY_KEYWORD
        /// 	 | BINARY_CHECKSUM
        /// 	 | BINDING
        /// 	 | BLOB_STORAGE
        /// 	 | BROKER
        /// 	 | BROKER_INSTANCE
        /// 	 | BULK_LOGGED
        /// 	 | CALLER
        /// 	 | CAP_CPU_PERCENT
        /// 	 | CAST
        /// 	 | TRY_CAST
        /// 	 | CATALOG
        /// 	 | CATCH
        /// 	 | CHANGE
        /// 	 | CHANGE_RETENTION
        /// 	 | CHANGE_TRACKING
        /// 	 | CHAR
        /// 	 | CHARINDEX
        /// 	 | CHECKSUM
        /// 	 | CHECKSUM_AGG
        /// 	 | CLEANUP
        /// 	 | COL_LENGTH
        /// 	 | COL_NAME
        /// 	 | COLLECTION
        /// 	 | COLUMN_ENCRYPTION_KEY
        /// 	 | COLUMN_MASTER_KEY
        /// 	 | COLUMNPROPERTY
        /// 	 | COLUMNS
        /// 	 | COLUMNSTORE
        /// 	 | COLUMNSTORE_ARCHIVE
        /// 	 | COMMITTED
        /// 	 | COMPATIBILITY_LEVEL
        /// 	 | COMPRESS_ALL_ROW_GROUPS
        /// 	 | COMPRESSION_DELAY
        /// 	 | CONCAT
        /// 	 | CONCAT_WS
        /// 	 | CONCAT_NULL_YIELDS_NULL
        /// 	 | CONTENT
        /// 	 | CONTROL
        /// 	 | COOKIE
        /// 	 | COUNT
        /// 	 | COUNT_BIG
        /// 	 | COUNTER
        /// 	 | CPU
        /// 	 | CREATE_NEW
        /// 	 | CREATION_DISPOSITION
        /// 	 | CREDENTIAL
        /// 	 | CRYPTOGRAPHIC
        /// 	 | CUME_DIST
        /// 	 | CURSOR_CLOSE_ON_COMMIT
        /// 	 | CURSOR_DEFAULT
        /// 	 | DATA
        /// 	 | DATABASE_PRINCIPAL_ID
        /// 	 | DATABASEPROPERTYEX
        /// 	 | DATE_CORRELATION_OPTIMIZATION
        /// 	 | DATEADD
        /// 	 | DATEDIFF
        /// 	 | DATENAME
        /// 	 | DATEPART
        /// 	 | DAYS
        /// 	 | DB_CHAINING
        /// 	 | DB_FAILOVER
        /// 	 | DB_ID
        /// 	 | DB_NAME
        /// 	 | DECRYPTION
        /// 	 | DEFAULT_DOUBLE_QUOTE
        /// 	 | DEFAULT_FULLTEXT_LANGUAGE
        /// 	 | DEFAULT_LANGUAGE
        /// 	 | DEFINITION
        /// 	 | DELAY
        /// 	 | DELAYED_DURABILITY
        /// 	 | DELETED
        /// 	 | DENSE_RANK
        /// 	 | DEPENDENTS
        /// 	 | DES
        /// 	 | DESCRIPTION
        /// 	 | DESX
        /// 	 | DETERMINISTIC
        /// 	 | DHCP
        /// 	 | DIALOG
        /// 	 | DIFFERENCE
        /// 	 | DIRECTORY_NAME
        /// 	 | DISABLE
        /// 	 | DISABLE_BROKER
        /// 	 | DISABLED
        /// 	 | DOCUMENT
        /// 	 | DROP_EXISTING
        /// 	 | DYNAMIC
        /// 	 | ELEMENTS
        /// 	 | EMERGENCY
        /// 	 | EMPTY
        /// 	 | ENABLE
        /// 	 | ENABLE_BROKER
        /// 	 | ENCRYPTED
        /// 	 | ENCRYPTED_VALUE
        /// 	 | ENCRYPTION
        /// 	 | ENCRYPTION_TYPE
        /// 	 | ENDPOINT_URL
        /// 	 | ERROR_BROKER_CONVERSATIONS
        /// 	 | EXCLUSIVE
        /// 	 | EXECUTABLE
        /// 	 | EXIST
        /// 	 | EXPAND
        /// 	 | EXPIRY_DATE
        /// 	 | EXPLICIT
        /// 	 | FAIL_OPERATION
        /// 	 | FAILOVER_MODE
        /// 	 | FAILURE
        /// 	 | FAILURE_CONDITION_LEVEL
        /// 	 | FAST
        /// 	 | FAST_FORWARD
        /// 	 | FILE_ID
        /// 	 | FILE_IDEX
        /// 	 | FILE_NAME
        /// 	 | FILEGROUP
        /// 	 | FILEGROUP_ID
        /// 	 | FILEGROUP_NAME
        /// 	 | FILEGROUPPROPERTY
        /// 	 | FILEGROWTH
        /// 	 | FILENAME
        /// 	 | FILEPATH
        /// 	 | FILEPROPERTY
        /// 	 | FILEPROPERTYEX
        /// 	 | FILESTREAM
        /// 	 | FILTER
        /// 	 | FIRST
        /// 	 | FIRST_VALUE
        /// 	 | FMTONLY
        /// 	 | FOLLOWING
        /// 	 | FORCE
        /// 	 | FORCE_FAILOVER_ALLOW_DATA_LOSS
        /// 	 | FORCED
        /// 	 | FORCEPLAN
        /// 	 | FORCESCAN
        /// 	 | FORMAT
        /// 	 | FORWARD_ONLY
        /// 	 | FULLSCAN
        /// 	 | FULLTEXT
        /// 	 | FULLTEXTCATALOGPROPERTY
        /// 	 | FULLTEXTSERVICEPROPERTY
        /// 	 | GB
        /// 	 | GENERATED
        /// 	 | GETDATE
        /// 	 | GETUTCDATE
        /// 	 | GLOBAL
        /// 	 | GO
        /// 	 | GROUP_MAX_REQUESTS
        /// 	 | GROUPING
        /// 	 | GROUPING_ID
        /// 	 | HADR
        /// 	 | HASH
        /// 	 | HEALTH_CHECK_TIMEOUT
        /// 	 | HIDDEN_KEYWORD
        /// 	 | HIGH
        /// 	 | HONOR_BROKER_PRIORITY
        /// 	 | HOURS
        /// 	 | IDENTITY_VALUE
        /// 	 | IGNORE_CONSTRAINTS
        /// 	 | IGNORE_DUP_KEY
        /// 	 | IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX
        /// 	 | IGNORE_TRIGGERS
        /// 	 | IMMEDIATE
        /// 	 | IMPERSONATE
        /// 	 | IMPLICIT_TRANSACTIONS
        /// 	 | IMPORTANCE
        /// 	 | INCLUDE_NULL_VALUES
        /// 	 | INCREMENTAL
        /// 	 | INDEX_COL
        /// 	 | INDEXKEY_PROPERTY
        /// 	 | INDEXPROPERTY
        /// 	 | INITIATOR
        /// 	 | INPUT
        /// 	 | INSENSITIVE
        /// 	 | INSERTED
        /// 	 | INT
        /// 	 | IP
        /// 	 | ISOLATION
        /// 	 | JOB
        /// 	 | JSON
        /// 	 | KB
        /// 	 | KEEP
        /// 	 | KEEPDEFAULTS
        /// 	 | KEEPFIXED
        /// 	 | KEEPIDENTITY
        /// 	 | KEY_SOURCE
        /// 	 | KEYS
        /// 	 | KEYSET
        /// 	 | LAG
        /// 	 | LAST
        /// 	 | LAST_VALUE
        /// 	 | LEAD
        /// 	 | LEN
        /// 	 | LEVEL
        /// 	 | LIST
        /// 	 | LISTENER
        /// 	 | LISTENER_URL
        /// 	 | LOB_COMPACTION
        /// 	 | LOCAL
        /// 	 | LOCATION
        /// 	 | LOCK
        /// 	 | LOCK_ESCALATION
        /// 	 | LOGIN
        /// 	 | LOOP
        /// 	 | LOW
        /// 	 | LOWER
        /// 	 | LTRIM
        /// 	 | MANUAL
        /// 	 | MARK
        /// 	 | MASKED
        /// 	 | MATERIALIZED
        /// 	 | MAX
        /// 	 | MAX_CPU_PERCENT
        /// 	 | MAX_DOP
        /// 	 | MAX_FILES
        /// 	 | MAX_IOPS_PER_VOLUME
        /// 	 | MAX_MEMORY_PERCENT
        /// 	 | MAX_PROCESSES
        /// 	 | MAX_QUEUE_READERS
        /// 	 | MAX_ROLLOVER_FILES
        /// 	 | MAXDOP
        /// 	 | MAXRECURSION
        /// 	 | MAXSIZE
        /// 	 | MB
        /// 	 | MEDIUM
        /// 	 | MEMORY_OPTIMIZED_DATA
        /// 	 | MESSAGE
        /// 	 | MIN
        /// 	 | MIN_ACTIVE_ROWVERSION
        /// 	 | MIN_CPU_PERCENT
        /// 	 | MIN_IOPS_PER_VOLUME
        /// 	 | MIN_MEMORY_PERCENT
        /// 	 | MINUTES
        /// 	 | MIRROR_ADDRESS
        /// 	 | MIXED_PAGE_ALLOCATION
        /// 	 | MODE
        /// 	 | MODIFY
        /// 	 | MOVE
        /// 	 | MULTI_USER
        /// 	 | NAME
        /// 	 | NCHAR
        /// 	 | NESTED_TRIGGERS
        /// 	 | NEW_ACCOUNT
        /// 	 | NEW_BROKER
        /// 	 | NEW_PASSWORD
        /// 	 | NEWNAME
        /// 	 | NEXT
        /// 	 | NO
        /// 	 | NO_TRUNCATE
        /// 	 | NO_WAIT
        /// 	 | NOCOUNT
        /// 	 | NODES
        /// 	 | NOEXEC
        /// 	 | NOEXPAND
        /// 	 | NOLOCK
        /// 	 | NON_TRANSACTED_ACCESS
        /// 	 | NORECOMPUTE
        /// 	 | NORECOVERY
        /// 	 | NOTIFICATIONS
        /// 	 | NOWAIT
        /// 	 | NTILE
        /// 	 | NULL_DOUBLE_QUOTE
        /// 	 | NUMANODE
        /// 	 | NUMBER
        /// 	 | NUMERIC_ROUNDABORT
        /// 	 | OBJECT
        /// 	 | OBJECT_DEFINITION
        /// 	 | OBJECT_ID
        /// 	 | OBJECT_NAME
        /// 	 | OBJECT_SCHEMA_NAME
        /// 	 | OBJECTPROPERTY
        /// 	 | OBJECTPROPERTYEX
        /// 	 | OFFLINE
        /// 	 | OFFSET
        /// 	 | OLD_ACCOUNT
        /// 	 | ONLINE
        /// 	 | ONLY
        /// 	 | OPEN_EXISTING
        /// 	 | OPENJSON
        /// 	 | OPTIMISTIC
        /// 	 | OPTIMIZE
        /// 	 | OPTIMIZE_FOR_SEQUENTIAL_KEY
        /// 	 | ORIGINAL_DB_NAME
        /// 	 | OUT
        /// 	 | OUTPUT
        /// 	 | OVERRIDE
        /// 	 | OWNER
        /// 	 | OWNERSHIP
        /// 	 | PAD_INDEX
        /// 	 | PAGE_VERIFY
        /// 	 | PAGECOUNT
        /// 	 | PAGLOCK
        /// 	 | PARAMETERIZATION
        /// 	 | PARSENAME
        /// 	 | PARSEONLY
        /// 	 | PARTITION
        /// 	 | PARTITIONS
        /// 	 | PARTNER
        /// 	 | PATH
        /// 	 | PATINDEX
        /// 	 | PAUSE
        /// 	 | PERCENT_RANK
        /// 	 | PERCENTILE_CONT
        /// 	 | PERCENTILE_DISC
        /// 	 | PERSIST_SAMPLE_PERCENT
        /// 	 | POISON_MESSAGE_HANDLING
        /// 	 | POOL
        /// 	 | PORT
        /// 	 | PRECEDING
        /// 	 | PRIMARY_ROLE
        /// 	 | PRIOR
        /// 	 | PRIORITY
        /// 	 | PRIORITY_LEVEL
        /// 	 | PRIVATE
        /// 	 | PRIVATE_KEY
        /// 	 | PRIVILEGES
        /// 	 | PROCEDURE_NAME
        /// 	 | PROPERTY
        /// 	 | PROVIDER
        /// 	 | PROVIDER_KEY_NAME
        /// 	 | QUERY
        /// 	 | QUEUE
        /// 	 | QUEUE_DELAY
        /// 	 | QUOTED_IDENTIFIER
        /// 	 | QUOTENAME
        /// 	 | RANDOMIZED
        /// 	 | RANGE
        /// 	 | RANK
        /// 	 | RC2
        /// 	 | RC4
        /// 	 | RC4_128
        /// 	 | READ_COMMITTED_SNAPSHOT
        /// 	 | READ_ONLY
        /// 	 | READ_ONLY_ROUTING_LIST
        /// 	 | READ_WRITE
        /// 	 | READCOMMITTED
        /// 	 | READCOMMITTEDLOCK
        /// 	 | READONLY
        /// 	 | READPAST
        /// 	 | READUNCOMMITTED
        /// 	 | READWRITE
        /// 	 | REBUILD
        /// 	 | RECEIVE
        /// 	 | RECOMPILE
        /// 	 | RECOVERY
        /// 	 | RECURSIVE_TRIGGERS
        /// 	 | RELATIVE
        /// 	 | REMOTE
        /// 	 | REMOTE_PROC_TRANSACTIONS
        /// 	 | REMOTE_SERVICE_NAME
        /// 	 | REMOVE
        /// 	 | REORGANIZE
        /// 	 | REPEATABLE
        /// 	 | REPEATABLEREAD
        /// 	 | REPLACE
        /// 	 | REPLICA
        /// 	 | REPLICATE
        /// 	 | REQUEST_MAX_CPU_TIME_SEC
        /// 	 | REQUEST_MAX_MEMORY_GRANT_PERCENT
        /// 	 | REQUEST_MEMORY_GRANT_TIMEOUT_SEC
        /// 	 | REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT
        /// 	 | RESAMPLE
        /// 	 | RESERVE_DISK_SPACE
        /// 	 | RESOURCE
        /// 	 | RESOURCE_MANAGER_LOCATION
        /// 	 | RESTRICTED_USER
        /// 	 | RESUMABLE
        /// 	 | RETENTION
        /// 	 | REVERSE
        /// 	 | ROBUST
        /// 	 | ROOT
        /// 	 | ROUTE
        /// 	 | ROW
        /// 	 | ROW_NUMBER
        /// 	 | ROWGUID
        /// 	 | ROWLOCK
        /// 	 | ROWS
        /// 	 | RTRIM
        /// 	 | SAMPLE
        /// 	 | SCHEMA_ID
        /// 	 | SCHEMA_NAME
        /// 	 | SCHEMABINDING
        /// 	 | SCOPE_IDENTITY
        /// 	 | SCOPED
        /// 	 | SCROLL
        /// 	 | SCROLL_LOCKS
        /// 	 | SEARCH
        /// 	 | SECONDARY
        /// 	 | SECONDARY_ONLY
        /// 	 | SECONDARY_ROLE
        /// 	 | SECONDS
        /// 	 | SECRET
        /// 	 | SECURABLES
        /// 	 | SECURITY
        /// 	 | SECURITY_LOG
        /// 	 | SEEDING_MODE
        /// 	 | SELF
        /// 	 | SEMI_SENSITIVE
        /// 	 | SEND
        /// 	 | SENT
        /// 	 | SEQUENCE
        /// 	 | SEQUENCE_NUMBER
        /// 	 | SERIALIZABLE
        /// 	 | SERVERPROPERTY
        /// 	 | SESSION_TIMEOUT
        /// 	 | SETERROR
        /// 	 | SHARE
        /// 	 | SHARED
        /// 	 | SHOWPLAN
        /// 	 | SHOWPLAN_ALL
        /// 	 | SHOWPLAN_TEXT
        /// 	 | SHOWPLAN_XML
        /// 	 | SIGNATURE
        /// 	 | SIMPLE
        /// 	 | SINGLE_USER
        /// 	 | SIZE
        /// 	 | SMALLINT
        /// 	 | SNAPSHOT
        /// 	 | SORT_IN_TEMPDB
        /// 	 | SOUNDEX
        /// 	 | SPACE_KEYWORD
        /// 	 | SPARSE
        /// 	 | SPATIAL_WINDOW_MAX_CELLS
        /// 	 | STANDBY
        /// 	 | START_DATE
        /// 	 | STATIC
        /// 	 | STATISTICS_INCREMENTAL
        /// 	 | STATISTICS_NORECOMPUTE
        /// 	 | STATS_DATE
        /// 	 | STATS_STREAM
        /// 	 | STATUS
        /// 	 | STATUSONLY
        /// 	 | STDEV
        /// 	 | STDEVP
        /// 	 | STOPLIST
        /// 	 | STR
        /// 	 | STRING_AGG
        /// 	 | STRING_ESCAPE
        /// 	 | STUFF
        /// 	 | SUBJECT
        /// 	 | SUBSCRIBE
        /// 	 | SUBSCRIPTION
        /// 	 | SUBSTRING
        /// 	 | SUM
        /// 	 | SUSPEND
        /// 	 | SYMMETRIC
        /// 	 | SYNCHRONOUS_COMMIT
        /// 	 | SYNONYM
        /// 	 | SYSTEM
        /// 	 | TABLOCK
        /// 	 | TABLOCKX
        /// 	 | TAKE
        /// 	 | TARGET_RECOVERY_TIME
        /// 	 | TB
        /// 	 | TEXTIMAGE_ON
        /// 	 | THROW
        /// 	 | TIES
        /// 	 | TIME
        /// 	 | TIMEOUT
        /// 	 | TIMER
        /// 	 | TINYINT
        /// 	 | TORN_PAGE_DETECTION
        /// 	 | TRACKING
        /// 	 | TRANSACTION_ID
        /// 	 | TRANSFORM_NOISE_WORDS
        /// 	 | TRANSLATE
        /// 	 | TRIM
        /// 	 | TRIPLE_DES
        /// 	 | TRIPLE_DES_3KEY
        /// 	 | TRUSTWORTHY
        /// 	 | TRY
        /// 	 | TSQL
        /// 	 | TWO_DIGIT_YEAR_CUTOFF
        /// 	 | TYPE
        /// 	 | TYPE_ID
        /// 	 | TYPE_NAME
        /// 	 | TYPE_WARNING
        /// 	 | TYPEPROPERTY
        /// 	 | UNBOUNDED
        /// 	 | UNCOMMITTED
        /// 	 | UNICODE
        /// 	 | UNKNOWN
        /// 	 | UNLIMITED
        /// 	 | UNMASK
        /// 	 | UOW
        /// 	 | UPDLOCK
        /// 	 | UPPER
        /// 	 | USING
        /// 	 | VALID_XML
        /// 	 | VALIDATION
        /// 	 | VALUE
        /// 	 | VAR
        /// 	 | VARBINARY_KEYWORD
        /// 	 | VARP
        /// 	 | VERSION
        /// 	 | VIEW_METADATA
        /// 	 | VIEWS
        /// 	 | WAIT
        /// 	 | WELL_FORMED_XML
        /// 	 | WITHOUT_ARRAY_WRAPPER
        /// 	 | WORK
        /// 	 | WORKLOAD
        /// 	 | XLOCK
        /// 	 | XML
        /// 	 | XML_COMPRESSION
        /// 	 | XMLDATA
        /// 	 | XMLNAMESPACES
        /// 	 | XMLSCHEMA
        /// 	 | XSINIL
        /// 	 | ZONE
        /// 	 | ABORT_AFTER_WAIT
        /// 	 | ABSENT
        /// 	 | ADMINISTER
        /// 	 | AES
        /// 	 | ALLOW_CONNECTIONS
        /// 	 | ALLOW_MULTIPLE_EVENT_LOSS
        /// 	 | ALLOW_SINGLE_EVENT_LOSS
        /// 	 | ANONYMOUS
        /// 	 | APPEND
        /// 	 | APPLICATION
        /// 	 | ASYMMETRIC
        /// 	 | ASYNCHRONOUS_COMMIT
        /// 	 | AUTHENTICATE
        /// 	 | AUTHENTICATION
        /// 	 | AUTOMATED_BACKUP_PREFERENCE
        /// 	 | AUTOMATIC
        /// 	 | AVAILABILITY_MODE
        /// 	 | BEFORE
        /// 	 | BLOCK
        /// 	 | BLOCKERS
        /// 	 | BLOCKSIZE
        /// 	 | BLOCKING_HIERARCHY
        /// 	 | BUFFER
        /// 	 | BUFFERCOUNT
        /// 	 | CACHE
        /// 	 | CALLED
        /// 	 | CERTIFICATE
        /// 	 | CHANGETABLE
        /// 	 | CHANGES
        /// 	 | CHECK_POLICY
        /// 	 | CHECK_EXPIRATION
        /// 	 | CLASSIFIER_FUNCTION
        /// 	 | CLUSTER
        /// 	 | COMPRESS
        /// 	 | COMPRESSION
        /// 	 | CONNECT
        /// 	 | CONNECTION
        /// 	 | CONFIGURATION
        /// 	 | CONNECTIONPROPERTY
        /// 	 | CONTAINMENT
        /// 	 | CONTEXT
        /// 	 | CONTEXT_INFO
        /// 	 | CONTINUE_AFTER_ERROR
        /// 	 | CONTRACT
        /// 	 | CONTRACT_NAME
        /// 	 | CONVERSATION
        /// 	 | COPY_ONLY
        /// 	 | CURRENT_REQUEST_ID
        /// 	 | CURRENT_TRANSACTION_ID
        /// 	 | CYCLE
        /// 	 | DATA_COMPRESSION
        /// 	 | DATA_SOURCE
        /// 	 | DATABASE_MIRRORING
        /// 	 | DATASPACE
        /// 	 | DDL
        /// 	 | DECOMPRESS
        /// 	 | DEFAULT_DATABASE
        /// 	 | DEFAULT_SCHEMA
        /// 	 | DIAGNOSTICS
        /// 	 | DIFFERENTIAL
        /// 	 | DISTRIBUTION
        /// 	 | DTC_SUPPORT
        /// 	 | ENABLED
        /// 	 | ENDPOINT
        /// 	 | ERROR
        /// 	 | ERROR_LINE
        /// 	 | ERROR_MESSAGE
        /// 	 | ERROR_NUMBER
        /// 	 | ERROR_PROCEDURE
        /// 	 | ERROR_SEVERITY
        /// 	 | ERROR_STATE
        /// 	 | EVENT
        /// 	 | EVENTDATA
        /// 	 | EVENT_RETENTION_MODE
        /// 	 | EXECUTABLE_FILE
        /// 	 | EXPIREDATE
        /// 	 | EXTENSION
        /// 	 | EXTERNAL_ACCESS
        /// 	 | FAILOVER
        /// 	 | FAILURECONDITIONLEVEL
        /// 	 | FAN_IN
        /// 	 | FILE_SNAPSHOT
        /// 	 | FORCESEEK
        /// 	 | FORCE_SERVICE_ALLOW_DATA_LOSS
        /// 	 | FORMATMESSAGE
        /// 	 | GET
        /// 	 | GET_FILESTREAM_TRANSACTION_CONTEXT
        /// 	 | GETANCESTOR
        /// 	 | GETANSINULL
        /// 	 | GETDESCENDANT
        /// 	 | GETLEVEL
        /// 	 | GETREPARENTEDVALUE
        /// 	 | GETROOT
        /// 	 | GOVERNOR
        /// 	 | HASHED
        /// 	 | HEALTHCHECKTIMEOUT
        /// 	 | HEAP
        /// 	 | HIERARCHYID
        /// 	 | HOST_ID
        /// 	 | HOST_NAME
        /// 	 | IIF
        /// 	 | IO
        /// 	 | INCLUDE
        /// 	 | INCREMENT
        /// 	 | INFINITE
        /// 	 | INIT
        /// 	 | INSTEAD
        /// 	 | ISDESCENDANTOF
        /// 	 | ISNULL
        /// 	 | ISNUMERIC
        /// 	 | KERBEROS
        /// 	 | KEY_PATH
        /// 	 | KEY_STORE_PROVIDER_NAME
        /// 	 | LANGUAGE
        /// 	 | LIBRARY
        /// 	 | LIFETIME
        /// 	 | LINKED
        /// 	 | LINUX
        /// 	 | LISTENER_IP
        /// 	 | LISTENER_PORT
        /// 	 | LOCAL_SERVICE_NAME
        /// 	 | LOG
        /// 	 | MASK
        /// 	 | MATCHED
        /// 	 | MASTER
        /// 	 | MAX_MEMORY
        /// 	 | MAXTRANSFER
        /// 	 | MAXVALUE
        /// 	 | MAX_DISPATCH_LATENCY
        /// 	 | MAX_DURATION
        /// 	 | MAX_EVENT_SIZE
        /// 	 | MAX_SIZE
        /// 	 | MAX_OUTSTANDING_IO_PER_VOLUME
        /// 	 | MEDIADESCRIPTION
        /// 	 | MEDIANAME
        /// 	 | MEMBER
        /// 	 | MEMORY_PARTITION_MODE
        /// 	 | MESSAGE_FORWARDING
        /// 	 | MESSAGE_FORWARD_SIZE
        /// 	 | MINVALUE
        /// 	 | MIRROR
        /// 	 | MUST_CHANGE
        /// 	 | NEWID
        /// 	 | NEWSEQUENTIALID
        /// 	 | NOFORMAT
        /// 	 | NOINIT
        /// 	 | NONE
        /// 	 | NOREWIND
        /// 	 | NOSKIP
        /// 	 | NOUNLOAD
        /// 	 | NO_CHECKSUM
        /// 	 | NO_COMPRESSION
        /// 	 | NO_EVENT_LOSS
        /// 	 | NOTIFICATION
        /// 	 | NTLM
        /// 	 | OLD_PASSWORD
        /// 	 | ON_FAILURE
        /// 	 | OPERATIONS
        /// 	 | PAGE
        /// 	 | PARAM_NODE
        /// 	 | PARTIAL
        /// 	 | PASSWORD
        /// 	 | PERMISSION_SET
        /// 	 | PER_CPU
        /// 	 | PER_DB
        /// 	 | PER_NODE
        /// 	 | PERSISTED
        /// 	 | PLATFORM
        /// 	 | POLICY
        /// 	 | PREDICATE
        /// 	 | PROCESS
        /// 	 | PROFILE
        /// 	 | PYTHON
        /// 	 | R
        /// 	 | READ_WRITE_FILEGROUPS
        /// 	 | REGENERATE
        /// 	 | RELATED_CONVERSATION
        /// 	 | RELATED_CONVERSATION_GROUP
        /// 	 | REQUIRED
        /// 	 | RESET
        /// 	 | RESOURCES
        /// 	 | RESTART
        /// 	 | RESUME
        /// 	 | RETAINDAYS
        /// 	 | RETURNS
        /// 	 | REWIND
        /// 	 | ROLE
        /// 	 | ROUND_ROBIN
        /// 	 | ROWCOUNT_BIG
        /// 	 | RSA_512
        /// 	 | RSA_1024
        /// 	 | RSA_2048
        /// 	 | RSA_3072
        /// 	 | RSA_4096
        /// 	 | SAFETY
        /// 	 | SAFE
        /// 	 | SCHEDULER
        /// 	 | SCHEME
        /// 	 | SCRIPT
        /// 	 | SERVER
        /// 	 | SERVICE
        /// 	 | SERVICE_BROKER
        /// 	 | SERVICE_NAME
        /// 	 | SESSION
        /// 	 | SESSION_CONTEXT
        /// 	 | SETTINGS
        /// 	 | SHRINKLOG
        /// 	 | SID
        /// 	 | SKIP_KEYWORD
        /// 	 | SOFTNUMA
        /// 	 | SOURCE
        /// 	 | SPECIFICATION
        /// 	 | SPLIT
        /// 	 | SQL
        /// 	 | SQLDUMPERFLAGS
        /// 	 | SQLDUMPERPATH
        /// 	 | SQLDUMPERTIMEOUT
        /// 	 | STATE
        /// 	 | STATS
        /// 	 | START
        /// 	 | STARTED
        /// 	 | STARTUP_STATE
        /// 	 | STOP
        /// 	 | STOPPED
        /// 	 | STOP_ON_ERROR
        /// 	 | SUPPORTED
        /// 	 | SWITCH
        /// 	 | TAPE
        /// 	 | TARGET
        /// 	 | TCP
        /// 	 | TOSTRING
        /// 	 | TRACE
        /// 	 | TRACK_CAUSALITY
        /// 	 | TRANSFER
        /// 	 | UNCHECKED
        /// 	 | UNLOCK
        /// 	 | UNSAFE
        /// 	 | URL
        /// 	 | USED
        /// 	 | VERBOSELOGGING
        /// 	 | VISIBILITY
        /// 	 | WAIT_AT_LOW_PRIORITY
        /// 	 | WINDOWS
        /// 	 | WITHOUT
        /// 	 | WITNESS
        /// 	 | XACT_ABORT
        /// 	 | XACT_STATE
        /// 	 | VARCHAR
        /// 	 | NVARCHAR
        /// 	 | PRECISION
        /// </summary>
        public override AstRoot VisitKeyword(TSqlParser.KeywordContext context)
        {
            return new AstKeyword(context, context.GetText());
        }
        
        /// <summary>
        /// file_size_unity
        /// 	 : KB
        /// 	 | MB
        /// 	 | GB
        /// 	 | TB
        /// 	 | MODULE
        /// </summary>
        public override AstRoot VisitFile_size_unity(TSqlParser.File_size_unityContext context)
        {
            return new AstFileSizeUnity(context, context.GetText());
        }
        
        /// <summary>
        /// decimal_string
        /// 	 : DECIMAL
        /// 	 | STRING
        /// </summary>
        public override AstRoot VisitDecimal_string(TSqlParser.Decimal_stringContext context)
        {
            return new AstDecimalString(context, context.GetText());
        }
    }
}
