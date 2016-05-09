using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackIris.Common.Exceptions;
using BlackIris;
using UnitTestFile.Support;

namespace UnitTests.Tests
{
    [TestClass]
    public class ContractResolverTests
    {
        [TestMethod]
        [ExpectedException(typeof(VerbNotFoundException))]
        public void CmdlineContractResolver_VerbNotFound()
        {
            // in the event that no verb exists, an exception should be thrown if the verb is required.
            string[] args = new string[] { "-hostZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));
            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        [ExpectedException(typeof(VerbNotFoundException))]
        public void CmdlineContractResolver__VerbNotSupported()
        {
            // in the event that the verb is not correct or does not appear in the verbs for the 
            // contract... should throw an exception if a verb is required.
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_CreateProject));

            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidVerbPositionException))]
        public void CmdlineContractResolver__VerbIsNotFirstArg()
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
        public void CmdlineContractResolver__OnlyOneVerbAllowed_1()
        {
            string[] args = new string[] { "gen", "generate", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));

            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        [ExpectedException(typeof(MultipleVerbsFoundException))]
        public void CmdlineContractResolver__OnlyOneVerbAllowed_2()
        {
            string[] args = new string[] { "gen", "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            CmdlineContractResolver contractResolver = new CmdlineContractResolver();
            contractResolver.Add(typeof(NxtGen_Generate));

            Type contractType = contractResolver.GetContract(args);
        }

        [TestMethod]
        public void CmdlineContractResolver__SuccessFetchingContractForVerb()
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
    }
}
