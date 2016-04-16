using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Weatherbot.WSLweather
{
    [DataContract]
    public class QData
    {
        [DataMember]
        string City;

        [DataMember]
        Dictionary<DateTime, QDataWeatherDay> itemsDays;
    }

    [DataContract]
    public class QDataWeatherDay
    {
        [DataMember]
        List<QDataWeatherDayPart> itemsParts;
    }

    [DataContract]
    public class QDataWeatherDayPart
    {
        [DataMember]
        double temp;
        [DataMember]
        string dayPart;
        [DataMember]
        string state;
    }

    public interface IModuleIO
    {

    }


    [ServiceContract]
    public interface IWeatherDbQuery
    {
        [OperationContract]
        QDataWeatherDay GetWeatherDay(DateTime dataTime);
        [OperationContract]
        QDataWeatherDay GetWeatherCityDataWeatherDay(string city);


    }
}
