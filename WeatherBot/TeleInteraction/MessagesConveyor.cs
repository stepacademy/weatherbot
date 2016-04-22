///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using WeatherBot.TeleInteraction.Adapters;
using WeatherBot.TeleInteraction.InteractionStrategy;

namespace WeatherBot.TeleInteraction {

    public class MessagesConveyor : IMessagesConveyorService {

        private IInteractionStrategy _interaction;

        public void SendResponse(Message message) {
            throw new NotImplementedException();
        }

        public void Start() {
            _interaction.Start();
        }

        public void Stop() {
            _interaction.Stop();
        }

        public MessagesConveyor() {
            _interaction = new BasedGetUpdates();
        }

        public MessagesConveyor(InteractionMode iMode) {

            if (iMode == InteractionMode.WebHookBased)
                _interaction = new BasedWebHook();
            else
                _interaction = new BasedGetUpdates();
        }

    }
}