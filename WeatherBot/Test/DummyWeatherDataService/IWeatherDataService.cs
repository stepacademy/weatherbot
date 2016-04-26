using System.ServiceModel;

namespace Test.DummyWeatherDataService {

    [ServiceContract(CallbackContract = typeof(ICallbackDatabaseResponse))]
    public interface IWeatherDataService {

        [OperationContract(IsOneWay = true)]
        void SendToDatabaseQuery(QueryData q);

    }
}
