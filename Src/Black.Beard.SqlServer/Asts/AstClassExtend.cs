namespace Bb.Asts
{
    public static class AstClassExtend
    {


        public static bool Is<T>(this object a)
        {
            return a is T;
        }
              
    }


}
