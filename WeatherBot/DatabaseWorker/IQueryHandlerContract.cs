using System.ServiceModel;
using QueryData = System.IntPtr; // <-- is a dummy, because QueryData not implemented

namespace WeatherBot.DatabaseWorker {    

    [ServiceContract(CallbackContract = typeof(ICallbackResponseContract))]
    public interface IQueryHandlerContract {

        [OperationContract(IsOneWay = true)]
        void Query(QueryData query);
    }
}