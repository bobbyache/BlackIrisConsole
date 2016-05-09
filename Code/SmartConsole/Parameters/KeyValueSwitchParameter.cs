using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris.Parameters
{
    internal class KeyValueSwitchParameter : ISwitchParameter
    {
        public readonly string Text;
        public string Switch { get; private set; }
        //public char SwitchPrefix { get; private set; }

        internal KeyValueSwitchParameter(string argument, string containedSwitch)
        {
            this.Switch = argument.Substring(0, containedSwitch.Length);
            this.Text = argument.Substring(containedSwitch.Length, argument.Length - (containedSwitch.Length));
            //this.SwitchPrefix = char.Parse(containedSwitch.Substring(0, 1));
        }
    }
}
