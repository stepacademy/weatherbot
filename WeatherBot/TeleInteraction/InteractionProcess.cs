
/// InteractionProcess class / Stanislav Kuzmich / Art.Stea1th

namespace WeatherBot.TeleInteraction {

    using System.Threading;
    using WeatherBot.TeleInteraction.TelegramAdapters;

    public enum InteractionProcessState { Stopped, Launched };
    public delegate Message NextQueryProcessingEvent(Message message);

    public class InteractionProcess {

        private TeleInteractor _teleInteractor;
        private InteractionProcessState _state;

        public InteractionProcessState State {
            get {
                return _state;
            }
            set {
                if (_state == InteractionProcessState.Stopped) {
                    if (value == InteractionProcessState.Launched) {
                        _state = value;
                        Instance.Process();
                    }                    
                }
                else {
                    if (value == InteractionProcessState.Stopped) {
                        _state = InteractionProcessState.Stopped;
                    }
                }
            }
        }

        public event NextQueryProcessingEvent ProcessingEventHandlers;
        private static InteractionProcess _instance;

        public static InteractionProcess Instance {
            get {
                if (_instance == null)
                    _instance = new InteractionProcess();
                return _instance;
            }
        }

        private void Process() {

            while (_state == InteractionProcessState.Launched) {
                _teleInteractor.SendResponse(ProcessingEventHandlers.Invoke(_teleInteractor.GetNextMessage()));
                Thread.Sleep(10);
            }
        }

        private InteractionProcess() {
            _teleInteractor = new TeleInteractor();
            _state = InteractionProcessState.Stopped;
        }
    }
}