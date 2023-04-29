namespace Bb.Asts
{

    public class StrategyBase
    {


        public StrategySerializationItem GetFrom(string instance)
        {
            return GetStrategy().GetStrategy(instance.GetType().Name);
        }

        public SerializationStrategies GetStrategy()
        {

            if (_strategy == null)
            {

                _strategy = new SerializationStrategies();

                //foreach (var item in CollectStrategy(this.GetType(), this))
                //    _strategy.AddStrategy(item);

            }

            return _strategy;

        }


        //private static IEnumerable<StrategySerializationItem> CollectStrategy(Type type, object instance)
        //{

        //    if (instance != null)
        //    {
        //        var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        //        foreach (var property in properties)
        //        {

        //            var value = property.GetValue(instance);
        //            if (value != null)
        //            {
        //                if (value is StrategySerializationItem v)
        //                {
        //                    v.AstName = property.Name;
        //                    yield return v;
        //                }

        //                else
        //                    foreach (var item in CollectStrategy(value.GetType(), value))
        //                        yield return item;
        //            }
        //        }
        //        var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        //        foreach (var field in fields)
        //        {

        //            var value = field.GetValue(instance);
        //            if (value != null)
        //            {
        //                if (value is StrategySerializationItem v)
        //                {
        //                    v.AstName = field.Name;
        //                    yield return v;
        //                }

        //                else
        //                    foreach (var item in CollectStrategy(value.GetType(), value))
        //                        yield return item;
        //            }
        //        }

        //    }


        //    var properties_ = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        //    foreach (var property in properties_)
        //    {

        //        var value = property.GetValue(null);
        //        if (value != null)
        //        {
        //            if (value is StrategySerializationItem v)
        //            {
        //                v.AstName = property.Name;
        //                yield return v;
        //            }

        //            else
        //                foreach (var item in CollectStrategy(value.GetType(), null))
        //                    yield return item;
        //        }
        //    }

        //    var fields_ = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        //    foreach (var field in fields_)
        //    {

        //        var value = field.GetValue(null);
        //        if (value != null)
        //        {
        //            if (value is StrategySerializationItem v)
        //            {
        //                v.AstName = field.Name;
        //                yield return v;
        //            }

        //            else
        //                foreach (var item in CollectStrategy(value.GetType(), value))
        //                    yield return item;
        //        }
        //    }

        //}


        private SerializationStrategies _strategy;


    }
}

