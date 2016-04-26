using System.ServiceModel;

namespace Test.DummyWeatherDataService {

    [ServiceContract]
    public interface ICallbackDatabaseResponse {

        [OperationContract(IsOneWay = true)]
        void CallbackResponseInvoke(QueryData q);

    }
}
