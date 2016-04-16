using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    class WeatherDbContext : DbContext
    {
        public WeatherDbContext() : base("ConnectionString")
        {
            throw new NotImplementedException();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<WeatherDate> WeatherDates { get; set; }
        public DbSet<WeatherState> WeatherStates { get; set; }

    }
}
