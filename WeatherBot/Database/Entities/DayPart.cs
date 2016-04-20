using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class DayPart
    {
        public int Id { get; set; }
        public ForecastWeather ForecastWeather { get; set; }

        public DayTimeType DayTime { get; set; }
        public WeatherData WeatherData { get; set; }
    }
}
