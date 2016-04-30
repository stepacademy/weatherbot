///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using Adapters;

    internal sealed class OutcomingSender {

        public async void Response(Response response) {

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
    }
}