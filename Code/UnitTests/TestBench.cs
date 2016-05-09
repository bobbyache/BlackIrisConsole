using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UnitTestFile.Support;
using BlackIris;
using BlackIris.Services;

namespace UnitTestFile
{

    [TestClass]
    public class TestBench
    {
        [TestMethod]
        public void CreateCommandlineContracts_SuccessFetchingContractForVerb()
        {
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            SupportedContracts contracts = new SupportedContracts(args);
            contracts.Add(typeof(NxtGen_Generate));

            Type contractType = contracts.GetContract();
            Assert.IsTrue(contractType == typeof(NxtGen_Generate));

            // from here on in you should be able to:
            //  - populate the properties
            //  - run the required process
        }

        [TestMethod]
        public void CreateCommandlineContracts_VerbNotSupported()
        {
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            SupportedContracts contracts = new SupportedContracts(args);
            contracts.Add(typeof(NxtGen_CreateProject));

            Type contractType = null;
            contracts.GetContract(out contractType);
            Assert.IsTrue(contractType == null);

            // from here on in you should be able to:
            //  - populate the properties
            //  - run the required process
        }
    }
}
