﻿using Antlr4.Runtime;
using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("{Value}")]
    public class AstEbnfSuffix : AstBase
    {

        public AstEbnfSuffix(ParserRuleContext ctx)
            : base(ctx)
        {
            
        }

        public OccurenceEnum Occurence { get; set; }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        public override void Accept(IAstBaseVisitor visitor)
        {
            visitor.VisitAstEbnfSuffix(this);
        }



        public override bool ContainsOnlyRules { get => false; }
        public override bool ContainsOnlyTerminals { get => false; }
        public override bool ContainsOnlyBlocks { get => false; }
        public override bool ContainsOnlyAlternatives { get => false; }



        public override bool ContainsOneRule { get => false; }
        public override bool ContainsOneTerminal { get => false; }
        public override bool ContainsOneBlock { get => false; }
        public override bool ContainsOneAlternative { get => false; }



        public override bool ContainsRules { get => false; }
        public override bool ContainsTerminals { get => false; }
        public override bool ContainsBlocks { get => false; }
        public override bool ContainsAlternatives { get => false; }


    }


}
