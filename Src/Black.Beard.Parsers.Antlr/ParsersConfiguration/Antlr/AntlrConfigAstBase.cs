using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.ParsersConfiguration.Antlr
{


    public abstract class AntlrConfigAstBase : AstBase<IAstConfigBaseVisitor>
    {

        public AntlrConfigAstBase(ParserRuleContext ctx)
            : this(new Position(ctx))
        {

        }

        public AntlrConfigAstBase(ITerminalNode n)
            : this(new Position(n.Symbol, n.Symbol))
        {

        }

        public AntlrConfigAstBase(Position position)
            : base(position)
        {
        }

    }

}
