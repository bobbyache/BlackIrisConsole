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

            WriteToContract(contract, CreateArguments(contract, args));

            return contract;
        }

        private void WriteToContract(T contract, List<Argument> arguments)
        {
            ContractWriter<T> contractWriter = new ContractWriter<T>();
            contractWriter.Write(contract, arguments);
        }

        private List<Argument> CreateArguments(T contract, string[] args)
        {
            // using the SwitchStack, the switches are presented in a matter so
            // that the switches with greater length beginning with the same
            // characters are grabbed first together with their matching args
            // so that conflicts are avoided.
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

        private bool IsCommandlineContract(T contract)
        {
            try
            {
                Type t = contract.GetType();
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
    }
}
