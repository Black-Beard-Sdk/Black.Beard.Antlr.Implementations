using Bb.Parsers;
using System.Xml.Linq;

namespace Bb.Generators
{

    public class ScriptList : List<ScriptBase>
    {


        public ScriptList(string prefix = null)
        {
            this.Usings = new HashSet<string>();
            this.Prefix = prefix ?? string.Empty;
            if (!this.Prefix.EndsWith('.'))
                this.Prefix = this.Prefix + ".";
        }


        public ScriptList Add<TScriptBase>(string name = null, Action<TScriptBase> action = null)
            where TScriptBase : ScriptBase, new()
        {
            var _name = name ?? ScriptBase.DefaultFilename<TScriptBase>();
            var script = new TScriptBase()
            {
                Filename = Prefix + _name,
                Namespace = this.Namespace
            };
            foreach (var item in this.Usings)
                script.Usings.Add(item);

            if (action != null)
                action(script);

            this.Add(script);
            return this;
        }

        public ScriptList Generate(Context context)
        {

            foreach (var generator in this)
                context.AddStrategyKey(generator.StrategyTemplateKeys);

            var names = context.GetGeneratedFiles();

            HashSet<string> generateds = new HashSet<string>();

            foreach (var generator in this)
            {
                generator.Configuration = context.Configuration;
                generator.Generate(context, generateds);
            }

            foreach (var name in generateds)
                if (names.Contains(name))
                    names.Remove(name);

            context.RemoveFiles(names);

            return this;

        }

        public ScriptList Using(params string[] items)
        {
            foreach (var item in items)
                this.Usings.Add(item);
            return this;

        }

        public HashSet<string> Usings { get; }

        public string Prefix { get; }
        public string Namespace { get; set; }
    }

}
