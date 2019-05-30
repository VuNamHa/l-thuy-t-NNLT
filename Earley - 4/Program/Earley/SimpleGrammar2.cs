using System;

namespace Earley
{
    class SimpleGrammar2 : Grammar
    {
        public SimpleGrammar2()
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
            //----------------------------
            string[] s2 = {"Det","N"};
            string[] s3 = {"NP","PP"};
            string[] s4 = {"Name"};
            RHS[] sRHS1 = { new RHS(s2), new RHS(s3), new RHS(s4)};
            Rules.Add(sRHS1, "NP");
            //-----------------------------
            string[] s5 = { "VP","PP"};
            string[] s6 = { "V", "NP"};
            RHS[] sRHS2 = {new RHS(s6), new RHS(s5)};
            Rules.Add(sRHS2, "VP");
            //------------------------------
            string[] s7 = {"P", "NP"};
            RHS[] sRHS3 = {new RHS (s7) };
            Rules.Add(sRHS3, "PP");
            //-------------------------------
            string[] s8 = {"caviar"};
            string[] s9 = {"spoon"};
            RHS[] sRHS4 = {new RHS(s8), new RHS(s9) };
            Rules.Add(sRHS4, "N");
            //-------------------------------
            string[] s10 = {"the"};
            string[] s11 = { "a" };
            RHS[] sRHS5 = { new RHS(s10), new RHS(s11)};
            Rules.Add(sRHS5, "Det");
            //--------------------------------
            string[] s12 = { "ate" };
            RHS[] sRHS6 = { new RHS(s12)};
            Rules.Add(sRHS6, "V");
            //--------------------------------
            string[] s13 = { "with" };
            RHS[] sRHS7 = { new RHS(s13) };
            Rules.Add(sRHS7, "P");
            //--------------------------------
            string[] s14 = { "PaPa" };
            RHS[] sRHS8 = { new RHS(s14) };
            Rules.Add(sRHS8, "Name");
        }

        //private void initRules()
        //{
     

        //    string[] s1 = { "NP", "VP" };
        //    RHS[] sRHS = { new RHS(s1) };
        //    Rules.Add(sRHS, "S");
        //    //----------------------------
          
        //    string[] s2 = { "Det", "N" };
           
        //    string[] s4 = { "Name" };
        //    string[] s3 = { "NP", "PP" }; 
        //    RHS[] sRHS1 = {  new RHS(s2), new RHS(s4),new RHS(s3) };
        //    Rules.Add(sRHS1, "NP");      

        //    //-----------------------------
            
        //    string[] s6 = { "V", "NP" };
            
        //    string[] s66 = { "V" };
        //    RHS[] sRHS2 = { new RHS(s6), new RHS(s66) };
        //    Rules.Add(sRHS2, "VP");
        //    //------------------------------
        //    string[] s67 = { "P", "NP" };
        //    RHS[] sRHS66 = { new RHS(s6), new RHS(s67) };
        //    Rules.Add(sRHS66, "PP");
        //    //-------------------------------
        //    string[] s8 = { "caviar" };
        //    string[] s9 = { "spoon" };
        //    RHS[] sRHS4 = { new RHS(s8),new RHS(s9)};
        //    Rules.Add(sRHS4, "N");
        //    //-------------------------------
        //    string[] s10 = { "the" };
        //    string[] s11 = { "a" };
        //    RHS[] sRHS5 = { new RHS(s10), new RHS(s11) };
        //    Rules.Add(sRHS5, "Det");
        //    //--------------------------------
        //    string[] s12 = { "ate" };
        //    RHS[] sRHS6 = { new RHS(s12) };
        //    Rules.Add(sRHS6, "V");

        //    //--------------------------------
        //    string[] nam = { "PaPa" };
        //    RHS[] namRHS = { new RHS(nam) };
        //    Rules.Add(namRHS, "Name");
        //    //--------------------------------
        //    string[] nam1 = { "with" };
        //    RHS[] nam1RHS = { new RHS(nam1) };
        //    Rules.Add(nam1RHS, "P");

           
        //}
        private void initPOS()
        {
            POS.Add("Name");
            POS.Add("V");
            POS.Add("Det");
            POS.Add("N");
            POS.Add("P");
           
           /* POS.Add("Prep");
            POS.Add("Det");
            POS.Add("Verb");
            POS.Add("Noun");
            POS.Add("Name");*/
           
        }
    }
}