using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class FactWeather
    {
        public int Id { get; set; }
        public Weather Weather { get; set; }

        public WeatherData WeatherData { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}
