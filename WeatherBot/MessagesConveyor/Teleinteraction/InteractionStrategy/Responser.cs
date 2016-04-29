///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.MessagesConveyor.TeleInteraction.InteractionStrategy {

    using Adapters;

    internal class Responser {

        public async void SendResponse(Message message) {

            if (message != null && message.Response != null) {

                if (message.Response.Text != null)
                    await Bot.Api.SendTextMessage(message.User.Id, message.Response.Text);
            }
        }
    }
}