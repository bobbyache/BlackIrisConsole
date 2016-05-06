using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris
{
    internal class FlagSwitchParameter : ISwitchParameter
    {
        public string Switch { get; private set; }

        public FlagSwitchParameter(string keySwitch)
        {
            this.Switch = keySwitch;
        }
    }
}
