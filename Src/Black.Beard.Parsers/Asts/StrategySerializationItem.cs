namespace Bb.Asts
{
    public class StrategySerializationItem
    {

        public static StrategySerializationItem Default = new StrategySerializationItem("Default") { Indent = true };

        public StrategySerializationItem()
        {

        }

        public StrategySerializationItem(string name)
        {
            this.Name = name;
        }



        public string Name { get; set; }

        public bool Indent { get; set; }
        public bool ReturnLineAfterItems { get; set; }
    }


}
