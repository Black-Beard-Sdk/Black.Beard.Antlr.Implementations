#nullable disable
// Generated by ScriptClassTerminals.cs (Sunday, April 16, 2023)
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bb.SqlServer.Asts
{
    using System;
    using Bb.Asts;
    using Bb.Parsers;
    using Antlr4.Runtime;
    using Antlr4.Runtime.Tree;
    
    
    /// <summary>
    /// break_statement
    /// 	 : BREAK
    /// </summary>
    public partial class AstBreakStatement : AstBnfRule
    {
        
        public AstBreakStatement() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitBreakStatement(this);
        }
        
        /// <summary>
        /// break_statement : 
        ///    BREAK 
        /// </summary>
        public static AstBreakStatement Break()
        {
            return new AstBreakStatement();
        }
    }
    
    /// <summary>
    /// continue_statement
    /// 	 : CONTINUE
    /// </summary>
    public partial class AstContinueStatement : AstBnfRule
    {
        
        public AstContinueStatement() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitContinueStatement(this);
        }
        
        /// <summary>
        /// continue_statement : 
        ///    CONTINUE 
        /// </summary>
        public static AstContinueStatement Continue()
        {
            return new AstContinueStatement();
        }
    }
    
    /// <summary>
    /// empty_statement
    /// 	 : SEMI
    /// </summary>
    public partial class AstEmptyStatement : AstBnfRule
    {
        
        public AstEmptyStatement() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitEmptyStatement(this);
        }
        
        /// <summary>
        /// empty_statement : 
        ///    ; 
        /// </summary>
        public static AstEmptyStatement Semi()
        {
            return new AstEmptyStatement();
        }
    }
    
    /// <summary>
    /// alter_assembly_from_clause_start
    /// 	 : FROM
    /// </summary>
    public partial class AstAlterAssemblyFromClauseStart : AstBnfRule
    {
        
        public AstAlterAssemblyFromClauseStart() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyFromClauseStart(this);
        }
        
        /// <summary>
        /// alter_assembly_from_clause_start : 
        ///    FROM 
        /// </summary>
        public static AstAlterAssemblyFromClauseStart From()
        {
            return new AstAlterAssemblyFromClauseStart();
        }
    }
    
    /// <summary>
    /// alter_assembly_drop
    /// 	 : DROP
    /// </summary>
    public partial class AstAlterAssemblyDrop : AstBnfRule
    {
        
        public AstAlterAssemblyDrop() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitAlterAssemblyDrop(this);
        }
        
        /// <summary>
        /// alter_assembly_drop : 
        ///    DROP 
        /// </summary>
        public static AstAlterAssemblyDrop Drop()
        {
            return new AstAlterAssemblyDrop();
        }
    }
    
    /// <summary>
    /// network_file_start
    /// 	 : DOUBLE_BACK_SLASH
    /// </summary>
    public partial class AstNetworkFileStart : AstBnfRule
    {
        
        public AstNetworkFileStart() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitNetworkFileStart(this);
        }
        
        /// <summary>
        /// network_file_start : 
        ///    DOUBLE_BACK_SLASH 
        /// </summary>
        public static AstNetworkFileStart DoubleBackSlash()
        {
            return new AstNetworkFileStart();
        }
    }
    
    /// <summary>
    /// file_directory_path_separator
    /// 	 : BACKSLASH
    /// </summary>
    public partial class AstFileDirectoryPathSeparator : AstBnfRule
    {
        
        public AstFileDirectoryPathSeparator() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitFileDirectoryPathSeparator(this);
        }
        
        /// <summary>
        /// file_directory_path_separator : 
        ///    BACKSLASH 
        /// </summary>
        public static AstFileDirectoryPathSeparator Backslash()
        {
            return new AstFileDirectoryPathSeparator();
        }
    }
    
    /// <summary>
    /// local_drive
    /// 	 : DISK_DRIVE
    /// </summary>
    public partial class AstLocalDrive : AstTerminalString
    {
        
        public AstLocalDrive(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstLocalDrive(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstLocalDrive(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstLocalDrive(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstLocalDrive(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstLocalDrive(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitLocalDrive(this);
        }
        
        /// <summary>
        /// local_drive : 
        ///    DISK_DRIVE 
        /// </summary>
        public static AstLocalDrive DiskDrive(String var)
        {
            return new AstLocalDrive(var);
        }
    }
    
    /// <summary>
    /// mirroring_partner
    /// 	 : PARTNER
    /// </summary>
    public partial class AstMirroringPartner : AstBnfRule
    {
        
        public AstMirroringPartner() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitMirroringPartner(this);
        }
        
        /// <summary>
        /// mirroring_partner : 
        ///    PARTNER 
        /// </summary>
        public static AstMirroringPartner Partner()
        {
            return new AstMirroringPartner();
        }
    }
    
    /// <summary>
    /// mirroring_witness
    /// 	 : WITNESS
    /// </summary>
    public partial class AstMirroringWitness : AstBnfRule
    {
        
        public AstMirroringWitness() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitMirroringWitness(this);
        }
        
        /// <summary>
        /// mirroring_witness : 
        ///    WITNESS 
        /// </summary>
        public static AstMirroringWitness Witness()
        {
            return new AstMirroringWitness();
        }
    }
    
    /// <summary>
    /// witness_partner_equal
    /// 	 : EQUAL
    /// </summary>
    public partial class AstWitnessPartnerEqual : AstBnfRule
    {
        
        public AstWitnessPartnerEqual() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitWitnessPartnerEqual(this);
        }
        
        /// <summary>
        /// witness_partner_equal : 
        ///    EQUAL 
        /// </summary>
        public static AstWitnessPartnerEqual Equal()
        {
            return new AstWitnessPartnerEqual();
        }
    }
    
    /// <summary>
    /// empty_value
    /// 	 : DOUBLE_QUOTE_ID
    /// </summary>
    public partial class AstEmptyValue : AstTerminalString
    {
        
        public AstEmptyValue(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstEmptyValue(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstEmptyValue(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstEmptyValue(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstEmptyValue(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstEmptyValue(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitEmptyValue(this);
        }
        
        /// <summary>
        /// empty_value : 
        ///    DOUBLE_QUOTE_ID 
        /// </summary>
        public static AstEmptyValue DoubleQuoteId(String txt)
        {
            return new AstEmptyValue(txt);
        }
    }
    
    /// <summary>
    /// ipv4
    /// 	 : IPV4_ADDR
    /// </summary>
    public partial class AstIpv4 : AstTerminalString
    {
        
        public AstIpv4(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIpv4(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstIpv4(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIpv4(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstIpv4(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstIpv4(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIpv4(this);
        }
        
        /// <summary>
        /// ipv4 : 
        ///    IPV4_ADDR 
        /// </summary>
        public static AstIpv4 Ipv4Addr(String ip)
        {
            return new AstIpv4(ip);
        }
    }
    
    /// <summary>
    /// ipv6
    /// 	 : IPV6_ADDR
    /// </summary>
    public partial class AstIpv6 : AstTerminalString
    {
        
        public AstIpv6(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIpv6(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstIpv6(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIpv6(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstIpv6(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstIpv6(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIpv6(this);
        }
        
        /// <summary>
        /// ipv6 : 
        ///    IPV6_ADDR 
        /// </summary>
        public static AstIpv6 Ipv6Addr(String ip)
        {
            return new AstIpv6(ip);
        }
    }
    
    /// <summary>
    /// float
    /// 	 : FLOAT
    /// </summary>
    public partial class AstFloat : AstTerminalDouble
    {
        
        public AstFloat(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstFloat(ITerminalNode t, Double value) : 
                base(t, value)
        {
        }
        
        public AstFloat(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstFloat(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstFloat(ParserRuleContext ctx, Double value) : 
                base(ctx, value)
        {
        }
        
        public AstFloat(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstFloat(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstFloat(Position t, Double value) : 
                base(t, value)
        {
        }
        
        public AstFloat(string value) : 
                base(Position.Default, value)
        {
        }
        
        public AstFloat(Double value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitFloat(this);
        }
        
        /// <summary>
        /// float : 
        ///    FLOAT 
        /// </summary>
        public static AstFloat Float(Double real)
        {
            return new AstFloat(real);
        }
    }
    
    /// <summary>
    /// decimal
    /// 	 : DECIMAL
    /// </summary>
    public partial class AstDecimal : AstTerminalDecimal
    {
        
        public AstDecimal(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstDecimal(ITerminalNode t, Decimal value) : 
                base(t, value)
        {
        }
        
        public AstDecimal(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstDecimal(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstDecimal(ParserRuleContext ctx, Decimal value) : 
                base(ctx, value)
        {
        }
        
        public AstDecimal(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstDecimal(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstDecimal(Position t, Decimal value) : 
                base(t, value)
        {
        }
        
        public AstDecimal(string value) : 
                base(Position.Default, value)
        {
        }
        
        public AstDecimal(Decimal value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitDecimal(this);
        }
        
        /// <summary>
        /// decimal : 
        ///    DECIMAL 
        /// </summary>
        public static AstDecimal Decimal(Decimal _decimal)
        {
            return new AstDecimal(_decimal);
        }
    }
    
    /// <summary>
    /// id_simple
    /// 	 : ID
    /// </summary>
    public partial class AstIdSimple : AstTerminalString
    {
        
        public AstIdSimple(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstIdSimple(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstIdSimple(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstIdSimple(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstIdSimple(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstIdSimple(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitIdSimple(this);
        }
        
        /// <summary>
        /// id_simple : 
        ///    ID 
        /// </summary>
        public static AstIdSimple Id(String txt)
        {
            return new AstIdSimple(txt);
        }
    }
    
    /// <summary>
    /// binary_
    /// 	 : BINARY
    /// </summary>
    public partial class AstBinary : AstTerminalString
    {
        
        public AstBinary(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstBinary(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstBinary(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstBinary(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstBinary(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstBinary(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitBinary(this);
        }
        
        /// <summary>
        /// binary_ : 
        ///    BINARY 
        /// </summary>
        public static AstBinary Binary(String _binary)
        {
            return new AstBinary(_binary);
        }
    }
    
    /// <summary>
    /// local_id
    /// 	 : LOCAL_ID
    /// </summary>
    public partial class AstLocalId : AstTerminalString
    {
        
        public AstLocalId(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstLocalId(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstLocalId(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstLocalId(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstLocalId(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstLocalId(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitLocalId(this);
        }
        
        /// <summary>
        /// local_id : 
        ///    LOCAL_ID 
        /// </summary>
        public static AstLocalId LocalId(String txt)
        {
            return new AstLocalId(txt);
        }
    }
    
    /// <summary>
    /// stringtext
    /// 	 : STRING
    /// </summary>
    public partial class AstStringtext : AstTerminalString
    {
        
        public AstStringtext(ITerminalNode t) : 
                base(t, t.GetText())
        {
        }
        
        public AstStringtext(ITerminalNode t, String value) : 
                base(t, value)
        {
        }
        
        public AstStringtext(ParserRuleContext ctx) : 
                base(ctx, ctx.GetText())
        {
        }
        
        public AstStringtext(ParserRuleContext ctx, String value) : 
                base(ctx, value)
        {
        }
        
        public AstStringtext(Position t, string value) : 
                base(t, value)
        {
        }
        
        public AstStringtext(string value) : 
                base(Position.Default, value)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitStringtext(this);
        }
        
        /// <summary>
        /// stringtext : 
        ///    STRING 
        /// </summary>
        public static AstStringtext String(String txt)
        {
            return new AstStringtext(txt);
        }
    }
    
    /// <summary>
    /// parameter
    /// 	 : PLACEHOLDER
    /// </summary>
    public partial class AstParameter : AstBnfRule
    {
        
        public AstParameter() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitParameter(this);
        }
        
        /// <summary>
        /// parameter : 
        ///    ? 
        /// </summary>
        public static AstParameter Placeholder()
        {
            return new AstParameter();
        }
    }
    
    /// <summary>
    /// star_asterisk
    /// 	 : STAR
    /// </summary>
    public partial class AstStarAsterisk : AstBnfRule
    {
        
        public AstStarAsterisk() : 
                base(Position.Default)
        {
        }
        
        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitStarAsterisk(this);
        }
        
        /// <summary>
        /// star_asterisk : 
        ///    STAR 
        /// </summary>
        public static AstStarAsterisk Star()
        {
            return new AstStarAsterisk();
        }
    }
}