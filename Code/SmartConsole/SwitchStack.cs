using Iris.ConsoleArguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Iris.ConsoleArguments
{
    internal class SwitchStack<T> where T : class
    {
        private T contract = null;
        private Stack<string> switchStack = new Stack<string>();

        public bool Empty { get { return switchStack.Count == 0; } }

        public SwitchStack(T contract)
        {
            this.contract = contract;
        }

        public string Pop()
        {
            if (switchStack.Count > 0)
                return switchStack.Pop();
            return null;
        }

        public void Reset()
        {
            Type t = contract.GetType();
            PropertyInfo[] properties = t.GetProperties();

            List<string> switchKeys = new List<string>();
            List<ArgumentContractAttribute> attributes = new List<ArgumentContractAttribute>();

            foreach (PropertyInfo property in properties)
            {
                ArgumentContractAttribute attr = GetProperyContract(property);
                if (attr != null)
                    switchKeys.AddRange(attr.Switches);
            }

            /*
             * Very important: The default sort is [A-z] which is excellent because this
             * is how it must be pushed to the stack. A is pushed first, so Z will be the
             * first item popped. This means that longer strings will be popped before
             * "like" shorter strings.
             * */
            switchKeys.Sort();

            switchStack.Clear();
            foreach (string switchKey in switchKeys)
                switchStack.Push(switchKey);

        }

        private ArgumentContractAttribute GetProperyContract(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            ArgumentContractAttribute attr = (from obj in attrs
                                              select obj).OfType<ArgumentContractAttribute>().SingleOrDefault();
            return attr;
        }
    }
}
