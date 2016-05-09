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
        [TestMethod]
        //[ExpectedException(typeof(MultipleVerbsFoundException))]
        public void InvalidCmdlineParameterDetected_1()
        {
            // test ensures that an exception is raised when and invalid parameter is detected.
            // tests to ensure that an invalid parameter does not exist in the args

            //string[] args = new string[] { "gen", "generate", "-blu", "blueprint.blu", "-src", "data.dat" };

            //CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            //contractResolver.Add(typeof(NxtGen_Generate));

            //Type contractType = contractResolver.GetContract(args);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidVerbPositionException))]
        public void VerbIsNotFirstArg()
        {
            // Test ensures that an exception is thrown when the detected verb is 
            // not at the first index of the args array.

            string[] args = new string[] { "-blu", "gen", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));

            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        [ExpectedException(typeof(MultipleVerbsFoundException))]
        public void OnlyOneVerbAllowed_1()
        {
            string[] args = new string[] { "gen", "generate", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));

            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        [ExpectedException(typeof(MultipleVerbsFoundException))]
        public void OnlyOneVerbAllowed_2()
        {
            string[] args = new string[] { "gen", "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));

            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        public void InvalidKeyValueSwitch()
        {
            Assert.Fail("A key value switch was not identified.");
        }

        [TestMethod]
        public void InvalidFlagSwitch()
        {
            Assert.Fail("A flag switch was not identified.");
        }

        [TestMethod]
        public void DuplicateSwitchDetected()
        {
            Assert.Fail("A duplicate switch was detected.");
        }

        [TestMethod]
        public void MandatorySwitchAbsent()
        {
            Assert.Fail("A mandatory switch was not provided.");
        }

        [TestMethod]
        public void CreateCommandlineContracts_SuccessFetchingContractForVerb()
        {
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));

            Type contractType = contractResolver.GetContract(args);
            Assert.IsTrue(contractType == typeof(NxtGen_Generate));

            // from here on in you should be able to:
            //  - populate the properties
            //  - run the required process
        }

        [TestMethod]
        public void CreateCommandlineContracts_VerbNotSupported()
        {
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_CreateProject));

            Type contractType = contractResolver.GetContract(args);

            Assert.IsTrue(contractType == null);

            // from here on in you should be able to:
            //  - populate the properties
            //  - run the required process
        }
    }
}
