using Antlr4.Runtime;
using Bb.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Black.Beard.Parsers.SqlServer.UnitTests
{

    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestMethod1()
        {

            var sb = new StringBuilder("SELECT * FROM [a]");
            var parser = SqlServerScriptParser.ParseString(sb);          
            
            var result = parser.Visit(new ScriptTSqlVisitor());

        }

    }
}