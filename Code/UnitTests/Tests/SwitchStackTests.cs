using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Iris.ConsoleArguments;
using UnitTestFile.Consoles;

namespace UnitTestFile.Tests
{
    [TestClass]
    public class SwitchStackTests
    {
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
    }
}
