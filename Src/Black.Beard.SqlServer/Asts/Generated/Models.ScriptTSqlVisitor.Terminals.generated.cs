#nullable disable
// Generated by ScriptClassVisitorTerminalAlias.cs (vendredi 28 avril 2023)
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bb.SqlServer.Parser
{
    using System;
    using Bb.Asts;
    using Bb.Parsers;
    using Bb.SqlServer.Asts;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using System.Collections;
    
    
    public partial class ScriptTSqlVisitor
    {
        
        /// <summary>
        /// break_statement
        /// 	 : BREAK
        /// </summary>
        public override AstRoot VisitBreak_statement(TSqlParser.Break_statementContext context)
        {
            return new AstBreakStatement(context);
        }
        
        /// <summary>
        /// continue_statement
        /// 	 : CONTINUE
        /// </summary>
        public override AstRoot VisitContinue_statement(TSqlParser.Continue_statementContext context)
        {
            return new AstContinueStatement(context);
        }
        
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
        /// empty_value
        /// 	 : DOUBLE_QUOTE_ID
        /// </summary>
        public override AstRoot VisitEmpty_value(TSqlParser.Empty_valueContext context)
        {
            return new AstEmptyValue(context);
        }
        
        /// <summary>
        /// ipv4
        /// 	 : IPV4_ADDR
        /// </summary>
        public override AstRoot VisitIpv4(TSqlParser.Ipv4Context context)
        {
            return new AstIpv4(context);
        }
        
        /// <summary>
        /// ipv6
        /// 	 : IPV6_ADDR
        /// </summary>
        public override AstRoot VisitIpv6(TSqlParser.Ipv6Context context)
        {
            return new AstIpv6(context);
        }
        
        /// <summary>
        /// float
        /// 	 : FLOAT
        /// </summary>
        public override AstRoot VisitFloat(TSqlParser.FloatContext context)
        {
            return new AstFloat(context);
        }
        
        /// <summary>
        /// decimal
        /// 	 : DECIMAL
        /// </summary>
        public override AstRoot VisitDecimal(TSqlParser.DecimalContext context)
        {
            return new AstDecimal(context);
        }
        
        /// <summary>
        /// id_simple
        /// 	 : ID
        /// </summary>
        public override AstRoot VisitId_simple(TSqlParser.Id_simpleContext context)
        {
            return new AstIdSimple(context);
        }
        
        /// <summary>
        /// binary_
        /// 	 : BINARY
        /// </summary>
        public override AstRoot VisitBinary_(TSqlParser.Binary_Context context)
        {
            return new AstBinary(context);
        }
        
        /// <summary>
        /// local_id
        /// 	 : LOCAL_ID
        /// </summary>
        public override AstRoot VisitLocal_id(TSqlParser.Local_idContext context)
        {
            return new AstLocalId(context);
        }
        
        /// <summary>
        /// stringtext
        /// 	 : STRING
        /// </summary>
        public override AstRoot VisitStringtext(TSqlParser.StringtextContext context)
        {
            return new AstStringtext(context);
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
    }
}
