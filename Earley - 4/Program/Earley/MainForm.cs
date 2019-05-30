using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Earley
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Work0();
           // Work1();
            Work2();
        }

        private void test(string[] sent, EarleyParser parser)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < sent.Length - 1; i++)
                output.Append(sent[i] + " ");

            output.Append(sent[sent.Length - 1] + ".");
            string sentence = output.ToString();
            textBox1.Text += "Xâu Input: " + sentence + "\r\n";
            bool successful = parser.parseSentence(sent);
            textBox1.Text += "Kết quả: " + successful.ToString() + "\r\n";
            Chart[] charts = parser.getCharts();
            textBox1.Text += "Bảng  phân tích trạng thái xâu: " + sentence + "\r\n";

            for (int i = 0; i < charts.Length; i++)
            {
                textBox1.Text += "Bảng[" + i.ToString() + "] :\r\n";
                textBox1.Text += charts[i].ToString() + "\r\n";
            }

            textBox1.Text += "\r\n";
        }

        private void Work0()
        {
            string[] sentence0 = { "2", "+", "3", "*", "4" };

            Grammar grammar = new SimpleGrammar1();
            EarleyParser parser = new EarleyParser(grammar);

            test(sentence0, parser);
        }

        private void Work1()
        {
            string[] sentence1 = { "John", "called", "Mary" };
            string[] sentence2 = { "John", "called", "Mary",
                                     "from", "Denver" };
            Grammar grammar = new SimpleGrammar();
            EarleyParser parser = new EarleyParser(grammar);

            test(sentence1, parser);
            test(sentence2, parser);
        }
        private void Work2()
        {
            string[] sentence3 = { "PaPa", "ate", "the", "caviar","with","a","spoon"};
            //string[] sentence2 = { "John", "called", "Mary",
            //   "from", "Denver" };
            Grammar grammar = new SimpleGrammar2();
            EarleyParser parser = new EarleyParser(grammar);

            test(sentence3, parser);
            //test(sentence2, parser);
        }
        //private void readtext()
        //{
        //    string tenfile;
        //    OpenFileDialog dia = new OpenFileDialog();
        //    dia.Filter = "*.txt";
        //    dia.Title = "Chon File";
        //    dia.Multiselect = false;
        //    tenfile = dia.FileNames();
        //    StreamReader rd = new StreamReader(tenfile);
        //}  
    }
}