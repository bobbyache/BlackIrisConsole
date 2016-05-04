using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.ConsoleArguments.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ArgumentContractAttribute : System.Attribute
    {
        public ArgumentContractAttribute(string Switch)
        {
            this.SwitchCharacter = Switch;
        }
        public string SwitchCharacter { get; set; }
        public Type ExpectedType { get; set; }
    }
}
