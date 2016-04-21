using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeatherBot.WSLweather
{
    [DataContract]
    public class QDataWeatherDay
    {
        [DataMember] public List<QDataWeatherDayPart> ItemsParts;
    }
}