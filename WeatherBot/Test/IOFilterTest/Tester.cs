using System;
using WeatherBot.IOFilter;

namespace Test.IOFilterTest
{
    class Tester
    {
        IoFilter iof;
        delegate void TestedMethod();

        public Tester()
        {
            Console.WriteLine("CREATE TESTOR");
            testRUN(InitIOFilterator, "InitIOFilterator");
            DateTime dtstart = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 1);
            while (true)
            {
                if (ts < DateTime.Now - dtstart)
                {
                    dtstart = DateTime.Now;
                  
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
            iof = new IoFilter(DebugOut);
        }

        private void DebugOut(string debug_text)
        {
            Console.WriteLine(debug_text);
        }
    }
}