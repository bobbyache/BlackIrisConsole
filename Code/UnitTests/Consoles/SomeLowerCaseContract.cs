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
        [ArgumentContract("-H,-host", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [ArgumentContract("-d", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [ArgumentContract("-t", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [ArgumentContract("-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [ArgumentContract("-p", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [ArgumentContract("-o", ExpectedType = typeof(Int32))]
        public int Timeout { get; set; }

        [ArgumentContract("-date", ExpectedType = typeof(DateTime))]
        public DateTime RunDate { get; set; }
    }
}
