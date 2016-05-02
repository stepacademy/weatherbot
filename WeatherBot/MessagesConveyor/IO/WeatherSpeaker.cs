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

            Response resp = new Response();
            resp.InitiatorId = response.InitiatorId;

            if (response.Error != null)
                resp.Text = response.Error;
            else
                resp.Text = "request: success";

            _sender.Response(resp);

        }

        public WeatherSpeaker() {
            _sender = new OutcomingSender();
        }
    }
}