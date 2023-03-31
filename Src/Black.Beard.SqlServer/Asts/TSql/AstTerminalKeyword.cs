﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;

namespace Bb.Asts.TSql
{

    public partial class AstTerminalKeyword : AstTerminal<string>
    {

        public AstTerminalKeyword(ITerminalNode t, string type, string value) :
        base(t, value)
        {
            this.Type = type;
        }

        public AstTerminalKeyword(ITerminalNode t, string value) :
                base(t, value)
        {
        }

        public AstTerminalKeyword(ParserRuleContext ctx) :
                base(ctx, ctx.GetText())
        {
        }

        public AstTerminalKeyword(Position t, string type, string value) :
        base(t, value)
        {
            this.Type = type;
        }

        public AstTerminalKeyword(Position t, string value) :
                base(t, value)
        {
        }

        public override void Accept(IAstTSqlVisitor visitor)
        {
            visitor.VisitKeyword(this);
        }

        public override void ToString(Writer w)
        {
            w.Append(base.Value);
        }

        public string Type { get; internal set; }
    
    }


}
