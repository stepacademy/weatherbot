using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherBot.TeleInteraction;

namespace Test.TeleInteractionTest {

    class TeleIntrTest {

        static async void TestProcess(ITeleInteractor ti) {

            Message m = await ti.GetNextMessageAsync();
            Console.WriteLine(m.Text);
        }

        static void Main(string[] args) {

            ITeleInteractor test = new TeleInteractor();

            while (true) {
                TestProcess(test);
            }

            Thread.Sleep(Timeout.Infinite);
        }

    }
}