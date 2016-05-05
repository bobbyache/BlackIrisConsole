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
        public void CommandlineContract_PopulateAll_Properties()
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
        public void SwitchStack_PopOrder()
        {
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };

            CmdlineAgent<SimilarSwitchContract> agent = new CmdlineAgent<SimilarSwitchContract>();
            SimilarSwitchContract contract = new SimilarSwitchContract();

            SwitchStack<SimilarSwitchContract> switchStack = new SwitchStack<SimilarSwitchContract>(contract);
            switchStack.Reset();

            Assert.IsFalse(switchStack.Empty);
            
            Assert.AreEqual("-userpass", switchStack.Pop());
            Assert.AreEqual("-user", switchStack.Pop());

            Assert.AreEqual("-tr", switchStack.Pop());
            Assert.AreEqual("-t", switchStack.Pop());

            Assert.AreEqual("-hdatabasetbl", switchStack.Pop());
            Assert.AreEqual("-hdatabase", switchStack.Pop());
            Assert.AreEqual("-h", switchStack.Pop());

            Assert.IsTrue(switchStack.Empty);

            switchStack.Reset();

            Assert.IsFalse(switchStack.Empty);
        }

        [TestMethod]
        public void ArgsStash_PopArg()
        {
            /*
             * The order is important! Must insure that the longer switches are fetched before the 
             * short switches in order for this to work. Perhaps in the future can find a clever method
             * but for now this will have to do.
             * 
             * If more than one match is found, then an error will be generated.
             * */
            string[] args = new string[] { "-hZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };
            ArgsStash argStash = new ArgsStash(args);

            Assert.IsTrue(argStash.Exists("-userpass"));
            Assert.AreEqual("-userpassPassword", argStash.Pop("-userpass"));
            Assert.IsFalse(argStash.Exists("-userpass"));

            argStash.Pop("-user");
            
            Assert.AreEqual("-hdatabasetblTableName", argStash.Pop("-hdatabasetbl"));
            argStash.Pop("-hdatabase");
            Assert.AreEqual("-hZACTN51", argStash.Pop("-h"));
            
            argStash.Pop("-tr");
            argStash.Pop("-t");

            Assert.IsTrue(argStash.Empty);

            Assert.IsNull(argStash.Pop("-tr"));

            argStash.Reset();

            Assert.IsTrue(argStash.Exists("-userpass"));

            Assert.IsFalse(argStash.Empty);

        }

        [TestMethod]
        public void CommandlineContract_Support_Switches_StartingWith_Same_Segment()
        {
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };

            CmdlineAgent<SimilarSwitchContract> agent = new CmdlineAgent<SimilarSwitchContract>();
            SimilarSwitchContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CBMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 2000);
            //Assert.AreEqual(contract.RunDate, 2000);
        }
    }
}
