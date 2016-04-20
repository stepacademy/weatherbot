using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public enum DayTimeType
    {
        Morning,
        Day,
        Evening,
        Night
    }

    public class DayPart
    {
        public int Id { get; set; }
        public DayTimeType DayTime { get; set; }
        public WeatherData WeatherData { get; set; }
    }
}
