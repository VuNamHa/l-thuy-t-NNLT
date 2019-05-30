using System;

namespace Earley
{
    class SimpleGrammar1 : Grammar
    {
        public SimpleGrammar1()
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
            string[] s1 = { "S", "+", "M" };
            string[] s2 = { "M" };
            RHS[] sRHS = { new RHS(s1), new RHS(s2) };
            Rules.Add(sRHS, "S");
            string[] s3 = { "M", "*", "T"};
            string[] s4 = { "T" };
            RHS[] s3RHS = { new RHS(s3), new RHS(s4) };
            Rules.Add(s3RHS, "M");
            string[] s5 = { "Number" };
            RHS[] s5RHS = { new RHS(s5) };
            Rules.Add(s5RHS, "T");
            string[] s6 = { "1", "2", "3", "4" };
            RHS[] s6RHS = { new RHS(s6) };
            Rules.Add(s6RHS, "Number");
            string[] s7 = { "+" };
            RHS[] s7RHS = { new RHS(s7) };
            Rules.Add(s7RHS, "+");
            string[] s8 = { "*"};
            RHS[] s8RHS = { new RHS(s8) };
            Rules.Add(s8RHS, "*");
        }

        private void initPOS()
        {
            POS.Add("+");
            POS.Add("*");
            POS.Add("Number");
        }
    }
}