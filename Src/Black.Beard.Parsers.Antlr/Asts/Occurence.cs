using System.Diagnostics;

namespace Bb.Asts
{

    public struct Occurence
    {

        public Occurence(bool optional = false)
        {
            this.Value = OccurenceEnum.One;
            this.Optional = optional;
        }

        public Occurence(OccurenceEnum value, bool optional)
        {
            this.Value = value;
            this.Optional = optional;
        }

        public bool Optional { get; set; }


        public OccurenceEnum Value { get; set; }

        public int Int { get => (int)Value; }

        public static implicit operator OccurenceEnum(Occurence value)
        {
            return value.Value;
        }

        public static implicit operator Occurence(OccurenceEnum value)
        {
            return new Occurence(value, false);
        }

        public static implicit operator int(Occurence value)
        {
            return value.Int;
        }

        public Occurence Clone()
        {
            return new Occurence(Value, this.Optional);
        }
        public Occurence CloneNoOptional()
        {
            return new Occurence(Value, false);
        }

        public override string ToString()
        {

            if (Optional)
            {
                if (Value == OccurenceEnum.One)
                    return Value.ToString() + "?";
                
                return "*";

            }

            if (Value == OccurenceEnum.One)
                return Value.ToString();

            return "*";

        }

    }


    public enum OccurenceEnum
    {
        One,
        Any,
    }



}
