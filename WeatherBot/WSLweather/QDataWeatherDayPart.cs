using System.Runtime.Serialization;

namespace WeatherBot.WSLweather
{
    [DataContract]
    public class QDataWeatherDayPart
    {
        [DataMember]
        public double Temp;
        [DataMember]
        public string DayPart;
        [DataMember]
        public string State;
    }
}