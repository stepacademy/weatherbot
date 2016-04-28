using System;


namespace WeatherBot.Database.Entities {

    public class UpdateError {

        public int      Id        { get; set; }
        public City     City      { get; set; }
        public string   Exception { get; set; }
        public DateTime DateTime  { get; set; }

    }
}