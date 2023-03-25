// See https://aka.ms/new-console-template for more information
using Bb.Parsers;
using Bb.Parsers.TSql;
using Bb.Asts.TSql;
using System.Text;

Console.WriteLine("Hello, World!");


//AstTsqlFile.


var sb = new StringBuilder("SELECT * FROM [a]");
var parser = SqlServerScriptParser.ParseString(sb);

var result = parser.Visit(new ScriptTSqlVisitor());