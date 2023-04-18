#nullable disable
// Generated by ScriptClassTerminals.cs (Monday, April 17, 2023)
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
        
        private static string _ruleValue = "BREAK\r\n";
        
        private static string _ruleName = "break_statement";
        
        private static bool _isTerminal = true;
        
        public AstBreakStatement() : 
                base(Position.Default)
        {
        }
        
        public AstBreakStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "CONTINUE\r\n";
        
        private static string _ruleName = "continue_statement";
        
        private static bool _isTerminal = true;
        
        public AstContinueStatement() : 
                base(Position.Default)
        {
        }
        
        public AstContinueStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "SEMI\r\n";
        
        private static string _ruleName = "empty_statement";
        
        private static bool _isTerminal = true;
        
        public AstEmptyStatement() : 
                base(Position.Default)
        {
        }
        
        public AstEmptyStatement(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "FROM\r\n";
        
        private static string _ruleName = "alter_assembly_from_clause_start";
        
        private static bool _isTerminal = true;
        
        public AstAlterAssemblyFromClauseStart() : 
                base(Position.Default)
        {
        }
        
        public AstAlterAssemblyFromClauseStart(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "DROP\r\n";
        
        private static string _ruleName = "alter_assembly_drop";
        
        private static bool _isTerminal = true;
        
        public AstAlterAssemblyDrop() : 
                base(Position.Default)
        {
        }
        
        public AstAlterAssemblyDrop(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "DOUBLE_BACK_SLASH\r\n";
        
        private static string _ruleName = "network_file_start";
        
        private static bool _isTerminal = true;
        
        public AstNetworkFileStart() : 
                base(Position.Default)
        {
        }
        
        public AstNetworkFileStart(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "BACKSLASH\r\n";
        
        private static string _ruleName = "file_directory_path_separator";
        
        private static bool _isTerminal = true;
        
        public AstFileDirectoryPathSeparator() : 
                base(Position.Default)
        {
        }
        
        public AstFileDirectoryPathSeparator(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "DISK_DRIVE\r\n";
        
        private static string _ruleName = "local_drive";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "PARTNER\r\n";
        
        private static string _ruleName = "mirroring_partner";
        
        private static bool _isTerminal = true;
        
        public AstMirroringPartner() : 
                base(Position.Default)
        {
        }
        
        public AstMirroringPartner(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "WITNESS\r\n";
        
        private static string _ruleName = "mirroring_witness";
        
        private static bool _isTerminal = true;
        
        public AstMirroringWitness() : 
                base(Position.Default)
        {
        }
        
        public AstMirroringWitness(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "EQUAL\r\n";
        
        private static string _ruleName = "witness_partner_equal";
        
        private static bool _isTerminal = true;
        
        public AstWitnessPartnerEqual() : 
                base(Position.Default)
        {
        }
        
        public AstWitnessPartnerEqual(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "DOUBLE_QUOTE_ID\r\n";
        
        private static string _ruleName = "empty_value";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "IPV4_ADDR\r\n";
        
        private static string _ruleName = "ipv4";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "IPV6_ADDR\r\n";
        
        private static string _ruleName = "ipv6";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "FLOAT\r\n";
        
        private static string _ruleName = "float";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "DECIMAL\r\n";
        
        private static string _ruleName = "decimal";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "ID\r\n";
        
        private static string _ruleName = "id_simple";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "BINARY\r\n";
        
        private static string _ruleName = "binary_";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "LOCAL_ID\r\n";
        
        private static string _ruleName = "local_id";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "STRING\r\n";
        
        private static string _ruleName = "stringtext";
        
        private static bool _isTerminal = true;
        
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
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "PLACEHOLDER\r\n";
        
        private static string _ruleName = "parameter";
        
        private static bool _isTerminal = true;
        
        public AstParameter() : 
                base(Position.Default)
        {
        }
        
        public AstParameter(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
        
        private static string _ruleValue = "STAR\r\n";
        
        private static string _ruleName = "star_asterisk";
        
        private static bool _isTerminal = true;
        
        public AstStarAsterisk() : 
                base(Position.Default)
        {
        }
        
        public AstStarAsterisk(ParserRuleContext ctx) : 
                base(ctx)
        {
        }
        
        public override string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
        
        public override string RuleValue
        {
            get
            {
                return _ruleValue;
            }
        }
        
        public override bool IsTerminal
        {
            get
            {
                return _isTerminal;
            }
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
