﻿using System;
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
        /* *************************************************************************************************************************************************
        * CURRENTLY YOU NEED TO GET THIS NEXT TEST TO PASS
        * -------------------------------------------------
        *  - Support for switches that start with the same text segment as another shorter segment.
        *  - Use SwitchStack to retrieve switches in the correct order.
        *  - Fetch from ArgStash in the correct order.
        *  
        * ORDER IS IMPORTANT !!!
        * ----------------------
        * ALSO, KeyValueSwitchAttribute - is now correctly named, because it represents a switch that has both a key and a value.
        * However, Argument class doesn't seem to be consistent, and we'll have to look at how that works when we change the Factory.
        * 
        ************************************************************************************************************************************************* */


        [TestMethod]
        public void CommandlineContract_Support_Switches_StartingWith_Same_Segment()
        {
            string[] args = new string[] { "-h", "ZACTN51", "-hdatabasetblTableName", "-hdatabaseCMDB", "-userpassPassword", "-userRob", "-tr2010/09/02", "-t200" };

            CmdlineAgent<SimilarSwitchContract> agent = new CmdlineAgent<SimilarSwitchContract>();
            SimilarSwitchContract contract = agent.Deserialize(args);

            Assert.AreEqual(contract.Host, "ZACTN51");
            Assert.AreEqual(contract.Database, "CMDB");
            Assert.AreEqual(contract.Username, "Rob");
            Assert.AreEqual(contract.Password, "Password");
            Assert.AreEqual(contract.TargetTable, "TableName");
            Assert.AreEqual(contract.Timeout, 200);
            Assert.AreEqual(contract.RunDate, DateTime.Parse("2010/09/02"));
        }
    }
}
