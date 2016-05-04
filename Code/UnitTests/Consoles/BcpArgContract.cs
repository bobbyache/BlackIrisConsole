using Iris.ConsoleArguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestFile.Consoles
{
    [CommandlineContract]
    public class BcpArgContract
    {
        [ArgumentContract("-H", ExpectedType = typeof(string))]
        [ArgumentContract("-host", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [ArgumentContract("-D", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [ArgumentContract("-T", ExpectedType = typeof(string))]
        [ArgumentContract("-t", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [ArgumentContract("-U", ExpectedType = typeof(string))]
        [ArgumentContract("-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [ArgumentContract("-P", ExpectedType = typeof(string))]
        [ArgumentContract("-p", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [ArgumentContract("-o", ExpectedType = typeof(Int32))]
        [ArgumentContract("-O", ExpectedType = typeof(Int32))]
        public int Timeout { get; set; }

        [ArgumentContract("-date", ExpectedType = typeof(DateTime))]
        public DateTime RunDate { get; set; }
    }
}
