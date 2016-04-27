namespace WeatherBot.DatabaseWorker.Database.Entities {


    public class WeatherData {

        public int Id { get; set; }
        public WeatherState WeatherState { get; set; }

        public WindDirectionType WindDirection { get; set; }
        public double WindSpeed   { get; set; }
        public int    Humidity    { get; set; }
        public int    Pressure    { get; set; }
        public double Temperature { get; set; }

    }
}