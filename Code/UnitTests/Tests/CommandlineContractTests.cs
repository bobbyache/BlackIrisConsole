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
        public void CreateCommandlineContracts_CustomSwitchPrefix()
        {
            string[] args = new string[] { "gen", "/o", "200", "/date", "2015/03/02", "/amt", "3456.23", "/ascii", "D" };

            CmdlineContractAgent<DifferentSwitchContract> agent = new CmdlineContractAgent<DifferentSwitchContract>();
            DifferentSwitchContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Timeout, 200);
            Assert.AreEqual(contract.RunDate, DateTime.Parse("2015/03/02"));
            Assert.AreEqual(contract.Amount, 3456.23);
            Assert.AreEqual(contract.AsciiChar, 'D');
        }

        [TestMethod]
        public void CreateCommandlineContracts_TestNullableTypes()
        {
            string[] args = new string[] { "gen", "-o", "200", "-date", "2015/03/02", "-amt", "3456.23", "-ascii", "D" };

            CmdlineContractAgent<NullableContract> agent = new CmdlineContractAgent<NullableContract>();
            NullableContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Timeout, 200);
            Assert.AreEqual(contract.RunDate, DateTime.Parse("2015/03/02"));
            Assert.AreEqual(contract.Amount, 3456.23);
            Assert.AreEqual(contract.AsciiChar, 'D');
        }

        [TestMethod]
        public void CreateCommandlineContracts_TestNullableTypes_NotSet()
        {
            string[] args = new string[] { "gen", "-i" };

            CmdlineContractAgent<NullableContract> agent = new CmdlineContractAgent<NullableContract>();
            NullableContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Timeout, null);
            Assert.AreEqual(contract.RunDate, null);
            Assert.AreEqual(contract.Amount, null);
            Assert.AreEqual(contract.AsciiChar, null);
        }


        [TestMethod]
        public void CommandlineContract_KeyValueSwitch_MergedSwitchAndValue()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000" };

            CmdlineContractAgent<BcpArgContract> agent = new CmdlineContractAgent<BcpArgContract>();
            BcpArgContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 2000);
        }


        [TestMethod]
        public void CommandlineContract_KeyValueSwitch_AdjacentSwitchAndValue()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-d", "CBMDB", "-t", "TableName", "-u", "Rob", "-p", "Password", "-O", "2000" };

            CmdlineContractAgent<BcpArgContract> agent = new CmdlineContractAgent<BcpArgContract>();
            BcpArgContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 2000);
        }

        [TestMethod]
        public void CommandlineContract_KeyValueSwitch_IsCaseSensitive()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-URob", "-pPassword", "-O2000" };

            CmdlineContractAgent<SomeLowerCaseContract> agent = new CmdlineContractAgent<SomeLowerCaseContract>();
            SomeLowerCaseContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, null);               // upper case switch is not supported
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, null);                // upper case switch is not supported
            Assert.AreEqual(contract.RunDate, null);                // was not provided
        }

        [TestMethod]
        public void CommandlineContract_KeyValueSwitch_Supports_SwitchStartingWithSameChars()
        {
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };

            CmdlineContractAgent<SimilarSwitchContract> agent = new CmdlineContractAgent<SimilarSwitchContract>();
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
        public void CommandlineContract_FlagSwitch_PopulatesContract()
        {
            string[] args = new string[] { "-h", "-up" };

            CmdlineContractAgent<FlagSwitchContract2> agent = new CmdlineContractAgent<FlagSwitchContract2>();
            FlagSwitchContract2 contract = agent.Deserialize(args);

            Assert.IsTrue(contract.BruteForce);
            Assert.IsFalse(contract.IgnoreWarnings);
            Assert.IsTrue(contract.UpperCase);
        }

        //[TestMethod]
        //public void CommandlineContract_CommandAndSwitch_PopulatesAll()
        //{
        //    string[] args = new string[] { "move", "-hard", "-ignore", "-up", "-u", "Username", "-pPassword" };

        //    CmdlineAgent<CommandAndSwitchContract> agent = new CmdlineAgent<CommandAndSwitchContract>();
        //    CommandAndSwitchContract contract = agent.Deserialize(args);

        //    Assert.AreEqual("move", contract.ProcessCmd);
        //    Assert.IsTrue(contract.IgnoreWarnings);
        //    Assert.IsTrue(contract.BruteForce);
        //    Assert.IsTrue(contract.UpperCase);
        //    Assert.AreEqual("Username", contract.Username);
        //    Assert.AreEqual("Password", contract.Password);
        //}

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CommandlineContract_ThrowsException_NoContractFound()
        {
            string[] args = new string[] { "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000" };

            CmdlineContractAgent<Contractless> agent = new CmdlineContractAgent<Contractless>();
            Contractless contract = agent.Deserialize(args);
        }
    }
}
