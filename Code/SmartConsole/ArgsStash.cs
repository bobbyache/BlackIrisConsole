using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.ConsoleArguments
{
    public class ArgsStash
    {
        string[] args;
        List<string> argList = new List<string>();

        public bool Empty { get { return argList.Count == 0; } }

        public ArgsStash(string[] args)
        {
            this.args = args;
            Reset();
        }

        public void Reset()
        {
            argList.Clear();
            argList.AddRange(args);
        }

        public bool Exists(string switchKey)
        {
            string arg = argList.SingleOrDefault(a => a.StartsWith(switchKey));
            return arg == null ? false : true;
        }

        public string Pop(string switchKey)
        {
            if (Exists(switchKey))
            {
                string arg = argList.SingleOrDefault(a => a.StartsWith(switchKey));
                argList.Remove(arg);
                return arg;
            }
            return null;
        }
    }
}
