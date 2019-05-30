using System;

namespace Earley
{
    class SimpleGrammar : Grammar
    {
        public SimpleGrammar()
        {
            initialize();
        }

        private void initialize()
        {
            initRules();
            initPOS();
        }

        private void initRules()
        {
            string[] s1 = { "NP", "VP" };
            RHS[] sRHS = { new RHS(s1) };
            Rules.Add(sRHS, "S");
            string[] np1 = { "NP", "PP" };
            string[] np2 = { "Noun" };
            RHS[] npRHS = { new RHS(np1), new RHS(np2) };
            Rules.Add(npRHS, "NP");
            string[] vp1 = { "VP", "NP" };
            string[] vp2 = { "Verb", "PP" };
            string[] vp3 = { "Verb", "Noun" };
            string[] vp4 = { "Verb" };
            RHS[] vpRHS = { new RHS(vp1), new RHS(vp2),
                              new RHS(vp3), new RHS(vp4) };
            Rules.Add(vpRHS, "VP");
            string[] pp1 = { "Prep", "NP" };
            string[] pp2 = { "Prep" };
            RHS[] ppRHS = { new RHS(pp1), new RHS(pp2) };
            Rules.Add(ppRHS, "PP");
            string[] noun1 = { "John" };
            string[] noun2 = { "Mary" };
            string[] noun3 = { "Denver" };
            RHS[] nounRHS = { new RHS(noun1), new RHS(noun2), new RHS(noun3) };
            Rules.Add(nounRHS, "Noun");
            string[] verb = { "called" };
            RHS[] verbRHS = { new RHS(verb) };
            Rules.Add(verbRHS, "Verb");
            string[] prep = { "from" };
            RHS[] prepRHS = { new RHS(prep) };
            Rules.Add(prepRHS, "Prep");
        }

        private void initPOS()
        {
            POS.Add("Noun");
            POS.Add("Verb");
            POS.Add("Prep");
        }
    }
}