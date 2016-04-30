///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.MessagesConveyor.IO {

    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;
    using DatabaseWorker.QueryComponents;

    internal sealed class WeatherSpeaker {

        private OutcomingSender _sender;

        public void Response(QueryData response) {

            // ... form reply

            _sender.Response(new Response() { InitiatorId = response.InitiatorId, Text = "response: duplex work" });

        }

        public WeatherSpeaker() {
            _sender = new OutcomingSender();
        }
    }
}