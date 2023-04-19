namespace Bb.Asts
{

    public class AstRootList<T> : List<T>
    {

        public AstRootList()
             : base()
        {

        }

        public AstRootList(int capacity)
             : base(capacity)
        {
            
        }

        public Iterator<T> Get()
        {
            return new Iterator<T>(this);
        }

    }

}
