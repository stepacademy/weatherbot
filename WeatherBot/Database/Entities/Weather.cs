using System.Collections.Generic;

namespace WeatherBot.Database.Entities
{
    public class Weather
    {
        public int Id { get; set; }

        public City City { get; set; }
        public FactWeather Fact { get; set; }
        public ICollection<ForecastWeather> Forecast { get; set; }
    }
}