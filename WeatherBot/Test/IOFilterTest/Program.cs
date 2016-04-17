using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBot.IOFilter;

namespace Test.IOFilterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            IOFilterator iof = new IOFilterator();
          
                Console.WriteLine("Init IOFilterator");
                iof.MsgOutput += Messaging;
                Console.WriteLine("Attach messaging");

                DateTime dtstart = DateTime.Now;
                TimeSpan ts = new TimeSpan(0, 0, 2);
                while (true)
                {

                    Console.WriteLine(dtstart.ToString());
                    if (ts < DateTime.Now - dtstart)
                    {
                        dtstart = DateTime.Now;
                        Console.WriteLine(dtstart.ToString());

                    }

                }
          
        }

        static void Messaging(string msgin, string msgout)
        {
            Console.WriteLine("IN:");
            Console.WriteLine(msgin);
            Console.WriteLine("OUT:");
            Console.WriteLine(msgout);
        }
    }
}


/*
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherBot.IOFilter;

    namespace Test.IOFilerTest
{
    public partial class FormTestIOFilter : Form
    {
        IOFilterator iof;
        public FormTestIOFilter()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            iof = new IOFilterator();
            iof.MsgOutput+= Messaging;
        }

        private void Messaging(string msgin, string msgout)
        {
            textBox1.Text += msgin;
            textBox2.Text += msgout;
        }
    }
}

*/
