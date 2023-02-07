using Bb.Asts;
using Bb.Configurations;

namespace Bb.Parsers
{
    public class Context
    {

        public Context()
        {
            TerminalsToExcludes = new HashSet<string>();
            Identifiers = new HashSet<string>();
        }

        public string Path { get; set; }

        public string Namespace { get; set; }

        public ConfigurationList Configuration { get; set; }

        public ConfigurationRule CurrentConfiguration { get; internal set; }
    
        public ConfigurationType CurrentConfigurationType { get; internal set; }
        
        public ConfigurationMethod CurrentConfigurationMethod { get; internal set; }
        
        public HashSet<string> TerminalsToExcludes { get; set; }
        public HashSet<string> Identifiers { get; set; }

        public string Strategy { get; internal set; }

        public string AntlrParserRootName { get; set; }

        public HashSet<string> GetGeneratedFiles()
        {

            HashSet<string> names = new HashSet<string>();

            var dir = new DirectoryInfo(this.Path);
            if (!dir.Exists)
                dir.Create();

            foreach (var item in dir.GetFiles("*.generated.cs"))
                names.Add(item.FullName);

            return names;

        }

        public void RemoveFiles(HashSet<string> names)
        {
            foreach (var item in names)
                File.Delete(item);
        }

    }
}
