using Iris.ConsoleArguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestFile.Consoles
{
    [CommandlineContract]
    public class SimilarSwitchContract
    {
        [ArgumentContract("-h", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [ArgumentContract("-hdatabase", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [ArgumentContract("-hdatabasetbl", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [ArgumentContract("-user", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [ArgumentContract("-userpass", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [ArgumentContract("-t", ExpectedType = typeof(Int32))]
        public int Timeout { get; set; }

        [ArgumentContract("-tr", ExpectedType = typeof(DateTime))]
        public DateTime RunDate { get; set; }
    }
}
