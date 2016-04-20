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

        public enum WindDirectionType
        {
            S, Se, Sw,
            N, Nw, Ne,
            W,
            E
        }

        public int Id { get; set; }
        public City City { get; set; }
        public WeatherDate Date { get; set; }
        public double Temperature { get; set; }
        public DayTimeType DayTime { get; set; }
        public WeatherState WeatherState { get; set; }
        public WindDirectionType WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}
