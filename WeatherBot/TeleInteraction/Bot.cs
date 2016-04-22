///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.IO;

namespace WeatherBot.TeleInteraction {

    internal class Bot {

        private string           _botToken;        
        private static Bot       _instance;
        private Telegram.Bot.Api _api;


        public static Telegram.Bot.Api Api {
            get {
                if (_instance == null)
                    _instance = new Bot();
                return _instance._api;
            }
        }

        private void Initialize(string tokenPath) {

            using (StreamReader file = new StreamReader(tokenPath)) {

                if ((_botToken = file.ReadLine()) != null)
                    _api = new Telegram.Bot.Api(_botToken);

                file.Close();
            }
        }

        private Bot() {
            Initialize("botToken.txt");
        }

    }
}