using System.Threading;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace WeatherBot.TeleInteraction {

    public enum InteractionProcessState { Launched, Suspended, Stopped }
    public delegate Message NextQueryProcessingEvent(Message message);

    public class InteractionProcess {

        private ITeleInteractor _teleInteractor;

        public InteractionProcessState State;
        public event NextQueryProcessingEvent ProcessingEventHandlers;

        private static InteractionProcess _instance;

        public static InteractionProcess Instance {
            get {
                if (_instance == null)
                    _instance = new InteractionProcess();
                return _instance;
            }
        }

        public void Process() {

            while (State != InteractionProcessState.Stopped) {
                if (State == InteractionProcessState.Launched) {
                    _teleInteractor.SendResponse(ProcessingEventHandlers.Invoke(_teleInteractor.GetNextMessage()));
                }
                Thread.Sleep(10);
            }
        }

        private InteractionProcess() {
            _teleInteractor = new TeleInteractor();
        }
    }
}