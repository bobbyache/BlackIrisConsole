﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.ConsoleArguments.Arguments
{
    internal class ArgumentFactory
    {
        private string[] expectedSwitches;
        public ArgumentFactory(string[] expectedSwitches)
        {
            this.expectedSwitches = expectedSwitches;
        }

        public virtual List<Argument> ConstructArguments(string[] args)
        {
            string argument = "";
            bool concatNext = false;
            List<Argument> arguments = new List<Argument>();

            for (int x = 0; x < args.Count(); x++)
            {
                if (IsSwitch(args[x]))
                {
                    argument = args[x];

                    if (x < args.Count() - 1)
                    {
                        if (IsSwitch(args[x + 1]))
                        {
                            arguments.Add(new Argument(argument, GetSwitch(argument)));
                            concatNext = false;
                        }
                        else
                        {
                            concatNext = true;
                        }
                    }
                    else
                    {
                        arguments.Add(new Argument(argument, GetSwitch(argument)));
                        concatNext = false;
                    }
                }
                else if (concatNext)
                {
                    argument = argument + args[x];
                    arguments.Add(new Argument(argument, GetSwitch(argument)));
                    concatNext = false;
                }
                else
                {
                    argument = args[x];
                    arguments.Add(new Argument(argument, GetSwitch(argument)));
                }
            }
            return arguments;
        }

        protected virtual string GetSwitch(string arg)
        {
            foreach (string sw in expectedSwitches)
            {
                if (sw.Length <= arg.Length)
                {
                    string compareText = arg.Substring(0, sw.Length);
                    if (compareText == sw)
                        return compareText;
                }
            }

            return "";
        }
        protected virtual bool IsSwitch(string arg)
        {
            foreach (string sw in expectedSwitches)
            {
                if (sw.Length <= arg.Length)
                {
                    string compareText = arg.Substring(0, sw.Length);
                    if (compareText == sw)
                        return true;
                }
            }

            return false;
        }

        //private bool hasSwitch(string arg, string[] switches)
        //{
        //    string trimArg = arg.Trim();

        //    foreach (string sw in switches)
        //    {
        //        string trimSwitch = sw.Trim();
        //        if (trimArg.Length >= trimSwitch.Length)
        //        {
        //            string compareText = arg.Substring(0, trimSwitch.Length - 1);
        //            if (trimSwitch == compareText)
        //                return true;
        //        }
        //    }
        //    return false;
        //}
    }
}