using Antlr4.Runtime.Tree;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{
    public class ConstantConfig : AntlrConfigAstBase
    {

        public ConstantConfig(ITerminalNode terminal)
            : base(terminal)
        {
            this.Text = terminal.GetText();
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

        public override void ToString(Writer writer)
        {
            writer.Append(Text);
        }

    }

}
