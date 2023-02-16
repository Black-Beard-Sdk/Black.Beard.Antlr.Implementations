namespace Bb.Parsers
{
    public class TreeRuleItemVisitor<T>
    {
        public virtual T Visit(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual T VisitAlternative(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual T VisitBlock(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual T VisitRange(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual T VisitTerminal(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual T VisitRuleRef(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        protected void Pause()
        {
            System.Diagnostics.Debugger.Break();
        }


    }

    public class TreeRuleItemVisitor
    {
        public virtual void Visit(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual void VisitAlternative(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual void VisitBlock(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual void VisitRange(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual void VisitTerminal(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        public virtual void VisitRuleRef(TreeRuleItem i)
        {
            Pause();
            throw new NotImplementedException();
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        protected void Pause()
        {
            System.Diagnostics.Debugger.Break();
        }

    }

}
