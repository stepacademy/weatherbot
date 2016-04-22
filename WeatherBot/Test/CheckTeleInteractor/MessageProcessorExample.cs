///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.ServiceModel;
using WeatherBot.TeleInteraction;
using Test.CheckTeleInteractor.TeleInteractionReference;
using Message = WeatherBot.TeleInteraction.Adapters.Message;

namespace Test.CheckTeleInteractor {

    internal class MessageProcessorExample : IMessageProcessorCallback {

        public void Process(Message message) {
            
        }

        public void Initialize() {

            var instanceContext = new InstanceContext(this);
            var client = new MessagesConveyorServiceClient(instanceContext);
            Console.WriteLine("Ready...");
        }

        public MessageProcessorExample() {
            Initialize();
        }

        static void Main(string[] args) {

            MessageProcessorExample mpEx = new MessageProcessorExample();
            mpEx.Process(null);

            Console.ReadLine();
        }            
    }
}