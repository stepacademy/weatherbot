using System;
using System.Data.Entity;
using WeatherBot.Database.Entities;

namespace WeatherBot.Database
{
    public class WeatherDbContext : DbContext
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
