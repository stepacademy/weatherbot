using System.Collections.Generic;
using System.Globalization;
using WeatherBot.DatabaseWorker.Database.Entities;

namespace WeatherBot.WSLweather
{
    internal class ForecastUpdate : WeatherUpdate
    {
        public override void UpdateCity(IEnumerable<City> cities)
        {
            var stackCities = cities as Stack<City>;

            var formatSepar = new NumberFormatInfo {NumberDecimalSeparator = "."};

            while (stackCities != null && stackCities.Count > 0)
            {
                var city = stackCities.Pop();
            }
        }
    }
}