using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{

    public enum LabeledElementAssignEnum
    {
        Assign,
        PlusAssign,

    }


    [DebuggerDisplay("{Left} = {Right}")]
    public class AstLabeledElement : AstBase
    {

        public AstLabeledElement(ParserRuleContext ctx, AstIdentifier identifier, LabeledElementAssignEnum mode, AstBase value)
            : base(ctx)
        {
            this.Left = identifier;
            this.Assign = mode;
            this.Right = value;
        }

        public AstIdentifier Left { get; }
        public LabeledElementAssignEnum Assign { get; }
        public AstBase Right { get; }
        public OccurenceEnum Occurence { get; internal set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitLabeledElement(this);
        }

    }


}
