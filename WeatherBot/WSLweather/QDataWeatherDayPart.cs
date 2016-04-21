using System.Runtime.Serialization;

namespace WeatherBot.WSLweather
{
    [DataContract]
    public class QDataWeatherDayPart
    {
        [DataMember] public string DayPart;

        [DataMember] public string State;

        [DataMember] public double Temp;
    }
}