using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FlagSwitchAttribute : System.Attribute, ISwitchAttribute
    {
        public string[] Switches { get; private set; }

        public FlagSwitchAttribute(string switches)
        {
            this.Switches = switches.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
