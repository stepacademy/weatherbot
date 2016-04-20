namespace Test.TeleInteractionTest {

    using WeatherBot.TeleInteraction;
    using WeatherBot.TeleInteraction.TelegramAdapters;

    class UsingTIProcessorExample : IMessageProcessor {

        private event OnChangeDelegate OnChangeEvent = (Message message) => { }; // <- null-reference Invoke protection

        public Message MessageProcessing(Message message) {

            if (message != null) {

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

        public void Start(OnChangeDelegate onChangeEvent) {

            OnChangeEvent += onChangeEvent;
            //ReceiveActionListener.Instance.MessageProcessingEventHandlers += MessageProcessing;
            //ReceiveActionListener.Instance.Start();
            MessagesConveyorService s = new MessagesConveyorService();
            s.Start();
        }

    }
}