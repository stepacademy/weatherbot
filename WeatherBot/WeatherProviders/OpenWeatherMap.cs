///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace WeatherBot.WeatherProviders {

    using DatabaseWorker.QueryComponents;

    public class OpenWeatherMap : IWeatherProvider {

        private string _owmToken;

        private string GetWindDirection(int degress) {

            string[] directions = new string[] {
                "южный",     "юго-восточный",
                "восточный", "северо-восточный",
                "северный",  "северо-западный",
                "западный",  "юго-западный", "южный"
            };

            int index = (degress + 23) / 45;
            return directions[index];
        }

        public async Task SetCurrentAsync(QueryData query) {

            List<DateTime> queryDateTimes = new List<DateTime>(query.WeatherAtTimes.Keys);

            foreach (var dateTime in queryDateTimes) {

                using (var client = new HttpClient()) {

                    var response =
                    await client.GetStringAsync(
                        $"http://api.openweathermap.org/data/2.5/weather?q={query.City}&mode=json&units=metric&APPID="
                        + _owmToken);
                    dynamic dR = JsonConvert.DeserializeObject(response);

                    if (dR != null) {

                        //if (dR.weather.main != null) query.WeatherAtTimes[dateTime].State = dR.weather.main;
                        if (dR.main.temp != null) query.WeatherAtTimes[dateTime].Temperature = dR.main.temp;
                        if (dR.main.humidity != null) query.WeatherAtTimes[dateTime].Humidity = dR.main.humidity;
                        if (dR.main.pressure != null) query.WeatherAtTimes[dateTime].Pressure = dR.main.pressure * 0.75;
                        if (dR.wind.deg != null) query.WeatherAtTimes[dateTime].WindDirection = GetWindDirection((int)dR.wind.deg);
                        if (dR.wind.speed != null) query.WeatherAtTimes[dateTime].WindSpeed = dR.wind.speed;
                    }                    
                }
            }
        }

        public OpenWeatherMap(string owmToken) {
            _owmToken = owmToken;
        }
    }
}