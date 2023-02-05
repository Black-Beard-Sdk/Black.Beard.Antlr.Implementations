using Antlr4.Runtime;
using Bb.Asts;
using System.Data;

namespace Bb.ParsersConfiguration.Ast
{


    public class SelectorExpressionItemIs : SelectorExpressionItem
    {

        public SelectorExpressionItemIs(ParserRuleContext ctx)
            : base(ctx)
        {
            _targets = new HashSet<FilterExpressionTargetEnum>();
        }

        public IEnumerable<FilterExpressionTargetEnum> Targets { get => _targets; }

        public bool IsNot { get; set; }

        public string ListName { get; set; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitSelectorExpressionItemIs(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitSelectorExpressionItemIs(this);
        }

        public override void ToString(Writer writer)
        {

            if (writer.EndBy("\t"))
                writer.Append("  ");

            writer.Append("RULE IS ");

            if (IsNot)
                writer.Append("NOT ");

            foreach (var item in _targets)
                switch (item)
                {

                    case FilterExpressionTargetEnum.Block:
                        writer.Append("BLOCK ");
                        break;

                    case FilterExpressionTargetEnum.Rule:
                        writer.Append("RULE ");
                        break;
                    case FilterExpressionTargetEnum.Term:
                        writer.Append("TERM ");
                        break;
                    case FilterExpressionTargetEnum.Alternative:
                        writer.Append("ALTERNATIVE ");
                        break;

                    default:
                        break;

                }


        }

        public void Add(FilterExpressionTargetEnum term)
        {
            _targets.Add(term);
        }

        private readonly HashSet<FilterExpressionTargetEnum> _targets;

    }


}

