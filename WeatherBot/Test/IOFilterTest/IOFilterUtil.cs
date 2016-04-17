using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBot.IOFilter;

namespace Test.IOFilterTest
{
    class Tester
    {
        IOFilterator iof;
        delegate void TestedMethod();

        public Tester()
        {
            Console.WriteLine("CREATE TESTOR");
            testRUN(InitIOFilterator, "InitIOFilterator");
            DateTime dtstart = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 10);
            while (true)
            {
                if (ts < DateTime.Now - dtstart)
                {
                    dtstart = DateTime.Now;
                    //Console.Clear();
                    Console.WriteLine(DateTime.Now + "waiting...");
                }
            }
        }

        private void testRUN(TestedMethod method, string description)
        {
            Console.Write(description + "...");
            try
            {
                method();
            }
            catch
            {
                Console.Write(description + "FAULT");
                return;
            }
            Console.Write(description + "OK");
        }

        private void InitIOFilterator()
        {
            iof = new IOFilterator();
            iof.DebugOutEvent += Messaging;
        }

        private void Messaging(string debug_text)
        {
            Console.WriteLine(debug_text);
        }
    }
    class IOFilterUtil
    {
        static void Main(string[] args)
        {
            Tester tester = new Tester();
        }
    }
}

