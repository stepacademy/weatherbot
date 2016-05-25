///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;
using WeatherBot.DatabaseWorker.QueryComponents;

namespace WeatherBot.DatabaseWorker {    

    [ServiceContract]
    interface ICallbackResponseContract {

        [OperationContract(IsOneWay = true)]
        void Response(QueryData response);
    }
}