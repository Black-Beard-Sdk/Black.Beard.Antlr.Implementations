using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{
    public class Variable
    {

        public Variable(string name, string type1, Type type2)
        {
            this.Name = name;
            this.Type1 = type1;
            this.Type2 = type2;
        }

        public string Name { get; }

        public string Type1 { get; }

        public Type Type2 { get; }

        public CodeTypeReference Type
        {
            get
            {
                if (Type1 != null)
                    return Type1.AsType();

                return Type2.AsType();

            }
        }


    }


}
