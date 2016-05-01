using System.Collections.Generic;
using WeatherBot.Database.Entities;

namespace WeatherBot.DatabaseWorker.WeatherUpdate
{
    internal class FactUpdate : WeatherUpdate
    {
        public override void UpdateCity(IEnumerable<City> cities)
        {
            if (cities != null) UpdateCityFactWeather(cities);
        }
    }
}