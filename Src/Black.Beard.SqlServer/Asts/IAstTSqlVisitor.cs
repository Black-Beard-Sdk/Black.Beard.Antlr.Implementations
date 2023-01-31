namespace Bb.Asts
{

    public partial interface IAstTSqlVisitor
    {
        
        void VisitIdentifier(AstTerminalIdentifier a);

        void VisitKeyword(AstTerminalKeyword a);

    }


}
