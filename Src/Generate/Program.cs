// See https://aka.ms/new-console-template for more information

using Bb.Parsers;
using Generate;



var file = new FileInfo("C:\\Src\\Black.Beard.Antlr.Implementations\\Src\\Black.Beard.SqlServer\\Parsers\\Grammar\\TSqlParser.g4");

var configFile = Path.Combine(file.Directory.FullName, Path.GetFileNameWithoutExtension(file.Name) + ".antlr.json");
var configFileIdentifiers = Path.Combine(file.Directory.FullName, Path.GetFileNameWithoutExtension(file.Name) + ".Identifiers.txt");
var configFileEnum = Path.Combine(file.Directory.FullName, Path.GetFileNameWithoutExtension(file.Name) + ".terminalsToExcludesFromEnums.txt");

var ctx = new Context()
{
    Path = "C:\\Src\\Black.Beard.Antlr.Implementations\\Src\\Black.Beard.SqlServer\\Asts\\",
    Namespace = "Bb.Asts",
    Configuration = Bb.Configurations.ConfigurationList.LoadRules(configFile),
    TerminalsToExcludes = Bb.Configurations.ConfigurationList.LoadTerminalsToExcludesFromEnums(configFileEnum),
    Identifiers = Bb.Configurations.ConfigurationList.LoadIdentifiers(configFileIdentifiers),
};

var p = new Process();
p.Run(file, ctx);

ctx.Configuration.Save();



