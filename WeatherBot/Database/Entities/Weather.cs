using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeatherBot.Database.Entities {

    public class Weather {

        [ForeignKey("City")]
        public int         Id   { get; set; }
        public City        City { get; set; }
        public FactWeather Fact { get; set; }

        public ICollection<ForecastWeather> Forecast { get; set; }
    }
}