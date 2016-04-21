using System;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types;
using Message = WeatherBot.TeleInteraction.TelegramAdapters.Message;

namespace WeatherBot.TeleInteraction
{
    public class TeleInteractor
    {
        private static TeleInteractor _instance;

        private Api _api;

        private string _botToken;
        private DateTime _lastRequestTime;

        private int _lastUdateId;
        private readonly double _updateIntervalMs;
        private Update[] _updates;

        private TeleInteractor(double updateIntervalMs = 1000)
        {
            Initialize();
            _updateIntervalMs = updateIntervalMs; // will be dynamic recalculate
        }

        public static TeleInteractor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TeleInteractor();
                return _instance;
            }
        }

        public event ReceivedUpdatesEventHandler Received = (Queue<Update> uQ) => { };


        // ------------ This section of code will be replaced by up to receive updates using the WebHook ------------ //
        //                                                                                                            //

        public async void RequestAsync()
        {
            if ((DateTime.Now - _lastRequestTime).TotalMilliseconds > _updateIntervalMs)
            {
                _lastRequestTime = DateTime.Now;

                _updates = await _api.GetUpdates(_lastUdateId);

                if (_updates != null && _lastUdateId != _updates[_updates.Length - 1].Id)
                {
                    OnReceive(new Queue<Update>(_updates)); // <- Receive Updates Event Initiator
                    _lastUdateId = _updates[_updates.Length - 1].Id;
                }
            }
        }

        //                                                                                                            //
        // ------------ This section of code will be replaced by up to receive updates using the WebHook ------------ //


        public async void SendResponse(Message message)
        {
            // <- quick issue impl.

            if (message != null && message.Response != null)
            {
                if (message.Response.Text != null)
                    await _api.SendTextMessage(message.User.Id, message.Response.Text);
            }
        }

        private void OnReceive(Queue<Update> updatesQueue)
        {
            if (Received != null)
                Received(updatesQueue);
        }

        private void Initialize(string tokenPath = "botToken.txt")
        {
            using (var file = new StreamReader(tokenPath))
            {
                if ((_botToken = file.ReadLine()) != null)
                    _api = new Api(_botToken);

                file.Close();
            }
        }
    }
}