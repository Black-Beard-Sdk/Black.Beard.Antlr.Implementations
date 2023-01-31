// See https://aka.ms/new-console-template for more information

using Bb.Parsers;
using Generate;
using System.Reflection;


var DirRoot = new DirectoryInfo
(
    Path.Combine(
        new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName
        , @".\\..\\..\\..\\..\\Black.Beard.SqlServer"
    )
);
var file = new FileInfo(Path.Combine(DirRoot.FullName, @"Parsers\\Grammar\\TSqlParser.g4"));



var configFile = Path.Combine(DirRoot.FullName, @"Parsers\\Grammar", Path.GetFileNameWithoutExtension(file.Name) + ".antlr.json");
var configFileIdentifiers = Path.Combine(DirRoot.FullName, @"Parsers\\Grammar", Path.GetFileNameWithoutExtension(file.Name) + ".Identifiers.txt");
var configFileEnum = Path.Combine(DirRoot.FullName, @"Parsers\\Grammar", Path.GetFileNameWithoutExtension(file.Name) + ".terminalsToExcludesFromEnums.txt");
var configDirOutput = Path.Combine(DirRoot.FullName, @"Asts\\");

var ctx = new Context()
{
    Path = configDirOutput,
    Namespace = "Bb.Asts",
    Configuration = Bb.Configurations.ConfigurationList.LoadRules(configFile),
    TerminalsToExcludes = Bb.Configurations.ConfigurationList.LoadTerminalsToExcludesFromEnums(configFileEnum),
    Identifiers = Bb.Configurations.ConfigurationList.LoadIdentifiers(configFileIdentifiers),
};

var p = new Process();
p.Run(file, ctx);

ctx.Configuration.Save();



