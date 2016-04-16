using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    class WeatherState
    {
        public int Id { get; set; }
        public string State { get; set; }
        public List<Weather> Weathers { get; set; }
    }
}
