using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UnitTestFile.Support;
using BlackIris;
using BlackIris.Services;
using BlackIris.Common.Exceptions;

namespace UnitTestFile
{
    [TestClass]
    public class TestBench
    {
        // ---------------------------------------------------------------------------------------------------------------------------------------
        // These tests still need to be passed.
        // ---------------------------------------------------------------------------------------------------------------------------------------

        //[TestMethod]
        ////[ExpectedException(typeof(MultipleVerbsFoundException))]
        //public void InvalidCmdlineParameterDetected_1()
        //{
        //    // test ensures that an exception is raised when and invalid parameter is detected.
        //    // tests to ensure that an invalid parameter does not exist in the args

        //    //string[] args = new string[] { "gen", "generate", "-blu", "blueprint.blu", "-src", "data.dat" };

        //    //CmdlineContractResolver contractResolver = new CmdlineContractResolver();
        //    //contractResolver.Add(typeof(NxtGen_Generate));

        //    //Type contractType = contractResolver.GetContract(args);

        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void InvalidKeyValueSwitch()
        //{
        //    Assert.Fail("A key value switch was not identified.");
        //}

        //[TestMethod]
        //public void InvalidFlagSwitch()
        //{
        //    Assert.Fail("A flag switch was not identified.");
        //}

        //[TestMethod]
        //public void MandatorySwitchAbsent()
        //{
        //    Assert.Fail("A mandatory switch was not provided.");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(DuplicateSwitchException))]
        //public void DuplicateSwitchDetected_SwitchAlias()
        //{
        //    string[] args = new string[] { "-hostZACTN51", "-dCBMDB", "-tTableName", "-TDuplicateTableName", "-uRob", "-pPassword", "-O2000" };

        //    CmdlineContractAgent<BcpArgContract> agent = new CmdlineContractAgent<BcpArgContract>();
        //    BcpArgContract contract = agent.Deserialize(args);
        //}

        // ---------------------------------------------------------------------------------------------------------------------------------------


        [TestMethod]
        [ExpectedException(typeof(DuplicateSwitchException))]
        public void DuplicateSwitchDetected_Exact()
        {
            string[] args = new string[] { "-hostZACTN51", "-dCBMDB", "-tTableName", "-tDuplicateTableName", "-uRob", "-pPassword", "-O2000" };

            CmdlineContractAgent<BcpArgContract> agent = new CmdlineContractAgent<BcpArgContract>();
            BcpArgContract contract = agent.Deserialize(args);
        }
    }
}
