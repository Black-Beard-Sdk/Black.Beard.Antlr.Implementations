using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class GrammarRuleTermConfig : AntlrConfigAstBase
    {

        public GrammarRuleTermConfig(ParserRuleContext ctx, TokenTypeEnum type, RuleTuneInherit? extendedDatas)
            : base(ctx)
        {
            this.Kind = type;
            this.ExtendedPattern = extendedDatas;
        }

        public GrammarRuleTermConfig(Position position, TokenTypeEnum type, RuleTuneInherit? extendedDatas)
            : base(position)
        {
            this.Kind = type;
            this.ExtendedPattern = extendedDatas;
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitRuleTerm(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitRuleTerm(this);
        }

        public TokenTypeEnum Kind { get; internal set; }

        public RuleTuneInherit? ExtendedPattern { get; }

        public override void ToString(Writer writer)
        {

            writer.Append("KIND ");

            switch (this.Kind)
            {
                
                case TokenTypeEnum.Other:
                    writer.AppendEndLine("#OTHER");
                    break;
                
                case TokenTypeEnum.Constant:
                    writer.AppendEndLine("#CONSTANT");
                    break;
                
                case TokenTypeEnum.Identifier:
                    writer.AppendEndLine("#IDENTIFIER");
                    break;
                
                case TokenTypeEnum.Comment:
                    writer.AppendEndLine("#COMMENT");
                    break;

                case TokenTypeEnum.Boolean:
                    writer.AppendEndLine("#BOOLEAN");
                    break;

                case TokenTypeEnum.String:
                    writer.AppendEndLine("#STRING");
                    break;

                case TokenTypeEnum.Decimal:
                    writer.AppendEndLine("#DECIMAL");
                    break;

                case TokenTypeEnum.Int:
                    writer.AppendEndLine("#INTEGER");
                    break;

                case TokenTypeEnum.Real:
                    writer.AppendEndLine("#REAL");
                    break;

                case TokenTypeEnum.Hexa:
                    writer.AppendEndLine("#HEXA");
                    break;

                case TokenTypeEnum.Binary:
                    writer.AppendEndLine("#BINARY");
                    break;

                case TokenTypeEnum.Pattern:
                    writer.Append("#PATTERN");
                    break;

                case TokenTypeEnum.Operator:
                    writer.AppendEndLine("#OPERATOR");
                    break;

                case TokenTypeEnum.Ponctuation:
                    writer.AppendEndLine("#PONCTUATION");
                    break;

                default:
                    break;

            }

            if (ExtendedPattern != null)
            {

                writer.Append(" ");

                ExtendedPattern.ToString(writer);
                writer.AppendEndLine();
            }

            writer.AppendEndLine();

        }


    }

}
