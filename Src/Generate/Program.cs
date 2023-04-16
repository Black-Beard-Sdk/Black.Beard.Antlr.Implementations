// See https://aka.ms/new-console-template for more information

using Bb.Parsers;
using Generate;
using System.Reflection;


var DirRoot = new DirectoryInfo
(
    Path.Combine(
        new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName
        , @"..\..\..\..\Black.Beard.SqlServer"
    )
);
var file = new FileInfo(Path.Combine(DirRoot.FullName, @"Parsers\Grammar\TSqlParser.g4"));
var configDirOutput = Path.Combine(DirRoot.FullName, @"Asts\Generated");


var ctx = new Context()
{
    OutputPath = configDirOutput,
    Namespace = "Bb.SqlServer.Asts",
};

var p = new Process();
p.Run(file, ctx);

Console.WriteLine("End.");

//ctx.Configuration.Save();



