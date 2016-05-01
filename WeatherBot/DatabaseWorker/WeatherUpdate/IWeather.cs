using System.ServiceModel;
using System.Xml;
using WeatherBot.Database.Entities;

namespace WeatherBot.DatabaseWorker.WeatherUpdate
{
    [ServiceContract]
    internal interface IWeather
    {
        [OperationContract(IsOneWay = true)]
        void CitiesInit();

        [OperationContract(IsOneWay = true)]
        void LocationsInit(City city, XmlElement root);

        [OperationContract(IsOneWay = true)]
        void UpdateWeather();
    }
}