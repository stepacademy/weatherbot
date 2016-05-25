///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Text;

namespace WeatherBot.MessagesConveyor.IO {

    using TeleInteraction.Adapters;
    using TeleInteraction.InteractionStrategy;
    using DatabaseWorker.QueryComponents;
    
    internal sealed class WeatherSpeaker {

        private OutcomingSender _sender;

        private string FormReply(QueryData response) {  // <-- HARDCODE method

            StringBuilder result = new StringBuilder();

            result.Append(response.City + ", ");

            foreach (var weather in response.WeatherAtTimes) {

                string date = weather.Key.ToLocalTime().ToShortDateString();
                string time = weather.Key.ToLocalTime().ToShortTimeString();

                result
                    .Append(date + " - " + time + "\n\n")
                    .Append("Ожидается: " + weather.Value.State + '\n')
                    .Append("Температура: " + weather.Value.Temperature + " °C\n")
                    .Append("Ветер: " + weather.Value.WindDirection.ToString() + ' ' + weather.Value.WindSpeed + " м/с\n")
                    .Append("Относительная влажность: " + weather.Value.Humidity + " %\n")
                    .Append("Атмосферное давление: " + weather.Value.Pressure + " мм рт. ст. \n");
            }
            return result.ToString();
        }

        public void Response(QueryData response) {

            Response resp = new Response();
            resp.InitiatorId = response.InitiatorId;

            if (response.Error != null)
                resp.Text = response.Error;
            else
                resp.Text = FormReply(response);

            _sender.Response(resp);

        }

        public WeatherSpeaker() {
            _sender = new OutcomingSender();
        }
    }
}