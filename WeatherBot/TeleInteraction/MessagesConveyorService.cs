///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;
using WeatherBot.TeleInteraction.Adapters;
using WeatherBot.TeleInteraction.InteractionStrategy;

namespace WeatherBot.TeleInteraction {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MessagesConveyorService : IMessagesConveyorService {

        private IInteractionStrategy _interaction;
        public static string BotToken;

        public async void SendResponse(Message message) {

            if (message != null && message.Response != null) {

                if (message.Response.Text != null)
                    await Bot.Api.SendTextMessage(message.User.Id, message.Response.Text);
            }
        }

        public void Start(string botToken) {
            BotToken = botToken;
            _interaction.Start();
        }

        public void Stop() {
            _interaction.Stop();
        }

        public MessagesConveyorService() {
            _interaction = new BasedGetUpdates();
        }

        public MessagesConveyorService(InteractionMode iMode) {

            if (iMode == InteractionMode.WebHookBased)
                _interaction = new BasedWebHook();
            else
                _interaction = new BasedGetUpdates();
        }
    }
}