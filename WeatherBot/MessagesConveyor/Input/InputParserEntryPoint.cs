///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.Input {

    using Parser;
    using DataInteraction;
    using WeatherProviders;
    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;
    using DatabaseWorker.QueryComponents;
    
    internal sealed class InputParserEntryPoint {

        private DatabaseWorkerProxy _database;
        private InputParser           _parser;
        private OutcomingSender _directSender;
        private IWeatherProvider     _weather;

        private void DirectResponse(int initiatorId, string message) {
            _directSender.Response(new QueryData { InitiatorId = initiatorId, Error = message});
        }

        private async void Incoming(Message message) {

            string city = _parser.ExtractFirstCity(message.Text);

            if (city != null) {

                QueryData query = new QueryData {
                    InitiatorId = message.User.Id,
                    City = city,
                    WeatherAtTimes = new Dictionary<DateTime, WeatherEntities>()
                };
                query.WeatherAtTimes.Add(new DateTime(/**/), new WeatherEntities());

                //DirectResponse(message.User.Id, "Возможно вы имели в виду: " + city + "?");
                //_database.Query(query);
                await _weather.SetCurrentAsync(query);
                _directSender.Response(query);

            }
            else {
                DirectResponse(
                    message.User.Id,
                    "Извините, но мы не смогли распознать введённый вами текст, попробуйте повторить попытку."
                    );
            }            
        }

        public InputParserEntryPoint(IInteractionStrategy sender, DatabaseWorkerProxy proxy, string owmToken) {
            sender.Incoming += Incoming;
            _database        = proxy;
            _parser          = new InputParser();
            _directSender    = new OutcomingSender();
            _weather         = new OpenWeatherMap(owmToken);
        }
    }
}