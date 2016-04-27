using System.Collections.Generic;
using WeatherBot.DatabaseWorker.Database.Entities;

namespace WeatherBot.WSLweather
{
    internal interface IWeatherUpdate
    {
        void UpdateCity(IEnumerable<City> cities);
    }
}