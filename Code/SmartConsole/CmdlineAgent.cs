using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using BlackIris.Attributes;
using BlackIris.Arguments;

namespace BlackIris
{
    public class CmdlineAgent<TContract> where TContract : class, new()
    {
        public TContract Deserialize(string[] args)
        {

            TContract contract = new TContract();

            if (!IsCommandlineContract(contract))
                throw new NotSupportedException("The object contract is not decorated with the CommandlineContract attribute.");

            List<Argument> keyValueSwitchParams = CreateKeyValueSwitchParameters(contract, args);
            KeyValueSwitchWriter<TContract> contractWriter = new KeyValueSwitchWriter<TContract>();
            contractWriter.Write(contract, keyValueSwitchParams);

            return contract;
        }

        private List<Argument> CreateKeyValueSwitchParameters(TContract contract, string[] args)
        {
            // using the SwitchStack, the switches are presented in a matter so
            // that the switches with greater length beginning with the same
            // characters are grabbed first together with their matching args
            // so that conflicts are avoided.
            SwitchStack<TContract, KeyValueSwitchAttribute> switchStack = new SwitchStack<TContract, KeyValueSwitchAttribute>(contract);
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

        private bool IsCommandlineContract(TContract contract)
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
