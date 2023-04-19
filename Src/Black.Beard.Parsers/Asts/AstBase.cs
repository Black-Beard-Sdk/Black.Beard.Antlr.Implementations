using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace Bb.Asts
{


    public class Iterator<T>
    {

        public Iterator(AstRootList<T> items)
        {
            _items = items;
            this._index = 0;
        }

        public void Reset()
        {
            _index = 0;
            Current = _items[_index];
        }

        public bool Next()
        {

            _index++;

            if (_index < _items.Count)
            {
                Current = _items[_index];
                return true;
            }

            Current = default(T);
            return false;

        }

        public int Index => _index;

        public int Count => _items.Count;

        public T Current { get; private set; }

        private readonly AstRootList<T> _items;
        private int _index;

    }


    public abstract class AstBase<TVisitor> : IStrategyResolver
    {

        public AstBase(ParserRuleContext ctx)
            : this(new Position(ctx))
        {

        }

        public AstBase(ITerminalNode n)
            : this(new Position(n.Symbol, n.Symbol))
        {

        }

        public AstBase(Position position)
        {
            this.Position = position;
        }


        public Position Position { get; }


        public AstBase<TVisitor> Parent { get; set; }

        public abstract void Accept(TVisitor visitor);

        public T? Ancestor<T>()
           where T : AstBase<TVisitor>
        {

            T ancestor = default(T);

            if (this.Parent == null)
                return ancestor;

            if (this.Parent is T model)
                ancestor = model;

            else
                ancestor = this.Parent?.Ancestor<T>();

            return ancestor;

        }


        protected virtual SerializationStrategy StrategySerialization() => null;


        public StrategySerializationItem GetFrom(object instance)
        {

            var str = StrategySerialization();
            if (str != null)
                return str.GetStrategy(instance.GetType().Name);

            return null;

        }


        public override string ToString()
        {

            var wrt = new Writer();
            wrt.Strategy = StrategySerialization();

            ToString(wrt);
            return wrt.ToString();

        }


        public virtual void ToString(Writer writer)
        {


        }

        public virtual string RuleName { get; }

        public virtual string RuleValue { get; }

        public virtual bool IsTerminal { get; }

        public virtual AstKindEnum Kind { get; }

    }

    public enum AstKindEnum
    {
        Rule,
        Constant,
        Identifier,
        Boolean,
        String,
        Decimal,
        Int,
        Real,
        Hexa,
        Binary
    }

}
