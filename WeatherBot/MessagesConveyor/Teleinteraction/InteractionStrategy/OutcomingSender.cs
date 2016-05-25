///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using Adapters;
    using UserSpeaker;
    using DatabaseWorker.QueryComponents;

    public class OutcomingSender {

        private IUserSpeaker    _speaker;

        private async void Response(Response response) {

            if (response != null) {

                if (response.Text != null)
                    await Bot.Api.SendTextMessage(response.InitiatorId, response.Text);
                else
                    await Bot.Api.SendTextMessage(response.InitiatorId, "response.Text: null");
            }
            else {
                await Bot.Api.SendTextMessage(response.InitiatorId, "response: null");
            }
        }

        public void Response(QueryData response) {

            Response resp = new Response();
            resp.InitiatorId = response.InitiatorId;

            if (response.Error != null)
                resp.Text = response.Error;
            else
                resp.Text = _speaker.FormReply(response);

            Response(resp);

        }

        public OutcomingSender() {
            _speaker = new WeatherSpeaker();
        }
    }
}