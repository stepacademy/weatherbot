using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using WeatherBot.TeleInteraction.TelegramAdapters;


namespace WeatherBot.TeleInteraction {

    public class TeleInteractor : ITeleInteractor {

        private string _botToken;

        private Telegram.Bot.Api            _bot;
        private Telegram.Bot.Types.User     _me;
        private Telegram.Bot.Types.Update[] _updates;

        private int      _lastUdateId;
        private DateTime _lastRequestTime;
        private double   _updateIntervalMs;

        private Queue<Telegram.Bot.Types.Update> _updatesQueue;

        public string BotName {
            get {
                return _me.FirstName;
            }
        }

        private async void RequestAsync() {

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
                    while (_updatesQueue.Count > 100)
                        _updatesQueue.Dequeue();

                    _updatesQueue.Enqueue(update);
                    _lastUdateId = update.Id;
                }
            }            
        }

        public Message GetNextMessage() {

            if (_updatesQueue == null || _updatesQueue.Count == 0) {
                if ((DateTime.Now - _lastRequestTime).TotalMilliseconds > _updateIntervalMs) {
                    RequestAsync();
                    _lastRequestTime = DateTime.Now;
                }
            }
            else {
                return new Message(_updatesQueue.Dequeue());
            }

            return null;
        }

        public async void SendResponse(Message message) {                                // quick issue impl. need test

            if (message != null && message.Response != null) {

                if (message.Response.Text != null) {
                    await _bot.SendTextMessage(message.User.Id, message.Response.Text);
                }
            }
        }

        private void Initialize(string tokenPath = "botToken.txt") {

            using (StreamReader file = new StreamReader(tokenPath)) {
                if ((_botToken = file.ReadLine()) != null) {

                    _bot = new Telegram.Bot.Api(_botToken);
                    _me = _bot.GetMe().Result;
                }
                file.Close();
            }
        }

        public TeleInteractor(double updateIntervalMs = 1000) {
            Initialize();
            _updateIntervalMs = updateIntervalMs;                                        // will be dynamic recalculate
        }
    }
}