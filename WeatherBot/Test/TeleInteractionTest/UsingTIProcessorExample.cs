namespace Test.TeleInteractionTest {

    using WeatherBot.TeleInteraction;
    using WeatherBot.TeleInteraction.TelegramAdapters;

    public delegate void OnChangeDelegate(Message message);

    class UsingTIProcessorExample {

        private event OnChangeDelegate OnChangeEvent = (Message message) => { }; // <- null-reference Invoke protection

        private Message MessageProcessing(Message message) {

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
            ReceiveActionListener.Instance.MessageProcessingEventHandlers += MessageProcessing;
            ReceiveActionListener.Instance.Process();
        }

    }
}