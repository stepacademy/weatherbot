using System;
using System.Threading;
using WeatherBot.TeleInteraction;
using WeatherBot.TeleInteraction.ReqResp;

namespace Test.TeleInteractionTest {

    class TeleIntrTest {

        static void TestProcess(ITeleInteractor ti) {

            Message m = ti.GetNextMessage();
            if (m != null) {
                Console.WriteLine(m.Text);
            }            
        }

        static void Main(string[] args) {

            ITeleInteractor test = new TeleInteractor();

            while (true) {
                TestProcess(test);
                Thread.Sleep(1);
            }
        }
    }
}