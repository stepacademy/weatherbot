using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WeatherBot.WSLweather
{
    [DataContract]
    public class QData
    {
        [DataMember]
        public string City;
        [DataMember]
        public Dictionary<DateTime, QDataWeatherDay> ItemsDays;
    }

    [DataContract]
    public class QDataWeatherDay
    {
        [DataMember]
        public List<QDataWeatherDayPart> ItemsParts;
    }

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

    [ServiceContract]
    interface IWeather
    {
        [OperationContract]
        QDataWeatherDay GetDataWeatherDay(DateTime dataTime);
        [OperationContract]
        QDataWeatherDay GetWeatherDay(string city);

        [OperationContract(IsOneWay = true)]
        void UpdateCities();

        [OperationContract(IsOneWay = true)]
        void UpdateWeather();
    }
}
