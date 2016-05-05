using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UnitTestFile.Consoles;
using Iris.ConsoleArguments;

namespace UnitTestFile
{
    //string[] argies = new string[] { "-host", "-d", "-t", "-u", "-p", "-O" };
    //string[] argies = new string[] { "" };
    //string[] argies = new string[] { "-host", "-d", "-t", "-u", "-p", "-O", "-date 2010/09/02" };
    //string[] argies = new string[] { "-date","2010-09-02" };
    //string[] argies = new string[] { "-date", "2010-09" };

    [TestClass]
    public class TestBench
    {
        [TestMethod]
        public void BcpArgContract_Tests()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000"  };

            CmdlineAgent<BcpArgContract> agent = new CmdlineAgent<BcpArgContract>();
            BcpArgContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 2000);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Contractless_Object_ThrowsException()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000" };

            CmdlineAgent<Contractless> agent = new CmdlineAgent<Contractless>();
            Contractless contract = agent.Deserialize(args);
        }

        [TestMethod]
        public void Contract_Supports_Only_LowerCase()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-URob", "-pPassword", "-O2000" };

            CmdlineAgent<SomeLowerCaseContract> agent = new CmdlineAgent<SomeLowerCaseContract>();
            SomeLowerCaseContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, null);           // upper case switch is not supported
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 0);               // upper case switch is not supported
        }
    }
}
