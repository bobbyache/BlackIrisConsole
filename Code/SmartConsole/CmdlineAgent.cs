using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using BlackIris.Attributes;
using BlackIris.Parameters;
using BlackIris.ContractWriters;
using BlackIris.Services;

namespace BlackIris
{
    public class CmdlineAgent<TContract> where TContract : class, new()
    {
        public TContract Deserialize(string[] args)
        {

            TContract contract = new TContract();

            if (!IsCommandlineContract(contract))
                throw new NotSupportedException("The object contract is not decorated with the CommandlineContract attribute.");

            // Get all key-value switches
            List<KeyValueSwitchParameter> keyValueSwitchParams = CreateKeyValueSwitchParameters(contract, args);
            KeyValueSwitchWriter<TContract> keyValueSwitchWriter = new KeyValueSwitchWriter<TContract>();
            keyValueSwitchWriter.Write(contract, keyValueSwitchParams);

            // Get all flag switches
            List<FlagSwitchParameter> flagSwitchParams = CreateFlagSwitchParameters(contract, args);
            FlagSwitchWriter<TContract> flagSwitchWriter = new FlagSwitchWriter<TContract>();
            flagSwitchWriter.Write(contract, flagSwitchParams);

            return contract;
        }

        private List<FlagSwitchParameter> CreateFlagSwitchParameters(TContract contract, string[] args)
        {
            // using the SwitchStack, the switches are presented in a matter so
            // that the switches with greater length beginning with the same
            // characters are grabbed first together with their matching args
            // so that conflicts are avoided.
            SwitchStack<TContract, FlagSwitchAttribute> switchStack = new SwitchStack<TContract, FlagSwitchAttribute>(contract);
            ArgsStash argStash = new ArgsStash(args);
            List<FlagSwitchParameter> argumentsList = new List<FlagSwitchParameter>();

            while (!switchStack.Empty)
            {
                string switchKey = switchStack.Pop();
                string arg = argStash.Pop(switchKey);
                if (arg != null)
                {
                    FlagSwitchParameter argument = new FlagSwitchParameter(arg);
                    argumentsList.Add(argument);
                }
            }
            return argumentsList;
        }

        private List<KeyValueSwitchParameter> CreateKeyValueSwitchParameters(TContract contract, string[] args)
        {
            // using the SwitchStack, the switches are presented in a matter so
            // that the switches with greater length beginning with the same
            // characters are grabbed first together with their matching args
            // so that conflicts are avoided.
            SwitchStack<TContract, KeyValueSwitchAttribute> switchStack = new SwitchStack<TContract, KeyValueSwitchAttribute>(contract);
            ArgsStash argStash = new ArgsStash(args);
            List<KeyValueSwitchParameter> argumentsList = new List<KeyValueSwitchParameter>();

            while (!switchStack.Empty)
            {
                string switchKey = switchStack.Pop();
                string arg = argStash.Pop(switchKey);
                if (arg != null)
                {
                    KeyValueSwitchParameter argument = new KeyValueSwitchParameter(arg, switchKey);
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
