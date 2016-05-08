///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Threading;

namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using IO;
    using Message = Adapters.Message;

    internal sealed class BasedGetUpdates : IInteractionStrategy {

        private int            _offset;
        private Timer          _stateTimer;
        private AutoResetEvent _autoEvent;

        public event MessageIncomingEvent Incoming;

        private async void PerformStep(object stateInfo) {

            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;

            var updates = await Bot.Api.GetUpdates(_offset);

            foreach (var update in updates) {
                _offset = update.Id + 1;
                Incoming.Invoke(new Message(update));                
            }

            autoEvent.Set();
        }

        public void Start() {
            _autoEvent = new AutoResetEvent(false);
            _stateTimer = new Timer(PerformStep, _autoEvent, 0, 500);
        }

        public void Stop() {
            if (_stateTimer != null)
                _stateTimer.Dispose();
        }
    }
}