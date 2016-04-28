namespace WeatherBot.Database.Entities {

    public class DayPart {

        public int Id { get; set; }
        public ForecastWeather ForecastWeather { get; set; }

        public DayTimeType DayTime     { get; set; }
        public WeatherData WeatherData { get; set; }

    }
}