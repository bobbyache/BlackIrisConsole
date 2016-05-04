using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.ConsoleArguments
{
    public abstract class ArgumentReader
    {
        protected Argument[] arguments;

        //public ArgumentReader(string[] args)
        //{
        //    this.arguments = ConstructArguments(args).ToArray();
        //}

        //public Argument[] ArgumentList()
        //{
        //    return this.arguments;
        //}

        //public abstract void ProcessArguments();
        //public abstract string GetUsageInfo();

        //protected virtual bool IsSwitch(string arg)
        //{
        //    if (((arg.Substring(0, 1) == "/") || (arg.Substring(0, 1) == "-")) && arg.Length == 2)
        //        return true;
        //    return false;
        //}

        //protected virtual List<Argument> ConstructArguments(string[] args)
        //{
        //    string argument = "";
        //    bool concatNext = false;
        //    List<Argument> arguments = new List<Argument>();

        //    for (int x = 0; x < args.Count(); x++)
        //    {
        //        if (IsSwitch(args[x]))
        //        {
        //            argument = args[x];

        //            if (x < args.Count() - 1)
        //            {
        //                if (IsSwitch(args[x + 1]))
        //                {
        //                    arguments.Add(new Argument(argument));
        //                    concatNext = false;
        //                }
        //                else
        //                {
        //                    concatNext = true;
        //                }
        //            }
        //            else
        //            {
        //                arguments.Add(new Argument(argument));
        //                concatNext = false;
        //            }
        //        }
        //        else if (concatNext)
        //        {
        //            argument = argument + args[x];
        //            arguments.Add(new Argument(argument));
        //            concatNext = false;
        //        }
        //        else
        //        {
        //            argument = args[x];
        //            arguments.Add(new Argument(argument));
        //        }
        //    }
        //    return arguments;
        //}

    }
}
