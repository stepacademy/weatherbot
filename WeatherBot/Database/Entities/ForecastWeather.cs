using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class ForecastWeather
    {
        public WeatherDate Date { get; set; }

        public List<DayPart> DayParts { get; set; } 
    }
}
