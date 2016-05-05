using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Iris.ConsoleArguments.Attributes;
using Iris.ConsoleArguments.Arguments;

namespace Iris.ConsoleArguments
{
    public class CmdlineAgent<T> where T : new()
    {
        public T Deserialize(string[] args)
        {

            T contract = new T();

            if (!IsCommandlineContract(contract))
                throw new NotSupportedException("The object contract is not decorated with the CommandlineContract attribute.");

            PopulateContract(contract, IsolateArguments(contract, args));

            return contract;
        }

        private List<Argument> IsolateArguments(object argContract, string[] args)
        {
            Type t = argContract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();
            List<ArgumentContractAttribute> argumentContractAttributes = new List<ArgumentContractAttribute>();

            foreach (PropertyInfo propInfo in propInfos)
                argumentContractAttributes.AddRange(PropertyArgumentContracts(propInfo));

            string[] contractSwitches = new string[0];
            foreach (var a in argumentContractAttributes)
            {
                contractSwitches = contractSwitches.Concat(a.Switches).ToArray();
            }

            ArgumentFactory info = new ArgumentFactory(contractSwitches);
            return info.ConstructArguments(args);
        }

        private void PopulateContract(object argContract, List<Argument> arguments)
        {
            Type t = argContract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();

            foreach (PropertyInfo propInfo in propInfos)
                WriteArgument(argContract, propInfo, arguments);
        }

        /// <summary>
        /// Checks that object being passed in is decorated with the command line attribute.
        /// </summary>
        private bool IsCommandlineContract(object argContract)
        {
            try
            {
                Type t = argContract.GetType();
                object[] attributes = t.GetCustomAttributes(false);

                var result = (from a in attributes
                              select a).OfType<CommandlineContractAttribute>().SingleOrDefault();

                return result == null ? false : true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private ArgumentContractAttribute[] PropertyArgumentContracts(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            ArgumentContractAttribute[] argumentAttributes = (from obj in attrs
                                                              select obj).OfType<ArgumentContractAttribute>().ToArray();
            return argumentAttributes;
        }

        private void WriteArgument(object argumentContract, PropertyInfo property, List<Argument> arguments)
        {
            ArgumentContractAttribute[] argumentAttributes = PropertyArgumentContracts(property);

            foreach (ArgumentContractAttribute argAttr in argumentAttributes)
            {
                foreach (Argument argument in arguments)
                {
                    if (argAttr.Switches.Contains(argument.Switch))
                    {
                        switch (property.PropertyType.ToString())
                        {
                            case "System.String":
                                property.SetValue(argumentContract, argument.Text, null);
                                break;
                            case "System.Int32":
                                {
                                    int value;
                                    bool success = int.TryParse(argument.Text, out value);
                                    if (success)
                                        property.SetValue(argumentContract, value, null);
                                    else
                                        property.SetValue(argumentContract, 0, null);
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    DateTime value;
                                    bool success = DateTime.TryParse(argument.Text, out value);
                                    if (success)
                                        property.SetValue(argumentContract, value, null);
                                    else
                                        property.SetValue(argumentContract, DateTime.MinValue, null);
                                    break;
                                    
                                }
                        }
                        return;
                    }
                }
            }
        }
    }
}
