using System.Collections.Generic;

namespace WeatherBot.DatabaseWorker.Database.Entities {

    public class WeatherState {

        public int Id { get; set; }
        public ICollection<Weather> Weathers { get; set; }

        //пасмурно, облачно с прояснениями
        public string State { get; set; }
        //ovc, bkb_d ...
        public string Code  { get; set; }

    }
}