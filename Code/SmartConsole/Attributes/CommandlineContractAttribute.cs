using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackIris.Attributes
{
    public enum ContractKeyValuePattern
    {
        Default,
        Squashed
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandlineContractAttribute : Attribute
    {
        public string[] Verbs { get; private set; }
        public char SwitchPrefix { get; private set; }
        public ContractKeyValuePattern KeyValuePattern { get; private set; }

        public CommandlineContractAttribute(string verbs, ContractKeyValuePattern keyValuePattern = ContractKeyValuePattern.Default, char switchPrefix = '-')
        {
            this.SwitchPrefix = switchPrefix;
            this.KeyValuePattern = keyValuePattern;
            this.Verbs = verbs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public CommandlineContractAttribute()
        {
            this.Verbs = new string[0];
            this.SwitchPrefix = '-';
            this.KeyValuePattern = ContractKeyValuePattern.Default;
        }
    }
}
