using System.ServiceModel;
using WeatherBot.DatabaseWorker.QueryComponents;

namespace WeatherBot.DatabaseWorker {    

    [ServiceContract]
    interface ICallbackResponseContract {

        [OperationContract(IsOneWay = true)]
        void Response(QueryData response);
    }
}