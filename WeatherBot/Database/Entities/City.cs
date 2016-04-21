namespace WeatherBot.Database.Entities
{
    public class City
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public Location Location { get; set; }
        public Weather Weather { get; set; }

        public string Name { get; set; }
        //28650, 27612 ...
        public string XmlCode { get; set; }
    }
}