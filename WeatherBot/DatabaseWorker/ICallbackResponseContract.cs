using System.ServiceModel;
using QueryData = System.IntPtr; // <-- is a dummy, because QueryData not implemented

namespace WeatherBot.DatabaseWorker {    

    [ServiceContract]
    interface ICallbackResponseContract {

        [OperationContract(IsOneWay = true)]
        void Response(QueryData response);
    }
}