namespace Bb.Asts.TSql
{

    public partial interface IAstTSqlVisitor
    {

        void VisitIdentifier(AstTerminalIdentifier a);

        void VisitKeyword(AstTerminalKeyword a);
    
        void VisitTerminalString(AstTerminalString a);
    
    }


}
