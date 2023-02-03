using System.Diagnostics;

namespace Bb.Configurations
{

    [DebuggerDisplay("{Name}")]
    public class ConfigurationRule
    {

        public ConfigurationRule()
        {
            this.Types = new List<ConfigurationType>();
        }

        public string Name { get; set; }

        public bool Generate { get; set; }

        public string Strategy { get; set; }


        public List<ConfigurationType> Types { get; }

        public string CalculatedStrategy { get; set; }

        public ConfigurationType GetType(string name)
        {
            var result = this.Types.FirstOrDefault(c => c.Name == name);
            if (result == null)
            {
                result = new ConfigurationType()
                {
                    Name = name,
                    Generate = true,
                };
                this.Types.Add(result);
            }

            return result;

        }

    }


    public class ConfigurationType
    {

        public ConfigurationType()
        {
            this.Methods = new List<ConfigurationMethod>();
        }

        public string Name { get; set; }

        public bool Generate { get; set; }

        public List<ConfigurationMethod> Methods { get; }

        public ConfigurationMethod GetMethod(string methodName)
        {
            var result = Methods.FirstOrDefault(c => c.Name == methodName);
            if (result == null)
            {
                result = new ConfigurationMethod()
                {
                    Name = methodName,
                    Generate = true,
                };
                this.Methods.Add(result);
            }

            return result;
        }

    }

}
