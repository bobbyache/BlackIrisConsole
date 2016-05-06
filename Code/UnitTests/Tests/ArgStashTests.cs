using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackIris;

namespace UnitTestFile.Tests
{
    [TestClass]
    public class ArgStashTests
    {
        /* *************************************************************************************************************************************************
         * Tests the ArgStash class (A class representing a stash of command-line arguments.
         * 
         *  - Once reset, the arguments are available to grab.
         *  - Find them by their switch keys, pull the switch and the value back (key value)
         *  - Once pulled, the applicable arguments (key and value) should no longer exist in the stash, so it cannot be found again.
         *  
         * ORDER IS IMPORTANT !!!
         *  - Must pull by longest switches first!
         *  
         ************************************************************************************************************************************************* */

        [TestMethod]
        public void ArgsStash_PopArg_Standalone_Next_Is_Switch()
        {
            /*
             * Order is important!
             * Although we've found the switch, it doesn't have a value, because the
             * next arg is also a switch. In this case can only return the switch but
             * this should never actually happen with switches that expect values.
             * */
            string[] args = new string[] { "-h", "-hdatabase", "-userpass" };
            ArgsStash argStash = new ArgsStash(args);
            Assert.AreEqual("-hdatabase", argStash.Pop("-hdatabase"));
            Assert.AreEqual("-userpass", argStash.Pop("-userpass"));
            Assert.AreEqual("-h", argStash.Pop("-h"));

            Assert.IsTrue(argStash.Empty);
        }

        [TestMethod]
        public void ArgsStash_PopArg_SingleArgIndexes()
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
        public void ArgsStash_PopArg_SeperateArgIndexes()
        {
            /*
             * The order is important! Must insure that the longer switches are fetched before the 
             * short switches in order for this to work. Perhaps in the future can find a clever method
             * but for now this will have to do.
             * 
             * If more than one match is found, then an error will be generated.
             * */
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetbl", "TableName", "-hdatabase", "CMDB", "-userpass", "Password", "-user", "Rob", "-tr", "2010/09/02", "-t", "200" };
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
    }
}
