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
        [KeyValueSwitch("-h", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [KeyValueSwitch("-hdatabase", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [KeyValueSwitch("-hdatabasetbl", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [KeyValueSwitch("-user", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [KeyValueSwitch("-userpass", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [KeyValueSwitch("-t", ExpectedType = typeof(Int32))]
        public int Timeout { get; set; }

        [KeyValueSwitch("-tr", ExpectedType = typeof(DateTime))]
        public DateTime RunDate { get; set; }
    }
}
