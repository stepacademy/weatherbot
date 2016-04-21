using WeatherBot.TeleInteraction;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace Test.TeleInteractionTest
{
    internal class UsingTIProcessorExample : IMessageProcessor
    {
        public Message MessageProcessing(Message message)
        {
            if (message != null)
            {
                OnChangeEvent.Invoke(message);

                // --- message processing ---

                message.Response = new MResponse();
                message.Response.Text = "Cам " + message.Text + ", " + message.User.FirstName + "! =)";

                // --- message processing ---

                OnChangeEvent.Invoke(message);

                return message;
            }
            return null;
        }

        private event OnChangeDelegate OnChangeEvent = (Message message) => { }; // <- null-reference Invoke protection

        public void Start(OnChangeDelegate onChangeEvent)
        {
            OnChangeEvent += onChangeEvent;
            //ReceiveActionListener.Instance.MessageProcessingEventHandlers += MessageProcessing;
            //ReceiveActionListener.Instance.Start();
            var s = new MessagesConveyorService();
            s.Start();
        }
    }
}