using System.Collections.Generic;
using WeatherBot.Database.Entities;

namespace WeatherBot.DatabaseWorker.WeatherUpdate
{
    internal interface IWeatherUpdate
    {
        void UpdateCity(IEnumerable<City> cities);
    }
}