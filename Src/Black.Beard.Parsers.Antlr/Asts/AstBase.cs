using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Asts
{


    public abstract class AstBase : AstBase<IAstBaseVisitor>
    {

        public AstBase(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public AstBase(ITerminalNode n)
            : base(n)
        {

        }

        public AstBase(Position position)
            : base(position)
        {

        }


        public AstGrammarSpec? Root()
        {

            if (this.Parent != null)
                return this.Ancestor<AstGrammarSpec>();

            return null;

        }


        public bool IsList { get; protected set; }


        public virtual TokenTypeEnum TerminalKind { get; protected set; }


        public virtual bool IsTerminal { get => this is AstTerminal || this is AstTerminalText; }



        public virtual bool IsRule { get; }
        public virtual bool IsBlock { get => this is AstBlock; }
        public virtual bool IsAlternative { get => this is AstAlternativeList; }


        public AstBase Link { get; internal set; }


        public virtual bool ContainsRules { get; }
        public virtual bool ContainsOneRule { get; }
        public virtual bool ContainsOnlyRules { get; }
        public virtual IEnumerable<AstRuleRef> GetRules() { yield break; }




        public virtual bool ContainsTerminals => false;
        public virtual bool ContainsOneTerminal => false;
        public virtual bool ContainsOnlyTerminals => false;
        public virtual AstTerminalText GetTerminal() => null;
        public virtual IEnumerable<AstTerminalText> GetTerminals() { yield break; }




        public virtual bool ContainsBlocks => false;
        public virtual bool ContainsOneBlock => false;
        public virtual bool ContainsOnlyBlocks => false;
        public virtual IEnumerable<AstBlock> GetBlocks() { yield break; }




        public virtual bool ContainsAlternatives => false;
        public virtual bool ContainsOneAlternative => false;
        public virtual bool ContainsOnlyAlternatives => false;
        public virtual IEnumerable<AstAlternative> GetAlternatives() { yield break; }


        public virtual string Type => GetType().Name;

        public virtual string ResolveName()
        {
            throw new NotImplementedException();
        }

        public virtual Occurence ResolveOccurence()
        {
            return new Occurence();
        }


        public abstract T Accept<T>(IAstVisitor<T> visitor);

        protected override SerializationStrategy StrategySerialization()
        {
            return new StrategyConfiguration().GetStrategy();
        }

        protected void WriteOccurence(Writer wrt, Occurence occurence)
        {
            switch (occurence.Value)
            {

                case OccurenceEnum.Any:
                    if (occurence.Optional)
                        wrt.Append("*");
                    else
                        wrt.Append("+");
                    break;

                case OccurenceEnum.One:
                    if (occurence.Optional)
                        wrt.Append("?");
                    break;

                default:
                    break;

            }


        }

    }

    public enum GrammarType
    {
        None,
        Lexer,
        Parser
    }

}
