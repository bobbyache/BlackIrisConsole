﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackIris.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandlineContractAttribute : Attribute
    {
        public string[] Verbs { get; private set; }
        public char SwitchPrefix { get; private set; }

        public CommandlineContractAttribute(string verbs, char switchPrefix = '-')
        {
            this.SwitchPrefix = switchPrefix;
            this.Verbs = verbs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public CommandlineContractAttribute()
        {
            this.Verbs = new string[0];
            this.SwitchPrefix = '-';
        }
    }
}
