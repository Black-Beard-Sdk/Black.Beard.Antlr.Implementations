using Bb.Asts;
using Bb.Asts.Codes;

namespace Bb.Parsers
{

    public class CodeGeneratorVisitor : IAstBaseVisitor
    {


        public CodeGeneratorVisitor(Context ctx)
        {
            this._ctx = ctx;            
            this._items = new HashSet<Generator>();
        }


        public void Visit(AstBase a)
        {

            this._items.Clear();

            a.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);


            Generate();

        }

        private void Generate()
        {

            CleanFolder();

            foreach (var item in _items)
            {

                var filename = item.Name + ".generated.cs";

                item.GenerateTo(filename);

            }

        }

        private void CleanFolder()
        {
            if (!Directory.Exists(this._ctx.Path))
                Directory.CreateDirectory(this._ctx.Path);

            foreach (var item in Directory.GetFiles("*.generated.cs"))
                File.Delete(item);
        }

        public void VisitGrammarSpec(AstGrammarSpec a)
        {
            a.Declaration?.Accept(this);
            a.Modes?.Accept(this);
            a.Prequels?.Accept(this);
            a.Rules?.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }


        public void VisitActionBlock(AstActionBlock a)
        {

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }


        public void VisitAlternative(AstAlternative a)
        {
            
            a.Rule?.Accept(this);
            a.Options?.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }


        public void VisitArgActionBlock(AstArgActionBlock a)
        {

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }


        public void VisitAtom(AstAtom a)
        {
            
            a.Value.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }


        public void VisitBlock(AstBlock a)
        {


            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitElement(AstElement a)
        {
            
            a.Child?.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitElementList(AstElementList a)
        {
            
            foreach (var item in a)
                item.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitElementOptionList(AstElementOptionList a)
        {
            
            foreach (var item in a)
                item.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitElmentOption(AstElementOption a)
        {
            
            a.Key?.Accept(this);
            a.Value?.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitExceptionGroup(AstExceptionGroup a)
        {
            
            a.FinallyClause?.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitExceptionHandler(AstExceptionHandler a)
        {
            
            a.ArgActionBlock.Accept(this);
            a.ActionBlock.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitFinallyClause(AstFinallyClause a)
        {
            
            a.Block.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitGrammerDecl(AstGrammarDecl a)
        {
            
            a.Name.Accept(this);
            a.Code.Name = a.Name.Text;

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitIdentifierList(AstIdentifierList a)
        {
            
            foreach (var item in a)
                item.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitLabeledAlt(AstLabeledAlt a)
        {
            
            a.Identifier?.Accept(this);
            a.Rule?.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitLabeledElement(AstLabeledElement a)
        {
            
            a.Left.Accept(this);
            a.Right.Accept(this);
            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitModeSpec(AstModeSpec a)
        {

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitModeSpecList(AstModeSpecList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitOption(AstOption a)
        {
            
            a.Key?.Accept(this);
            a.Value?.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitOptionList(AstOptionList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitParserRuleSpec(AstParserRuleSpec a)
        {

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitPrequel(AstPrequel a)
        {
            
            a.Child?.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitPrequelConstruct(AstPrequelConstruct a)
        {
            
            a.Child?.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitPrequelConstructList(AstPrequelConstructList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitPrequelList(AstPrequelList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
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

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitRuleAction(AstRuleAction a)
        {
            
            a.Action?.Accept(this);
            a.Name?.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitRuleAltList(AstRuleAltList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitRuleModifier(AstRuleModifier a)
        {

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitRuleModifierList(AstRuleModifierList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitRuleRef(AstRuleRef a)
        {
            
            a.Identifier?.Accept(this);
            a.Action?.Accept(this);
            a.Option?.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitRulesList(AstRulesList a)
        {
            
            foreach (var item in a)
                item.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitTerminal(AstTerminal a)
        {
            a.Options?.Accept(this);
            a.Value?.Accept(this);

            if (a.Code.Generate)
                this._items.Add(a.Code);
        }

        public void VisitTerminalText(AstTerminalText a)
        {
            a.Code.Name = a.Text;

        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        private void Stop()
        {
            System.Diagnostics.Debugger.Break();
        }

        private readonly Context _ctx;        
        private readonly HashSet<Generator> _items;

    }

    public class Context
    {

        public Context()
        {

        }

        public string Path { get; set; }


    }
}
