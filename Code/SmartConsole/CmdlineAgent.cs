using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Iris.ConsoleArguments.Attributes;
using Iris.ConsoleArguments.Arguments;

namespace Iris.ConsoleArguments
{
    public class CmdlineAgent<T> where T : class, new()
    {
        public T Deserialize(string[] args)
        {

            T contract = new T();

            if (!IsCommandlineContract(contract))
                throw new NotSupportedException("The object contract is not decorated with the CommandlineContract attribute.");

            PopulateContract(contract, CreateArguments(contract, args));

            return contract;
        }

        private List<Argument> CreateArguments(T contract, string[] args)
        {
            SwitchStack<T> switchStack = new SwitchStack<T>(contract);
            ArgsStash argStash = new ArgsStash(args);
            List<Argument> argumentsList = new List<Argument>();

            while (!switchStack.Empty)
            {
                string switchKey = switchStack.Pop();
                string arg = argStash.Pop(switchKey);
                if (arg != null)
                {
                    Argument argument = new Argument(arg, switchKey);
                    argumentsList.Add(argument);
                }
            }
            return argumentsList;
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

        private KeyValueSwitchAttribute[] PropertyArgumentContracts(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            KeyValueSwitchAttribute[] argumentAttributes = (from obj in attrs
                                                            select obj).OfType<KeyValueSwitchAttribute>().ToArray();
            return argumentAttributes;
        }

        private void WriteArgument(object argumentContract, PropertyInfo property, List<Argument> arguments)
        {
            KeyValueSwitchAttribute[] argumentAttributes = PropertyArgumentContracts(property);

            foreach (KeyValueSwitchAttribute argAttr in argumentAttributes)
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
