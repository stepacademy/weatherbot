using System;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace Test.TeleInteractionTest {

    class TIProcessorTest {

        static void Display(Message message) {

            if (message.Text != null && message.Response == null) {
                Console.WriteLine(message.Text);
            }
            if (message.Response != null) {
                Console.WriteLine(message.Response.Text);
            }
        }

        static void Main(string[] args) {

            UsingTIProcessorExample example = new UsingTIProcessorExample();
            example.Start(Display);
        }
    }
}