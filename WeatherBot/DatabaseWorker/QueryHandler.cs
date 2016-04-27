using System;
using QueryData = System.IntPtr; // <-- is a dummy, because QueryData not implemented

namespace WeatherBot.DatabaseWorker {

    internal class QueryHandler : IQueryHandlerContract {

        public void Query(QueryData query) {
            throw new NotImplementedException();
        }
    }
}