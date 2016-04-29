///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.ServiceModel;

namespace WeatherBot.MessagesConveyor {

    using DatabaseQueryHandler;

    internal sealed class DatabaseWorkerInstance {

        private static readonly Lazy<DatabaseWorkerInstance> _instance =
            new Lazy<DatabaseWorkerInstance>(
                () => new DatabaseWorkerInstance());

        private static readonly Lazy<QueryHandlerContractClient> _proxy =
            new Lazy<QueryHandlerContractClient>(
                () => new QueryHandlerContractClient(new InstanceContext(_instance)));

        public static QueryHandlerContractClient Proxy { get { return _proxy.Value; } }

        private DatabaseWorkerInstance() { }        
    }
}