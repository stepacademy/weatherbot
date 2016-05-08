///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.IO {

    using Parser;
    using DataInteraction;
    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;
    using DatabaseWorker.QueryComponents;

    internal sealed class InputParserEntryPoint {

        private DatabaseWorkerProxy _database;
        private InputParser           _parser;
        private WeatherSpeaker _directSpeaker;

        private void DirectResponse(int initiatorId, string message) {
            _directSpeaker.Response(new QueryData { InitiatorId = initiatorId, Error = message});
        }

        private void Incoming(Message message) {

            string city = _parser.ExtractFirstCity(message.Text);

            if (city != null) {

                QueryData query = new QueryData {
                    InitiatorId = message.User.Id,
                    City = city,
                    WeatherAtTimes = new Dictionary<DateTime, WeatherEntities>()
                };
                query.WeatherAtTimes.Add(new DateTime(2016, 5, 8, 12, 0, 0), new WeatherEntities());

                DirectResponse(message.User.Id, "Возможно вы имели в виду: " + city + "?");
                _database.Query(query);
            }
            else {
                DirectResponse(
                    message.User.Id,
                    "Извините, но мы не смогли распознать введённый вами текст, попробуйте повторить попытку."
                    );
            }            
        }

        public InputParserEntryPoint(IInteractionStrategy sender, DatabaseWorkerProxy proxy) {
            sender.Incoming += Incoming;
            _database = proxy;
            _parser = new InputParser();
            _directSpeaker = new WeatherSpeaker();
        }
    }
}