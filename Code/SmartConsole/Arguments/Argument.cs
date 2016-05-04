using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.ConsoleArguments.Arguments
{

    internal class Argument
    {
        public readonly bool HasSwitch;
        public readonly string Switch;
        public readonly string Text;

        internal Argument(string argument, string containedSwitch)
        {
            //string arg = argument.Replace("\"", "");
            this.HasSwitch = hasSwitch(argument);

            if (this.HasSwitch)
            {
                this.Switch = argument.Substring(0, containedSwitch.Length);
                this.Text = argument.Substring(containedSwitch.Length, argument.Length - (containedSwitch.Length));
            }
            else
            {
                this.Switch = "";
                this.Text = argument;
            }
        }

        private bool hasSwitch(string argument)
        {
            if (argument.Count() == 0)
                return false;

            if (argument[0] == '-' || argument[0] == '/')
                return true;
            else
                return false;
        }
    }
}
