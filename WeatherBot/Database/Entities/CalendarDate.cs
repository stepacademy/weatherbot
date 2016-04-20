using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class CalendarDate
    {
        public int Id { get; set; }
        public List<ForecastWeather> ForecastWeathers { get; set; }
        
        public DateTime Date { get; set; }
    }
}
