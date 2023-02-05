using Antlr4.Runtime;
using Bb.Asts;
using Bb.Configurations;
using Bb.Parsers;

namespace Bb.ParsersConfiguration.Ast
{
    public class AdditionalValues : AntlrConfigAstBase
    {

        public AdditionalValues(ParserRuleContext ctx)
            : base(ctx)
        {
            this._list = new Dictionary<string, AdditionalValue>();
        }

        public AdditionalValues(Position position)
           : base(position)
        {
            this._list = new Dictionary<string, AdditionalValue>();
        }

        public void Add(AdditionalValue item)
        {
            this._list.Add(item.Key, item);
        }

        public override void Accept(IAstConfigBaseVisitor visitor)
        {
            visitor.VisitAdditionalValues(this);
        }

        public override T Accept<T>(IAstConfigBaseWithResultVisitor<T> visitor)
        {
            return visitor.VisitAdditionalValues(this);
        }

        public override void ToString(Writer writer)
        {

            if (_list.Count > 0)
            {

                using (writer.Indent())
                    foreach (var item in _list)
                        item.Value.ToString(writer);

                writer.AppendEndLine();
                writer.AppendEndLine(";");

            }

        }

        internal void Merge(AdditionalValues t1)
        {
            foreach (var item in t1._list)
                this.Add(item.Value);
        }

        private readonly Dictionary<string, AdditionalValue> _list;

    }

}
