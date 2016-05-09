using BlackIris.Attributes;
using BlackIris.Common.Exceptions;
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

            if (!SingleVerb(supportedVerbs, args))
                throw new MultipleVerbsFoundException("Multiple verbs are not supported.");

            string commandVerb = FindVerb(supportedVerbs, args);

            if (!VerbIsFirst(commandVerb, args))
                throw new InvalidVerbPositionException("Verb is out of position.");

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

        private bool VerbIsFirst(string verb, string[] args)
        {
            if (args[0] != verb)
                return false;
            return true;
        }

        private bool SingleVerb(string[] supportedVerbs, string[] args)
        {
            int noVerbs = 0;

            for (int k = 0; k < supportedVerbs.Length; k++)
            {
                int numFound = args.Where(a => a == supportedVerbs[k]).Count();
                noVerbs += numFound;
            }

            return noVerbs <= 1;
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
