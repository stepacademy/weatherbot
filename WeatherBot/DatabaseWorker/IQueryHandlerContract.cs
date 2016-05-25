///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;
using WeatherBot.DatabaseWorker.QueryComponents;

namespace WeatherBot.DatabaseWorker {    

    [ServiceContract(CallbackContract = typeof(ICallbackResponseContract))]
    public interface IQueryHandlerContract {

        [OperationContract(IsOneWay = true)]
        void QueryAsync(QueryData query);
    }
}