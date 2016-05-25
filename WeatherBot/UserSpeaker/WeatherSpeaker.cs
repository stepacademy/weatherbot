///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Text;


namespace WeatherBot.UserSpeaker {

    using DatabaseWorker.QueryComponents;

    public class WeatherSpeaker : IUserSpeaker {

        public string FormReply(QueryData response) {

            StringBuilder result = new StringBuilder();

            result.Append(response.City + ", ");

            foreach (var weather in response.WeatherAtTimes) {

                string date = weather.Key.ToLocalTime().ToShortDateString();
                string time = weather.Key.ToLocalTime().ToShortTimeString();

                result
                    .Append("текущая погода" + /*date + " - " + time +*/ "\n\n")
                    //.Append("Ожидается: " + weather.Value.State + '\n')
                    .Append("Температура: " + weather.Value.Temperature + " °C\n")
                    .Append("Ветер: " + weather.Value.WindDirection.ToString() + ' ' + weather.Value.WindSpeed + " м/с\n")
                    .Append("Относительная влажность: " + weather.Value.Humidity + " %\n")
                    .Append("Атмосферное давление: " + weather.Value.Pressure + " мм рт. ст. \n");
            }
            return result.ToString();
        }
    }
}