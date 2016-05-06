using BlackIris.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris
{
    public class SupportedContracts
    {
        private List<Type> supportedContracts = new List<Type>();

        public void Add(Type contract)
        {
            supportedContracts.Add(contract);
        }

        public bool GetContract(string[] args, out Type contractType)
        {
            string[] supportedVerbs = GetVerbs();
            string commandVerb = FindVerb(supportedVerbs, args);


            contractType = null;

            foreach (Type contract in supportedContracts)
            {
                object[] attrs = contract.GetCustomAttributes(false);
                CommandlineContractAttribute attr = (from obj in attrs
                                                     select obj).OfType<CommandlineContractAttribute>().SingleOrDefault();
                bool supported = attr.Verbs.Contains(commandVerb);
                if (supported)
                {
                    contractType = contract;
                    return true;
                }
            }
            return false;
        }

        private string FindVerb(string[] supportedVerbs, string[] args)
        {
            foreach (string verb in supportedVerbs)
            {
                bool found = args.Contains(verb);
                if (found)
                    return verb;
            }
            return null;
        }

        private string[] GetVerbs()
        {
            List<string> verbs = new List<string>();

            foreach (Type contract in supportedContracts)
            {
                object[] attrs = contract.GetCustomAttributes(false);
                CommandlineContractAttribute attr = (from obj in attrs
                                                     select obj).OfType<CommandlineContractAttribute>().SingleOrDefault();
                verbs.AddRange(attr.Verbs);
            }

            return verbs.ToArray();
        }
    }
}
