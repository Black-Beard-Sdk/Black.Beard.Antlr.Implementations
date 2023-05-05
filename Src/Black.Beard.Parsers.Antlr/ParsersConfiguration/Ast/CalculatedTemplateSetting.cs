﻿using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class CalculatedTemplateSetting : AntlrConfigAstBase
    {

        public CalculatedTemplateSetting(ParserRuleContext ctx, TemplateSetting setting)
            : base(ctx)
        {
            this.Setting = setting;
        }

        public CalculatedTemplateSetting(Position position, TemplateSetting setting)
            : base(position)
        {
            this.Setting = setting;
        }

        public TemplateSetting Setting { get; }


        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitCalculatedTemplateSetting(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitCalculatedTemplateSetting(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            writer.Append("CALCULATED ");
            writer.ToString(Setting);
            return true;
        }

        private readonly string _value;

    }


}

