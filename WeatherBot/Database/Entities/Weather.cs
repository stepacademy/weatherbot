using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{

    public class Weather
    {
     
        public int Id { get; set; }
        public City City { get; set; }

        public FactWeather Fact { get; set; }
        public ForecastWeather Forecast { get; set; }



        //public double Temperature { get; set; }

        //public WeatherState WeatherState { get; set; }
        //public WindDirectionType WindDirection { get; set; }
        //public string WindSpeed { get; set; }
        //public string Humidity { get; set; }
        //public string Pressure { get; set; }
        //public DateTime ObservationTime { get; set; }
    }
}
