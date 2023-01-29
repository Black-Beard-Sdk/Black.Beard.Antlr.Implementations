namespace Bb.Asts
{
    public class SerializationStrategy
    {

        public SerializationStrategy()
        {

        }

        public StrategySerializationItem GetStrategyFrom(object instance)
        {

            if (instance is IStrategyResolver r)
                return r.GetFrom(instance);

            return GetStrategy(instance.GetType().Name);

        }

        public StrategySerializationItem GetStrategy(string strategyName)
        {

            if (!_strategies.TryGetValue(strategyName, out var item))
                _strategies.Add(strategyName, item = new StrategySerializationItem(strategyName));

            return item;

        }

        public StrategySerializationItem AddStrategy(string strategyName)
        {
            return AddStrategy(new StrategySerializationItem(strategyName));
        }

        public StrategySerializationItem AddStrategy(StrategySerializationItem strategy)
        {
            this._strategies.Add(strategy.Name, strategy);
            return strategy;
        }


        private Dictionary<string, StrategySerializationItem> _strategies = new Dictionary<string, StrategySerializationItem>();

    }


}
