using Antlr4.Runtime;
using System.Diagnostics.CodeAnalysis;

namespace Bb.Asts
{
    public class AstRules : AstListBase<AstLexerRule>
    {

        public AstRules(ParserRuleContext ctx)
            : base(ctx)
        {

            _index = new Dictionary<string, AstRuleBase>();


            this.Rules = new AstRulesList(ctx);
            this.Terminals = new AstLexerRulesList(ctx);
        }

        public AstLexerRulesList Terminals { get; }


        public AstRulesList Rules { get; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitRules(this);
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitRules(this);
        }

        public AstRuleBase? ResolveByName(string name)
        {

            if (_index.Count == 0)
            {

                foreach (var rule in Rules)
                    _index.Add(rule.Name.Text, rule);

                foreach (var terminal in Terminals)
                    _index.Add(terminal.Name.Text, terminal);

            }

            if (name != null)
            {
                if (_index.TryGetValue(name, out var result))
                    return result;
            }

            return null;

        }

        public bool TryGetValue(string name, out AstRuleBase? result)
        {

            result = null;

            if (_index.Count == 0)
            {

                foreach (var rule in Rules)
                    _index.Add(rule.Name.Text, rule);

                foreach (var terminal in Terminals)
                    _index.Add(terminal.Name.Text, terminal);

            }

            if (name != null)
                return _index.TryGetValue(name, out result);

            return false;

        }

        private readonly Dictionary<string, AstRuleBase> _index;

    }


}
