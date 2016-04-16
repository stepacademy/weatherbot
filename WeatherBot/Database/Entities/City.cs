using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string XmlCode { get; set; }
        public Country Country { get; set; }
        public Location Location { get; set; }
    }
}
