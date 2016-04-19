namespace WeatherBot.TeleInteraction {

    using System;
    using System.IO;
    using System.Collections.Generic;
    using TelegramAdapters;

    public delegate void ReceivedUpdatesEventHandler(Queue<Telegram.Bot.Types.Update> updatesQueue);

    public class TeleInteractor {

        public event ReceivedUpdatesEventHandler Received = (Queue<Telegram.Bot.Types.Update> uQ) => { };

        private string _botToken;

        private Telegram.Bot.Api            _api;
        private Telegram.Bot.Types.Update[] _updates;

        private int      _lastUdateId;
        private DateTime _lastRequestTime;
        private double   _updateIntervalMs;

        private static TeleInteractor _instance;

        public static TeleInteractor Instance {
            get {
                if (_instance == null)
                    _instance = new TeleInteractor();
                return _instance;
            }
        }


        // ------------ This section of code will be replaced by up to receive updates using the WebHook ------------ //
        //                                                                                                            //

        public async void RequestAsync() {

            if ((DateTime.Now - _lastRequestTime).TotalMilliseconds > _updateIntervalMs) {

                _lastRequestTime = DateTime.Now;

                _updates = await _api.GetUpdates(_lastUdateId);

                if (_updates != null && _lastUdateId != _updates[_updates.Length - 1].Id) {
                    OnReceive(new Queue<Telegram.Bot.Types.Update>(_updates));  // <- Receive Updates Event Initiator
                    _lastUdateId = _updates[_updates.Length - 1].Id;
                }                
            }
        }

        //                                                                                                            //
        // ------------ This section of code will be replaced by up to receive updates using the WebHook ------------ //


        public async void SendResponse(Message message) {                                     // <- quick issue impl.

            if (message != null && message.Response != null) {

                if (message.Response.Text != null)
                    await _api.SendTextMessage(message.User.Id, message.Response.Text);
            }
        }

        private void OnReceive(Queue<Telegram.Bot.Types.Update> updatesQueue) {
            if (Received != null)
                Received(updatesQueue);
        }

        private void Initialize(string tokenPath = "botToken.txt") {

            using (StreamReader file = new StreamReader(tokenPath)) {

                if ((_botToken = file.ReadLine()) != null)
                    _api = new Telegram.Bot.Api(_botToken);

                file.Close();
            }
        }

        private TeleInteractor(double updateIntervalMs = 1000) {
            Initialize();
            _updateIntervalMs = updateIntervalMs;                                      // will be dynamic recalculate
        }
    }
}