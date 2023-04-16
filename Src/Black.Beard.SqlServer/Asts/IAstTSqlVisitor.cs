namespace Bb.SqlServer.Asts
{

    public partial interface IAstTSqlVisitor
    {

        void VisitIdentifier(AstTerminalIdentifier a);

        void VisitKeyword(AstTerminalKeyword a);
        void VisitTerminalDecimal(AstTerminalDecimal a);
        void VisitTerminalFloat(AstTerminalDouble a);
        void VisitTerminalString(AstTerminalString a);
    
    }


}
