using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class Weather
    {
        public enum DayTimeType
        {
            Morning,
            Day,
            Evening,
            Night
        }

        public int Id { get; set; }
        public City City { get; set; }
        public WeatherDate Date { get; set; }
        public int Temperature { get; set; }
        public DayTimeType DayTime { get; set; }
        public WeatherState WeatherState { get; set; }

    }
}
