using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris.Parameters
{
    internal class KeyValueSwitchParameter : ISwitchParameter
    {
        public readonly bool HasSwitch;
        public readonly string Text;

        public string Switch { get; private set; }

        internal KeyValueSwitchParameter(string argument, string containedSwitch)
        {
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
