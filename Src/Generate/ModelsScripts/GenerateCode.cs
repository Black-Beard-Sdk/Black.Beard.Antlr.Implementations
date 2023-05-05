using Bb.Asts;
using Bb.Generators;
using System.CodeDom;
using System.Linq;

namespace Generate.ModelsScripts
{


    public class GenerateCodeFromAst : IAstVisitor<CodeExpression>
    {

        public GenerateCodeFromAst(CodeStatementCollection code, string strategy)
        {
            this._code = new CodeBlock(code);
            this.Strategy = strategy;
        }

        public string Strategy { get; }

        public virtual CodeExpression VisitRule(AstRule a)
        {
            Pause();
            throw new NotImplementedException();
        }


        public virtual CodeExpression VisitAlternative(AstAlternative a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElementList(AstElementList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLabeledAlt(AstLabeledAlt a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitAlternativeList(AstAlternativeList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitArgActionBlock(AstArgActionBlock a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitAtom(AstAtom a)
        {
            return a.Value.Accept(this);
        }

        public virtual CodeExpression VisitBlock(AstBlock a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression AstLexerRulesList(AstLexerRulesList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitActionBlock(AstActionBlock a)
        {
            Pause();
            throw new NotImplementedException();

        }

        public virtual CodeExpression VisitEbnfSuffix(AstEbnfSuffix a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElement(AstElement a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLabeledElement(AstLabeledElement a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerAlternative(AstLexerAlternative a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerAlternativeList(AstLexerAlternativeList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerBlock(AstLexerBlock a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerCommand(AstLexerCommand a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerElementList(AstLexerElementList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerElementList(AstLexerCommandList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerLabeledElement(AstLexerLabeledElement a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerRule(AstLexerRule a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitLexerRulesList(AstLexerRulesList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitModeSpec(AstModeSpec a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitModeSpecList(AstModeSpecList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitNot(AstNot a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitOption(AstOption a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitOptionList(AstOptionList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitParserRuleSpec(AstParserRuleSpec a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequel(AstPrequel a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequelConstruct(AstPrequelConstruct a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequelConstructList(AstPrequelConstructList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitPrequelList(AstPrequelList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRange(AstRange a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleAction(AstRuleAction a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleAltList(AstRuleAltList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleModifier(AstRuleModifier a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleModifierList(AstRuleModifierList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRuleRef(AstRuleRef a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRules(AstRules a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitRulesList(AstRulesList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitTerminal(AstTerminal a)
        {
            return "context".Var().Call(a.Value.Text);
        }

        public virtual CodeExpression VisitTerminalText(AstTerminalText a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElementOption(AstElementOption a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitElementOptionList(AstElementOptionList a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitExceptionGroup(AstExceptionGroup a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitExceptionHandler(AstExceptionHandler a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitFinallyClause(AstFinallyClause a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitGrammarSpec(AstGrammarSpec a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitGrammerDecl(AstGrammarDecl a)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual CodeExpression VisitIdentifierList(AstIdentifierList a)
        {
            Pause();
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
        public void Pause() => _code.Stop();
        protected LevelBloc CurrentBlock => _code.CurrentBlock;
        protected LevelBloc Stack(object sourceItem, CodeStatementCollection c = null) => _code.Stack(sourceItem, c);
        protected Data GetDataFor(object key) => _code.GetDataFor(key);

        private CodeBlock _code;


    }


}
