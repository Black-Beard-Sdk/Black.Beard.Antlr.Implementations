using Bb.Asts;
using Bb.Generators;
using System.CodeDom;
using System.Linq;

namespace Generate.ModelsScripts
{

    public class GenerateCode : IAstVisitor<CodeExpression>
    {

        public GenerateCode(CodeStatementCollection code, string strategy)
        {
            this._code = new CodeBlock(code);
            this.Strategy = strategy;
        }

        public string Strategy { get; }

        public virtual CodeExpression VisitRule(AstRule a)
        {
            Stop();
            throw new NotImplementedException();
        }


        public virtual CodeExpression VisitAlternative(AstAlternative a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElementList(AstElementList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLabeledAlt(AstLabeledAlt a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitAlternativeList(AstAlternativeList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitArgActionBlock(AstArgActionBlock a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitAtom(AstAtom a)
        {
            return a.Value.Accept(this);
        }

        public virtual CodeExpression VisitBlock(AstBlock a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression AstLexerRulesList(AstLexerRulesList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitActionBlock(AstActionBlock a)
        {
            Stop();
            throw new NotImplementedException();

        }

        public virtual CodeExpression VisitEbnfSuffix(AstEbnfSuffix a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElement(AstElement a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLabeledElement(AstLabeledElement a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerAlternative(AstLexerAlternative a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerBlock(AstLexerBlock a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerCommand(AstLexerCommand a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerElementList(AstLexerElementList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerElementList(AstLexerCommandList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerRule(AstLexerRule a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerRulesList(AstLexerRulesList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitModeSpec(AstModeSpec a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitModeSpecList(AstModeSpecList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitNot(AstNot a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitOption(AstOption a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitOptionList(AstOptionList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitParserRuleSpec(AstParserRuleSpec a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequel(AstPrequel a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequelConstruct(AstPrequelConstruct a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequelConstructList(AstPrequelConstructList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequelList(AstPrequelList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRange(AstRange a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleAction(AstRuleAction a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleAltList(AstRuleAltList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleModifier(AstRuleModifier a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleModifierList(AstRuleModifierList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleRef(AstRuleRef a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRules(AstRules a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRulesList(AstRulesList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitTerminal(AstTerminal a)
        {
            return "context".Var().Call(a.Value.Text);
        }

        public virtual CodeExpression VisitTerminalText(AstTerminalText a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElementOption(AstElementOption a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElementOptionList(AstElementOptionList a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitExceptionGroup(AstExceptionGroup a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitExceptionHandler(AstExceptionHandler a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitFinallyClause(AstFinallyClause a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitGrammarSpec(AstGrammarSpec a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitGrammerDecl(AstGrammarDecl a)
        {
            Stop();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitIdentifierList(AstIdentifierList a)
        {
            Stop();
            throw new NotImplementedException();
        }


        protected static string GetStrategy(AstRule ast)
        {
            var config = ast.Configuration.Config;
            var strategy = config.TemplateSetting?.TemplateName ?? config.CalculatedTemplateSetting?.Setting.TemplateName;
            return strategy;
        }



        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public void Stop() => _code.Stop();
        protected LevelBloc CurrentBlock => _code.CurrentBlock;
        protected LevelBloc Stack(CodeStatementCollection c = null) => _code.Stack(c);
        private CodeBlock _code;


    }


}
