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
    //string[] argies = new string[] { "-host", "-d", "-t", "-u", "-p", "-O" };
    //string[] argies = new string[] { "" };
    //string[] argies = new string[] { "-host", "-d", "-t", "-u", "-p", "-O", "-date 2010/09/02" };
    //string[] argies = new string[] { "-date","2010-09-02" };
    //string[] argies = new string[] { "-date", "2010-09" };

    [TestClass]
    public class TestBench
    {
        [TestMethod]
        public void CreateCommandlineContracts_SuccessFetchingContractForVerb()
        {
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            SupportedContracts contracts = new SupportedContracts();
            contracts.Add(typeof(NxtGen_Generate));

            Type contractType = null;
            contracts.GetContract(args, out contractType);
            Assert.IsTrue(contractType == typeof(NxtGen_Generate));

            // from here on in you should be able to:
            //  - populate the properties
            //  - run the required process
        }

        [TestMethod]
        public void CreateCommandlineContracts_VerbNotSupported()
        {
            string[] args = new string[] { "gen", "-blu", "blueprint.blu", "-src", "data.dat" };

            SupportedContracts contracts = new SupportedContracts();
            contracts.Add(typeof(NxtGen_CreateProject));

            Type contractType = null;
            contracts.GetContract(args, out contractType);
            Assert.IsTrue(contractType == null);

            // from here on in you should be able to:
            //  - populate the properties
            //  - run the required process
        }

        // Not sure if this is needed yet, test is basically to check that more than one verb is not handed down
        // through the command line.

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void CommandlineContract_ThrowsException_NoContractFound()
        //{
        //    string[] args = new string[] { "gen", "-host", "ZACTN51", "-dCBMDB", "-tTableName", "-uRob", "-pPassword", "-O2000", "generate"};

        //    SupportedContracts contracts = new SupportedContracts();
        //    contracts.Add(typeof(NxtGen_CreateProject));

        //    Type contractType = null;
        //    contracts.GetContract(args, out contractType);
        //    Assert.IsTrue(contractType == null);
        //}
    }
}
