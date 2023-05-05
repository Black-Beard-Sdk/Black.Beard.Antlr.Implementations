using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers;
using System.Runtime.Serialization;

namespace Bb.ParsersConfiguration.Ast
{

    public class ConstantStringConfig : AntlrConfigAstBase
    {


        public ConstantStringConfig(ITerminalNode terminal)
            : base(terminal)
        {
            this.Text = Format(terminal.GetText());
        }


        public ConstantStringConfig(string text)
           : this(Position.Default, text)
        {
            this.Text = Format(text);
        }

        private string Format(string text)
        {

            if (!string.IsNullOrEmpty(text))
                return text.Trim('\'');

            return string.Empty;

        }

        public ConstantStringConfig(Position position, string text)
            : base(position)
        {

        }


        public string Text { get; }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitConstant(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitConstant(this);
        }

        public override bool ToString(Writer writer, StrategySerializationItem strategy)
        {
            writer.Append("'", Text, "'");
            return true;
        }

    }

}
