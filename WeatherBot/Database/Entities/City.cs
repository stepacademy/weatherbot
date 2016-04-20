using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class City
    {
        public int Id { get; set; }
        public Country Country { get; set; }

        public string Name { get; set; }
        //28650, 27612 ...
        public string XmlCode { get; set; }
        public Location Location { get; set; }

        public Weather Weather { get; set; }
    }
}
