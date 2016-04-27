using System.Data.Entity;


namespace WeatherBot.DatabaseWorker.Database {

    using Entities;

    public class WeatherDbContext : DbContext {        

        public DbSet<City>            Cities           { get; set; }
        public DbSet<Country>         Countries        { get; set; }
        public DbSet<Location>        Locations        { get; set; }
        public DbSet<Weather>         Weathers         { get; set; }
        public DbSet<CalendarDate>    CalendarDates    { get; set; }
        public DbSet<WeatherState>    WeatherStates    { get; set; }
        public DbSet<UpdateError>     UpdateErrors     { get; set; }
        public DbSet<FactWeather>     FactWeathers     { get; set; }
        public DbSet<ForecastWeather> ForecastWeathers { get; set; }
        public DbSet<WeatherData>     WeatherDatas     { get; set; }
        public DbSet<DayPart>         DayParts         { get; set; }

        public WeatherDbContext() : base("ConnectionString") { }
    }
}