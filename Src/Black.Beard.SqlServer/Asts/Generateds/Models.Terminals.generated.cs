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
    /// empty_statement
    /// 	 : SEMI
    /// </summary>
    public partial class AstEmptyStatement : AstTerminal<string>
    {
        
        public AstEmptyStatement(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstEmptyStatement(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstEmptyStatement(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitEmptyStatement(this);
        }
    }
    
    /// <summary>
    /// alter_assembly_from_clause_start
    /// 	 : FROM
    /// </summary>
    public partial class AstAlterAssemblyFromClauseStart : AstTerminal<string>
    {
        
        public AstAlterAssemblyFromClauseStart(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstAlterAssemblyFromClauseStart(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstAlterAssemblyFromClauseStart(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyFromClauseStart(this);
        }
    }
    
    /// <summary>
    /// alter_assembly_drop
    /// 	 : DROP
    /// </summary>
    public partial class AstAlterAssemblyDrop : AstTerminal<string>
    {
        
        public AstAlterAssemblyDrop(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstAlterAssemblyDrop(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstAlterAssemblyDrop(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyDrop(this);
        }
    }
    
    /// <summary>
    /// alter_assembly_file_name
    /// 	 : STRING
    /// </summary>
    public partial class AstAlterAssemblyFileName : AstTerminal<string>
    {
        
        public AstAlterAssemblyFileName(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstAlterAssemblyFileName(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstAlterAssemblyFileName(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyFileName(this);
        }
    }
    
    /// <summary>
    /// alter_assembly_as
    /// 	 : AS
    /// </summary>
    public partial class AstAlterAssemblyAs : AstTerminal<string>
    {
        
        public AstAlterAssemblyAs(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstAlterAssemblyAs(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstAlterAssemblyAs(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyAs(this);
        }
    }
    
    /// <summary>
    /// alter_assembly_with
    /// 	 : WITH
    /// </summary>
    public partial class AstAlterAssemblyWith : AstTerminal<string>
    {
        
        public AstAlterAssemblyWith(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstAlterAssemblyWith(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstAlterAssemblyWith(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyWith(this);
        }
    }
    
    /// <summary>
    /// network_file_start
    /// 	 : DOUBLE_BACK_SLASH
    /// </summary>
    public partial class AstNetworkFileStart : AstTerminal<string>
    {
        
        public AstNetworkFileStart(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstNetworkFileStart(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstNetworkFileStart(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitNetworkFileStart(this);
        }
    }
    
    /// <summary>
    /// file_directory_path_separator
    /// 	 : BACKSLASH
    /// </summary>
    public partial class AstFileDirectoryPathSeparator : AstTerminal<string>
    {
        
        public AstFileDirectoryPathSeparator(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstFileDirectoryPathSeparator(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstFileDirectoryPathSeparator(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitFileDirectoryPathSeparator(this);
        }
    }
    
    /// <summary>
    /// local_drive
    /// 	 : DISK_DRIVE
    /// </summary>
    public partial class AstLocalDrive : AstTerminal<string>
    {
        
        public AstLocalDrive(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstLocalDrive(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstLocalDrive(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitLocalDrive(this);
        }
    }
    
    /// <summary>
    /// multiple_local_file_start
    /// 	 : SINGLE_QUOTE
    /// </summary>
    public partial class AstMultipleLocalFileStart : AstTerminal<string>
    {
        
        public AstMultipleLocalFileStart(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstMultipleLocalFileStart(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstMultipleLocalFileStart(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitMultipleLocalFileStart(this);
        }
    }
    
    /// <summary>
    /// entity_to
    /// 	 : TO
    /// </summary>
    public partial class AstEntityTo : AstTerminal<string>
    {
        
        public AstEntityTo(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstEntityTo(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstEntityTo(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitEntityTo(this);
        }
    }
    
    /// <summary>
    /// colon_colon
    /// 	 : DOUBLE_COLON
    /// </summary>
    public partial class AstColonColon : AstTerminal<string>
    {
        
        public AstColonColon(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstColonColon(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstColonColon(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitColonColon(this);
        }
    }
    
    /// <summary>
    /// server_instance
    /// 	 : STRING
    /// </summary>
    public partial class AstServerInstance : AstTerminal<string>
    {
        
        public AstServerInstance(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstServerInstance(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstServerInstance(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitServerInstance(this);
        }
    }
    
    /// <summary>
    /// ip_v4_failover
    /// 	 : STRING
    /// </summary>
    public partial class AstIpV4Failover : AstTerminal<string>
    {
        
        public AstIpV4Failover(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIpV4Failover(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIpV4Failover(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIpV4Failover(this);
        }
    }
    
    /// <summary>
    /// ip_v6_failover
    /// 	 : STRING
    /// </summary>
    public partial class AstIpV6Failover : AstTerminal<string>
    {
        
        public AstIpV6Failover(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIpV6Failover(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIpV6Failover(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIpV6Failover(this);
        }
    }
    
    /// <summary>
    /// mirroring_partner
    /// 	 : PARTNER
    /// </summary>
    public partial class AstMirroringPartner : AstTerminal<string>
    {
        
        public AstMirroringPartner(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstMirroringPartner(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstMirroringPartner(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitMirroringPartner(this);
        }
    }
    
    /// <summary>
    /// mirroring_witness
    /// 	 : WITNESS
    /// </summary>
    public partial class AstMirroringWitness : AstTerminal<string>
    {
        
        public AstMirroringWitness(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstMirroringWitness(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstMirroringWitness(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitMirroringWitness(this);
        }
    }
    
    /// <summary>
    /// witness_partner_equal
    /// 	 : EQUAL
    /// </summary>
    public partial class AstWitnessPartnerEqual : AstTerminal<string>
    {
        
        public AstWitnessPartnerEqual(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstWitnessPartnerEqual(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstWitnessPartnerEqual(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitWitnessPartnerEqual(this);
        }
    }
    
    /// <summary>
    /// mirroring_host_port_seperator
    /// 	 : COLON
    /// </summary>
    public partial class AstMirroringHostPortSeperator : AstTerminal<string>
    {
        
        public AstMirroringHostPortSeperator(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstMirroringHostPortSeperator(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstMirroringHostPortSeperator(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitMirroringHostPortSeperator(this);
        }
    }
    
    /// <summary>
    /// parameter
    /// 	 : PLACEHOLDER
    /// </summary>
    public partial class AstParameter : AstTerminal<string>
    {
        
        public AstParameter(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstParameter(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstParameter(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitParameter(this);
        }
    }
    
    /// <summary>
    /// star_asterisk
    /// 	 : STAR
    /// </summary>
    public partial class AstStarAsterisk : AstTerminal<string>
    {
        
        public AstStarAsterisk(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstStarAsterisk(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstStarAsterisk(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitStarAsterisk(this);
        }
    }
    
    /// <summary>
    /// id_
    /// 	 : ID
    /// 	 | DOUBLE_QUOTE_ID
    /// 	 | DOUBLE_QUOTE_BLANK
    /// 	 | SQUARE_BRACKET_ID
    /// 	 | keyword
    /// </summary>
    public partial class AstId : AstTerminal<string>
    {
        
        public AstId(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstId(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstId(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitId(this);
        }
    }
    
    /// <summary>
    /// simple_id
    /// 	 : ID
    /// </summary>
    public partial class AstSimpleId : AstTerminal<string>
    {
        
        public AstSimpleId(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstSimpleId(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstSimpleId(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitSimpleId(this);
        }
    }
}
