using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherBot.Database.Entities {


    public class Location {

        [ForeignKey("City")]
        public int    Id        { get; set; }
        public City   City      { get; set; }

        public double Latitude  { get; set; }
        public double Longitude { get; set; }
    }
}