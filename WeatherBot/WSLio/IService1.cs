using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WeatherBot.WSLio
{
    [DataContract]
    public class QData
    {
        [DataMember] string City;

        [DataMember] Dictionary<DateTime, QDataWeatherDay> itemsDays;
    }

    [DataContract]
    public class QDataWeatherDay
    {
        [DataMember] List<QDataWeatherDayPart> itemsParts;
    }

    [DataContract]
    public class QDataWeatherDayPart
    {
        [DataMember] double temp;
        [DataMember] string dayPart;
        [DataMember] string state;
    }

    public interface IModuleIO
    {
         
    }



    public interface IWeatherDbQuery
    {
        QDataWeatherDay GetWeatherDay(DateTime dataTime);
        QDataWeatherDay GetWeatherCityDataWeatherDay(string city);


    }
}
