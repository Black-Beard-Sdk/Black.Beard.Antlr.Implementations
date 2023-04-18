//// See https://aka.ms/new-console-template for more information

using Bb.SqlServer;
using Bb.SqlServer.Asts;
using Bb.SqlServer.Parser;
using System.Text;

var sb = new StringBuilder("SELECT * FROM [a]");

var parser = SqlServerScriptParser.ParseString(sb);
var result = parser.GetModel();