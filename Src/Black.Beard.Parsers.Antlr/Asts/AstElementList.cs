﻿using Antlr4.Runtime;
using System.Text;

namespace Bb.Asts
{


    public class AstElementList : AstListBase<AstBase>
    {

        public AstElementList(ParserRuleContext ctx, int capacity)
            : base(ctx, capacity)
        {
            _charSplit = "  ";
        }

        public AstElementList(ParserRuleContext ctx)
            : base(ctx)
        {

        }

        public bool OutputContainsAlwayOneTerminal
        {
            get
            {

                if (this.Count != 1)
                    return false;

                foreach (AstBase item in this)
                {

                    if (item is AstAtom a)
                    {

                        if (!a.Value.IsTerminal)
                            return false;

                    }
                    else
                    {

                    }

                }

                return true;

            }
        }


        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitElementList(this);
        }

        public override string ResolveName()
        {
            if (this.Count == 1)
                return this[0].ResolveName();

            return base.ResolveName();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitElementList(this);
        }


    }


}
