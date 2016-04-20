using System;
using System.Data.Entity;
using WeatherBot.Database.Entities;

namespace WeatherBot.Database
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext() : base("ConnectionString")
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<CalendarDate> CalendarDates { get; set; }
        public DbSet<WeatherState> WeatherStates { get; set; }
        public DbSet<UpdateError> UpdateErrors { get; set; }
        public DbSet<FactWeather> FactWeathers { get; set; }
        public DbSet<ForecastWeather> ForecastWeathers { get; set; }
        public DbSet<WeatherData> WeatherDatas { get; set; }
        public DbSet<DayPart> DayParts { get; set; }
    }
}
