using Microsoft.VisualStudio.TestTools.UnitTesting;
using T = Bb.SqlServer.TSql;
using A = Bb.SqlServer;
using Bb.SqlServer;

namespace TSql.UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        public UnitTest1()
        {
            _path = "C:\\Program Files\\Microsoft SQL Server\\MSSQL15.MSSQLSERVER\\MSSQL\\DATA";
        }

        [TestMethod]
        public void TestMethod1()
        {

            var base1 = T.Create.Database
            (
                _path, 
                "mybase", 
                false, 
                Collations.French.French_100_BIN2
            );

        }


        private readonly string _path;

    }
}