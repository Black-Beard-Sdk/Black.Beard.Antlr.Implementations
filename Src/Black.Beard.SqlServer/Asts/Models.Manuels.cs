namespace Bb.Asts.TSql
{

    public partial class AstTsqlFile
    {

        public AstTsqlFile Add(AstBatch ast)
        {
            this._mode = 0;
            this._list.Add(ast);
            return this;
        }

        protected bool Ensure(int mode)
        {
            if (_mode != mode)
                return true;
            else
                _mode = mode;
            return false;
        }

        public AstTsqlFile Add(AstExecuteBodyBatch ast, params AstGoStatement[] goStatements)
        {

            //if ( Ensure(1))
            //    throw new InvalidStrategyAstException("batch", "execute_body_batch", _rule);

            this._list.Add(ast);
            this._list.AddRange(goStatements);
            
            return this;
        
        }

        public AstTsqlFile Add(params AstGoStatement[] goStatements)
        {
            this._mode = 1;
            this._list.AddRange(goStatements);
            return this;
        }

        public AstTsqlFile Remove(AstRoot ast)
        {
            this._list.Remove(ast);
            return this;
        }

        public bool Exists(Predicate<AstRoot> predicate)
        {
            return this._list.Exists(predicate);
        }

        private int _mode = -1;

    }


}
