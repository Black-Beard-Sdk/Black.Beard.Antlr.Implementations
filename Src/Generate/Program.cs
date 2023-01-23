// See https://aka.ms/new-console-template for more information

using Bb;
using Bb.Parsers;
using Generate;



//var i =  Crc32.CalculateCrc32("FR125987652GT");
//var u = i.ToString("X2");

/*
 
\Src\Generate\bin\Debug\net6.0
D:\src\Black.Beard.Antlr.Implementations\Src\Black.Beard.SqlServer\Parsers\Grammar\TSqlParser.g4
 
 */


var file = new FileInfo("D:\\src\\Black.Beard.Antlr.Implementations\\Src\\Black.Beard.SqlServer\\Parsers\\Grammar\\TSqlParser.g4");

var ctx = new Context()
{
    Path = "D:\\src\\Black.Beard.Antlr.Implementations\\Src\\Black.Beard.SqlServer\\Asts\\",
    Namespace = "Bb.Asts",
}
           ;

var p = new Process(file, ctx);





