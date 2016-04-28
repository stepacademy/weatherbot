using System;
using System.ServiceModel;
using System.Xml;
using WeatherBot.Database.Entities;

namespace WeatherBot.WSLweather
{
    [ServiceContract]
    internal interface IWeather
    {
        [OperationContract]
        QDataWeatherDay GetDataWeatherDay(DateTime dataTime);

        [OperationContract]
        QDataWeatherDay GetWeatherDay(string city);

        [OperationContract(IsOneWay = true)]
        void CitiesInit();

        [OperationContract(IsOneWay = true)]
        void LocationsInit(City city, XmlElement root);

        [OperationContract(IsOneWay = true)]
        void UpdateWeather();
    }
}