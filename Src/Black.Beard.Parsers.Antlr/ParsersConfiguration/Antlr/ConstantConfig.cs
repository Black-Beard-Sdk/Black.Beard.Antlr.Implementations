using Antlr4.Runtime.Tree;

namespace Bb.ParsersConfiguration.Antlr
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

    }

}
