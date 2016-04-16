using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace WeatherBot.TeleInteraction {

    public class TeleInteractor : ITeleInteractor {

        private string _botToken;
        private int _lastUdateId;

        private Telegram.Bot.Api _bot;
        private Telegram.Bot.Types.User _me;
        private Telegram.Bot.Types.Update[] _updates;

        private Queue<Telegram.Bot.Types.Update> _updatesQueue;

        public string BotName {
            get {
                return _me.FirstName;
            }
        }

        private async Task RequestAsync() {

            _updates = await _bot.GetUpdates();

            if (_updatesQueue == null) {
                _updatesQueue = new Queue<Telegram.Bot.Types.Update>(_updates);
                _lastUdateId = _updatesQueue.Count > 0 ? _updatesQueue.Last().Id : 0;
                return;
            }

            foreach (var update in _updates) {
                if (update.Id <= _lastUdateId) {
                    continue;
                }
                else {
                    while (_updatesQueue.Count > 100) {
                        _updatesQueue.Dequeue();
                    }
                    _updatesQueue.Enqueue(update);
                    _lastUdateId = update.Id;
                }
            }
        }

        public async Task<Message> GetNextMessageAsync() {

            await RequestAsync();

            if (_updatesQueue.Count > 0)
                return new Message(_updatesQueue.Dequeue());

            return null;
        }

        private void Initialize(string tokenPath = "botToken.txt") {

            using (StreamReader file = new StreamReader(tokenPath)) {
                if ((_botToken = file.ReadLine()) != null) {

                    _bot = new Telegram.Bot.Api(_botToken);
                    _me = _bot.GetMe().Result;
                }
                // file.Close();
            }
        }

        public TeleInteractor() {
            Initialize();
        }
    }
}