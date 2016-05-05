using Iris.ConsoleArguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestFile.Consoles
{
    public class Contractless
    {
        [ArgumentContract("-H,-host", ExpectedType = typeof(string))]
        public string Host { get; set; }

        [ArgumentContract("-d", ExpectedType = typeof(string))]
        public string Database { get; set; }

        [ArgumentContract("-T,-t", ExpectedType = typeof(string))]
        public string TargetTable { get; set; }

        [ArgumentContract("-U,-u", ExpectedType = typeof(string))]
        public string Username { get; set; }

        [ArgumentContract("-P,-p", ExpectedType = typeof(string))]
        public string Password { get; set; }

        [ArgumentContract("-o,-O", ExpectedType = typeof(Int32))]
        public int Timeout { get; set; }

        [ArgumentContract("-date", ExpectedType = typeof(DateTime))]
        public DateTime RunDate { get; set; }
    }
}
