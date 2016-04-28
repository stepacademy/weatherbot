using System;
using System.Collections.Generic;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    internal class FactUpdate : WeatherUpdate
    {
        public override void UpdateCity(IEnumerable<City> cities)
        {
            if (cities != null) UpdateCityFactWeather(cities);
        }
    }
}