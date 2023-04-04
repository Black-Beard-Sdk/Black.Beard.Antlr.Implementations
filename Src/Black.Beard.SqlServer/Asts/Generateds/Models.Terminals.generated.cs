#nullable disable
// Generate by Models.Terminals
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
    /// null
    /// 	 : NULL_
    /// </summary>
    public partial class AstNull : AstTerminal<string>
    {
        
        public AstNull(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstNull(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstNull(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitNull(this);
        }
        
        /// <summary>
        /// NULL_ : 
        ///    NULL_ 
        /// </summary>
        public static AstNULL NULL()
        {
            return AstNULL.NULL();
        }
    }
    
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
        
        /// <summary>
        /// FROM : 
        ///    FROM 
        /// </summary>
        public static AstFROM FROM()
        {
            return AstFROM.FROM();
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
        
        /// <summary>
        /// DROP : 
        ///    DROP 
        /// </summary>
        public static AstDROP DROP()
        {
            return AstDROP.DROP();
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
        
        /// <summary>
        /// DOUBLE_BACK_SLASH : 
        ///    DOUBLE_BACK_SLASH 
        /// </summary>
        public static AstDOUBLEBACKSLASH DOUBLEBACKSLASH()
        {
            return AstDOUBLEBACKSLASH.DOUBLEBACKSLASH();
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
        
        /// <summary>
        /// BACKSLASH : 
        ///    BACKSLASH 
        /// </summary>
        public static AstBACKSLASH BACKSLASH()
        {
            return AstBACKSLASH.BACKSLASH();
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
        
        /// <summary>
        /// DISK_DRIVE : 
        ///    DISK_DRIVE 
        /// </summary>
        public static AstDISKDRIVE DISKDRIVE()
        {
            return AstDISKDRIVE.DISKDRIVE();
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
        
        /// <summary>
        /// PARTNER : 
        ///    PARTNER 
        /// </summary>
        public static AstPARTNER PARTNER()
        {
            return AstPARTNER.PARTNER();
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
        
        /// <summary>
        /// WITNESS : 
        ///    WITNESS 
        /// </summary>
        public static AstWITNESS WITNESS()
        {
            return AstWITNESS.WITNESS();
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
        
        /// <summary>
        /// EQUAL : 
        ///    EQUAL 
        /// </summary>
        public static AstEQUAL EQUAL()
        {
            return AstEQUAL.EQUAL();
        }
    }
    
    /// <summary>
    /// empty_value
    /// 	 : DOUBLE_QUOTE_ID
    /// </summary>
    public partial class AstEmptyValue : AstTerminal<string>
    {
        
        public AstEmptyValue(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstEmptyValue(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstEmptyValue(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitEmptyValue(this);
        }
        
        /// <summary>
        /// DOUBLE_QUOTE_ID : 
        ///    DOUBLE_QUOTE_ID 
        /// </summary>
        public static AstDOUBLEQUOTEID DOUBLEQUOTEID()
        {
            return AstDOUBLEQUOTEID.DOUBLEQUOTEID();
        }
    }
    
    /// <summary>
    /// ipv4
    /// 	 : IPV4_ADDR
    /// </summary>
    public partial class AstIpv4 : AstTerminal<string>
    {
        
        public AstIpv4(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIpv4(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIpv4(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIpv4(this);
        }
        
        /// <summary>
        /// IPV4_ADDR : 
        ///    IPV4_ADDR 
        /// </summary>
        public static AstIPV4ADDR IPV4ADDR()
        {
            return AstIPV4ADDR.IPV4ADDR();
        }
    }
    
    /// <summary>
    /// ipv6
    /// 	 : IPV6_ADDR
    /// </summary>
    public partial class AstIpv6 : AstTerminal<string>
    {
        
        public AstIpv6(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIpv6(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIpv6(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIpv6(this);
        }
        
        /// <summary>
        /// IPV6_ADDR : 
        ///    IPV6_ADDR 
        /// </summary>
        public static AstIPV6ADDR IPV6ADDR()
        {
            return AstIPV6ADDR.IPV6ADDR();
        }
    }
    
    /// <summary>
    /// float
    /// 	 : FLOAT
    /// </summary>
    public partial class AstFloat : AstTerminal<string>
    {
        
        public AstFloat(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstFloat(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstFloat(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitFloat(this);
        }
        
        /// <summary>
        /// FLOAT : 
        ///    FLOAT 
        /// </summary>
        public static AstFLOAT FLOAT()
        {
            return AstFLOAT.FLOAT();
        }
    }
    
    /// <summary>
    /// decimal
    /// 	 : DECIMAL
    /// </summary>
    public partial class AstDecimal : AstTerminal<string>
    {
        
        public AstDecimal(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstDecimal(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstDecimal(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitDecimal(this);
        }
        
        /// <summary>
        /// DECIMAL : 
        ///    DECIMAL 
        /// </summary>
        public static AstDECIMAL DECIMAL()
        {
            return AstDECIMAL.DECIMAL();
        }
    }
    
    /// <summary>
    /// binary_
    /// 	 : BINARY
    /// </summary>
    public partial class AstBinary : AstTerminal<string>
    {
        
        public AstBinary(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstBinary(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstBinary(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitBinary(this);
        }
        
        /// <summary>
        /// BINARY : 
        ///    BINARY 
        /// </summary>
        public static AstBINARY BINARY()
        {
            return AstBINARY.BINARY();
        }
    }
    
    /// <summary>
    /// local_id
    /// 	 : LOCAL_ID
    /// </summary>
    public partial class AstLocalId : AstTerminal<string>
    {
        
        public AstLocalId(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstLocalId(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstLocalId(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitLocalId(this);
        }
        
        /// <summary>
        /// LOCAL_ID : 
        ///    LOCAL_ID 
        /// </summary>
        public static AstLOCALID LOCALID()
        {
            return AstLOCALID.LOCALID();
        }
    }
    
    /// <summary>
    /// stringtext
    /// 	 : STRING
    /// </summary>
    public partial class AstStringtext : AstTerminal<string>
    {
        
        public AstStringtext(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstStringtext(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstStringtext(Position t, string value) : 
                base(t, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitStringtext(this);
        }
        
        /// <summary>
        /// STRING : 
        ///    STRING 
        /// </summary>
        public static AstSTRING STRING()
        {
            return AstSTRING.STRING();
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
        
        /// <summary>
        /// STAR : 
        ///    STAR 
        /// </summary>
        public static AstSTAR STAR()
        {
            return AstSTAR.STAR();
        }
    }
}
