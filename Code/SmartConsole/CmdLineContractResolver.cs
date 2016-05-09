using BlackIris.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackIris
{
    public class CmdlineContractResolver
    {
        private List<Type> supportedContracts = new List<Type>();

        public void Add(Type contract)
        {
            supportedContracts.Add(contract);
        }

        public Type GetContract(string[] args)
        {
            string[] supportedVerbs = GetVerbs();
            string commandVerb = FindVerb(supportedVerbs, args);


            foreach (Type contract in supportedContracts)
            {
                object[] attrs = contract.GetCustomAttributes(false);
                CommandlineContractAttribute attr = (from obj in attrs
                                                     select obj).OfType<CommandlineContractAttribute>().SingleOrDefault();
                bool supported = attr.Verbs.Contains(commandVerb);
                if (supported)
                {
                    return contract;
                }
            }
            return null;
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
