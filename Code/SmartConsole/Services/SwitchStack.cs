using BlackIris.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlackIris.Services
{
    internal class SwitchStack<TContract, TSwitchAttribute> 
        where TContract : class
        where TSwitchAttribute : class, ISwitchAttribute
    {
        private TContract contract = null;
        private Stack<string> switchStack = new Stack<string>();

        public bool Empty { get { return switchStack.Count == 0; } }

        public SwitchStack(TContract contract)
        {
            this.contract = contract;
            Reset();
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
            List<TSwitchAttribute> attributes = new List<TSwitchAttribute>();

            foreach (PropertyInfo property in properties)
            {
                TSwitchAttribute attr = GetProperyContract(property);
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

        private TSwitchAttribute GetProperyContract(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            TSwitchAttribute attr = (from obj in attrs
                                     select obj).OfType<TSwitchAttribute>().SingleOrDefault();
            return attr;
        }
    }
}
