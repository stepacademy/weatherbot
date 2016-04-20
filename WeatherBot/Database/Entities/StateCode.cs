using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class StateCode
    {
        public int Id { get; set; }
        public List<WeatherState> WeatherStates { get; set; }

        //ovc, bkb_d ...
        public string Code { get; set; } 
    }
}
