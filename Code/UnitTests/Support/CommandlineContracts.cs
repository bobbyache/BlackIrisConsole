using BlackIris.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFile.Support
{
    [CommandlineContract]
    public class BcpArgContract
    {
        [KeyValueSwitch("-H,-host", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [KeyValueSwitch("-d", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [KeyValueSwitch("-T,-t", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [KeyValueSwitch("-U,-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-P,-p", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [KeyValueSwitch("-o,-O", ExpectedType = typeof(Int32))]
        public Nullable<int> Timeout { get; set; }

        [KeyValueSwitch("-date", ExpectedType = typeof(DateTime))]
        public Nullable<DateTime> RunDate { get; set; }
    }

    public class Contractless
    {
        [KeyValueSwitch("-H,-host", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [KeyValueSwitch("-d", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [KeyValueSwitch("-T,-t", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [KeyValueSwitch("-U,-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-P,-p", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [KeyValueSwitch("-o,-O", ExpectedType = typeof(Int32))]
        public Nullable<int> Timeout { get; set; }

        [KeyValueSwitch("-date", ExpectedType = typeof(DateTime))]
        public Nullable<DateTime> RunDate { get; set; }
    }

    [CommandlineContract]
    public class SimilarSwitchContract
    {
        [KeyValueSwitch("-h", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [KeyValueSwitch("-hdatabase", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [KeyValueSwitch("-hdatabasetbl", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [FlagSwitch("-b,-hard")]
        public bool BruteForce { get; set; }

        [FlagSwitch("-ignore")]
        public bool IgnoreWarnings { get; set; }

        [KeyValueSwitch("-user", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-userpass", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [KeyValueSwitch("-t", ExpectedType = typeof(Int32))]
        public int Timeout { get; set; }

        [KeyValueSwitch("-tr", ExpectedType = typeof(DateTime))]
        public Nullable<DateTime> RunDate { get; set; }
    }

    [CommandlineContract]
    public class SomeLowerCaseContract
    {
        [KeyValueSwitch("-H,-host", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [KeyValueSwitch("-d", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [KeyValueSwitch("-t", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [KeyValueSwitch("-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-p", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [KeyValueSwitch("-o", ExpectedType = typeof(Int32))]
        public Nullable<int> Timeout { get; set; }

        [KeyValueSwitch("-date", ExpectedType = typeof(DateTime))]
        public Nullable<DateTime> RunDate { get; set; }
    }

    //[CommandlineContract]
    //public class SomeNonAttrPropertiesContract
    //{
    //    [KeyValueSwitch("-H,-host", ExpectedType = typeof(string))]
    //    public string Host { get; set; }

    //    public string Database { get; set; }

    //    [KeyValueSwitch("-t", ExpectedType = typeof(string))]
    //    public string TargetTable { get; set; }

    //    public string Username { get; set; }
    //}

    [CommandlineContract]
    public class FlagSwitchContract
    {
        [FlagSwitch("-h,-hard")]
        public bool BruteForce { get; set; }

        [FlagSwitch("-ignore")]
        public bool IgnoreWarnings { get; set; }

        [KeyValueSwitch("-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-p", ExpectedType = typeof(string))]
        public string Password { get; set; }
    }

    [CommandlineContract]
    public class FlagSwitchContract2
    {
        [FlagSwitch("-h,-hard")]
        public bool BruteForce { get; set; }

        [FlagSwitch("-ignore")]
        public bool IgnoreWarnings { get; set; }

        [FlagSwitch("-up")]
        public bool UpperCase { get; set; }

        [KeyValueSwitch("-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-p", ExpectedType = typeof(string))]
        public string Password { get; set; }
    }

    [CommandlineContract]
    public class CommandAndSwitchContract
    {
        //[CommandVerb("move,MOVE")]
        //public string ProcessCmd { get; set; }

        [FlagSwitch("-h,-hard")]
        public bool BruteForce { get; set; }

        [FlagSwitch("-ignore")]
        public bool IgnoreWarnings { get; set; }

        [FlagSwitch("-up")]
        public bool UpperCase { get; set; }

        [KeyValueSwitch("-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-p", ExpectedType = typeof(string))]
        public string Password { get; set; }
    }

    [CommandlineContract("gen,generate")]
    public class NxtGen_Generate
    {
        [FlagSwitch("-s")]
        public bool SaveToFile { get; set; }

        [KeyValueSwitch("-blu", ExpectedType = typeof(string))]
        public string BlueprintFile { get; set; }

        [KeyValueSwitch("-src", ExpectedType = typeof(string))]
        public string SourceFile { get; set; }
    }

    [CommandlineContract("create,create-prj")]
    public class NxtGen_CreateProject
    {
        [KeyValueSwitch("-dir", ExpectedType = typeof(string))]
        public string ProjectDirectory { get; set; }

        [FlagSwitch("-D")]
        public bool DeleteExisting { get; set; }
    }

    [CommandlineContract("gen")]
    public class NullableContract
    {
        [KeyValueSwitch("-o", ExpectedType = typeof(Int32))]
        public Nullable<int> Timeout { get; set; }

        [KeyValueSwitch("-date", ExpectedType = typeof(DateTime))]
        public Nullable<DateTime> RunDate { get; set; }

        [KeyValueSwitch("-amt", ExpectedType = typeof(Double))]
        public Nullable<Double> Amount { get; set; }

        [KeyValueSwitch("-ascii", ExpectedType = typeof(char))]
        public Nullable<char> AsciiChar { get; set; }
    }
}
