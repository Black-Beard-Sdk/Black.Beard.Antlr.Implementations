namespace Bb.Asts
{

    public class StrategyConfiguration : StrategyBase
    {

        public StrategyConfiguration()
        {

        }

        public StrategySerializationItem AstRule { get; } = new StrategySerializationItem() { Indent = true, ReturnLineAfterItems = true };

        public StrategySerializationItem AstRuleAltList { get; } = new StrategySerializationItem() { Indent = false, ReturnLineAfterItems = true };

    }

}
