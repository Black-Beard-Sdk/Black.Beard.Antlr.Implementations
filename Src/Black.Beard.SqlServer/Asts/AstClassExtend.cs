namespace Bb.SqlServer.Asts
{
    public static class AstClassExtend
    {


        public static bool Is<T>(this object a)
        {
            return a is T;
        }

    }


}
