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

    }



}
