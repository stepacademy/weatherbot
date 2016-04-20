using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
