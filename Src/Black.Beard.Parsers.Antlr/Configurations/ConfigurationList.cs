using Bb.Asts;
using Newtonsoft.Json;

namespace Bb.Configurations
{


    public class ConfigurationList
    {


        public ConfigurationList()
        {
            this._items = new Dictionary<string, ConfigurationRule>();
        }


        public void Save()
        {

            List<ConfigurationRule> items = new List<ConfigurationRule>(_items.Count);
            foreach (var item in _items)
                items.Add(item.Value);

            var payload = JsonConvert.SerializeObject(items, Formatting.Indented, new JsonSerializerSettings());

            Filename.Save(payload);

        }


        public static ConfigurationList LoadRules(string file)
        {

            var e = new ConfigurationList()
            {
                Filename = file
            };

            if (File.Exists(file))
            {
                var payload = file.LoadFromFile();
                if (!string.IsNullOrEmpty(payload))
                {
                    var list = JsonConvert.DeserializeObject<List<ConfigurationRule>>(payload, new JsonSerializerSettings());
                    foreach (var item in list)
                        if (!e._items.ContainsKey(item.Name))
                            e._items.Add(item.Name, item);
                }
            }

            return e;

        }

        public static HashSet<string> LoadTerminalsToExcludesFromEnums(string file)
        {

            var e = new HashSet<string>();

            if (File.Exists(file))
            {
                var payload = file.LoadFromFile();
                if (!string.IsNullOrEmpty(payload))
                {
                    var list = payload.Split(' ', '\r', '\n');
                    foreach (var item in list)
                    {
                        var p = item.Trim();
                        if (!string.IsNullOrEmpty(p) && !p.StartsWith("--"))
                            e.Add(p);
                    }
                }
            }

            return e;

        }

        public static HashSet<string> LoadIdentifiers(string file)
        {

            var e = new HashSet<string>();

            if (File.Exists(file))
            {
                var payload = file.LoadFromFile();
                if (!string.IsNullOrEmpty(payload))
                {
                    var list = payload.Split(' ', '\r', '\n');
                    foreach (var item in list)
                    {
                        var p = item.Trim();
                        if (!string.IsNullOrEmpty(p) && !p.StartsWith("--"))
                            e.Add(p);
                    }
                }
            }

            return e;

        }


        public ConfigurationRule GetConfiguration(object ast)
        {

            string key = GetKey(ast);

            if (!_items.TryGetValue(key, out var item))
                _items.Add(key, (item = new ConfigurationRule()
                {
                    Name = key,
                    Generate = true
                }));

            return item;

        }


        private string GetKey(object ast)
        {

            if (ast is string s)
                return s;

            if (ast is AstRule r)
                return r.RuleName.Text;

            if (ast is AstLabeledAlt r2)
                return r2.Identifier.Text;

            throw new NotImplementedException();

        }

        private readonly Dictionary<string, ConfigurationRule> _items;


        public string Filename { get; private set; }
    }


}
