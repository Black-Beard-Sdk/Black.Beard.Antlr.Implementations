//// See https://aka.ms/new-console-template for more information

using Bb.SqlServer;
using Bb.SqlServer.Asts;
using Bb.SqlServer.Parser;
using System.Text;

var result = "SELECT * FROM [a]".ParseSql();

Console.WriteLine(result.ToString());
