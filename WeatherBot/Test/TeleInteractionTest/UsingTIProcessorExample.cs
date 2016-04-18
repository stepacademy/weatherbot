using WeatherBot.TeleInteraction;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace Test.TeleInteractionTest {

    public delegate void OnChangeDelegate(Message message);

    class UsingTIProcessorExample {

        private event OnChangeDelegate OnChangeEvent;

        private Message MessageProcessing(Message message) {

            if (message != null) {

                if (OnChangeEvent != null)
                    OnChangeEvent.Invoke(message);

                // --- message processing ---

                message.Response = new MessageResponse();
                message.Response.Text = "Cам " + message.Text + ", " + message.User.FirstName + "! =)";

                // --- message processing ---

                if (OnChangeEvent != null)
                    OnChangeEvent.Invoke(message);

                return message;
            }
            return null;
        }

        public void Start(OnChangeDelegate onChangeEvent) {

            OnChangeEvent += onChangeEvent;
            InteractionProcess.Instance.ProcessingEventHandlers += MessageProcessing;
            InteractionProcess.Instance.State = InteractionProcessState.Launched;
        }

    }
}