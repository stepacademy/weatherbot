///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherBot.DatabaseWorker {

    using QueryComponents;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class QueryHandler : IQueryHandlerContract {

        private ICallbackResponseContract _currentOperationContext;

        public async void QueryAsync(QueryData query) {

            if (_currentOperationContext == null)
                _currentOperationContext = OperationContext.Current.GetCallbackChannel<ICallbackResponseContract>();

            _currentOperationContext.Response(await Response(query));
        }

        private async Task<QueryData> Response(QueryData query) { // <- for debug, will be replaced

            List<DateTime> queryDateTimes = new List<DateTime>(query.WeatherAtTimes.Keys);

            foreach (var dateTime in queryDateTimes) {

                query.WeatherAtTimes[dateTime].State         = "Sunless";
                query.WeatherAtTimes[dateTime].Temperature   = 16.1;
                query.WeatherAtTimes[dateTime].Humidity      = 97;
                query.WeatherAtTimes[dateTime].Pressure      = 768;
                query.WeatherAtTimes[dateTime].WindDirection = "NorthEast";
                query.WeatherAtTimes[dateTime].WindSpeed     = 2.9;
            }

            return query;
        }
    }
}