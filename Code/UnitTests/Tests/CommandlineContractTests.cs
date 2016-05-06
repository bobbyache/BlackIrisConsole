using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackIris;
using UnitTestFile.Support;

namespace UnitTestFile.Tests
{
    [TestClass]
    public class CommandlineContractTests
    {
        [TestMethod]
        public void CommandlineContract_PopulateAll_Properties()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000" };

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
        public void CommandlineContract_Contractless_Object_ThrowsException()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000" };

            CmdlineAgent<Contractless> agent = new CmdlineAgent<Contractless>();
            Contractless contract = agent.Deserialize(args);
        }

        [TestMethod]
        public void CommandlineContract_Supports_Only_LowerCase()
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

        [TestMethod]
        public void CommandlineContract_Gaps_Between_Switch_And_Value()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-d", "CBMDB", "-t", "TableName", "-u", "Rob", "-p", "Password", "-O", "2000" };

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
        public void CommandlineContract_Support_Switches_StartingWith_Same_Segment()
        {
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };

            CmdlineAgent<SimilarSwitchContract> agent = new CmdlineAgent<SimilarSwitchContract>();
            SimilarSwitchContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 200);
            Assert.AreEqual(contract.RunDate, DateTime.Parse("2010/09/02"));
        }

        [TestMethod]
        public void CommandlineContract_FlagSwitches_PopulateContract()
        {
            string[] args = new string[] { "-h", "-up" };

            CmdlineAgent<FlagSwitchContract2> agent = new CmdlineAgent<FlagSwitchContract2>();
            FlagSwitchContract2 contract = agent.Deserialize(args);

            Assert.IsTrue(contract.BruteForce);
            Assert.IsFalse(contract.IgnoreWarnings);
            Assert.IsTrue(contract.UpperCase);
        }
    }
}
