using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackIris.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class KeyValueSwitchAttribute : System.Attribute, ISwitchAttribute
    {
        public string[] Switches { get; private set; }
        public Type ExpectedType { get; set; }

        public KeyValueSwitchAttribute(string switches)
        {
            this.Switches = switches.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            this.ExpectedType = typeof(string);
        }
    }
}
