///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.IO;
using System.ServiceModel;
using Test.CheckMessagesConveyor.MessagesConveyorReference;

namespace Test.CheckMessagesConveyor {

    public class MessageProcessorExample : IMessagesConveyorServiceCallback {

        MessagesConveyorServiceClient _proxy;

        public async void CallbackInvoke(Message message) {

            if (message != null) {

                Console.WriteLine("User message: {0}", message.Text);

                message.Response = new MResponse();
                message.Response.Text = "Echo: " + message.Text;

                Console.WriteLine("Bot response: {0}", message.Response.Text);

                await _proxy.SendResponseAsync(message);
            }
        }

        public void Initialize() {

            InstanceContext instanceContext = new InstanceContext(this);
            _proxy = new MessagesConveyorServiceClient(instanceContext);
            _proxy.Open();

            string botToken;

            try {
                using (StreamReader file = new StreamReader("botToken.txt")) {

                    if ((botToken = file.ReadLine()) != null) {
                        _proxy.Start(botToken);
                        Console.WriteLine("Telegram API Interaction ready...\n");
                    }
                    file.Close();
                }
            }
            catch (FileNotFoundException e) {
                Console.WriteLine(e.Message);
            }            
        }

        public MessageProcessorExample() {
            Initialize();
        }
    }
}