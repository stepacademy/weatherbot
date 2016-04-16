namespace WeatherBot.TeleInteraction {

    using System.IO;
    using System.Threading.Tasks;

    public class TeleInteractor : ITeleInteractor {

        private string _botToken;

        private Telegram.Bot.Api _bot;
        private Telegram.Bot.Types.User _me;
        private Telegram.Bot.Types.Update[] _updates;

        public string BotName {
            get {
                return _me.FirstName;
            }
        }

        public async Task<Message> GetNextMessageAsync() {

            //dummy await
            System.Threading.Thread.Sleep(2400);

            return new Message();
        }

        private void Initialize(string tokenPath = "botToken.txt") {

            using (StreamReader file = new StreamReader(tokenPath)) {
                if ((_botToken = file.ReadLine()) != null) {

                    _bot = new Telegram.Bot.Api(_botToken);
                    _me = _bot.GetMe().Result;

                }
                //file.Close();
            }
        }

        public TeleInteractor() {
            Initialize();
        }
    }
}