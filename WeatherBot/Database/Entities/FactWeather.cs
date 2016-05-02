using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeatherBot.Database.Entities {

    public class FactWeather {

        [ForeignKey("Weather")]
        public int         Id              { get; set; }
        public Weather     Weather         { get; set; }
        public WeatherData WeatherData     { get; set; }

        public DateTime    ObservationTime { get; set; }
    }
}