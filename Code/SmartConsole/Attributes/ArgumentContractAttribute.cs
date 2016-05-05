using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.ConsoleArguments.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ArgumentContractAttribute : System.Attribute
    {
        public string[] Switches { get; private set; }
        public Type ExpectedType { get; set; }

        public ArgumentContractAttribute(string switches)
        {
            this.Switches = switches.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            this.ExpectedType = typeof(string);
        }
    }
}
