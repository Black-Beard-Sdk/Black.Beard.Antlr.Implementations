using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Asts.Codes;
using Bb.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Asts
{


    public abstract class AstBase
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
            this.Code = new Generator(this);
        }



        public Position Position { get; }

        public AstBase Parent { get; set; }

        public Generator Code { get; }

        public abstract void Accept(IAstBaseVisitor visitor);

        public T? Ancestor<T>()
           where T : AstBase
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

    }

    public enum GrammarType
    {
        None,
        Lexer,
        Parser
    }


}
