using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackIris.Services
{
    internal class ArgsStash
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
                if (IsStandaloneSwitch(switchKey))
                {
                    // get the next index
                    int nextIndex = argList.IndexOf(switchKey) + 1;

                    if (nextIndex > argList.Count - 1 || IsLikeSwitch(argList[nextIndex]))
                    {
                        // if the next segment doesn't exist, or it looks
                        // like a switch just remove this arg.
                        argList.Remove(switchKey);
                        return switchKey;
                    }
                    else
                    {
                        // the next segment (arg) is likely to be a value
                        // for the switch, so fetch it and remove it together
                        // with this arg.
                        string arg = switchKey + argList[nextIndex];
                        argList.RemoveAt(nextIndex);
                        argList.Remove(switchKey);
                        return arg;
                    }
                }
                else
                {
                    // the switch is squashed to the value for the switch, so
                    // just remove this segment (arg).
                    string arg = argList.SingleOrDefault(a => a.StartsWith(switchKey));
                    argList.Remove(arg);
                    return arg;
                }
            }
            return null;
        }

        protected virtual bool IsLikeSwitch(string arg)
        {
            if (arg.Substring(0, 1) == "-")
                return true;
            return false;
        }

        private bool IsStandaloneSwitch(string switchKey)
        {
            return argList.Contains(switchKey);
        }
    }
}
