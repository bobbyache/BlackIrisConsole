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
    [TestClass]
    public class TestBench
    {
        [TestMethod]
        public void BcpArgContract_Tests()
        {
            string[] argies = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000"  };
            //string[] argies = new string[] { "-host", "-d", "-t", "-u", "-p", "-O" };
            //string[] argies = new string[] { "" };
            //string[] argies = new string[] { "-host", "-d", "-t", "-u", "-p", "-O", "-date 2010/09/02" };
            //string[] argies = new string[] { "-date","2010-09-02" };
            //string[] argies = new string[] { "-date", "2010-09" };

            BcpArgContract contract = new BcpArgContract();
            CmdlineAgent.Deserialize(argies, contract);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 2000);
        }
    }
}
