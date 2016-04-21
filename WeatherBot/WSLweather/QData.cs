using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeatherBot.WSLweather
{
    [DataContract]
    public class QData
    {
        [DataMember] public string City;

        [DataMember] public Dictionary<DateTime, QDataWeatherDay> ItemsDays;
    }
}