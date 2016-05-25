///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Threading;

namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using Input;
    using Message = Adapters.Message;

    internal sealed class BasedGetUpdates : IInteractionStrategy {

        private int            _offset;
        private Timer          _stateTimer;

        public event MessageIncomingEvent Incoming;

        private async void PerformStep(object stateInfo) {

            var updates = await Bot.Api.GetUpdates(_offset);

            foreach (var update in updates) {                
                _offset = update.Id + 1;
                Incoming.Invoke(new Message(update));
            }
        }

        public void Start() {
            _stateTimer = new Timer(PerformStep);
            _stateTimer.Change(0, 1000);
        }

        public void Stop() {
            if (_stateTimer != null)
                _stateTimer.Dispose();
        }

    }
}