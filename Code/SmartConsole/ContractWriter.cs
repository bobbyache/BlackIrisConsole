using Iris.ConsoleArguments.Arguments;
using Iris.ConsoleArguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Iris.ConsoleArguments
{
    internal class ContractWriter<T> where T : class, new()
    {
        public void Write(T contract, List<Argument> arguments)
        {
            Type t = contract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();

            foreach (PropertyInfo propInfo in propInfos)
                WriteArgument(contract, propInfo, arguments);
        }

        private void WriteArgument(T contract, PropertyInfo property, List<Argument> arguments)
        {
            KeyValueSwitchAttribute[] attrs = PropertyArgumentContracts(property);

            foreach (KeyValueSwitchAttribute attr in attrs)
            {
                foreach (Argument argument in arguments)
                {
                    if (attr.Switches.Contains(argument.Switch))
                    {
                        switch (property.PropertyType.ToString())
                        {
                            case "System.String":
                                property.SetValue(contract, argument.Text, null);
                                break;
                            case "System.Int32":
                                {
                                    int value;
                                    bool success = int.TryParse(argument.Text, out value);
                                    if (success)
                                        property.SetValue(contract, value, null);
                                    else
                                        property.SetValue(contract, 0, null);
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    DateTime value;
                                    bool success = DateTime.TryParse(argument.Text, out value);
                                    if (success)
                                        property.SetValue(contract, value, null);
                                    else
                                        property.SetValue(contract, DateTime.MinValue, null);
                                    break;

                                }
                        }
                        return;
                    }
                }
            }
        }

        private KeyValueSwitchAttribute[] PropertyArgumentContracts(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            KeyValueSwitchAttribute[] keyValueAttrs = (from obj in attrs
                                                       select obj).OfType<KeyValueSwitchAttribute>().ToArray();
            return keyValueAttrs;
        }
    }
}
