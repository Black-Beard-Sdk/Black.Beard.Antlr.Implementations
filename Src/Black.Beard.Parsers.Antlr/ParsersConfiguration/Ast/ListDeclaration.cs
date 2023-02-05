using Antlr4.Runtime;
using Bb.Asts;

namespace Bb.ParsersConfiguration.Ast
{
    public class ListDeclaration : AntlrConfigAstBase
    {

        public ListDeclaration(ParserRuleContext ctx)
            : base(ctx)
        {
            _targets = new HashSet<string>();
        }

        public IEnumerable<string> Targets { get => _targets; }

        public string ListName { get; set; }

        public bool Find(string name)
        {
            return _targets.Contains(name);
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitListDeclaration(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitListDeclaration(this);
        }

        public override void ToString(Writer writer)
        {

            writer.Append("LIST ");
            writer.Append(ListName);
            writer.Append(" ");

            int count = 0;
            using (writer.Indent())
            {

                writer.TrimEnd('\t');
                foreach (var target in Targets.OrderBy(c => c))
                {
                    count++;
                    writer.Append(target.ToString() + " ");
                    if (count == 10)
                    {
                        count = 0;
                        writer.AppendEndLine();
                    }
                }
                writer.TrimEnd();
                writer.AppendEndLine();
                writer.AppendEndLine(";");

            }

            writer.AppendEndLine();

        }

        public void Add(string term)
        {
            _targets.Add(term);
        }

        private readonly HashSet<string> _targets;

    }


}

