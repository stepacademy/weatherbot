///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

namespace WeatherBot.TeleInteraction {

    internal class Bot {

        private static Bot  _instance;
        private Telegram.Bot.Api _api;

        public static Telegram.Bot.Api Api {
            get {
                if (_instance == null)
                    _instance = new Bot();
                return _instance._api;
            }
        }

        private Bot() {
            _api = new Telegram.Bot.Api(MessagesConveyorService.BotToken);
        }
    }
}