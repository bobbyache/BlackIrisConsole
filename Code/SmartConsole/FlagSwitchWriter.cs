using BlackIris.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris
{
    internal class FlagSwitchWriter<TContract> where TContract : class, new()
    {
        public void Write(TContract contract, List<FlagSwitchParameter> arguments)
        {
            Type t = contract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();

            foreach (PropertyInfo propInfo in propInfos)
                WriteValue(contract, propInfo, arguments);
        }

        private void WriteValue(TContract contract, PropertyInfo property, List<FlagSwitchParameter> arguments)
        {
            FlagSwitchAttribute[] attrs = PropertyArgumentContracts(property);

            foreach (FlagSwitchAttribute attr in attrs)
            {
                foreach (FlagSwitchParameter argument in arguments)
                {
                    if (attr.Switches.Contains(argument.Switch))
                    {
                        if (property.PropertyType.ToString() == "System.Boolean")
                            property.SetValue(contract, true, null);
                    }
                }
            }
        }

        private FlagSwitchAttribute[] PropertyArgumentContracts(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            FlagSwitchAttribute[] keyValueAttrs = (from obj in attrs
                                                   select obj).OfType<FlagSwitchAttribute>().ToArray();
            return keyValueAttrs;
        }
    }
}
