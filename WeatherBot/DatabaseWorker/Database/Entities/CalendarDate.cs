using System;
using System.Collections.Generic;

namespace WeatherBot.DatabaseWorker.Database.Entities {

    public class CalendarDate {

        public int      Id   { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<ForecastWeather> ForecastWeathers { get; set; }

    }
}