using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class WeatherState
    {
        public int Id { get; set; }
        public List<Weather> Weathers { get; set; }

        //пасмурно, облачно с прояснениями
        public string State { get; set; }
        //ovc, bkb_d ...
        public string Code { get; set; }

    }
}
