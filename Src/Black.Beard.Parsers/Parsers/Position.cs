﻿

using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Parsers
{


    [DebuggerDisplay("{Start.StartIndex}, {Stop.StartIndex}")]
    public class Position
    {

        public Position(ParserRuleContext ctx) : this(ctx.Start, ctx.Stop)
        {



        }

        public Position(IToken start, IToken stop) : this(new TokenLocation(start), new TokenLocation(stop))
        {


        }

        public Position(TokenLocation start, TokenLocation stop)
        {
            Start = start;
            Stop = stop;

        }


        public TokenLocation Start { get; }

        public TokenLocation Stop { get; }

    }

}
