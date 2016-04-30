///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;

namespace WeatherBot.MessagesConveyor.DataInteraction {

    using DatabaseQueryHandler;
    using DatabaseWorker.QueryComponents;

    internal class DatabaseWorkerProxy {

        private QueryHandlerContractClient _proxy;       

        public void Query(QueryData query) {
            _proxy.QueryAsync(query);
        }

        public DatabaseWorkerProxy(IQueryHandlerContractCallback databaseWorkerCallback) {

            _proxy = new QueryHandlerContractClient(new InstanceContext(databaseWorkerCallback));
            _proxy.Open();
        }
    }
}