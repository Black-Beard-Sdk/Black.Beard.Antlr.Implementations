using Bb.Asts;

namespace Bb.SqlServer.Asts
{
    public static class AstClassExtend
    {


        public static bool Is<T>(this object a)
        {
            return a is T;
        }

        public static T? Get<T>(this AstRootList<AstRoot> items, int index) 
            //where T : AstRoot
        {

            if (index < items.Count)
                return (T)(object)items[index];

            return default(T);

        }



    }


}
