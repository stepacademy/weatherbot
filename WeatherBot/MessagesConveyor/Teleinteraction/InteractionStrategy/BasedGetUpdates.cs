///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Threading;

namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using Message = Adapters.Message;

    internal sealed class BasedGetUpdates : IInteractionStrategy {

        private int            _offset;
        private Timer          _stateTimer;
        private AutoResetEvent _autoEvent;

        public event MessageIncomingEvent Incoming;

        private async void PerformStep(object stateInfo) {

            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            autoEvent.Set();

            var updates = await Bot.Api.GetUpdates(_offset);

            foreach (var update in updates) {
                Incoming.Invoke(new Message(update));
                _offset = update.Id + 1;
            }
        }

        public void Start() {
            _autoEvent = new AutoResetEvent(false);
            _stateTimer = new Timer(PerformStep, _autoEvent, 0, 1000);
        }

        public void Stop() {
            if (_stateTimer != null)
                _stateTimer.Dispose();
        }
    }
}