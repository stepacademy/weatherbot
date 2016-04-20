using System;
using System.ServiceModel;

namespace WeatherBot.WSLweather
{
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