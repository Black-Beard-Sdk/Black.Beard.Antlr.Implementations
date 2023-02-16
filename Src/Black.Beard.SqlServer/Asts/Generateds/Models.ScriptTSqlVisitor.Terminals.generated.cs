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
        /// empty_statement
        /// 	 : SEMI
        /// </summary>
        public override AstRoot VisitEmpty_statement(TSqlParser.Empty_statementContext context)
        {
            return new AstEmptyStatement(context);
        }
        
        /// <summary>
        /// alter_assembly_from_clause_start
        /// 	 : FROM
        /// </summary>
        public override AstRoot VisitAlter_assembly_from_clause_start(TSqlParser.Alter_assembly_from_clause_startContext context)
        {
            return new AstAlterAssemblyFromClauseStart(context);
        }
        
        /// <summary>
        /// alter_assembly_drop
        /// 	 : DROP
        /// </summary>
        public override AstRoot VisitAlter_assembly_drop(TSqlParser.Alter_assembly_dropContext context)
        {
            return new AstAlterAssemblyDrop(context);
        }
        
        /// <summary>
        /// alter_assembly_file_name
        /// 	 : STRING
        /// </summary>
        public override AstRoot VisitAlter_assembly_file_name(TSqlParser.Alter_assembly_file_nameContext context)
        {
            return new AstAlterAssemblyFileName(context);
        }
        
        /// <summary>
        /// alter_assembly_as
        /// 	 : AS
        /// </summary>
        public override AstRoot VisitAlter_assembly_as(TSqlParser.Alter_assembly_asContext context)
        {
            return new AstAlterAssemblyAs(context);
        }
        
        /// <summary>
        /// alter_assembly_with
        /// 	 : WITH
        /// </summary>
        public override AstRoot VisitAlter_assembly_with(TSqlParser.Alter_assembly_withContext context)
        {
            return new AstAlterAssemblyWith(context);
        }
        
        /// <summary>
        /// network_file_start
        /// 	 : DOUBLE_BACK_SLASH
        /// </summary>
        public override AstRoot VisitNetwork_file_start(TSqlParser.Network_file_startContext context)
        {
            return new AstNetworkFileStart(context);
        }
        
        /// <summary>
        /// file_directory_path_separator
        /// 	 : BACKSLASH
        /// </summary>
        public override AstRoot VisitFile_directory_path_separator(TSqlParser.File_directory_path_separatorContext context)
        {
            return new AstFileDirectoryPathSeparator(context);
        }
        
        /// <summary>
        /// local_drive
        /// 	 : DISK_DRIVE
        /// </summary>
        public override AstRoot VisitLocal_drive(TSqlParser.Local_driveContext context)
        {
            return new AstLocalDrive(context);
        }
        
        /// <summary>
        /// multiple_local_file_start
        /// 	 : SINGLE_QUOTE
        /// </summary>
        public override AstRoot VisitMultiple_local_file_start(TSqlParser.Multiple_local_file_startContext context)
        {
            return new AstMultipleLocalFileStart(context);
        }
        
        /// <summary>
        /// entity_to
        /// 	 : TO
        /// </summary>
        public override AstRoot VisitEntity_to(TSqlParser.Entity_toContext context)
        {
            return new AstEntityTo(context);
        }
        
        /// <summary>
        /// colon_colon
        /// 	 : DOUBLE_COLON
        /// </summary>
        public override AstRoot VisitColon_colon(TSqlParser.Colon_colonContext context)
        {
            return new AstColonColon(context);
        }
        
        /// <summary>
        /// server_instance
        /// 	 : STRING
        /// </summary>
        public override AstRoot VisitServer_instance(TSqlParser.Server_instanceContext context)
        {
            return new AstServerInstance(context);
        }
        
        /// <summary>
        /// ip_v4_failover
        /// 	 : STRING
        /// </summary>
        public override AstRoot VisitIp_v4_failover(TSqlParser.Ip_v4_failoverContext context)
        {
            return new AstIpV4Failover(context);
        }
        
        /// <summary>
        /// ip_v6_failover
        /// 	 : STRING
        /// </summary>
        public override AstRoot VisitIp_v6_failover(TSqlParser.Ip_v6_failoverContext context)
        {
            return new AstIpV6Failover(context);
        }
        
        /// <summary>
        /// mirroring_partner
        /// 	 : PARTNER
        /// </summary>
        public override AstRoot VisitMirroring_partner(TSqlParser.Mirroring_partnerContext context)
        {
            return new AstMirroringPartner(context);
        }
        
        /// <summary>
        /// mirroring_witness
        /// 	 : WITNESS
        /// </summary>
        public override AstRoot VisitMirroring_witness(TSqlParser.Mirroring_witnessContext context)
        {
            return new AstMirroringWitness(context);
        }
        
        /// <summary>
        /// witness_partner_equal
        /// 	 : EQUAL
        /// </summary>
        public override AstRoot VisitWitness_partner_equal(TSqlParser.Witness_partner_equalContext context)
        {
            return new AstWitnessPartnerEqual(context);
        }
        
        /// <summary>
        /// mirroring_host_port_seperator
        /// 	 : COLON
        /// </summary>
        public override AstRoot VisitMirroring_host_port_seperator(TSqlParser.Mirroring_host_port_seperatorContext context)
        {
            return new AstMirroringHostPortSeperator(context);
        }
        
        /// <summary>
        /// parameter
        /// 	 : PLACEHOLDER
        /// </summary>
        public override AstRoot VisitParameter(TSqlParser.ParameterContext context)
        {
            return new AstParameter(context);
        }
        
        /// <summary>
        /// star_asterisk
        /// 	 : STAR
        /// </summary>
        public override AstRoot VisitStar_asterisk(TSqlParser.Star_asteriskContext context)
        {
            return new AstStarAsterisk(context);
        }
        
        /// <summary>
        /// id_
        /// 	 : ID
        /// 	 | DOUBLE_QUOTE_ID
        /// 	 | DOUBLE_QUOTE_BLANK
        /// 	 | SQUARE_BRACKET_ID
        /// 	 | keyword
        /// </summary>
        public override AstRoot VisitId_(TSqlParser.Id_Context context)
        {
            return new AstId(context);
        }
        
        /// <summary>
        /// simple_id
        /// 	 : ID
        /// </summary>
        public override AstRoot VisitSimple_id(TSqlParser.Simple_idContext context)
        {
            return new AstSimpleId(context);
        }
    }
}
