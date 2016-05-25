///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Runtime.Serialization;


namespace WeatherBot.DatabaseWorker.QueryComponents {

    [DataContract]
    public class WeatherEntities {

        [DataMember] public string            State         { get; set; }  // ясно, облачно, дождь и пр.
        [DataMember] public double            Temperature   { get; set; }  // температура
        [DataMember] public string            WindDirection { get; set; }  // направление ветра
        [DataMember] public double            WindSpeed     { get; set; }  // скорость ветра
        [DataMember] public double            Humidity      { get; set; }  // влажность
        [DataMember] public double            Pressure      { get; set; }  // давление
    }
}