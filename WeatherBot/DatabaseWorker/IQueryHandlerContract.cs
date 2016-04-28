using System.ServiceModel;
using WeatherBot.DatabaseWorker.QueryComponents;

namespace WeatherBot.DatabaseWorker {    

    [ServiceContract(CallbackContract = typeof(ICallbackResponseContract))]
    public interface IQueryHandlerContract {

        [OperationContract(IsOneWay = true)]
        void Query(QueryData query);
    }
}