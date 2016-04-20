namespace Test.TeleInteractionTest {

    using System;
    using System.ServiceModel;
    using WeatherBot.TeleInteraction;
    using WeatherBot.TeleInteraction.TelegramAdapters;

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

            //WSDualHttpBinding binding = new WSDualHttpBinding();
            //EndpointAddress endptadr = new EndpointAddress("http://localhost:12000/Duplex/Server");
            //binding.ClientBaseAddress = new Uri("http://localhost:8000/Duplex/Client/");

            UsingTIProcessorExample example = new UsingTIProcessorExample();

            using (var host = new ServiceHost(typeof(ReceiveActionListenerService))) {

                host.Open();
                
                example.Start(Display);
            }
        }
    }
}