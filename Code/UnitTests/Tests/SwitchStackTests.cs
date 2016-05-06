using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackIris;
using UnitTestFile.Support;
using BlackIris.Attributes;

namespace UnitTestFile.Tests
{
    [TestClass]
    public class SwitchStackTests
    {
        [TestMethod]
        public void SwitchStack_KeyValueSwitch_PopOrder()
        {
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };

            SimilarSwitchContract contract = new SimilarSwitchContract();
            SwitchStack<SimilarSwitchContract, KeyValueSwitchAttribute> switchStack = new SwitchStack<SimilarSwitchContract, KeyValueSwitchAttribute>(contract);

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

        //[TestMethod]
        //public void SwitchStack_KeyValueSwitch_OnlyKeyValueSwitchTypes()
        //{
        //    string[] args = new string[] { "-h", "ZACTN51", "-t200" };

        //    SomeNonAttrPropertiesContract contract = new SomeNonAttrPropertiesContract();
        //    SwitchStack<SomeNonAttrPropertiesContract, KeyValueSwitchAttribute> switchStack = new SwitchStack<SomeNonAttrPropertiesContract, KeyValueSwitchAttribute>(contract);


        //}
    }
}
