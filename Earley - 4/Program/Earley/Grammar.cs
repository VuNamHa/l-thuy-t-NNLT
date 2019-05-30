using System;
using System.Collections.Generic;

namespace Earley
{
    class Grammar
    {
        protected Dictionary<RHS[], string> Rules;
        protected List<string> POS;

        public Grammar()
        {
            Rules = new Dictionary<RHS[], string>();
            POS = new List<string>();
        }

        public RHS[] getRHS(string lhs)
        {
            RHS[] rhs = null;

            if (Rules.ContainsValue(lhs))
            {
                foreach (KeyValuePair<RHS[], string> kvp in Rules)
                {
                    if (kvp.Value == lhs)
                    {
                        rhs = kvp.Key;
                        break;
                    }
                }
            }

            return rhs;
        }

        public bool isPartOfSpeech(string s)
        {
            return POS.Contains(s);
        }
    }
}
