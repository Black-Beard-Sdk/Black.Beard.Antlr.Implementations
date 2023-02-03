using Antlr4.Runtime;
using Bb.Asts;
using Bb.Configurations;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Antlr
{
    public class GrammarSpecDefaultValues : AntlrConfigAstBase
    {

        public GrammarSpecDefaultValues(ParserRuleContext ctx)
            : base(ctx)
        {
            this._list = new Dictionary<string, GrammarSpecDefaultValue>();
        }

        public GrammarSpecDefaultValues(Position position)
           : base(position)
        {
            this._list = new Dictionary<string, GrammarSpecDefaultValue>();
        }

        public void Add(GrammarSpecDefaultValue item)
        {
            this._list.Add(item.Key, item);
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitDefaults(this);
        }


        public string Template
        {
            get
            {
                if (this._list.TryGetValue("TEMPLATE", out GrammarSpecDefaultValue value))
                    return value.Get();

                return "_";
            }
        }


        public override void ToString(Writer writer)
        {

            if (_list.Count > 0)
            {

                writer.Append("WITH DEFAULT ");
                using (writer.Indent())
                    foreach (var item in _list)
                    {
                        writer.Append(item.Key);
                        writer.Append(" : ");
                        writer.Append(item.Value.Get());
                    }

                writer.AppendEndLine();
                writer.AppendEndLine(";");

            }

        }

        private readonly Dictionary<string, GrammarSpecDefaultValue> _list;

    }

}
