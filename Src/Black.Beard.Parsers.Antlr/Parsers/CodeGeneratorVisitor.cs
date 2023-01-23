using Bb.Asts;
using Bb.Generators;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Bb.Parsers
{

    public class CodeGeneratorVisitor : IAstBaseVisitor
    {



        public CodeGeneratorVisitor(Context ctx)
        {
            this._ctx = ctx;
            this._items = new AstGenerators();
        }

        public CodeGeneratorVisitor Add(string name, Action<AstGenerator> action)
        {

            var g = new AstGenerator()
            {
                Name = name
            };

            this._items.Add(g);

            action(g);

            return this;

        }

        public void Visit(AstBase a)
        {
            this._items.Clear();
            a.Accept(this);
            Generate();
        }

        private void Generate()
        {
            CleanFolder();
            _items.Generate(_ctx);
        }

        private void CleanFolder()
        {
            var dir = new DirectoryInfo(this._ctx.Path);

            if (!dir.Exists)
                dir.Create();

            foreach (var item in dir.GetFiles("*.generated.cs"))
                item.Delete();

        }

        public void VisitRule(AstRule a)
        {


            a.Return?.Accept(this);
            a.RuleName?.Accept(this);
            a.Modifiers?.Accept(this);
            a.RuleBlock?.Accept(this);
            a.Rule?.Accept(this);
            a.ThrowsSpec?.Accept(this);
            a.Local?.Accept(this);
            a.Prequels?.Accept(this);
            a.ExceptionGroup?.Accept(this);

            _items.Add(a);

        }


        public void VisitGrammarSpec(AstGrammarSpec a)
        {
            a.Declaration?.Accept(this);
            a.Modes?.Accept(this);
            a.Prequels?.Accept(this);
            a.Rules?.Accept(this);

        }

        public void VisitActionBlock(AstActionBlock a)
        {

        }

        public void VisitAlternative(AstAlternative a)
        {

            a.Rule?.Accept(this);
            a.Options?.Accept(this);
         
        }

        public void VisitArgActionBlock(AstArgActionBlock a)
        {
            
        }

        public void VisitAtom(AstAtom a)
        {
            a.Value.Accept(this);
        }

        public void VisitBlock(AstBlock a)
        {

        }

        public void VisitElement(AstElement a)
        {

            a.Child?.Accept(this);
        }
      
        public void VisitElementList(AstElementList a)
        {

            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitElementOptionList(AstElementOptionList a)
        {

            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitElementOption(AstElementOption a)
        {

            a.Key?.Accept(this);
            a.Value?.Accept(this);
        }

        public void VisitExceptionGroup(AstExceptionGroup a)
        {

            a.FinallyClause?.Accept(this);
        }

        public void VisitExceptionHandler(AstExceptionHandler a)
        {

            a.ArgActionBlock.Accept(this);
            a.ActionBlock.Accept(this);
        }

        public void VisitFinallyClause(AstFinallyClause a)
        {
            a.Block.Accept(this);
        }

        public void VisitGrammerDecl(AstGrammarDecl a)
        {
            a.Name.Accept(this);
        }

        public void VisitIdentifierList(AstIdentifierList a)
        {
            foreach (var item in a)
                item.Accept(this);
        }

        public void VisitLabeledAlt(AstLabeledAlt a)
        {

            a.Identifier?.Accept(this);
            a.Rule?.Accept(this);
            
                
        }

        public void VisitLabeledElement(AstLabeledElement a)
        {

            a.Left.Accept(this);
            a.Right.Accept(this);
            
                
        }

        public void VisitModeSpec(AstModeSpec a)
        {

                
        }

        public void VisitModeSpecList(AstModeSpecList a)
        {
            foreach (var item in a)
                item.Accept(this);
   
        }

        public void VisitOption(AstOption a)
        {

            a.Key?.Accept(this);
            a.Value?.Accept(this);

            
                
        }

        public void VisitOptionList(AstOptionList a)
        {

            //foreach (var item in a)
            //    item.Accept(this);

            //
            //    

        }

        public void VisitParserRuleSpec(AstParserRuleSpec a)
        {

            
                
        }

        public void VisitPrequel(AstPrequel a)
        {

            a.Child?.Accept(this);

            
                
        }

        public void VisitPrequelConstruct(AstPrequelConstruct a)
        {

            a.Child?.Accept(this);

            
                
        }

        public void VisitPrequelConstructList(AstPrequelConstructList a)
        {

            foreach (var item in a)
                item.Accept(this);

            
                
        }

        public void VisitPrequelList(AstPrequelList a)
        {

            foreach (var item in a)
                item.Accept(this);

            
                
        }

        public void VisitRuleAction(AstRuleAction a)
        {

            a.Action?.Accept(this);
            a.Name?.Accept(this);

            
                
        }

        public void VisitRuleAltList(AstRuleAltList a)
        {

            foreach (var item in a)
                item.Accept(this);

            
                
        }

        public void VisitRuleModifier(AstRuleModifier a)
        {

            
                
        }

        public void VisitRuleModifierList(AstRuleModifierList a)
        {

            foreach (var item in a)
                item.Accept(this);

            
                
        }

        public void VisitRuleRef(AstRuleRef a)
        {

            a.Identifier?.Accept(this);
            a.Action?.Accept(this);
            a.Option?.Accept(this);

            
                
        }

        public void VisitRulesList(AstRulesList a)
        {
            foreach (var item in a)
                item.Accept(this);  
        }

        public void VisitTerminal(AstTerminal a)
        {
            a.Options?.Accept(this);
            a.Value?.Accept(this);

            
                
        }

        public void VisitTerminalText(AstTerminalText a)
        {
            //a.Code.Name = "Ast" + a.Text;

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        public void VisitAstAlternativeList(AstAlternativeList a)
        {
            foreach (var item in a)
                item.Accept(this);
        }
                   
        private readonly Context _ctx;
        private readonly AstGenerators _items;

    }

}
