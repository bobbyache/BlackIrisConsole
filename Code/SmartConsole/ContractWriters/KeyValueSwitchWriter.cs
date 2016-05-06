using BlackIris.Attributes;
using BlackIris.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlackIris.ContractWriters
{
    internal class KeyValueSwitchWriter<TContract> where TContract : class, new()
    {
        public void Write(TContract contract, List<KeyValueSwitchParameter> arguments)
        {
            Type t = contract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();

            foreach (PropertyInfo propInfo in propInfos)
                WriteValue(contract, propInfo, arguments);
        }

        private void WriteValue(TContract contract, PropertyInfo property, List<KeyValueSwitchParameter> arguments)
        {
            KeyValueSwitchAttribute[] attrs = PropertyArgumentContracts(property);

            foreach (KeyValueSwitchAttribute attr in attrs)
            {
                foreach (KeyValueSwitchParameter argument in arguments)
                {
                    if (attr.Switches.Contains(argument.Switch))
                    {
                        switch (property.PropertyType.ToString())
                        {
                            case "System.String":
                                SetString(contract, property, argument.Text);
                                break;

                            case "System.Int32":
                                SetInteger(contract, property, argument.Text);
                                break;

                            case "System.DateTime":
                                    SetDateTime(contract, property, argument.Text);
                                    break;
                        }
                        return;
                    }
                }
            }
        }

        private void SetDateTime(TContract contract, PropertyInfo property, string text)
        {
            DateTime value;
            bool success = DateTime.TryParse(text, out value);
            if (success)
                property.SetValue(contract, value, null);
            else
                property.SetValue(contract, DateTime.MinValue, null);
        }

        private void SetString(TContract contract, PropertyInfo property, string text)
        {
            property.SetValue(contract, text, null);
        }

        private void SetInteger(TContract contract, PropertyInfo property, string text)
        {
            int value;
            bool success = int.TryParse(text, out value);
            if (success)
                property.SetValue(contract, value, null);
            else
                property.SetValue(contract, 0, null);
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
