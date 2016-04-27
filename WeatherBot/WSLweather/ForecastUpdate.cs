using System.Collections.Generic;
using System.Globalization;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    internal class ForecastUpdate : WeatherUpdate
    {
        public override void UpdateCity(IEnumerable<City> cities)
        {
            UpdateCityWeather(cities);
        }
    }
}