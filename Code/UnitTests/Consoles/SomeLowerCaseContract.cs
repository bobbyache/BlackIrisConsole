using Iris.ConsoleArguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestFile.Consoles
{
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
        public int Timeout { get; set; }

        [KeyValueSwitch("-date", ExpectedType = typeof(DateTime))]
        public DateTime RunDate { get; set; }
    }
}
