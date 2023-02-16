using System.Diagnostics;

namespace Bb.Asts
{
    [DebuggerDisplay("{Value}")]
    public struct Occurence
    {

        public Occurence(bool optional = false)
        {
            this.Value = Enum.One;
            this.Optional = optional;
        }

        public Occurence(Enum value, bool optional)
        {
            this.Value = value;
            this.Optional = optional;
        }

        public bool Optional { get; set; }


        public Enum Value { get; }

        public enum Enum
        {
            One,
            Any,
        }

        public int Int { get => (int)Value; }

        public static implicit operator Enum(Occurence value)
        {
            return value.Value;
        }

        public static implicit operator Occurence(Enum value)
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
    }




}
