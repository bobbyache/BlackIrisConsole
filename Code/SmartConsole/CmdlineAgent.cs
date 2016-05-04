using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Iris.ConsoleArguments.Attributes;
using Iris.ConsoleArguments.Arguments;

namespace Iris.ConsoleArguments
{
    public class CmdlineAgent
    {
        public static void Deserialize(string[] args, object argContract)
        {
            if (isCommandlineContract(argContract))
            {
                populateContract(argContract, isolateArguments(argContract, args));
            }
            else
                throw new ApplicationException("The object contract is not decorated with the CommandlineContract attribute.");
        }

        private static List<Argument> isolateArguments(object argContract, string[] args)
        {
            Type t = argContract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();
            List<ArgumentContractAttribute> argumentContractAttributes = new List<ArgumentContractAttribute>();

            foreach (PropertyInfo propInfo in propInfos)
                argumentContractAttributes.AddRange(PropertyArgumentContracts(propInfo));

            string[] contractSwitches = (from a in argumentContractAttributes
                                        select a.SwitchCharacter).ToArray();

            ArgumentFactory info = new ArgumentFactory(contractSwitches);
            return info.ConstructArguments(args);
        }

        private static void populateContract(object argContract, List<Argument> arguments)
        {
            Type t = argContract.GetType();
            PropertyInfo[] propInfos = t.GetProperties();

            foreach (PropertyInfo propInfo in propInfos)
                writeArgument(argContract, propInfo, arguments);
        }

        private static bool isCommandlineContract(object argContract)
        {
            try
            {
                Type t = argContract.GetType();
                object[] attributes = t.GetCustomAttributes(false);

                CommandlineContractAttribute attr = (from a in attributes
                                                     select a).OfType<CommandlineContractAttribute>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private static ArgumentContractAttribute[] PropertyArgumentContracts(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(false);
            ArgumentContractAttribute[] argumentAttributes = (from obj in attrs
                                                              select obj).OfType<ArgumentContractAttribute>().ToArray();
            return argumentAttributes;
        }

        private static void writeArgument(object argumentContract, PropertyInfo property, List<Argument> arguments)
        {
            ArgumentContractAttribute[] argumentAttributes = PropertyArgumentContracts(property);

            foreach (ArgumentContractAttribute argAttr in argumentAttributes)
            {
                foreach (Argument argument in arguments)
                {
                    if (argument.Switch == argAttr.SwitchCharacter.ToString())
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
