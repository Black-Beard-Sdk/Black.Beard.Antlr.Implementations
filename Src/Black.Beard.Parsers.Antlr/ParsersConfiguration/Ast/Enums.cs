namespace Bb.ParsersConfiguration.Ast
{
    public enum SelectorExpressionOperationEnum
    {
        And,
        Or,
    }

    public enum FilterExpressionTargetEnum
    {
        _Undefined,
        Block,
        Rule,
        Term,
        Alternative
    }

    public enum FilterExpressionEnum
    {
        _Undefined,
        One,
        Only,
        Any,
        No,
        Many,
    }


}

