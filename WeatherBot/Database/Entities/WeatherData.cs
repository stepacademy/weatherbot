using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }
        public WeatherState WeatherState { get; set; }

        public WindDirectionType WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double Temperature { get; set; }
    }
}
