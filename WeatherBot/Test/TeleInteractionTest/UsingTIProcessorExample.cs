using WeatherBot.TeleInteraction;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace Test.TeleInteractionTest {

    class UsingTIProcessorExample {

        private Message MessageProcessing(Message message) {

            if (message != null) {
                message.Response = new MessageResponse();
                message.Response.Text = "Using ReqResp Processor Example, Weather Bot Result from fake DB";

                return message;
            }
            return null;
        }

        // HARDCODE c-tor =)
        public UsingTIProcessorExample() {

            InteractionProcess.Instance.ProcessingEventHandlers += MessageProcessing;
            InteractionProcess.Instance.State = InteractionProcessState.Launched;
            InteractionProcess.Instance.Process();
        }

    }
}