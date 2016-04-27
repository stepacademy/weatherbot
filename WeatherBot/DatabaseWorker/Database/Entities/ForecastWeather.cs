using System.Collections.Generic;

namespace WeatherBot.DatabaseWorker.Database.Entities {

    public class ForecastWeather {

        public int                  Id       { get; set; }
        public Weather              Weather  { get; set; }
        public CalendarDate         Date     { get; set; }
        public ICollection<DayPart> DayParts { get; set; }
    }
}