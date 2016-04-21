using System;
using System.Collections.Generic;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    class FactUpdate: WeatherUpdate
    {
        public override void UpdateCity(IEnumerable<City> cities)
        {
            throw new NotImplementedException();
        }
    }
}